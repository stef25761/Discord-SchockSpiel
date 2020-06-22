using Discord.WebSocket;
using System;
using System.Collections.Generic;
using System.Text;

namespace SchockBot.Data
{
    public class Player
    {
        public int Round { get; set; }
        public int Caps { get; set; }

        public SocketUser User { get; set; }
      
    }
}
