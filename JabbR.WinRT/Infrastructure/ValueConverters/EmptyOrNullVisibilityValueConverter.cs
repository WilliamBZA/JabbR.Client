using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Data;

namespace JabbR.WinRT.Infrastructure.ValueConverters
{
    public class EmptyOrNullVisibilityValueConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            var objectString = value as string;

            if (string.IsNullOrWhiteSpace(objectString))
            {
                if (parameter as string == "Opposite")
                {
                    return Visibility.Visible;
                }

                return Visibility.Collapsed;
            }

            if (parameter as string == "Opposite")
            {
                return Visibility.Collapsed;
            }

            return Visibility.Visible;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}