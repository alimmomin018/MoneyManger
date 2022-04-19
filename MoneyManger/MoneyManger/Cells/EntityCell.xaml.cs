using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MoneyManger.Cells
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class EntityCell : StackLayout
    {
        public EntityCell()
        {
            InitializeComponent();
        }
    }
}