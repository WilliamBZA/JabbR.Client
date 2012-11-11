using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Messaging;
using GalaSoft.MvvmLight.Threading;
using JabbR.Client;
using JabbR.Client.Models;
using JabbR.WinRT.Infrastructure.Commands;
using JabbR.WinRT.Infrastructure.Messages;
using JabbR.WinRT.Infrastructure.ObjectMapper;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Input;

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

            SubscribeToJabbRMessages();
        }

        private void SubscribeToJabbRMessages()
        {
             Messenger.Default.Register<AddMessageContentMessage>(this, msg =>
                 {
                 
                 });

            Messenger.Default.Register<DisconnectMessage>(this, msg =>
                 {
                 
                 });

            Messenger.Default.Register<FlagChangedMessage>(this, msg =>
                 {
                 
                 });

            Messenger.Default.Register<GravatarChangedMessage>(this, msg =>
                 {
                 
                 });

            Messenger.Default.Register<JoinedRoomMessage>(this, msg =>
                 {
                 
                 });

            Messenger.Default.Register<KickedMessage>(this, msg =>
                 {
                 
                 });

            Messenger.Default.Register<LoggedOutMessage>(this, msg =>
                 {
                 
                 });

            Messenger.Default.Register<MeMessageReceivedMessage>(this, msg =>
                 {
                 
                 });

            Messenger.Default.Register<MessageReceivedMessage>(this, msg =>
                 {
                     if (!msg.HasBeenHandled)
                     {
                         msg.HasBeenHandled = true;
                         var room = Rooms.FirstOrDefault(r => r.Name == msg.Room);
                         if (room != null)
                         {
                             room.AddMessage(ObjectMapper.DefaultInstance.GetMapper<Message, MessageViewModel>().Map(msg.Message));

                             if (!room.IsActive)
                             {
                                 DispatcherHelper.InvokeAsync(() =>
                                     {
                                         room.UnreadMessages++;
                                     });
                             }

                             MessengerInstance.Send<MessageSuccessfullySentMessage>(new MessageSuccessfullySentMessage());
                         }
                     }
                 });

            Messenger.Default.Register<NoteChangedMessage>(this, msg =>
                 {
                 
                 });

            Messenger.Default.Register<OwnerAddedMessage>(this, msg =>
                 {
                 
                 });

            Messenger.Default.Register<OwnerRemovedMessage>(this, msg =>
                 {
                 
                 });

            Messenger.Default.Register<PrivateMessageMessage>(this, msg =>
                 {
                 
                 });

            Messenger.Default.Register<RoomCountChangedMessage>(this, msg =>
                 {
                 
                 });

            Messenger.Default.Register<TopicChangedMessage>(this, msg =>
                 {
                 
                 });

            Messenger.Default.Register<UserActivityChangedMessage>(this, msg =>
                 {
                 
                 });

            Messenger.Default.Register<UserJoinedMessage>(this, msg =>
                 {
                 
                 });

            Messenger.Default.Register<UserLeftMessage>(this, msg =>
                 {
                 
                 });

            Messenger.Default.Register<UsernameChangedMessage>(this, msg =>
                 {
                 
                 });

            Messenger.Default.Register<UsersInactiveMessage>(this, msg =>
                 {
                 
                 });

            Messenger.Default.Register<UserTypingMessage>(this, msg =>
                 {
                 
                 });
        }

        internal void LoadRoomDetails(RoomViewModel room)
        {
            Messenger.Default.Register<SendJabbRMessage>(this, (msg) =>
                {
                    if (!msg.HasBeenHandled)
                    {
                        msg.HasBeenHandled = true;

                        _client.Send(msg.Message, msg.RoomName)
                            .Then(result =>
                            {
                                MessengerInstance.Send<MessageSuccessfullySentMessage>(new MessageSuccessfullySentMessage());
                            });
                    }
                });

            _client.GetRoomInfo(room.Name)
                .Then(details =>
                {
                    details.Topic = Regex.Replace(details.Topic, "<[^>]*>", string.Empty);
                    details.Topic = Regex.Replace(details.Topic, @"^\s*$\n", string.Empty, RegexOptions.Multiline);

                    DispatcherHelper.InvokeAsync(() =>
                    {
                        ObjectMapper.DefaultInstance.GetMapper<Room, RoomViewModel>().MapOnto(details, room);
                        room.IsLoading = false;
                    });
                });
        }
    }
}