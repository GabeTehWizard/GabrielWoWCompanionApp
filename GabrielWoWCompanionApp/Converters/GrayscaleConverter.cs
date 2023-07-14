using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GabrielWoWCompanionApp.Converters;

/// <summary>
/// Converter to adjust the image path used by the talent objects.
/// </summary>
public class GrayscaleConverter : IMultiValueConverter
{
    public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
    {
        if (values.Length != 5)
            throw new ArgumentException("Exactly 5 values must be provided.");

        if (values[0] is not string path)
            throw new ArgumentException("The first value must be an image path string.");

        if (values[1] is int currentTalentPoints && values[2] is int requiredTalents && values[3] is int previousTalentsCurrentPoints && values[4] is int previousTalentsMax 
            && currentTalentPoints > requiredTalents && previousTalentsCurrentPoints == previousTalentsMax && currentTalentPoints != 71)
        {
            return path;
        }

        return $"gray-{path}";
    }

    public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
    {
        // This converter doesn't support converting back from an ImageSource to two values
        throw new NotImplementedException();
    }
}