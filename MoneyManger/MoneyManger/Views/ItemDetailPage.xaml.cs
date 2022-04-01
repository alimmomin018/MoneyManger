using MoneyManger.ViewModels;
using System.ComponentModel;
using Xamarin.Forms;

namespace MoneyManger.Views
{
    public partial class ItemDetailPage : ContentPage
    {
        public ItemDetailPage()
        {
            InitializeComponent();
            BindingContext = new ItemDetailViewModel();
        }
    }
}