using Discord;
using Discord.Commands;
using Discord.WebSocket;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace TigerBot.Modules
{
    [Group("ping")]
    public class Ping : ModuleBase<SocketCommandContext>
    {
        [Command]
        public async Task DefaultPing()
        {
            await ReplyAsync("pong!");
        }

        [Command("user")]
        public async Task UserPing(SocketGuildUser user)
        {
            await ReplyAsync($"pong, {user.Mention}!");
        }
    }
}
