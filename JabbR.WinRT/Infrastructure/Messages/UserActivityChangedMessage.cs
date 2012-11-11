using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JabbR.WinRT.Infrastructure.Messages
{
    public class UserActivityChangedMessage : BaseMessage
    {
        public Client.Models.User User { get; set; }
    }
}