using MoneyManger.ViewModels;

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
    }
}