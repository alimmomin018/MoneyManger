using MoneyManger.ViewModels;
using System.Threading.Tasks;
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
            await vm.LoadEntitiesCommand.ExecuteAsync();
        }

        private void SearchIcon_Tapped(object sender, System.EventArgs e)
        {
            if (sbEntity.IsVisible)
                CloseSearchBar();
            else
                OpenSearchBar();            
        }

        private void CloseSearchBar()
        {
            sbEntity.TranslateTo(0, -50);
            sbEntity.FadeTo(0, 500);
            sbEntity.IsVisible = false;
        }

        private void OpenSearchBar()
        {
            sbEntity.TranslateTo(0, 0);
            sbEntity.FadeTo(1, 500);
            sbEntity.IsVisible = true;
        }
    }
}