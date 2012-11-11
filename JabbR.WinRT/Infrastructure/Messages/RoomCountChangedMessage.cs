using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JabbR.WinRT.Infrastructure.Messages
{
    public class RoomCountChangedMessage : BaseMessage
    {
        public Client.Models.Room Room { get; set; }

        public int Count { get; set; }
    }
}