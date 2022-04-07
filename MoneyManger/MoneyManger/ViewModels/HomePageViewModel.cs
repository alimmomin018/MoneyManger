using Acr.UserDialogs;
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

        public ObservableRangeCollection<Person> Persons { get; }
        public AsyncCommand LoadPersonsCommand { get; }
        public AsyncCommand AddPersonCommand { get; }
        public AsyncCommand<Person> PersonTapped { get; }

        public HomePageViewModel()
        {
            Title = "Browse";
            Persons = new ObservableRangeCollection<Person>();
            LoadPersonsCommand = new AsyncCommand(() => ExecuteLoadPersonsCommand());

            PersonTapped = new AsyncCommand<Person>((p) => OnPersonSelected(p));
            AddPersonCommand = new AsyncCommand(() => AddPersonAsync());
        }

        async Task ExecuteLoadPersonsCommand()
        {
            IsBusy = true;

            try
            {
                var peoples = await PersonDataStore.GetAllPeopleAsync();
                Persons.Clear();
                Persons.AddRange(peoples);                
            }
            catch (Exception ex)
            {
                throw new ApplicationException(ex.Message);
            }
            finally
            {
                IsBusy = false;
            }
        }

        public Person SelectedItem
        {
            get => _selectedPerson;
            set => SetProperty(ref _selectedPerson, value);
        }

        async Task AddPersonAsync()
        {
            var response = await UserDialogs.Instance.PromptAsync(
                "What is the name of the entity you want to manage money for?",
                "Entity Name",
                "OK",
                "Cancel",
                "Name",
                InputType.Default);

            if(!string.IsNullOrWhiteSpace(response.Text))
            {
                var newPerson = new Person
                {
                    Name = response.Text
                };

                var success = await PersonDataStore.AddPersonAsync(newPerson);
                if (success)
                {
                    UserDialogs.Instance.Toast("Entity added successfully");
                }
            }
        }

        async Task OnPersonSelected(Person item)
        {
            if (item == null)
                return;

            // This will push the ItemDetailPage onto the navigation stack
            //await Shell.Current.GoToAsync($"{nameof(ItemDetailPage)}?{nameof(ItemDetailViewModel.ItemId)}={item.Id}");
        }
    }
}
