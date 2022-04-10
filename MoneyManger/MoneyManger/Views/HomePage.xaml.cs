using MoneyManger.ViewModels;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MoneyManger.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class HomePage : ContentPage
    {
        HomePageViewModel vm;
        public HomePage()
        {
            InitializeComponent();
            BindingContext = vm = new HomePageViewModel();
        }

        protected async override void OnAppearing()
        {
            await vm.LoadPersonsCommand.ExecuteAsync();
        }
    }
}