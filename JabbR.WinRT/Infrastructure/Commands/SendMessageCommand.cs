using JabbR.WinRT.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace JabbR.WinRT.Infrastructure.Commands
{
    public class SendMessageCommand : ICommand
    {
        public event EventHandler CanExecuteChanged;

        private RoomViewModel _room;

        public SendMessageCommand(RoomViewModel room)
        {
            _room = room;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            var message = parameter as string;
            if (!string.IsNullOrWhiteSpace(message))
            {
                _room.NotifyOfMessageToSend(message);
            }
        }
    }
}
