using Discord.Commands;
using Discord.WebSocket;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Configuration;
using System.Threading.Tasks;

namespace TigerBot
{
    class Program
    {
        static void Main(string[] args)
        {
            
        }

        private string _token;
        private DiscordSocketClient _client;
        private CommandService _commands;
        private IServiceProvider _services;

        public string placeholder;

        public async Task RunBotAsync()
        {
            _token = ConfigurationManager.AppSettings["Token"];
            _client = new DiscordSocketClient();
            _commands = new CommandService();
            _services = new ServiceCollection()
                        .AddSingleton(_client)
                        .AddSingleton(_commands)
                        .BuildServiceProvider();

            string botPrefix = _token;

        }

        public async Task RegisterCommandsAsync()
        {

        }
    }
}
