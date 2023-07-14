using System.Globalization;

namespace GabrielWoWCompanionApp.Converters;

/// <summary>
/// Converter to truncate display values.
/// </summary>
public class TruncateConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is decimal d)
        {
            return Math.Truncate(d * 10) / 10;
        }

        return null;
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}
