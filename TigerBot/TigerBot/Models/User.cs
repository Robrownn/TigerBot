using Discord.WebSocket;
using System;
using System.Collections.Generic;
using System.Text;

namespace TigerBot.Models
{
    public class User
    {
        public int Id { get; set; }
        public SocketGuildUser socketGuildUser { get; set; }
    }
}
