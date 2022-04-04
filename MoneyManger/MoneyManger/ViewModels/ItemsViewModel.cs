﻿using MoneyManger.Models;
using MoneyManger.Views;
using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace MoneyManger.ViewModels
{
    public class ItemsViewModel : BaseViewModel
    {
        private Person _selectedPerson;

        public ObservableCollection<Person> Persons { get; }
        public Command LoadPersonsCommand { get; }
        public Command AddPersonCommand { get; }
        public Command<Person> PersonTapped { get; }

        public ItemsViewModel()
        {
            Title = "Browse";
            Persons = new ObservableCollection<Person>();
            LoadPersonsCommand = new Command(async () => await ExecuteLoadPersonsCommand());

            PersonTapped = new Command<Person>(OnPersonSelected);

            AddPersonCommand = new Command(OnPersonItem);
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

        private async void OnPersonItem(object obj)
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