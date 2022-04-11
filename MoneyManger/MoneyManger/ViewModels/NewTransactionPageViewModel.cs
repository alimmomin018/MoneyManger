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
    [QueryProperty(nameof(TransactionId), nameof(TransactionId))]
    public class NewTransactionPageViewModel : BaseViewModel
    {
        public NewTransactionPageViewModel()
        {
            SaveCommand = new AsyncCommand(() => ExecuteSaveCommand());
        }

        private async Task ExecuteSaveCommand()
        {
            if (string.IsNullOrWhiteSpace(Notes))
            {
                IsNotesValid = true;
            }
        }

        private void OnNotesTextChanged()
        {
            // TODO: Add suggestion list view
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
                SelectedDate = DateTime.Now.AddDays(-3);
                SelectedTime = DateTime.Now.TimeOfDay;
            }
        }

        private bool IsNewTransaction;
        private string _transactionId;
        private DateTime _selectedDate;
        private TimeSpan _selectedTime;
        private string _notes;
        private bool _isNotesValid;
        private string _transactionTypeSelection;

        public AsyncCommand SaveCommand { get; }
        public string TransactionId
        {
            get => _transactionId;
            set
            {
                _transactionId = value;
                LoadTransactionId(value);
            }
        }

        public DateTime SelectedDate { get => _selectedDate; set => SetProperty(ref _selectedDate, value); }
        public TimeSpan SelectedTime { get => _selectedTime; set => SetProperty(ref _selectedTime, value); }
        public string Notes { get => _notes; set => SetProperty(ref _notes, value); }
        public string TransactionTypeSelection { get => _transactionTypeSelection; set => SetProperty(ref _transactionTypeSelection, value); }
        public bool IsNotesValid
        {
            get => _isNotesValid; 
            set
            {
                SetProperty(ref _isNotesValid, value);
                OnNotesTextChanged();
            }
        }
    }
}
