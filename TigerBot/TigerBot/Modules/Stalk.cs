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
            string name = user.Username;
            var stalkMessage = await Context.Channel.SendMessageAsync($"Getting all messages from {key}...");

            EmbedBuilder builder = new EmbedBuilder();
            builder.Title = $"Stalking {name}...";
            builder.Description = $"I will root out the truth from {key}'s words!";
            builder.ThumbnailUrl = user.GetAvatarUrl();
            builder.Color = Color.Gold;

            foreach(var m in _msgHistory[key])
            {
                builder.AddField($"At {m.Timestamp.ToString()}, {name} said... ", m.Content);
            }

            await ReplyAsync("", false, builder.Build());

        }
    }
}
