﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JabbR.WinRT.Infrastructure.Messages
{
    public class JoinedRoomMessage : BaseMessage
    {
        public Client.Models.Room Room { get; set; }
    }
}