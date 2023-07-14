using System.Globalization;


namespace GabrielWoWCompanionApp.Converters;

/// <summary>
/// Converter to provide different font colours based on the decimal value passed in.
/// </summary>
public class ValueToColorConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is decimal d)
        {

            // Set the color based on the value
            if (d == 100m)
            {
                return Color.FromArgb("#E5CC80");
            }
            else if (d > 99m)
            {
                return Color.FromArgb("#E268A8");
            }
            else if (d > 95m)
            {
                return Color.FromArgb("#C96D1E");
            }
            else if (d > 75m)
            {
                return Color.FromArgb("#A335EF");
            }
            else if (d > 50m)
            {
                return Color.FromArgb("#0070FF");
            }
            else if (d > 25m)
            {
                return Color.FromArgb("#1EFF00");
            }
        }

        // Default to black if the value cannot be converted or does not meet the conditions
        return Color.FromArgb("#1EFF00");
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}
