using Acr.UserDialogs;
using MoneyManger.Models;
using MoneyManger.Views;
using System;
using System.Threading.Tasks;
using Xamarin.CommunityToolkit.ObjectModel;
using Xamarin.Forms;

namespace MoneyManger.ViewModels
{
    public class HomePageViewModel : BaseViewModel
    {
        public HomePageViewModel()
        {
            Persons = new ObservableRangeCollection<Person>();
            LoadPersonsCommand = new AsyncCommand(() => ExecuteLoadPersonsCommand());

            PersonTapped = new AsyncCommand<Person>((p) => OnPersonSelected(p));
            AddPersonCommand = new AsyncCommand(() => AddPersonAsync());
            AddTransactionCommand = new AsyncCommand<Person>((p) => AddTransactionAsync(p));
            EditPersonCommand = new AsyncCommand<Person>((p) => EditPersonAsync(p));
            DeletePersonCommand = new AsyncCommand<Person>((p) => DeletePersonAsync(p));
        }

        async Task ExecuteLoadPersonsCommand()
        {
            try
            {
                IsBusy = true;
                var peoples = await PersonDataStore.GetAllPeopleAsync();
                Persons.Clear();
                Persons.AddRange(peoples);                
            }
            catch(ApplicationException aex)
            {
                aex.ShowApplicationExceptionErrorDialogAndHideLoading();
            }
            catch (Exception ex)
            {
                ex.ShowExceptionErrorDialogAndHideLoading();
            }
            finally { IsBusy = false; }
        }

        async Task AddPersonAsync()
        {
            var response = await UserDialogs.Instance.PromptAsync(
                Constants.PERSON_ADD_DIALOG_MESSAGE,
                Constants.PERSON_ADD_DIALOG_TITLE,
                Constants.OK_LABEL,
                Constants.CANCEL_LABEL,
                Constants.PERSON_DIALOG_PLACE_HOLDER,
                InputType.Default);

            if(!string.IsNullOrWhiteSpace(response.Text))
            {
                try
                {
                    var newPerson = new Person
                    {
                        Name = response.Text,
                        IsActive = true
                    };

                    var success = await PersonDataStore.AddPersonAsync(newPerson);
                    if (success)
                    {
                        UserDialogs.Instance.Toast(Constants.PERSON_ADD_SUCCESS);
                        await ExecuteLoadPersonsCommand();
                    }
                }
                catch (ApplicationException aex)
                {
                    aex.ShowApplicationExceptionErrorDialog();
                }
                catch (Exception ex)
                {
                    ex.ShowExceptionErrorDialog();
                }
            }
        }
        
        async Task EditPersonAsync(Person person)
        {
            PromptConfig promptConfig = new PromptConfig
            {
                Text = person.Name,
                Title = Constants.PERSON_ADD_DIALOG_TITLE,
                Message = Constants.PERSON_EDIT_DIALOG_MESSAGE,
                OkText = Constants.OK_LABEL,
                CancelText = Constants.CANCEL_LABEL,
                Placeholder = Constants.PERSON_DIALOG_PLACE_HOLDER,
                InputType = InputType.Default
            };
            var response = await UserDialogs.Instance.PromptAsync(promptConfig);

            if(!string.IsNullOrWhiteSpace(response.Text))
            {
                try
                {
                    person.Name = response.Text;

                    var success = await PersonDataStore.UpdatePersonAsync(person);
                    if (success)
                    {
                        UserDialogs.Instance.Toast(Constants.PERSON_UPDATE_SUCCESS);
                        await ExecuteLoadPersonsCommand();
                    }
                }
                catch (ApplicationException aex)
                {
                    aex.ShowApplicationExceptionErrorDialog();
                }
                catch (Exception ex)
                {
                    ex.ShowExceptionErrorDialog();
                }
            }
            else
            {
                await UserDialogs.Instance.ConfirmAsync(Constants.PERSON_NAME_EMPTY);
            }
        }
        
        async Task DeletePersonAsync(Person person)
        {
            var response = await UserDialogs.Instance.ConfirmAsync(
                Constants.PERSON_DELETE_DIALOG_MESSAGE,
                Constants.PERSON_DELETE_DIALOG_TITLE,
                Constants.OK_LABEL,
                Constants.CANCEL_LABEL);

            if(response)
            {
                try
                {
                    person.IsActive = false;

                    var success = await PersonDataStore.UpdatePersonAsync(person);
                    if (success)
                    {
                        UserDialogs.Instance.Toast(Constants.PERSON_DELETE_SUCCESS);
                        await ExecuteLoadPersonsCommand();
                    }
                }
                catch (ApplicationException aex)
                {
                    aex.ShowApplicationExceptionErrorDialog();
                }
                catch (Exception ex)
                {
                    ex.ShowExceptionErrorDialog();
                }
            }
        }

        async Task AddTransactionAsync(Person person)
        {
            await Shell.Current.GoToAsync($"{nameof(NewTransactionPage)}?{nameof(NewTransactionPageViewModel.PersonId)}={person.PersonId}&{nameof(NewTransactionPageViewModel.TransactionId)}=");
        }

        async Task OnPersonSelected(Person person)
        {
            if (person == null)
                return;

            await Shell.Current.GoToAsync($"{nameof(TransactionPage)}?{nameof(TransactionPageViewModel.PersonId)}={person.PersonId}");
        }

        #region Commands and Bindings

        public ObservableRangeCollection<Person> Persons { get; }
        public AsyncCommand LoadPersonsCommand { get; }
        public AsyncCommand AddPersonCommand { get; }
        public AsyncCommand<Person> AddTransactionCommand { get; }
        public AsyncCommand<Person> EditPersonCommand { get; }
        public AsyncCommand<Person> DeletePersonCommand { get; }
        public AsyncCommand<Person> PersonTapped { get; }

        #endregion
    }
}
