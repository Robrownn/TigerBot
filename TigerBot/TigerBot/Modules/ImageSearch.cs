using Discord.Commands;
using Google.Apis.Customsearch.v1;
using System;
using System.Configuration;
using System.Net;
using System.Threading.Tasks;
using static Google.Apis.Customsearch.v1.Data.Result;

namespace TigerBot.Modules
{
    public class ImageSearch : ModuleBase<SocketCommandContext>
    {
        private CustomsearchService _cs;

        public ImageSearch(CustomsearchService cs)
        {
            _cs = cs;
        }


        [Command("img")]
        public async Task Image([Remainder]string terms = null)
        {

            terms = terms?.Trim();

            string urlterms = WebUtility.UrlEncode(terms).Replace(' ', '+');

            var result = await SearchGoogleAsync(urlterms);

            //var embed = new EmbedBuilder()
            //    .WithColor(Color.Blue)
            //    .WithTitle($"Imgur Search for: {terms}");

            await ReplyAsync($"Imgur Search for `{terms}`: {result.link}");


        }

        private async Task<ImageResult> SearchGoogleAsync(string terms)
        {
            var request = _cs.Cse.List(terms);
            request.Cx = ConfigurationManager.AppSettings["seID"];

            var result = await request.ExecuteAsync();
             
            int rand = new Random().Next(0,10);

            return new ImageResult(result.Items[rand].Image, result.Items[rand].Link);
        }

        public struct ImageResult
        {
            public ImageData image;
            public string link;

            public ImageResult(ImageData image, string link)
            {
                this.image = image;
                this.link = link;
            }
        }
    }
}
