using GalaSoft.MvvmLight;
using JabbR.WinRT.Infrastructure.Settings;
using System;
using System.ComponentModel;
using JabbR.Client;
using System.Collections.Generic;
using System.Linq;
using JabbR.Client.Models;
using JabbR.WinRT.Infrastructure;
using JabbR.WinRT.Views;
using Windows.UI.Xaml;
using GalaSoft.MvvmLight.Threading;
using System.Threading.Tasks;

namespace JabbR.WinRT.ViewModel
{
    /// <summary>
    /// This class contains properties that the main View can data bind to.
    /// <para>
    /// Use the <strong>mvvminpc</strong> snippet to add bindable properties to this ViewModel.
    /// </para>
    /// <para>
    /// You can also use Blend to data bind with the tool's support.
    /// </para>
    /// <para>
    /// See http://www.galasoft.ch/mvvm
    /// </para>
    /// </summary>
    public class MainViewModel : ViewModelBase
    {
        private string _userId;
        private string _username;
        private JabbRClient _client;
        private Visibility _loadingControlVisible;

        public MainViewModel()
        {
            if (!IsInDesignMode)
            {
                PropertyChanged += (s, e) =>
                {
                    if (e.PropertyName == "UserId")
                    {
                        UserIdChanged();
                    }
                };

                DispatcherHelper.Initialize();

                Task.Factory.StartNew(() =>
                    {
                        if (string.IsNullOrWhiteSpace(((App)App.Current).Settings.UserId))
                        {
                            Username = null;
                            UserId = null;
                        }
                        else
                        {
                            DispatcherHelper.CheckBeginInvokeOnUI(() =>
                                {
                                    UserId = ((App)App.Current).Settings.UserId;
                                });
                        }
                    });
            }
        }

        public string Username
        {
            get { return _username; }

            set
            {
                if (_username != value)
                {
                    _username = value;
                    RaisePropertyChanged(() => Username);
                }
            }
        }

        public string UserId
        {
            get { return _userId; }

            set
            {
                if (_userId != value)
                {
                    _userId = value;
                    RaisePropertyChanged(() => UserId);
                }
            }
        }

        public Visibility LoadingControlVisible
        {
            get { return _loadingControlVisible; }
            
            set
            {
                if (_loadingControlVisible != value)
                {
                    _loadingControlVisible = value;
                    RaisePropertyChanged(() => LoadingControlVisible);
                }
            }
        }

        private void UserIdChanged()
        {
            if (string.IsNullOrWhiteSpace(UserId))
            {
                throw new NotImplementedException("implement log out");
            }
            else
            {
                LogOn();
            }
        }

        private void LogOn()
        {
            LoadingControlVisible = Visibility.Visible;

            _client = new JabbR.Client.JabbRClient("http://Jabbr.net");
            _client.Connect(UserId)
                .Then(logonInfo =>
                {
                    ((App)App.Current).Settings.UserId = UserId;
                    ((App)App.Current).Settings.Save();

                    MessengerInstance.Send(new NavigateToMessage
                    {
                        NavigateToType = typeof(DashboardPage),
                        Client = _client,
                        LogOnInfo = logonInfo
                    });
                })
                .Catch(ex =>
                {
                    _client.Disconnect();

                    LogOn();
                });
        }
    }
}