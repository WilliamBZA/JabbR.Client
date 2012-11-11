using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Threading;
using JabbR.WinRT.Infrastructure.Commands;
using JabbR.WinRT.Infrastructure.Messages;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace JabbR.WinRT.ViewModel
{
    public class RoomViewModel : ViewModelBase
    {
        private string _name;
        private string _topic;
        private int _count;
        private bool _private;
        private ObservableCollection<UserViewModel> _users;
        private ObservableCollection<MessageViewModel> _recentMessages;
        private ObservableCollection<string> _owners;
        private ICommand _sendMessageCommand;
        private bool _isLoading;
        private int _unreadMessages;
        private bool _isActive;

        public RoomViewModel()
        {
            IsLoading = true;
            _sendMessageCommand = new SendMessageCommand(this);
        }

        public string Name
        {
            get { return _name; }

            set
            {
                if (_name != value)
                {
                    _name = value;
                    RaisePropertyChanged(() => Name);
                }
            }
        }

        public string Topic
        {
            get { return _topic; }

            set
            {
                if (_topic != value)
                {
                    _topic = value;
                    RaisePropertyChanged(() => Topic);
                }
            }
        }

        public int Count
        {
            get { return _count; }

            set
            {
                if (_count != value)
                {
                    _count = value;
                    RaisePropertyChanged(() => Count);
                }
            }
        }

        public bool Private
        {
            get { return _private; }

            set
            {
                if (_private != value)
                {
                    _private = value;
                    RaisePropertyChanged(() => Private);
                }
            }
        }

        public bool IsLoading
        {
            get { return _isLoading; }

            set
            {
                if (_isLoading != value)
                {
                    _isLoading = value;
                    RaisePropertyChanged(() => IsLoading);
                }
            }
        }

        public bool IsActive
        {
            get { return _isActive; }

            set
            {
                if (_isActive != value)
                {
                    _isActive = value;
                    RaisePropertyChanged(() => IsActive);

                    if (IsActive)
                    {
                        UnreadMessages = 0;
                    }
                }
            }
        }

        public int UnreadMessages
        {
            get { return _unreadMessages; }

            set
            {
                if (_unreadMessages != value)
                {
                    _unreadMessages = value;
                    RaisePropertyChanged(() => UnreadMessages);
                }
            }
        }

        public ObservableCollection<UserViewModel> Users
        {
            get { return _users; }

            set
            {
                if (_users != value)
                {
                    _users = value;
                    RaisePropertyChanged(() => Users);
                }
            }
        }

        public ObservableCollection<string> Owners
        {
            get { return _owners; }

            set
            {
                if (_owners != value)
                {
                    _owners = value;
                    RaisePropertyChanged(() => Owners);
                }
            }
        }

        public ObservableCollection<MessageViewModel> RecentMessages
        {
            get { return _recentMessages; }

            set
            {
                if (_recentMessages != value)
                {
                    _recentMessages = value;
                    RaisePropertyChanged(() => RecentMessages);
                }
            }
        }

        public ICommand SendMessage
        {
            get { return _sendMessageCommand; }
        }

        public void NotifyOfMessageToSend(string message)
        {
            MessengerInstance.Send<SendJabbRMessage>(new SendJabbRMessage
                {
                    Message = message,
                    RoomName = Name
                });
        }

        internal void AddMessage(MessageViewModel messageViewModel)
        {
            if (RecentMessages != null)
            {
                RecentMessages.Add(messageViewModel);

                DispatcherHelper.InvokeAsync(() =>
                    {
                        RaisePropertyChanged(() => RecentMessages);
                    });
            }
        }
    }
}