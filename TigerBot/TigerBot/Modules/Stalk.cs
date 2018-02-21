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

        [Command("stalk")]
        public async Task StalkUser(SocketGuildUser user)
        {
            var stalkMessage = await Context.Channel.SendMessageAsync($"Getting all messages from {user.Mention}...");
            var lastMessageId = stalkMessage.Id;
            //.Where(m => m.Author.Mention == user.Mention).

            // fuck this shit right here im just gonna do a dictionary with kvp userid : message
            var messageHistory = await Context.Channel.GetMessagesAsync(lastMessageId,Direction.Before,100).OfType<IUserMessage>().Where(m => m.Author.Mention == user.Mention).ToList();

            EmbedBuilder builder = new EmbedBuilder();
            

            builder.AddField("Cached Messages", "All cached messages");
            foreach(IUserMessage m in messageHistory)
            {
                builder.AddField($"{m.Timestamp.ToString()}: ", m.Content);
            }

            await ReplyAsync("", false, builder.Build());

        }
    }
}
