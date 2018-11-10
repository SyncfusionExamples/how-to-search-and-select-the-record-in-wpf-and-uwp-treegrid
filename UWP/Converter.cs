using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Media.Imaging;

namespace SfTreeGridDemo
{
    class StringToImageConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            bool IsPassOrFail = (bool)value;
            if (IsPassOrFail)
            {
                Uri uri = new Uri("ms-appx:///Images/icon.png");
                BitmapImage image = new BitmapImage(uri);
                return image;
            }

            else
            {
                Uri uri1 = new Uri("ms-appx:///Images/Untied.png");
                BitmapImage image = new BitmapImage(uri1);
                return image;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}
