using System;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MoneyManger.Controls
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CustomTimePicker : StackLayout
    {
        public static BindableProperty CustomTimeProperty = BindableProperty.Create(nameof(CustomTime), typeof(TimeSpan),
                 typeof(CustomTimePicker), default(TimeSpan), BindingMode.TwoWay);

        public static BindableProperty PlaceHolderProperty = BindableProperty.Create(nameof(PlaceHolder), typeof(string),
                 typeof(CustomTimePicker), default(string), BindingMode.OneWay);

        public CustomTimePicker()
        {
            InitializeComponent();
        }

        public TimeSpan CustomTime
        {
            get { return (TimeSpan)GetValue(CustomTimeProperty); }
            set { SetValue(CustomTimeProperty, value); }
        }

        public string PlaceHolder
        {
            get { return (string)GetValue(PlaceHolderProperty); }
            set { SetValue(PlaceHolderProperty, value); }
        }

        private void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            edpTime.Focus();
        }
    }
}