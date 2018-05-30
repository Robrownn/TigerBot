using Discord.Commands;
using Google.Apis.Customsearch.v1;
using Microsoft.Extensions.Configuration;
using System;
using System.Net;
using System.Threading.Tasks;
using TigerBot.Data;
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

            await ReplyAsync($"Imgur Search for `{terms}`: {result.link}");


        }

        private async Task<ImageResult> SearchGoogleAsync(string terms)
        {
            var request = _cs.Cse.List(terms);
            request.Cx = GoogleCSeCredentials.Cx;
            request.SearchType = CseResource.ListRequest.SearchTypeEnum.Image;

            var result = await request.ExecuteAsync();
             
            int rand = new Random().Next(0,9);

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
