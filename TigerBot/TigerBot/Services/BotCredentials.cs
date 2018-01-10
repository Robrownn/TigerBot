using System;
using System.Collections.Generic;
using System.Configuration;
using System.Text;

namespace TigerBot.Services
{
    public class BotCredentials : IBotCredentials
    {
        public string GoogleApiKey { get; }

        public BotCredentials()
        {
            GoogleApiKey = ConfigurationManager.AppSettings["googleapi"];
        }
    }
}
