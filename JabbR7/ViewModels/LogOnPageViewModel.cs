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
using Caliburn.Micro;
using JabbR.Client;
using JabbR7.Common.ExtensionMethods;
using System.Linq;

namespace JabbR7.ViewModels
{
    public class LogOnPageViewModel : PropertyChangedBase
    {
        private INavigationService _navigationService;

        public LogOnPageViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;
            LoadingIsVisible = Visibility.Visible;

            if (System.IO.IsolatedStorage.IsolatedStorageSettings.ApplicationSettings.Contains("UserId"))
            {
                _userId = System.IO.IsolatedStorage.IsolatedStorageSettings.ApplicationSettings["UserId"] as string;
            }
        }

        public bool CanLogOn
        {
            get { return !string.IsNullOrWhiteSpace(UserId); }
        }

        private string _userId;
        public string UserId
        {
            get { return _userId; }

            set
            {
                if (_userId != value)
                {
                    _userId = value;
                    NotifyOfPropertyChange(() => UserId);
                    NotifyOfPropertyChange(() => CanLogOn);
                }
            }
        }

        private Visibility _loginIsVisible = Visibility.Visible;
        public Visibility LoginIsVisible
        {
            get { return _loginIsVisible; }

            set
            {
                _loginIsVisible = value;
                NotifyOfPropertyChange(() => LoginIsVisible);
            }
        }

        private Visibility _loadingIsVisible = Visibility.Collapsed;
        public Visibility LoadingIsVisible
        {
            get { return _loadingIsVisible; }

            set
            {
                _loadingIsVisible = value;
                NotifyOfPropertyChange(() => LoadingIsVisible);
            }
        }

        public void LogOn()
        {
            LoadingIsVisible = Visibility.Visible;

            var client = new JabbRClient(App.JabbRServerUrl);

            client.Connect(UserId)
                .Then(logOnInfo =>
                    {
                        Caliburn.Micro.Execute.OnUIThread(() => _navigationService.UriFor<MainChatPageViewModel>().Navigate());
                    })
                    .OnError(ex =>
                    {

                    })
                    .Then(() =>
                    {

                    });
        }
    }
}