using Acr.UserDialogs;
using MoneyManger.Models;
using MoneyManger.Views;
using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.CommunityToolkit.ObjectModel;
using Xamarin.Forms;

namespace MoneyManger.ViewModels
{
    public class HomePageViewModel : BaseViewModel
    {
        public HomePageViewModel()
        {
            SearchCommand = CommandFactory.Create<string>(SearchAsync);
            Entities = new ObservableRangeCollection<Entity>();
            LoadEntitiesCommand = new AsyncCommand(() => ExecuteLoadEntitiesCommand());

            EntityTapped = new AsyncCommand<Entity>((p) => OnEntitySelectedAsync(p));
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
                if (string.IsNullOrWhiteSpace(SearchText))
                {
                    Entities.AddRange(peoples);
                }
                else
                {
                    foreach (var item in peoples)
                    {
                        if (item.Name.ToLower().Contains(SearchText.ToLower()))
                        {
                            Entities.Add(item);
                        }
                    }
                }              
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

        async Task SearchAsync(string searchText)
        {
            await ExecuteLoadEntitiesCommand();
        }

        async Task AddEntityAsync()
        {
            var response = await UserDialogs.Instance.PromptAsync(
                Constants.ENTITY_ADD_DIALOG_MESSAGE,
                Constants.ENTITY_ADD_DIALOG_TITLE,
                Constants.OK_LABEL,
                Constants.CANCEL_LABEL,
                Constants.ENTITY_DIALOG_PLACE_HOLDER,
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
                        UserDialogs.Instance.Toast(Constants.ENTITY_ADD_SUCCESS);
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
        
        async Task EditEntityAsync(Entity entity)
        {
            PromptConfig promptConfig = new PromptConfig
            {
                Text = entity.Name,
                Title = Constants.ENTITY_ADD_DIALOG_TITLE,
                Message = Constants.ENTITY_EDIT_DIALOG_MESSAGE,
                OkText = Constants.OK_LABEL,
                CancelText = Constants.CANCEL_LABEL,
                Placeholder = Constants.ENTITY_DIALOG_PLACE_HOLDER,
                InputType = InputType.Default
            };
            var response = await UserDialogs.Instance.PromptAsync(promptConfig);

            if(!string.IsNullOrWhiteSpace(response.Text))
            {
                try
                {
                    entity.Name = response.Text;

                    var success = await EntityDataStore.UpdateEntityAsync(entity);
                    if (success)
                    {
                        UserDialogs.Instance.Toast(Constants.ENTITY_UPDATE_SUCCESS);
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
                await UserDialogs.Instance.ConfirmAsync(Constants.ENTITY_NAME_EMPTY);
            }
        }
        
        async Task DeleteEntityAsync(Entity entity)
        {
            var response = await UserDialogs.Instance.ConfirmAsync(
                Constants.ENTITY_DELETE_DIALOG_MESSAGE,
                Constants.ENTITY_DELETE_DIALOG_TITLE,
                Constants.OK_LABEL,
                Constants.CANCEL_LABEL);

            if(response)
            {
                try
                {
                    entity.IsActive = false;

                    var success = await EntityDataStore.UpdateEntityAsync(entity);
                    if (success)
                    {
                        UserDialogs.Instance.Toast(Constants.ENTITY_DELETE_SUCCESS);
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

        async Task AddTransactionAsync(Entity entity)
        {
            await Shell.Current.GoToAsync($"{nameof(NewTransactionPage)}?{nameof(NewTransactionPageViewModel.EntityId)}={entity.EntityId}&{nameof(NewTransactionPageViewModel.TransactionId)}=");
        }

        async Task OnEntitySelectedAsync(Entity entity)
        {
            if (entity == null)
                return;

            await Shell.Current.GoToAsync($"{nameof(TransactionPage)}?{nameof(TransactionPageViewModel.EntityId)}={entity.EntityId}");
        }

        #region Commands and Bindings

        private string _searchText;
        public ObservableRangeCollection<Entity> Entities { get; }
        public ICommand SearchCommand { get; }
        public AsyncCommand LoadEntitiesCommand { get; }
        public AsyncCommand AddEntityCommand { get; }
        public AsyncCommand<Entity> AddTransactionCommand { get; }
        public AsyncCommand<Entity> EditEntityCommand { get; }
        public AsyncCommand<Entity> DeleteEntityCommand { get; }
        public AsyncCommand<Entity> EntityTapped { get; }

        public string SearchText { get => _searchText; set => SetProperty(ref _searchText, value); }

        #endregion
    }
}
