using Discord;
using Discord.Commands;
using Discord.WebSocket;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TigerBot.Modules
{
    public class Stalk : ModuleBase<SocketCommandContext>
    {
        private IDictionary<string, List<SocketUserMessage>> _msgHistory;

        public Stalk(IDictionary<string,List<SocketUserMessage>> dictionary)
        {
            _msgHistory = dictionary;
        }

        [Command("stalk")]
        public async Task StalkUser(SocketGuildUser user)
        {
            string key = user.Mention;
            var stalkMessage = await Context.Channel.SendMessageAsync($"Getting all messages from {key}...");

            EmbedBuilder builder = new EmbedBuilder();
            

            builder.AddField("Cached Messages", "All cached messages");
            foreach(var m in _msgHistory[key])
            {
                builder.AddField($"{m.Timestamp.ToString()}: ", m.Content);
            }

            await ReplyAsync("", false, builder.Build());

        }
    }
}
