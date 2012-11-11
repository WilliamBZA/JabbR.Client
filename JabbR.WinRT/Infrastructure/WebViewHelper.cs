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

            webView.LoadCompleted += webView_LoadCompleted;
            string html = string.Format(@"<html><head>
<style>
hr{{
    margin:0;
    padding:0;
}}
.message{{
    padding: 0;
    border-left: 175px solid #F1F1F1;
    margin:0;
    display: inline-block;
}}
.left{{
    float: left;
    width: 170px;
    padding: 0px;
    margin:0;
    margin-left: -175px;
    font-weight: bold;
}}
.left img{{
    padding-right:10px;
}}

.message .middle {{
    margin-right: 115px;
    padding: 5px;
    word-wrap: break-word;
}}

.message .right {{
    position: absolute;
    padding: 5px;
    right: 0;
    width: 95px;
    font-size:0.9em;
}}
.middle .collapsible_content {{
    display:block;
    font-family:Georgia, ""Times New Roman"", Times, serif;
    white-space: pre;
    white-space: -moz-pre-wrap;
    white-space: -hp-pre-wrap;
    white-space: -o-pre-wrap;
    white-space: -pre-wrap;
    white-space: pre-wrap;
    white-space: pre-line;
    word-wrap: break-word;
}}
.collapsible_title {{
    display:none;
}}
</style>
<script type='text/javascript'>
function scrollToBottom(){{
    window.scrollTo(0, document.body.scrollHeight);
}}
</script></head><body><div id='htmlWebViewContentContainer' style='margin-right:20px;margin-bottom:20px;'>{0}</div></body></html>", e.NewValue);

            webView.NavigateToString(html);
        }

        static void webView_LoadCompleted(object sender, Windows.UI.Xaml.Navigation.NavigationEventArgs e)
        {
            var webView = (sender as WebView);
            webView.LoadCompleted -= webView_LoadCompleted;

            webView.InvokeScript("scrollToBottom", null);
        }

    }
}