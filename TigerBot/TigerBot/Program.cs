﻿using Discord;
using Discord.Commands;
using Discord.WebSocket;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Configuration;
using System.Reflection;
using System.Threading.Tasks;
using TigerBot.Services;

namespace TigerBot
{
    class Program
    {
        static void Main(string[] args) => new Program().RunBotAsync().GetAwaiter().GetResult();
        

        private string _token;
        private DiscordSocketClient _client;
        private CommandService _commands;
        private BotCredentials _creds;
        private IServiceProvider _services;

        public async Task RunBotAsync()
        {
            _token = ConfigurationManager.AppSettings["Token"];
            _client = new DiscordSocketClient();
            _commands = new CommandService();
            _creds = new BotCredentials();
            _services = new ServiceCollection()
                        .AddSingleton(_client)
                        .AddSingleton(_commands)
                        .AddSingleton(_creds)
                        .BuildServiceProvider();
                        

            string botToken = _token;

            // Event Subscriptions
            _client.Log += Log;
            _client.UserJoined += AnnounceUserJoined;

            // Register our commands
            await RegisterCommandsAsync();

            // Logs the bot in with our token
            await _client.LoginAsync(TokenType.Bot, botToken);

            // Starts the bot
            await _client.StartAsync();

            // Keeps the bot from shutting off immediately
            await Task.Delay(-1);
        }

        private async Task AnnounceUserJoined(SocketGuildUser user)
        {
            var guild = user.Guild;
            var channel = guild.DefaultChannel;
            await channel.SendMessageAsync($"Welcome, {user.Mention}!");
        }

        private Task Log(LogMessage arg)
        {
            Console.WriteLine(arg);

            return Task.CompletedTask;
        }

        public async Task RegisterCommandsAsync()
        {
            _client.MessageReceived += HandleCommandAsync;
            await _commands.AddModulesAsync(Assembly.GetEntryAssembly());
        }

        private async Task HandleCommandAsync(SocketMessage arg)
        {

            var message = arg as SocketUserMessage;

            if (message is null || message.Author.IsBot) return;

            int argPos = 0;

            if (message.HasStringPrefix("!", ref argPos) || message.HasMentionPrefix(_client.CurrentUser, ref argPos))
            {
                var context = new SocketCommandContext(_client, message);

                var result = await _commands.ExecuteAsync(context, argPos, _services);

                if (!result.IsSuccess)
                    Console.WriteLine(result.ErrorReason + "\n" + result.Error);


            }
        }
    }
}