using System.Globalization;

namespace Egwarancje.Converters;

public class BoolToObjectConverter : IValueConverter
{
    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        if (value is bool boolValue && parameter != null)
            return boolValue == bool.Parse(parameter.ToString()!);

        return false;
    }

    public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        if (value is bool boolValue && parameter != null)
            return boolValue == bool.Parse(parameter.ToString()!);

        return false;
    }
}
