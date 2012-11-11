using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JabbR.WinRT.ViewModel
{
    public class MessageViewModel : ViewModelBase
    {
        private string _id;
        private string _content;
        private DateTimeOffset _when;
        private UserViewModel _user;

        public string Id
        {
            get { return _id; }

            set
            {
                if (_id != value)
                {
                    _id = value;
                    RaisePropertyChanged(() => Id);
                }
            }
        }

        public string Content
        {
            get { return _content; }

            set
            {
                if (_content != value)
                {
                    _content = value;
                    RaisePropertyChanged(() => Content);
                }
            }
        }

        public DateTimeOffset When
        {
            get { return _when; }

            set
            {
                if (_when != value)
                {
                    _when = value;
                    RaisePropertyChanged(() => When);
                }
            }
        }

        public UserViewModel User
        {
            get { return _user; }

            set
            {
                if (_user != value)
                {
                    _user = value;
                    RaisePropertyChanged(() => User);
                }
            }
        }
    }
}
