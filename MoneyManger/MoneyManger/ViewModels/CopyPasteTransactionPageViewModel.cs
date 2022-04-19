using Acr.UserDialogs;
using MoneyManger.Models;
using System;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.CommunityToolkit.ObjectModel;
using Xamarin.Forms;

namespace MoneyManger.ViewModels
{
    [QueryProperty(nameof(TransactionId), nameof(TransactionId))]
    public class CopyPasteTransactionPageViewModel : BaseViewModel
    {
        public CopyPasteTransactionPageViewModel()
        {
            Entities = new ObservableRangeCollection<Entity>();
            ExecuteLoadEntitiesCommand().ConfigureAwait(false);
            CopyCommand = new AsyncCommand(() => CopyAsync());
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
            catch (ApplicationException aex)
            {
                aex.ShowApplicationExceptionErrorDialogAndHideLoading();
            }
            catch (Exception ex)
            {
                ex.ShowExceptionErrorDialogAndHideLoading();
            }
            finally { IsBusy = false; }
        }

        async Task CopyAsync()
        {
            try
            {                
                var selectedEntities = Entities.Where(x => x.IsSelected);
                if (selectedEntities.Count() == 0)
                {
                    UserDialogs.Instance.Alert(Constants.COPY_NOT_NULL_OR_EMPTY_SELECTION);
                    return;
                }

                var selectedEntityNames = selectedEntities.Select(x => x.Name);
                var selectedEntityNamesJoined = selectedEntityNames.Aggregate((a, b) => a + ", " + b);

                var response = await UserDialogs.Instance.ConfirmAsync(
                    $"{Constants.COPY_CONFIRM_MESSAGE}{selectedEntityNamesJoined}?",
                    Constants.COPY_CONFIRM_TITLE, 
                    Constants.OK_LABEL, 
                    Constants.CANCEL_LABEL);

                if (response)
                {
                    var transaction = await TransactionDataStore.GetTransactionAsync(int.Parse(TransactionId));

                    foreach (var entity in selectedEntities)
                    {
                        // copy transaction
                        var newTransaction = new Transaction
                        {
                            Date = transaction.Date,
                            Type = transaction.Type,
                            Amount = transaction.Amount,
                            Notes = transaction.Notes,
                            Description = transaction.Description,
                            EntityId = entity.EntityId,
                        };

                        // insert transaction
                        await TransactionDataStore.AddTransactionAsync(newTransaction);
                    }

                    UserDialogs.Instance.Toast(Constants.COPY_TRANSACTION_SUCCESS);
                    await Shell.Current.GoToAsync("..");
                }                
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

        private string _transactionId;
        public ObservableRangeCollection<Entity> Entities { get; }
        public AsyncCommand CopyCommand { get; }

        public string TransactionId
        {
            get => _transactionId;
            set
            {
                _transactionId = value;
            }
        }
        
        #endregion
    }
}
