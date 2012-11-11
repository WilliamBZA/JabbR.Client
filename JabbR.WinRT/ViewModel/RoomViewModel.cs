using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        public RoomViewModel()
        {
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
    }
}