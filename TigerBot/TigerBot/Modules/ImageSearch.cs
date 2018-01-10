using Discord;
using Discord.Commands;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using TigerBot.Services;

namespace TigerBot.Modules
{
    public class ImageSearch : ModuleBase<SocketCommandContext>
    {
        private IBotCredentials _creds;
        private IGoogleAPIService _google;

        public ImageSearch(IBotCredentials creds,IGoogleAPIService google)
        {
            _creds = creds;
            _google = google;
        }


        [Command("img")]
        public async Task Image([Remainder]string terms = null)
        {

            terms = terms?.Trim();

            terms = WebUtility.UrlEncode(terms).Replace(' ', '+');
            try
            {
                var result = await _google.GetImageAsync(terms).ConfigureAwait(false);
                var embed = new EmbedBuilder()
                    .WithColor(Color.Blue)
                    .WithDescription(result.Link)
                    .WithImageUrl(result.Link)
                    .WithTitle(Context.User.ToString());

                await ReplyAsync("", false, embed);
            }
            catch
            {
                
            }
        }
    }
}
