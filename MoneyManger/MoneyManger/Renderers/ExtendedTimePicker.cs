using Xamarin.Forms;

namespace MoneyManger.Renderers
{
    public class ExtendedTimePicker : TimePicker
    {
        public static readonly BindableProperty EnterTextProperty =
           BindableProperty.Create(propertyName: nameof(Placeholder),
                                   returnType: typeof(string),
                                   declaringType: typeof(ExtendedTimePicker),
                                   defaultValue: default(string));
        public string Placeholder { get; set; }
    }
}
