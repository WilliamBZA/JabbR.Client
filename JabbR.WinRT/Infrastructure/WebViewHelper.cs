using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace JabbR.WinRT.Infrastructure
{
    public class WebViewHelper : DependencyObject
    {
        public static readonly DependencyProperty HtmlProperty =
            DependencyProperty.RegisterAttached("Html", typeof(string), typeof(WebViewHelper), new PropertyMetadata(null, HtmlChanged));

        public static void SetHtml(DependencyObject obj, string value)
        {
            obj.SetValue(HtmlProperty, value);
        }

        public static string GetHtml(DependencyObject obj)
        {
            return (string)obj.GetValue(HtmlProperty);
        }

        private static void HtmlChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var webView = d as WebView;
            if (webView == null)
                return;

            webView.AllowedScriptNotifyUris = WebView.AnyScriptNotifyUri;
            webView.ScriptNotify += webView_ScriptNotify;
            webView.LoadCompleted += webView_LoadCompleted;

            webView.NavigateToString(string.Format(@"<html><head><script type='text/javascript'>
                                                function getHeight() {{
                                                   window.external.notify('{{Height : ' + document.getElementById('htmlWebViewContentContainer').offsetHeight + ',Width : ' + document.getElementById('htmlWebViewContentContainer').offsetWidth + '}}');
                                                }}
                                               </script></head><body><div id='htmlWebViewContentContainer'>{0}</div></body></html>", e.NewValue));
        }

        static void webView_LoadCompleted(object sender, Windows.UI.Xaml.Navigation.NavigationEventArgs e)
        {
            var webView = (sender as WebView);
            webView.LoadCompleted -= webView_LoadCompleted;

            webView.InvokeScript("getHeight", null);
        }

        static void webView_ScriptNotify(object sender, NotifyEventArgs e)
        {
            var webView = (sender as WebView);
            webView.ScriptNotify -= webView_ScriptNotify;

            var dimensions = JsonConvert.DeserializeObject<DimensionClass>(e.Value);

            webView.Height = dimensions.Height + 24;
            if (dimensions.Width != 0)
            {
                webView.Width = dimensions.Width + 24;
            }
        }
    }

    public class DimensionClass
    {
        public double Height { get; set; }
        public double Width { get; set; }
    }
}