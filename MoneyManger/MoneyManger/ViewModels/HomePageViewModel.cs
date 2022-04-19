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
            Entities = new ObservableRangeCollection<Entity>();
            LoadEntitiesCommand = new AsyncCommand(() => ExecuteLoadEntitiesCommand());

            EntityTapped = new AsyncCommand<Entity>((p) => OnEntitySelected(p));
            AddEntityCommand = new AsyncCommand(() => AddEntityAsync());
            AddTransactionCommand = new AsyncCommand<Entity>((p) => AddTransactionAsync(p));
            EditEntityCommand = new AsyncCommand<Entity>((p) => EditEntityAsync(p));
            DeleteEntityCommand = new AsyncCommand<Entity>((p) => DeleteEntityAsync(p));
        }

        async Task ExecuteLoadEntitiesCommand()
        {
            try
            {
                IsBusy = true;
                var peoples = await EntityDataStore.GetAllEntityAsync();
                Entities.Clear();
                Entities.AddRange(peoples);                
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

        async Task AddEntityAsync()
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
                    var newEntity = new Entity
                    {
                        Name = response.Text,
                        IsActive = true
                    };

                    var success = await EntityDataStore.AddEntityAsync(newEntity);
                    if (success)
                    {
                        UserDialogs.Instance.Toast(Constants.PERSON_ADD_SUCCESS);
                        await ExecuteLoadEntitiesCommand();
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
        
        async Task EditEntityAsync(Entity person)
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

                    var success = await EntityDataStore.UpdateEntityAsync(person);
                    if (success)
                    {
                        UserDialogs.Instance.Toast(Constants.PERSON_UPDATE_SUCCESS);
                        await ExecuteLoadEntitiesCommand();
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
        
        async Task DeleteEntityAsync(Entity person)
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

                    var success = await EntityDataStore.UpdateEntityAsync(person);
                    if (success)
                    {
                        UserDialogs.Instance.Toast(Constants.PERSON_DELETE_SUCCESS);
                        await ExecuteLoadEntitiesCommand();
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

        async Task AddTransactionAsync(Entity person)
        {
            await Shell.Current.GoToAsync($"{nameof(NewTransactionPage)}?{nameof(NewTransactionPageViewModel.EntityId)}={person.EntityId}&{nameof(NewTransactionPageViewModel.TransactionId)}=");
        }

        async Task OnEntitySelected(Entity person)
        {
            if (person == null)
                return;

            await Shell.Current.GoToAsync($"{nameof(TransactionPage)}?{nameof(TransactionPageViewModel.EntityId)}={person.EntityId}");
        }

        #region Commands and Bindings

        public ObservableRangeCollection<Entity> Entities { get; }
        public AsyncCommand LoadEntitiesCommand { get; }
        public AsyncCommand AddEntityCommand { get; }
        public AsyncCommand<Entity> AddTransactionCommand { get; }
        public AsyncCommand<Entity> EditEntityCommand { get; }
        public AsyncCommand<Entity> DeleteEntityCommand { get; }
        public AsyncCommand<Entity> EntityTapped { get; }

        #endregion
    }
}
