using Acr.UserDialogs;
using MoneyManger.Models;
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
    [QueryProperty(nameof(TransactionId), nameof(TransactionId))]
    public class NewTransactionPageViewModel : BaseViewModel
    {
        public NewTransactionPageViewModel()
        {
            SaveCommand = new AsyncCommand(() => ExecuteSaveCommand());
            Init();
        }

        private void Init()
        {
            IsNotesValid = true;
            IsAmountValid = true;
            TransactionTypeSelection = TransactionType.Expense.ToString();
        }

        private async Task ExecuteSaveCommand()
        {
            if (!IsFormValid())
                return;

            try
            {
                Enum.TryParse(TransactionTypeSelection, out TransactionType type);
                decimal.TryParse(Amount, out decimal amount);
                int.TryParse(PersonId, out int personId);

                if (IsNewTransaction)
                {
                    var success = await TransactionDataStore.AddTransactionAsync(new Transaction
                    {
                        Type = type,
                        Amount = amount,
                        Date = SelectedDate + SelectedTime,
                        Notes = new Note
                        {
                            Notes = Notes
                        },
                        PersonId = personId,
                        Description = ""
                    });

                    if (success)
                    {
                        UserDialogs.Instance.Toast(Constants.TRANSACTION_ADD_SUCCESS);
                        await Shell.Current.GoToAsync("..");
                    }
                    else
                    {
                        UserDialogs.Instance.Toast(Constants.TRANSACTION_ADD_FAILED);
                    }
                }
                else
                {
                    int.TryParse(TransactionId, out int transactionId);

                    var success = await TransactionDataStore.UpdateTransactionAsync(new Transaction
                    {
                        Type = type,
                        Amount = amount,
                        Date = SelectedDate + SelectedTime,
                        Notes = new Note
                        {
                            Notes = Notes
                        },
                        PersonId = personId,
                        Description = "",
                        TransactionId = transactionId
                    });

                    if (success)
                    {
                        UserDialogs.Instance.Toast(Constants.TRANSACTION_UPDATE_SUCCESS);
                        await Shell.Current.GoToAsync("..");
                    }
                    else
                    {
                        UserDialogs.Instance.Toast(Constants.TRANSACTION_UPDATE_FAILED);
                    }
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

        private void OnNotesTextChanged()
        {
            // TODO: Add suggestion list view
        }

        private bool IsFormValid()
        {
            IsNotesValid = !string.IsNullOrWhiteSpace(Notes);
            IsAmountValid = !string.IsNullOrWhiteSpace(Amount);

            return IsNotesValid && IsAmountValid;
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
                SelectedDate = DateTime.Now;
                SelectedTime = DateTime.Now.TimeOfDay;
            }
        }


        public AsyncCommand SaveCommand { get; }

        private bool IsNewTransaction;
        private string _transactionId;
        private string _personId;
        private DateTime _selectedDate;
        private TimeSpan _selectedTime;
        private string _notes;
        private string _amount;
        private bool _isNotesValid;
        private bool _isAmountValid;
        private string _transactionTypeSelection;
        public string PersonId { get => _personId; set => _personId = value; }
        public DateTime SelectedDate { get => _selectedDate; set => SetProperty(ref _selectedDate, value); }
        public TimeSpan SelectedTime { get => _selectedTime; set => SetProperty(ref _selectedTime, value); }
        public string Notes { get => _notes; set => SetProperty(ref _notes, value); }
        public string Amount { get => _amount; set => SetProperty(ref _amount, value); }
        public string TransactionTypeSelection { get => _transactionTypeSelection; set => SetProperty(ref _transactionTypeSelection, value); }
        public bool IsAmountValid { get => _isAmountValid; set => SetProperty(ref _isAmountValid, value); }
        public bool IsNotesValid
        {
            get => _isNotesValid;
            set
            {
                SetProperty(ref _isNotesValid, value);
                OnNotesTextChanged();
            }
        }
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
