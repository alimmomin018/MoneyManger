using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MoneyManger.Controls
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CustomDatePicker : Grid
    {
        public static BindableProperty CustomDateProperty = BindableProperty.Create(nameof(CustomDate), typeof(DateTime),
                 typeof(CustomDatePicker), default(DateTime), BindingMode.TwoWay);

        public static BindableProperty PlaceHolderProperty = BindableProperty.Create(nameof(PlaceHolder), typeof(string),
                 typeof(CustomDatePicker), default(string), BindingMode.OneWay);

        //private static void CustomDatePropertyChanged(BindableObject bindable, object oldValue, object newValue)
        //{
        //    var control = (CustomDatePicker)bindable;
        //    control.edpDate.Date = (DateTime)newValue;
        //}

        //private static void PlaceHolderPropertyChanged(BindableObject bindable, object oldValue, object newValue)
        //{
        //    var control = (CustomDatePicker)bindable;
        //    control.edpLabel.Text = newValue.ToString();
        //}

        public CustomDatePicker()
        {
            InitializeComponent();
            BindingContext = this;
        }

        public DateTime CustomDate
        {
            get { return (DateTime)GetValue(CustomDateProperty); }
            set
            {
                SetValue(CustomDateProperty, value);
            }
        }
        
        public string PlaceHolder
        {
            get { return (string)GetValue(PlaceHolderProperty); }
            set
            {
                SetValue(PlaceHolderProperty, value);
            }
        }

        private void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            edpDate.Focus();
        }
    }
}