using JabbR.WinRT.Infrastructure.Janrain;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The User Control item template is documented at http://go.microsoft.com/fwlink/?LinkId=234236

namespace JabbR.WinRT.Controls
{
    public sealed partial class JanrainLogonControl : UserControl
    {
        const int INTERNET_COOKIE_HTTPONLY = 0x00002000;
        private const string LOGON_HTML = @"
<!DOCTYPE html>
<html>
<head>
    <meta name=""MobileOptimized"" content=""width"" />
    <meta name=""HandheldFriendly"" content=""true"" />
    <meta name=""MobileOptimized"" content=""320"" />
    <meta name=""Viewport"" content=""width=320; initial-scale=1.0"" />
    <meta charset=""utf-8"" />
</head>
<body>
    <script type=""text/javascript"">
(function() {
    if (typeof window.janrain !== 'object') window.janrain = {};
    if (typeof window.janrain.settings !== 'object') window.janrain.settings = {};

    window.onerror = function(eventInfo, url, lineNumber){
        alert(eventInfo);
    };
    
    janrain.settings.tokenUrl = 'http://jabbr.net/auth/login.ashx';
    janrain.settings.popup = false;

    function isReady() { janrain.ready = true; };
    if (document.addEventListener) {
      document.addEventListener(""DOMContentLoaded"", isReady, false);
    } else {
      window.attachEvent('onload', isReady);
    }

    var e = document.createElement('script');
    e.type = 'text/javascript';
    e.id = 'janrainAuthWidget';

    if (document.location.protocol === 'https:') {
                e.src = 'https://rpxnow.com/js/lib/jabbr/engage.js';
            } else {
                e.src = 'http://widget-cdn.rpxnow.com/js/lib/jabbr/engage.js';
            }

    var s = document.getElementsByTagName('script')[0];
    s.parentNode.insertBefore(e, s);
})();
</script>
    
    <a class=""janrainEngage"" id=""janrainSignIn"" href=""#""></a>
    <div id=""janrainEngageEmbed""></div>
<script type=""text/javascript"">
    function fireClick() {
        var elem = ""janrainSignIn"";
        if(typeof elem == ""string"") elem = document.getElementById(elem);
        if(!elem) return;

        if(document.dispatchEvent) {   // W3C
            var oEvent = document.createEvent( ""MouseEvents"" );
            oEvent.initMouseEvent(""click"", true, true,window, 1, 1, 1, 1, 1, false, false, false, false, 0, elem);
            elem.dispatchEvent( oEvent );
        }
        else if(document.fireEvent) {   // IE
            elem.click();
        }

        var janrain = document.getElementById(""janrainModal"");
        janrain.style.top = ""0px"";
        janrain.style.marginTop = ""10px"";
    }

    setTimeout(""fireClick()"", 1500);
</script>
</body>
</html>";

        [DllImport("wininet.dll", CharSet = CharSet.Unicode, SetLastError = true)]
        static extern bool InternetGetCookieEx(string pchURL, string pchCookieName, StringBuilder pchCookieData, ref System.UInt32 pcchCookieData, int dwFlags, IntPtr lpReserved);

        public JanrainLogonControl()
        {
            this.InitializeComponent();
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs ev)
        {
            var webviewWrapper = new WebViewWrapper(JanrainBrowser);
            webviewWrapper.Navigating += (s, e) =>
             {
                 // Last received URL: http://jabbr.rpxnow.com/redirect?loc=2ed7673a7691fc646f27efded92b6439f6085c68
                 if (e.LeavingUri.ToString().ToLowerInvariant().StartsWith("http://jabbr.rpxnow.com/redirect?loc="))
                 {
                     var cookieString = InternetGetCookieEx("http://jabbr.net/");
                     var cookie = (from cookieStringPair in cookieString.Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries)
                                   let cookiePair = cookieStringPair.Split(new char[] { '=' }, StringSplitOptions.RemoveEmptyEntries)
                                   where cookiePair[0].Trim() == "jabbr.state"
                                   select new
                                       {
                                           Key = cookiePair[0].Trim(),
                                           Cookie = cookiePair[1].Trim()
                                       }).FirstOrDefault();

                     if (cookie != null)
                     {
                         var state = JsonConvert.DeserializeObject<JabbrState>(System.Net.WebUtility.UrlDecode(cookie.Cookie));
                         if (state != null && !string.IsNullOrWhiteSpace(state.userId))
                         {
                             UserId = state.userId;
                         }
                     }
                 }
             };

            JanrainBrowser.AllowedScriptNotifyUris = WebView.AnyScriptNotifyUri;
            JanrainBrowser.NavigateToString(LOGON_HTML);
        }

        private static string InternetGetCookieEx(string url)
        {
            uint sizeInBytes = 0;

            // Gets capacity length first
            InternetGetCookieEx(url, null, null, ref sizeInBytes, INTERNET_COOKIE_HTTPONLY, IntPtr.Zero);

            uint bufferCapacityInChars = (uint)Encoding.Unicode.GetMaxCharCount((int)sizeInBytes);

            // Now get cookie data
            var cookieData = new StringBuilder((int)bufferCapacityInChars);
            InternetGetCookieEx(url, null, cookieData, ref bufferCapacityInChars, INTERNET_COOKIE_HTTPONLY, IntPtr.Zero);

            return cookieData.ToString();
        }

        public string UserId
        {
            get { return (string)GetValue(UserIdProperty); }
            set { SetValue(UserIdProperty, value); }
        }

        // Using a DependencyProperty as the backing store for UserId.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty UserIdProperty = DependencyProperty.Register("UserId", typeof(string), typeof(JanrainLogonControl), new PropertyMetadata(0));
    }
}