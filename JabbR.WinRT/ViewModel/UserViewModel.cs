using GalaSoft.MvvmLight;
using JabbR.Client.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JabbR.WinRT.ViewModel
{
    public class UserViewModel : ViewModelBase
    {
        private string _name;
        private string _hash;
        private bool _active;
        private UserStatus _status;
        private string _note;
        private string _afkNote;
        private bool _isAfk;
        private string _flag;
        private string _country;
        private DateTime _lastActivity;

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

        public string Hash
        {
            get { return _hash; }

            set
            {
                if (_hash != value)
                {
                    _hash = value;
                    RaisePropertyChanged(() => Hash);
                }
            }
        }

        public bool Active
        {
            get { return _active; }

            set
            {
                if (_active != value)
                {
                    _active = value;
                    RaisePropertyChanged(() => Active);
                }
            }
        }

        public UserStatus Status
        {
            get { return _status; }

            set
            {
                if (_status != value)
                {
                    _status = value;
                    RaisePropertyChanged(() => Status);
                }
            }
        }

        public string Note
        {
            get { return _note; }

            set
            {
                if (_note != value)
                {
                    _note = value;
                    RaisePropertyChanged(() => Note);
                }
            }
        }

        public string AfkNote
        {
            get { return _afkNote; }

            set
            {
                if (_afkNote != value)
                {
                    _afkNote = value;
                    RaisePropertyChanged(() => AfkNote);
                }
            }
        }

        public bool IsAfk
        {
            get { return _isAfk; }

            set
            {
                if (_isAfk != value)
                {
                    _isAfk = value;
                    RaisePropertyChanged(() => IsAfk);
                }
            }
        }

        public string Flag
        {
            get { return _flag; }

            set
            {
                if (_flag != value)
                {
                    _flag = value;
                    RaisePropertyChanged(() => Flag);
                }
            }
        }

        public string Country
        {
            get { return _country; }

            set
            {
                if (_country != value)
                {
                    _country = value;
                    RaisePropertyChanged(() => Country);
                }
            }
        }

        public DateTime LastActivity
        {
            get { return _lastActivity; }

            set
            {
                if (_lastActivity != value)
                {
                    _lastActivity = value;
                    RaisePropertyChanged(() => LastActivity);
                }
            }
        }
    }
}
