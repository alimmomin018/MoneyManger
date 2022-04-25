using Acr.UserDialogs;
using MoneyManger.Models;
using MoneyManger.Views;
using System;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Xamarin.CommunityToolkit.ObjectModel;
using Xamarin.Essentials;
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
            ExportCommand = new AsyncCommand(() => ExportAsync());
            EditTransactionCommand = new AsyncCommand<Transaction>((t) => EditTransactionAsync(t));
            DeleteTransactionCommand = new AsyncCommand<Transaction>((t) => DeleteTransactionAsync(t));
        }

        async Task ExportAsync()
        {
            UserDialogs.Instance.ShowLoading("Exporting...");
            try
            {
                if (Transactions.Count == 0)
                {
                    UserDialogs.Instance.Toast("No transactions to export");
                    return;
                }
                
                var filename = $"{SelectedEntity.Name}-{DateTime.Now.ToString("yyyyMMddHHmmsstt")}.csv";
                var filePath = Path.Combine(FileSystem.AppDataDirectory, filename);
                
                var _stringBuilder = new StringBuilder(); 
                _stringBuilder.AppendLine($"");
                _stringBuilder.AppendLine($"");
                _stringBuilder.AppendLine($"Name, Total Income, Total Expense, Total Balance");
                _stringBuilder.AppendLine($"{SelectedEntity.Name}, {SelectedEntity.TotalIncome}, {SelectedEntity.TotalExpense}, {SelectedEntity.Total}");
                _stringBuilder.AppendLine($"");
                _stringBuilder.AppendLine($"");
                _stringBuilder.AppendLine($"{SelectedEntity.Transactions.Count} Transactions in total");
                _stringBuilder.AppendLine($"Date, Income, Expense, Notes, Description");
                
                foreach (var t in Transactions)
                {
                    var income = t.Type == TransactionType.Income ? t.Amount : 0;
                    var expense = t.Type == TransactionType.Expense ? t.Amount : 0;
                    _stringBuilder.AppendLine($"{t.Date.ToString("MM/dd/yyyy hh:mm tt")}, {income}, {expense}, {t.Notes}, {t.Description}");
                }

                string content = _stringBuilder.ToString();
                File.WriteAllText(filePath, content);

                await Share.RequestAsync(new ShareFileRequest
                {
                    Title = filename,
                    File = new ShareFile(filePath)
                });
            }
            catch(Exception ex)
            {
                UserDialogs.Instance.Toast("Error Exporting: " + ex.Message);
            }
            finally
            {
                UserDialogs.Instance.HideLoading();
            }
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
                LoadEntityId(EntityId, StartDate, EndDate);
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
                        Transactions.Remove(transaction);
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

        private async void LoadEntityId(string value, DateTime? startDate, DateTime endDate)
        {
            try
            {
                if (string.IsNullOrEmpty(value))
                    return;
                
                int entityId = int.Parse(value);
                SelectedEntity = await TransactionDataStore.GetAllTransactionsForEntityAsync(entityId, startDate, endDate);

                Transactions.Clear();
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
        public AsyncCommand ExportCommand { get; }
        public AsyncCommand<Transaction> TransactionTappedCommand { get; }
        public AsyncCommand<Transaction> EditTransactionCommand { get; }
        public AsyncCommand<Transaction> DeleteTransactionCommand { get; }

        private Entity _selectedEntity;
        private string _entityId;
        public string FilterTitle = null;
        public DateTime? StartDate = null;
        public DateTime EndDate = DateTime.Now;
        public string EntityId
        {
            get => _entityId;
            set
            {
                _entityId = value;
                LoadEntityId(value, StartDate, EndDate);
            }
        }

        public Entity SelectedEntity { get => _selectedEntity; set => SetProperty(ref _selectedEntity, value); }

        private DateTime _customEndDate = DateTime.Now;
        private DateTime _customStartDate = DateTime.Now;
        public DateTime CustomStartDate { get => _customStartDate; set { SetProperty(ref _customStartDate, value); LoadEntityId(SelectedEntity?.EntityId.ToString(), value, _customEndDate); } }
        public DateTime CustomEndDate { get => _customEndDate; set { SetProperty(ref _customEndDate, value); LoadEntityId(SelectedEntity?.EntityId.ToString(), _customStartDate, value); } }

        #endregion
    }
}
