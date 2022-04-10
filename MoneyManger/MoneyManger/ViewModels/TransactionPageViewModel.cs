using MoneyManger.Models;
using System;
using System.Collections.Generic;
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
            AddTransactionCommand = new AsyncCommand(() => AddTransactionAsync());
        }

        private async Task AddTransactionAsync()
        {
            //await Shell.Current.GoToAsync($"{nameof(TransactionPage)}?{nameof(TransactionPageViewModel.PersonId)}={person.Id}");
        }

        private async void LoadPersonId(string value)
        {
            try
            {
                int personId = int.Parse(value);
                var person = await TransactionDataStore.GetAllTransactionsForPersonAsync(personId);

                Title = person.Name;
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

        private Person _selectedPerson;
        public ObservableRangeCollection<Transaction> Transactions { get; }
        public AsyncCommand AddTransactionCommand { get; }

        private string _personId;
        public string PersonId
        {
            get => _personId;
            set
            {
                _personId = value;
                LoadPersonId(value);
            }
        }        
    }
}
