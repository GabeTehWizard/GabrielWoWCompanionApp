using System.Globalization;

namespace GabrielWoWCompanionApp.Converters;

/// <summary>
/// Converter that accepts a string and displays a different icon based on the currently selected specialization.
/// </summary>
public class StringToImageSpecConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is string source)
        {
            if (source == "Discipline")
                return ImageSource.FromFile("Resources/Images/" + "power_word_shield_icon.png");
            else if (source == "Holy")
                return ImageSource.FromFile("Resources/Images/" + "holy_icon.jpg");
            else if (source == "Shadow")
                return ImageSource.FromFile("Resources/Images/" + "shadow_icon.jpg");
        }

        return null;
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}
