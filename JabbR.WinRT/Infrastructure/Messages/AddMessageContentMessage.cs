using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JabbR.WinRT.Infrastructure.Messages
{
    public class AddMessageContentMessage : BaseMessage
    {
        public string MessageId { get; set; }

        public string ExtractedContent { get; set; }

        public string RoomName { get; set; }
    }
}