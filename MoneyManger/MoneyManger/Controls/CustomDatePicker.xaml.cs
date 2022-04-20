using System;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MoneyManger.Controls
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CustomDatePicker : StackLayout
    {
        public static BindableProperty CustomDateProperty = BindableProperty.Create(nameof(CustomDate), typeof(DateTime),
                 typeof(CustomDatePicker), default(DateTime), BindingMode.TwoWay);

        public static BindableProperty PlaceHolderProperty = BindableProperty.Create(nameof(PlaceHolder), typeof(string),
                 typeof(CustomDatePicker), default(string), BindingMode.OneWay);

        public CustomDatePicker()
        {
            InitializeComponent();
        }

        public DateTime CustomDate
        {
            get { return (DateTime)GetValue(CustomDateProperty); }
            set { SetValue(CustomDateProperty, value); }
        }
        
        public string PlaceHolder
        {
            get { return (string)GetValue(PlaceHolderProperty); }
            set { SetValue(PlaceHolderProperty, value); }
        }

        private void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            edpDate.Focus();
        }
    }
}