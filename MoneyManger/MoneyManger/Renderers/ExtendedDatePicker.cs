using Xamarin.Forms;

namespace MoneyManger.Renderers
{
    public class ExtendedDatePicker : DatePicker
    {
        public static readonly BindableProperty EnterTextProperty =
            BindableProperty.Create(propertyName: nameof(Placeholder),
                                    returnType: typeof(string),
                                    declaringType: typeof(ExtendedDatePicker),
                                    defaultValue: default(string));
        public string Placeholder { get; set; }
    }
}
