using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using Microsoft.Phone.Controls;
using JabbR7.Common.ExtensionMethods;
using Newtonsoft.Json;
using System.Threading.Tasks;
using JabbR7.ViewModels;

namespace JabbR7.Views
{
    // Awful, hacky break of the MVVM pattern. Need direct binding between UI and model for
    // some features though, so whatever. Accepted ugliness.
    public partial class LogOnPage : PhoneApplicationPage
    {
        private const string LogInPageHtml = @"<!DOCTYPE html>
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
        (function () {
            if (typeof window.janrain !== 'object') window.janrain = {};
            if (typeof window.janrain.settings !== 'object') window.janrain.settings = {};

            janrain.settings.tokenUrl = 'http://jabbr.net/auth/login.ashx';

            function isReady() {
                janrain.ready = true;
            };
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

        setTimeout(""window.external.notify('LoadingDone')"", 100);
    }

    function startProcess() {
        setTimeout(""fireClick()"", 1500);
    }
</script>
</body>
</html>";

        public string FinalRedirectUrl { get; set; }

        public LogOnPage()
        {
            InitializeComponent();
        }

        private void PhoneApplicationPage_Loaded(object sender, RoutedEventArgs e)
        {
            var context = DataContext as LogOnPageViewModel;
            context.LoadingIsVisible = System.Windows.Visibility.Visible;

            FinalRedirectUrl = "http://jabbr.net/";

            janrainBrowser.Navigated += new EventHandler<System.Windows.Navigation.NavigationEventArgs>(browser_Navigated);
            janrainBrowser.Navigating += new EventHandler<NavigatingEventArgs>(janrainBrowser_Navigating);
            janrainBrowser.IsScriptEnabled = true;

            janrainBrowser.NavigateToString(LogInPageHtml);
        }

        void janrainBrowser_Navigating(object sender, NavigatingEventArgs e)
        {
            SetLoading();
        }

        void browser_Navigated(object sender, System.Windows.Navigation.NavigationEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(e.Uri.ToString()))
            {
                Dispatcher.BeginInvoke(() => janrainBrowser.InvokeScript("eval", "setTimeout(\"startProcess()\", 100);"));

                janrainBrowser.ScriptNotify += (s, ev) =>
                    {
                        HideLoading();
                    };
            }
            else if (e.Uri.ToString() == FinalRedirectUrl)
            {
                Dispatcher.BeginInvoke(() => janrainBrowser.Visibility = System.Windows.Visibility.Collapsed);

                var jabbrState = JsonConvert.DeserializeObject<JabbRStateDTO>(janrainBrowser.GetCookies()["jabbr.state"].Value);

                var context = DataContext as LogOnPageViewModel;
                SetLoading();
                context.UserId = jabbrState.userId;

                context.LogOn();
            }
            else if (e.Uri.Host != "jabbr.rpxnow.com")
            {
                HideLoading();
            }
            else
            {

            }
        }

        private void SetLoading()
        {
            var context = DataContext as LogOnPageViewModel;
            context.LoadingIsVisible = System.Windows.Visibility.Visible;
        }

        private void HideLoading()
        {
            var context = DataContext as LogOnPageViewModel;
            context.LoadingIsVisible = System.Windows.Visibility.Collapsed;
        }

        public class JabbRStateDTO
        {
            public string userId { get; set; }
        }
    }
}