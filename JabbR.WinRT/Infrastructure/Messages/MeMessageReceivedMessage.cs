using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JabbR.WinRT.Infrastructure.Messages
{
    public class MeMessageReceivedMessage : BaseMessage
    {
        public string Content;

        public string User { get; set; }

        public string Room { get; set; }
    }
}