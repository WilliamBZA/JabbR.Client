using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Threading;
using JabbR.Client;
using JabbR.Client.Models;
using JabbR.WinRT.Infrastructure.ObjectMapper;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JabbR.WinRT.ViewModel
{
    public class DashboardViewModel : ViewModelBase
    {
        private ObservableCollection<RoomViewModel> _rooms;
        private JabbRClient _client;

        public DashboardViewModel()
        {
            DispatcherHelper.Initialize();
            Rooms = new ObservableCollection<RoomViewModel>();
        }

        public ObservableCollection<RoomViewModel> Rooms
        {
            get { return _rooms; }
            
            set
            {
                if (value != _rooms)
                {
                    _rooms = value;
                    RaisePropertyChanged(() => Rooms);
                }
            }
        }

        public void SetClient(JabbRClient client)
        {
            _client = client;
        }

        internal void LoadRoomDetails(RoomViewModel room)
        {
            _client.GetRoomInfo(room.Name)
                .Then(details =>
                {
                    DispatcherHelper.InvokeAsync(() =>
                    {
                        ObjectMapper.DefaultInstance.GetMapper<Room, RoomViewModel>().MapOnto(details, room);
                    });
                });
        }
    }
}