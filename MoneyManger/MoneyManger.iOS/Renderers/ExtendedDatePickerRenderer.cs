using MoneyManger.Renderers;
using MoneyManger.iOS.Renderers;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Material.iOS;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(ExtendedDatePicker), typeof(ExtendedDatePickerRenderer))]
namespace MoneyManger.iOS.Renderers
{
    public class ExtendedDatePickerRenderer : MaterialDatePickerRenderer, IMaterialEntryRenderer
    {
        public ExtendedDatePickerRenderer() : base() { }

        protected override void OnElementChanged(ElementChangedEventArgs<DatePicker> e)
        {
            base.OnElementChanged(e);

            if (Control != null)
            {
                UITextField entry = Control;
                try
                {
                    if (UIDevice.CurrentDevice.CheckSystemVersion(13, 2))
                    {
                        UIDatePicker picker = (UIDatePicker)entry.InputView;
                        picker.PreferredDatePickerStyle = UIDatePickerStyle.Wheels;
                    }
                }
                catch
                {
                    // do nothing
                }
            }

        }

        string IMaterialEntryRenderer.Placeholder
        {
            get
            {
                ExtendedDatePicker element = Element as ExtendedDatePicker;
                return element.Placeholder;
            }
        }
    }
}