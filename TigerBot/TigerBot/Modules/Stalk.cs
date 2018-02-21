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
            builder.Title = $"Stalking {key}...";
            builder.Description = $"I will root out the truth from {key}'s words!";
            builder.ThumbnailUrl = user.GetAvatarUrl();
            builder.Author.Name = Context.Message.Author.Mention;
            builder.Author.IconUrl = Context.Message.Author.GetAvatarUrl();
            builder.Color = Color.Gold;

            foreach(var m in _msgHistory[key])
            {
                builder.AddField($"{m.Timestamp.ToString()}: ", m.Content);
            }

            await ReplyAsync("", false, builder.Build());

        }
    }
}
