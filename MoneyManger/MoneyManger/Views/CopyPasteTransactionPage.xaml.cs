using MoneyManger.ViewModels;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MoneyManger.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CopyPasteTransactionPage : ContentPage
    {
        CopyPasteTransactionPageViewModel vm;
        public CopyPasteTransactionPage()
        {
            InitializeComponent();
            BindingContext = vm = new CopyPasteTransactionPageViewModel();
        }
    }
}