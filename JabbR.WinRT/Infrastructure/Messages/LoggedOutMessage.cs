using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JabbR.WinRT.Infrastructure.Messages
{
    public class LoggedOutMessage : BaseMessage
    {
        public IEnumerable<string> Rooms { get; set; }
    }
}