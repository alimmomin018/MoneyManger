using Acr.UserDialogs;
using MoneyManger.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.CommunityToolkit.ObjectModel;
using Xamarin.Forms;

namespace MoneyManger.ViewModels
{
    [QueryProperty(nameof(EntityId), nameof(EntityId))]
    [QueryProperty(nameof(IsReadOnly), nameof(IsReadOnly))]
    [QueryProperty(nameof(TransactionId), nameof(TransactionId))]
    public class NewTransactionPageViewModel : BaseViewModel
    {
        public NewTransactionPageViewModel()
        {
            SaveCommand = new AsyncCommand(() => ExecuteSaveCommand());
        }

        private async Task ExecuteSaveCommand()
        {
            if (!IsFormValid())
                return;

            try
            {
                Enum.TryParse(TransactionTypeSelection, out TransactionType type);
                decimal.TryParse(Amount, out decimal amount);
                int.TryParse(EntityId, out int entityId);
                                
                if (IsNewTransaction)
                {                    
                    var success = await TransactionDataStore.AddTransactionAsync(new Transaction
                    {
                        Type = type,
                        Amount = amount,
                        Date = SelectedDate + SelectedTime,
                        Notes = SelectedNote,
                        EntityId = entityId,
                        Description = Description
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
                    var success = await TransactionDataStore.UpdateTransactionAsync(new Transaction
                    {
                        Type = type,
                        Amount = amount,
                        Date = SelectedDate + SelectedTime,
                        Notes = SelectedNote,
                        EntityId = entityId,
                        Description = Description,
                        TransactionId = SelectedTransaction.TransactionId
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
            IsNotesValid = !string.IsNullOrWhiteSpace(SelectedNote);
            IsAmountValid = !string.IsNullOrWhiteSpace(Amount);

            return IsNotesValid && IsAmountValid;
        }

        private async void LoadTransactionId(string value)
        {
            try
            {
                IsNotesValid = true;
                IsAmountValid = true;

                if (!string.IsNullOrWhiteSpace(value))
                {
                    Title = "Edit Transaction";
                    IsNewTransaction = false;

                    SelectedTransaction = await TransactionDataStore.GetTransactionAsync(int.Parse(value));
                    SelectedDate = SelectedTransaction.Date.Date;
                    SelectedTime = SelectedTransaction.Date.TimeOfDay;
                    TransactionTypeSelection = SelectedTransaction.Type.ToString();
                    Amount = SelectedTransaction.Amount.ToString();
                    SelectedNote = SelectedTransaction.Notes;
                    Description = SelectedTransaction.Description;
                    Photos = SelectedTransaction.Photos;
                }
                else
                {
                    Title = "New Transaction";
                    IsNewTransaction = true;
                    SelectedDate = DateTime.Now;
                    SelectedTime = DateTime.Now.TimeOfDay;
                    TransactionTypeSelection = TransactionType.Expense.ToString();
                    Photos = new List<TransactionPhoto>();
                }

                if (!string.IsNullOrEmpty(IsReadOnly))
                {
                    Title = "Viewing Transaction";
                    IsEnabled = false;
                }
                else
                {
                    NotesString = await TransactionDataStore.GetAllTransactionNotesAsync();
                    IsEnabled = true;
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


        public AsyncCommand SaveCommand { get; }

        private bool IsNewTransaction;
        private Transaction SelectedTransaction;
        private string _transactionId;
        private string _entityId;
        private string _isReadOnly;
        private bool _isEnabled;
        private DateTime _selectedDate;
        private TimeSpan _selectedTime;
        private List<string> _notes;
        private List<TransactionPhoto> _photos;
        private string _selectedNote;
        public string _description;
        private string _amount;
        private bool _isNotesValid;
        private bool _isAmountValid;
        private string _transactionTypeSelection;
        public string EntityId { get => _entityId; set => _entityId = value; }
        public string IsReadOnly { get => _isReadOnly; set => _isReadOnly = value; }
        public bool IsEnabled { get => _isEnabled; set => SetProperty(ref _isEnabled, value); }
        public DateTime SelectedDate { get => _selectedDate; set => SetProperty(ref _selectedDate, value); }
        public TimeSpan SelectedTime { get => _selectedTime; set => SetProperty(ref _selectedTime, value); }
        public List<string> NotesString { get => _notes; set => SetProperty(ref _notes, value); }
        public List<TransactionPhoto> Photos { get => _photos; set => SetProperty(ref _photos, value); }
        public string SelectedNote { get => _selectedNote; set => SetProperty(ref _selectedNote, value); }
        public string Description { get => _description; set => SetProperty(ref _description, value); }
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
