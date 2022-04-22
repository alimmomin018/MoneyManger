using MoneyManger.ViewModels;
using System;
using System.ComponentModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MoneyManger.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TransactionPage : ContentPage
    {
        TransactionPageViewModel vm;
        public TransactionPage()
        {
            InitializeComponent();
            BindingContext = vm = new TransactionPageViewModel();
        }

        private async void All_Tapped(object sender, EventArgs e)
        {
            ClearSelection();
            vm.StartDate = null;
            vm.EndDate = DateTime.Now;
            lbAll.FontAttributes = FontAttributes.Bold;
            UpdateFilterTitle("Viewing All Transactions");
            await vm.LoadTransactionsCommand.ExecuteAsync();
        }

        private async void FourWeeks_Tapped(object sender, EventArgs e)
        {
            ClearSelection();
            vm.StartDate = DateTime.Now.AddDays(-28);
            vm.EndDate = DateTime.Now;
            UpdateFilterTitle("Viewing 4 weeks Transactions");
            lbFourWeeks.FontAttributes = FontAttributes.Bold;
            await vm.LoadTransactionsCommand.ExecuteAsync();
        }

        private async void ThreeMonths_Tapped(object sender, EventArgs e)
        {
            ClearSelection();
            vm.StartDate = DateTime.Now.AddMonths(-3);
            vm.EndDate = DateTime.Now;
            UpdateFilterTitle("Viewing 3 months Transactions");
            lbThreeMonths.FontAttributes = FontAttributes.Bold;
            await vm.LoadTransactionsCommand.ExecuteAsync();
        }

        private async void SixMonths_Tapped(object sender, EventArgs e)
        {
            ClearSelection();
            vm.StartDate = DateTime.Now.AddMonths(-6);
            vm.EndDate = DateTime.Now;
            UpdateFilterTitle("Viewing 6 months Transactions");
            lbSixMonths.FontAttributes = FontAttributes.Bold;
            await vm.LoadTransactionsCommand.ExecuteAsync();
        }

        private async void TwelveMonths_Tapped(object sender, EventArgs e)
        {
            ClearSelection();
            vm.StartDate = DateTime.Now.AddMonths(-12);
            vm.EndDate = DateTime.Now;
            UpdateFilterTitle("Viewing 12 months Transactions");
            lbTwelveMonths.FontAttributes = FontAttributes.Bold;
            await vm.LoadTransactionsCommand.ExecuteAsync();
        }

        private void Custom_Tapped(object sender, EventArgs e)
        {
            ClearSelection();
            UpdateFilterTitle("Viewing Custom Transactions");
            stCustomDateView.IsVisible = true;
        }

        private void UpdateFilterTitle(string title)
        {
            lbFilterTitle.Text = title;
        }

        private void ClearSelection()
        {
            lbAll.FontAttributes = FontAttributes.None;
            lbFourWeeks.FontAttributes = FontAttributes.None;
            lbThreeMonths.FontAttributes = FontAttributes.None;
            lbSixMonths.FontAttributes = FontAttributes.None;
            lbTwelveMonths.FontAttributes = FontAttributes.None;
            lbCustom.FontAttributes = FontAttributes.None;
            stCustomDateView.IsVisible = false;
        }

        private async void FilterTitle_Tapped(object sender, EventArgs e)
        {
            if(lbFilterTitle.Text == "Viewing Custom Transactions")
            {
                vm.StartDate = dtCustomStartDate.CustomDate;
                vm.EndDate = dtCustomEndDate.CustomDate;
                await vm.LoadTransactionsCommand.ExecuteAsync();
            }
        }
    }
}