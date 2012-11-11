using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JabbR.WinRT.Infrastructure.Messages
{
    public class SendJabbRMessage : BaseMessage
    {
        public string RoomName { get; set; }

        public string Message { get; set; }
    }
}