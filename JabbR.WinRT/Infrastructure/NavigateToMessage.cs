using JabbR.Client.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JabbR.WinRT.Infrastructure
{
    public class NavigateToMessage
    {
        public Type NavigateToType { get; set; }

        public Client.JabbRClient Client { get; set; }

        public LogOnInfo LogOnInfo { get; set; }
    }
}