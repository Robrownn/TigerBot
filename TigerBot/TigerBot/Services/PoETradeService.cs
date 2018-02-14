//using System;
//using System.Collections.Generic;
//using System.IO;
//using System.Net;
//using System.Text;
//using Newtonsoft.Json;
//using Newtonsoft.Json.Linq;

//namespace TigerBot.Services
//{
//    class PoETradeService : IPoETradeService
//    {
//        private Uri _uri;
//        dynamic _result;

//        public PoETradeService()
//        {
//            _uri = new Uri("http://www.pathofexile.com/api/public-stash-tabs");
//        }

//        public void SearchAsync()
//        {
            
//            var req = HttpWebRequest.Create(_uri);
//            req.Method = "GET";
//            req.ContentType = "application/json";

//            using (var resp = req.GetResponse())
//            {
//                var results = new StreamReader(resp.GetResponseStream()).ReadToEnd();
//                _result = JObject.Parse(results);
//            }

//            //SearchAsync(_result./*get next stash id*/);
//        }

//        //public void SearchAsync(string NextStash)
//        //{
//        //    dynamic result;
//        //    var req = HttpWebRequest
//        //}
//    }
//}
