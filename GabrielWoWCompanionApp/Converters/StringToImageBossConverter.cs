using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GabrielWoWCompanionApp.Converters;

/// <summary>
/// Converter that receives an encounter's string and displays the associated encounter image.
/// </summary>
public class StringToImageBossConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is string source)
        {
            if(source.ToLower().Contains("ignis"))
                return ImageSource.FromFile("Resources/Images/" + "ignis_icon.jpg");
            else if (source.ToLower().Contains("razorscale"))
                return ImageSource.FromFile("Resources/Images/" + "razorscale_icon.jpg");
            else if (source.ToLower().Contains("xt-002"))
                return ImageSource.FromFile("Resources/Images/" + "xt_icon.jpg");
            else if (source.ToLower().Contains("assembly"))
                return ImageSource.FromFile("Resources/Images/" + "assembly_icon.jpg");
            else if (source.ToLower().Contains("kologarn"))
                return ImageSource.FromFile("Resources/Images/" + "kologarn_icon.jpg");
            else if (source.ToLower().Contains("auriaya"))
                return ImageSource.FromFile("Resources/Images/" + "auriaya_icon.jpg");
            else if (source.ToLower().Contains("thorim"))
                return ImageSource.FromFile("Resources/Images/" + "thorim_icon.jpg");
            else if (source.ToLower().Contains("hodir"))
                return ImageSource.FromFile("Resources/Images/" + "hodir_icon.png");
            else if (source.ToLower().Contains("freya"))
                return ImageSource.FromFile("Resources/Images/" + "freya_icon.jpg");
            else if (source.ToLower().Contains("mimiron"))
                return ImageSource.FromFile("Resources/Images/" + "mimiron_icon.jpg");
            else if (source.ToLower().Contains("vezax"))
                return ImageSource.FromFile("Resources/Images/" + "vezax_icon.jpg");
            else if (source.ToLower().Contains("yogg"))
                return ImageSource.FromFile("Resources/Images/" + "yogg_icon.jpg");
            else if (source.ToLower().Contains("algalon"))
                return ImageSource.FromFile("Resources/Images/" + "algalon_icon.jpg");
        }

        return null;
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}
