using Google.Apis.Customsearch.v1;
using Google.Apis.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace TigerBot.Services
{
    public class GoogleApiService : IGoogleApiService
    {
        const string SEARCH_ENGINE_ID = "016977101793543495109:ntrr-sk-hcm";

        private IBotCredentials _creds;
        private CustomsearchService _cs;

        public GoogleApiService(IBotCredentials creds)
        {
            _creds = creds;

            var bcs = new BaseClientService.Initializer
            {
                ApplicationName = "Tiger Bot",
                ApiKey = _creds.GoogleApiKey,
            };

            _cs = new CustomsearchService(bcs);
        }

        public async Task<ImageResult> GetImageAsync(string query, int start = 1)
        {
            await Task.Yield();
            if (string.IsNullOrWhiteSpace(query))
                throw new ArgumentNullException(nameof(query));

            var request = _cs.Cse.List(query);
            request.Cx = SEARCH_ENGINE_ID;
            request.Num = 1;
            //request.Fields = "items(image(contextLink,thumbnailLink),link)";
            request.SearchType = CseResource.ListRequest.SearchTypeEnum.Image;
            request.Start = start;

            var search = await request.ExecuteAsync().ConfigureAwait(false);

            return new ImageResult(search.Items[0].Image, search.Items[0].Link);
        }
    }
}
