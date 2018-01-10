using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Google.Apis.Customsearch.v1.Data;

namespace TigerBot.Services
{
    public interface IGoogleApiService
    {
        Task<ImageResult> GetImageAsync(string query, int start = 1);
    }

    public struct ImageResult
    {
        public Result.ImageData Image { get; }
        public string Link { get; }

        public ImageResult(Result.ImageData image, string link)
        {
            this.Image = image;
            this.Link = link;
        }
    }
}
