using MoneyManger.iOS.Renderers;
using MoneyManger.Renderers;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Material.iOS;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(ExtendedTimePicker), typeof(ExtendedTimePickerRenderer))]
namespace MoneyManger.iOS.Renderers
{
    public class ExtendedTimePickerRenderer : MaterialTimePickerRenderer, IMaterialEntryRenderer
    {
        public ExtendedTimePickerRenderer() : base() { }

        protected override void OnElementChanged(ElementChangedEventArgs<TimePicker> e)
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
                ExtendedTimePicker element = Element as ExtendedTimePicker;
                return element.Placeholder;
            }
        }
    }
}