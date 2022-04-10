using MoneyManger.ViewModels;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MoneyManger.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class NewTransactionPage : ContentPage
    {
        NewTransactionPageViewModel vm;
        public NewTransactionPage()
        {
            InitializeComponent();
            BindingContext = vm = new NewTransactionPageViewModel();
        }
    }
}