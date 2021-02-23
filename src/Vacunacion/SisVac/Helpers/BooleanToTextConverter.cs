using System;
using System.Globalization;
using Xamarin.Forms;

namespace SisVac.Helpers
{
    public class BooleanToTextConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (System.Convert.ToBoolean(value))
                return "Sí";
            return "No";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return (((String)value) == "Sí");
        }
    }
}
