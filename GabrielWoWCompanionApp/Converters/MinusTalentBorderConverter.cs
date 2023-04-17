using System.Globalization;

namespace GabrielWoWCompanionApp.Converters;

public class MinusTalentBorderConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is bool selected)
        {

            // Set the color based on the true false value
            if (!selected)
            {
                return Color.FromArgb("#FDD771");
            }
        }
        return Color.FromArgb("#ff3632");
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}
