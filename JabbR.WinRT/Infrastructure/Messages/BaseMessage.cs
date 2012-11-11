using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JabbR.WinRT.Infrastructure.Messages
{
    public abstract class BaseMessage
    {
        public bool HasBeenHandled { get; set; }
    }
}