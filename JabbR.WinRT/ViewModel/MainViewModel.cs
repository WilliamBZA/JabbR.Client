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
using JabbR.WinRT.Infrastructure.Messages;

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
                        //((App)App.Current).Settings.UserId = null;

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
            SubscribeToEvents(_client);
            
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

        private void SubscribeToEvents(JabbRClient client)
        {
            client.AddMessageContent += (messageId, extractedContent, roomName) =>
            {
                MessengerInstance.Send(new AddMessageContentMessage
                {
                    MessageId = messageId,
                    ExtractedContent = extractedContent,
                    RoomName = roomName
                });
            };

            client.Disconnected += () =>
            {
                MessengerInstance.Send(new DisconnectMessage());
            };

            client.FlagChanged += (user, room) =>
            {
                MessengerInstance.Send(new FlagChangedMessage
                {
                    User = user,
                    Room = room
                });
            };

            client.GravatarChanged += (user, room) =>
            {
                MessengerInstance.Send(new GravatarChangedMessage
                {
                    User = user,
                    Room = room
                });
            };

            client.JoinedRoom += room =>
            {
                MessengerInstance.Send(new JoinedRoomMessage
                {
                    Room = room
                });
            };

            client.Kicked += room =>
            {
                MessengerInstance.Send(new KickedMessage
                {
                    Room = room
                });
            };

            client.LoggedOut += rooms =>
            {
                MessengerInstance.Send(new LoggedOutMessage
                {
                    Rooms = rooms
                });
            };

            client.MeMessageReceived += (user, content, room) =>
            {
                MessengerInstance.Send(new MeMessageReceivedMessage
                {
                    User = user,
                    Content = content,
                    Room = room
                });
            };

            client.MessageReceived += (message, room) =>
            {
                MessengerInstance.Send(new MessageReceivedMessage
                {
                    Message = message,
                    Room = room
                });
            };

            client.NoteChanged += (user, room) =>
            {
                MessengerInstance.Send(new NoteChangedMessage
                {
                    User = user,
                    Room = room
                });
            };

            client.OwnerAdded += (user, room) =>
            {
                MessengerInstance.Send(new OwnerAddedMessage
                {
                    User = user,
                    Room = room
                });
            };

            client.OwnerRemoved += (user, room) =>
            {
                MessengerInstance.Send(new OwnerRemovedMessage
                {
                    User = user,
                    Room = room
                });
            };

            client.PrivateMessage += (from, to, message) =>
            {
                MessengerInstance.Send(new PrivateMessageMessage
                {
                    From = from,
                    To = to,
                    Message = message
                });
            };

            client.RoomCountChanged += (room, count) =>
            {
                MessengerInstance.Send(new RoomCountChangedMessage
                {
                    Room = room,
                    Count = count
                });
            };

            client.TopicChanged += (room) =>
            {
                MessengerInstance.Send(new TopicChangedMessage
                {
                    Room = room
                });
            };

            client.UserActivityChanged += user =>
            {
                MessengerInstance.Send(new UserActivityChangedMessage
                {
                    User = user
                });
            };

            client.UserJoined += (user, room, isOwner) =>
            {
                MessengerInstance.Send(new UserJoinedMessage
                {
                    User = user,
                    Room = room,
                    IsOwner = isOwner
                });
            };

            client.UserLeft += (user, room) =>
            {
                MessengerInstance.Send(new UserLeftMessage
                {
                    User = user,
                    Room = room
                });
            };

            client.UsernameChanged += (oldUserName, user, room) =>
            {
                MessengerInstance.Send(new UsernameChangedMessage
                {
                    OldUserName = oldUserName,
                    User = user,
                    Room = room
                });
            };

            client.UsersInactive += (users) =>
            {
                MessengerInstance.Send(new UsersInactiveMessage
                {
                    Users = users
                });
            };

            client.UserTyping += (user, room) =>
            {
                MessengerInstance.Send(new UserTypingMessage
                {
                    User = user,
                    Room = room
                });
            };
        }
    }
}