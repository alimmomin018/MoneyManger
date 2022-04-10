using Android.Content;
using MoneyManger.Droid.Renderers;
using MoneyManger.Renderers;
using Xamarin.Forms;
using Xamarin.Forms.Material.Android;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(ExtendedDatePicker), typeof(ExtendedDatePickerRenderer))]
namespace MoneyManger.Droid.Renderers
{
    public class ExtendedDatePickerRenderer : MaterialDatePickerRenderer
    {
        public ExtendedDatePickerRenderer(Context context) : base(context)
        {
        }

        protected override void OnElementChanged(ElementChangedEventArgs<DatePicker> e)
        {
            base.OnElementChanged(e);

            ExtendedDatePicker element = Element as ExtendedDatePicker;
            Control.HintEnabled = true;
            Control.Hint = element.Placeholder;
        }
    }
}