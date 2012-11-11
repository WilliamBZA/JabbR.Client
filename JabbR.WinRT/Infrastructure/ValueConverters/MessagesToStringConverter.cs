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
            var messages = (value as IEnumerable<MessageViewModel>);
            if (messages != null)
            {
                var stringBuilder = new StringBuilder();
                string previousUsername = null;
                bool showUserIcon = false;

                foreach (var msg in messages.ToList())
                {
                    if (previousUsername == null || previousUsername != msg.User.Name)
                    {
                        if (previousUsername != null)
                        {
                            stringBuilder.Append("<hr/>");
                        }

                        previousUsername = msg.User.Name;
                        showUserIcon = true;
                    }

                    stringBuilder.Append("<div class='message'>");
                    stringBuilder.Append("<div class='left'>");

                    if (showUserIcon)
                    {
                        stringBuilder.Append("<img src='");
                        stringBuilder.Append("https://secure.gravatar.com/avatar/");
                        stringBuilder.Append(msg.User.Hash);
                        stringBuilder.Append("?s=16&d=mm' />");
                        stringBuilder.Append(msg.User.Name);
                    }
                    else
                    {
                        stringBuilder.Append("&nbsp;");
                    }
                    stringBuilder.Append("</div>"); // left
                    stringBuilder.Append("<div class='middle'>");
                    stringBuilder.Append(msg.Content);
                    stringBuilder.Append("</div>"); // middle

                    stringBuilder.Append("<div class='right'>");
                    stringBuilder.Append(msg.When.ToLocalTime().ToString("T"));
                    stringBuilder.Append("</div>"); // right

                    stringBuilder.Append("</div>"); // message

                    stringBuilder.Append("<br/>");

                    showUserIcon = false;
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