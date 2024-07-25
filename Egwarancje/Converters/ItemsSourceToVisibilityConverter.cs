using System.Collections;
using System.Globalization;

namespace Egwarancje.Converters
{
    public class ItemsSourceToVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var collection = value as ICollection;
            return collection != null && collection.Count > 0;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return false;
        }
    }
}
