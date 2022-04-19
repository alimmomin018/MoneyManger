using Acr.UserDialogs;
using MoneyManger.Models;
using MoneyManger.Views;
using System;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.CommunityToolkit.ObjectModel;
using Xamarin.Forms;

namespace MoneyManger.ViewModels
{
    [QueryProperty(nameof(EntityId), nameof(EntityId))]
    public class TransactionPageViewModel : BaseViewModel
    {
        public TransactionPageViewModel()
        {
            Transactions = new ObservableRangeCollection<Transaction>();

            LoadTransactionsCommand = new AsyncCommand(() => ExecuteLoadTransactionsCommand());
            CopyTransactionCommand = new AsyncCommand<Transaction>((t) => CopyTransactionAsync(t));
            TransactionTappedCommand = new AsyncCommand<Transaction>((t) => OnTransactionTappedAsync(t));
            AddTransactionCommand = new AsyncCommand(() => AddTransactionAsync());
            EditTransactionCommand = new AsyncCommand<Transaction>((t) => EditTransactionAsync(t));
            DeleteTransactionCommand = new AsyncCommand<Transaction>((t) => DeleteTransactionAsync(t));
        }

        async Task OnTransactionTappedAsync(Transaction t)
        {
            if (t == null)
                return;

            await Shell.Current.GoToAsync($"{nameof(NewTransactionPage)}?{nameof(NewTransactionPageViewModel.TransactionId)}={t.TransactionId}&{nameof(NewTransactionPageViewModel.IsReadOnly)}={true}");
        }

        private async Task ExecuteLoadTransactionsCommand()
        {
            IsBusy = true;
            if(EntityId != null)
            {
                LoadEntityId(EntityId);
            }
            IsBusy = false;
        }

        private async Task CopyTransactionAsync(Transaction transaction)
        {
            await Shell.Current.GoToAsync($"{nameof(CopyPasteTransactionPage)}?{nameof(CopyPasteTransactionPageViewModel.TransactionId)}={transaction.TransactionId}");
        }
        
        private async Task AddTransactionAsync()
        {
            await Shell.Current.GoToAsync($"{nameof(NewTransactionPage)}?{nameof(NewTransactionPageViewModel.EntityId)}={EntityId}&{nameof(NewTransactionPageViewModel.TransactionId)}=");
        }

        private async Task EditTransactionAsync(Transaction transaction)
        {
            await Shell.Current.GoToAsync($"{nameof(NewTransactionPage)}?{nameof(NewTransactionPageViewModel.EntityId)}={EntityId}&{nameof(NewTransactionPageViewModel.TransactionId)}={transaction.TransactionId}");
        }
        
        private async Task DeleteTransactionAsync(Transaction transaction)
        {
            var response = await UserDialogs.Instance.ConfirmAsync(
                   Constants.TRANSACTION_DELETE_DIALOG_MESSAGE,
                   Constants.TRANSACTION_DELETE_DIALOG_TITLE,
                   Constants.OK_LABEL,
                   Constants.CANCEL_LABEL);

            if (response)
            {
                try
                {
                    var success = await TransactionDataStore.DeleteTransactionAsync(transaction.TransactionId);
                    if (success)
                    {
                        UserDialogs.Instance.Toast(Constants.TRANSACTION_DELETE_SUCCESS);
                        LoadEntityId(transaction.EntityId.ToString());
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

        private async void LoadEntityId(string value)
        {
            try
            {
                Transactions.Clear();
                int entityId = int.Parse(value);
                SelectedEntity = await TransactionDataStore.GetAllTransactionsForEntityAsync(entityId);

                if (SelectedEntity.Transactions != null)
                    Transactions.AddRange(SelectedEntity.Transactions.OrderByDescending(t => t.Date));

            }
            catch (ApplicationException aex)
            {
                aex.ShowApplicationExceptionErrorDialogAndHideLoading();
            }
            catch (Exception ex)
            {
                ex.ShowExceptionErrorDialogAndHideLoading();
            }
        }

        #region Commands and Bindings

        public ObservableRangeCollection<Transaction> Transactions { get; }
        public AsyncCommand LoadTransactionsCommand { get; }
        public AsyncCommand<Transaction> CopyTransactionCommand { get; }
        public AsyncCommand AddTransactionCommand { get; }
        public AsyncCommand<Transaction> TransactionTappedCommand { get; }
        public AsyncCommand<Transaction> EditTransactionCommand { get; }
        public AsyncCommand<Transaction> DeleteTransactionCommand { get; }

        private Entity _selectedEntity;
        private string _entityId;
        private decimal _totalIncome;
        private decimal _totalExpense;
        public string EntityId
        {
            get => _entityId;
            set
            {
                _entityId = value;
                LoadEntityId(value);
            }
        }

        public Entity SelectedEntity { get => _selectedEntity; set => SetProperty(ref _selectedEntity, value); }

        #endregion
    }
}
