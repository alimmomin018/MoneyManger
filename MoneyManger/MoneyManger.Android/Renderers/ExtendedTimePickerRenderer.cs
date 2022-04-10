using Android.Content;
using MoneyManger.Droid.Renderers;
using MoneyManger.Renderers;
using Xamarin.Forms;
using Xamarin.Forms.Material.Android;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(ExtendedTimePicker), typeof(ExtendedTimePickerRenderer))]
namespace MoneyManger.Droid.Renderers
{
    public class ExtendedTimePickerRenderer : MaterialTimePickerRenderer
    {
        public ExtendedTimePickerRenderer(Context context) : base(context)
        {
        }

        protected override void OnElementChanged(ElementChangedEventArgs<TimePicker> e)
        {
            base.OnElementChanged(e);

            ExtendedTimePicker element = Element as ExtendedTimePicker;
            Control.HintEnabled = true;
            Control.Hint = element.Placeholder;
        }
    }
}