using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JabbR.WinRT.Infrastructure.Messages
{
    public class PrivateMessageMessage : BaseMessage
    {
        public string From { get; set; }

        public string To { get; set; }

        public string Message { get; set; }
    }
}