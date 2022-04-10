using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace MoneyManger.ViewModels
{
    [QueryProperty(nameof(TransactionId), nameof(TransactionId))]
    public class NewTransactionPageViewModel : BaseViewModel
    {
        public NewTransactionPageViewModel()
        {

        }

        private void LoadTransactionId(string value)
        {
            if (!string.IsNullOrWhiteSpace(value))
            {
                Title = "Edit Transaction";
                IsNewTransaction = false;
            }
            else
            {
                Title = "New Transaction";
                IsNewTransaction = true;
            }
        }

        private bool IsNewTransaction;
        private string _transactionId;
        public string TransactionId
        {
            get => _transactionId;
            set
            {
                _transactionId = value;
                LoadTransactionId(value);
            }
        }
    }
}
