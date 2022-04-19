using MoneyManger.Views;
using Xamarin.Forms;

namespace MoneyManger
{
    public partial class AppShell : Xamarin.Forms.Shell
    {
        public AppShell()
        {
            InitializeComponent();
            RegisterRoutes();
        }

        private void RegisterRoutes()
        {

            Routing.RegisterRoute(nameof(HomePage), typeof(HomePage));
            Routing.RegisterRoute(nameof(TransactionPage), typeof(TransactionPage));
            Routing.RegisterRoute(nameof(NewTransactionPage), typeof(NewTransactionPage));
            Routing.RegisterRoute(nameof(CopyPasteTransactionPage), typeof(CopyPasteTransactionPage));
        }
    }
}
