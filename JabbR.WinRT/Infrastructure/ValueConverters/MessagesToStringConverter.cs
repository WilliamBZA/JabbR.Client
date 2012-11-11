using JabbR.WinRT.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Data;

namespace JabbR.WinRT.Infrastructure.ValueConverters
{
    public class MessagesToStringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            var messages = value as IEnumerable<MessageViewModel>;
            if (messages != null)
            {
                var stringBuilder = new StringBuilder();
                string previousUsername = null;

                foreach (var msg in messages)
                {
                    if (previousUsername == null)
                    {
                        previousUsername = msg.User.Name;
                    }

                    stringBuilder.Append(msg.Content);
                    stringBuilder.Append("<br/>");
                    if (previousUsername != msg.User.Name)
                    {
                        stringBuilder.Append("<hr/>");
                    }
                }

                return stringBuilder.ToString();
            }

            return string.Empty;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}