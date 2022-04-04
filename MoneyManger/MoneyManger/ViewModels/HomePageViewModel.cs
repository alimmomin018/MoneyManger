using MoneyManger.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;
using Xamarin.CommunityToolkit.ObjectModel;
using Xamarin.Forms;

namespace MoneyManger.ViewModels
{
    public class HomePageViewModel : BaseViewModel
    {
        private Person _selectedPerson;

        public ObservableCollection<Person> Persons { get; }
        public Command LoadPersonsCommand { get; }
        public AsyncCommand<Person> AddPersonCommand { get; }
        public Command<Person> PersonTapped { get; }

        public HomePageViewModel()
        {
            Title = "Browse";
            Persons = new ObservableCollection<Person>();
            LoadPersonsCommand = new Command(async () => await ExecuteLoadPersonsCommand());

            PersonTapped = new Command<Person>(OnPersonSelected);
            AddPersonCommand = new AsyncCommand<Person>((person) => AddPersonAsync(person));
        }

        async Task ExecuteLoadPersonsCommand()
        {
            IsBusy = true;

            try
            {
                Persons.Clear();
                var items = await DataStore.GetPeoplesAsync(true);
                foreach (var item in items)
                {
                    Persons.Add(item);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            finally
            {
                IsBusy = false;
            }
        }

        public void OnAppearing()
        {
            IsBusy = true;
            SelectedItem = null;
        }

        public Person SelectedItem
        {
            get => _selectedPerson;
            set
            {
                SetProperty(ref _selectedPerson, value);
                OnPersonSelected(value);
            }
        }

        async Task AddPersonAsync(Person obj)
        {
            //await Shell.Current.GoToAsync(nameof(NewItemPage));
        }

        async void OnPersonSelected(Person item)
        {
            if (item == null)
                return;

            // This will push the ItemDetailPage onto the navigation stack
            //await Shell.Current.GoToAsync($"{nameof(ItemDetailPage)}?{nameof(ItemDetailViewModel.ItemId)}={item.Id}");
        }
    }
}
