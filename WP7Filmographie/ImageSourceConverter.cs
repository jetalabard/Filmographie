using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using System.Windows.Resources;
using System.Windows.Media.Imaging;

namespace WP7Filmographie
{

    /// <summary>
    /// permet de convertir une image
    /// </summary>
    public class ImageSourceConverter : System.Windows.Data.IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            string imageName = value as string;
            string valeurParDéfaut = parameter as string;
            if (string.IsNullOrWhiteSpace(imageName))
            {
                imageName = valeurParDéfaut;
            }
          
            BitmapImage image = new BitmapImage(new Uri(
                    string.Format("Data/Image/{0}", imageName),
                    UriKind.Relative));
         
            return image;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
