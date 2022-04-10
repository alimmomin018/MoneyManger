using Acr.UserDialogs;
using MoneyManger.Models;
using MoneyManger.Views;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Xamarin.CommunityToolkit.ObjectModel;
using Xamarin.Forms;

namespace MoneyManger.ViewModels
{
    [QueryProperty(nameof(PersonId), nameof(PersonId))]
    public class TransactionPageViewModel : BaseViewModel
    {
        public TransactionPageViewModel()
        {
            Transactions = new ObservableRangeCollection<Transaction>();

            LoadTransactionsCommand = new AsyncCommand(() => ExecuteLoadTransactionsCommand());
            AddTransactionCommand = new AsyncCommand(() => AddTransactionAsync());
            EditTransactionCommand = new AsyncCommand<Transaction>((p) => EditTransactionAsync(p));
            DeleteTransactionCommand = new AsyncCommand<Transaction>((p) => DeleteTransactionAsync(p));
        }

        private async Task ExecuteLoadTransactionsCommand()
        {
            IsBusy = true;
            if(PersonId != null)
            {
                LoadPersonId(PersonId);
            }
            IsBusy = false;
        }

        private async Task AddTransactionAsync()
        {
            await Shell.Current.GoToAsync($"{nameof(NewTransactionPage)}?{nameof(NewTransactionPageViewModel.TransactionId)}=");
        }

        private async Task EditTransactionAsync(Transaction transaction)
        {
            await Shell.Current.GoToAsync($"{nameof(NewTransactionPage)}?{nameof(NewTransactionPageViewModel.TransactionId)}={transaction.TransactionId}");
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
                        LoadPersonId(transaction.PersonId.ToString());
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

        private async void LoadPersonId(string value)
        {
            try
            {
                int personId = int.Parse(value);
                SelectedPerson = await TransactionDataStore.GetAllTransactionsForPersonAsync(personId);
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
        public AsyncCommand AddTransactionCommand { get; }
        public AsyncCommand<Transaction> EditTransactionCommand { get; }
        public AsyncCommand<Transaction> DeleteTransactionCommand { get; }

        private Person _selectedPerson;
        private string _personId;
        private decimal _totalIncome;
        private decimal _totalExpense;
        public string PersonId
        {
            get => _personId;
            set
            {
                _personId = value;
                LoadPersonId(value);
            }
        }

        public Person SelectedPerson { get => _selectedPerson; set => SetProperty(ref _selectedPerson, value); }

        #endregion
    }
}
