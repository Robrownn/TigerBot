using Discord;
using Discord.Commands;
using Discord.WebSocket;
using Google.Apis.Customsearch.v1;
using Google.Apis.Services;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Reflection;
using System.Threading.Tasks;
using TigerBot.Data;
using TigerBot.Models;
using TigerBot.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace TigerBot
{
    class Program
    {
        static void Main(string[] args) => new Program().RunBotAsync()
                                                        .GetAwaiter()
                                                        .GetResult();

        private IConfigurationRoot _config;
        private string _token;
        private string _googleToken;
        private DiscordSocketClient _client;
        private CommandService _commands;
        private IUserService _users;
        private IGameService _games;
        private IServiceProvider _services;

        public async Task RunBotAsync()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Environment.CurrentDirectory)
                .AddJsonFile("_configuration.json");
            _config = builder.Build();

            _token = _config["Discord"];                             
            _googleToken = _config["Google"];                        
            //_conn = _config.GetConnectionString("TigerBot");           
            _client = new DiscordSocketClient();
            _commands = new CommandService();
            _services = new ServiceCollection()
                        .AddSingleton(_client)
                        .AddSingleton(_commands)
                        .AddScoped<IUserService, UserService>()
                        .AddScoped<IGameService, GameService>()
                        .AddSingleton(new CustomsearchService(new BaseClientService.Initializer()
                        {
                            ApiKey = _googleToken,
                            MaxUrlLength = 256
                        }))
                        .AddDbContext<TigerBotDbContext>()
                        .BuildServiceProvider();
            
                        

            string botToken = _token;

            // Event Subscriptions
            _client.Log += Log;
            _client.UserJoined += AnnounceUserJoined;
            _client.GuildMemberUpdated += async (s, e) =>
            {
                // Check if User exists in User Table. Add user if false.
                if (!UserExists(e))
                    await AddUserToUserTable(e);
                // Check if Game exists in Game Table. Add game if false.
                if (!GameExists(e))
                    await AddGameToGameTable(e);
                // Check if UserGame exists in UserGameTable. Add combo if false.
            };

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
            var newUser = new User
            {
                UserName = user.Mention
            };
            _users.Add(newUser);
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
                    Console.WriteLine(result.ErrorReason + $"\nFull Error: {result.ToString()}" );


            }
        }

        private async Task AddUserToUserTable(SocketGuildUser user)
        {
            try
            {
                var newUser = new User
                {
                    UserName = user.Mention
                };
                _users.Add(newUser);

                await Log(CreateLogMessage(LogSeverity.Info, "Add user to user table", $"Added {newUser.UserName}"));
            }catch (Exception ex)
            {
                await Log(CreateLogMessage(LogSeverity.Critical, $"{ex.Source}", $"Exception: {ex.Message}"));
            }
        }

        private bool UserExists(SocketGuildUser user)
        {
            var newUser = new User
            {
                UserName = user.Mention
            };

            if (_users.Get(newUser) != null)
                return true;

            return false;
        }

        private async Task AddGameToGameTable(SocketGuildUser game)
        {
            try
            {
                var newGame = new TigerGame
                {
                    GameName = game.Game.ToString()
                };
                _games.Add(newGame);

                await Log(CreateLogMessage(LogSeverity.Info, "Add game to game table", $"Added {newGame.GameName}"));
            }
            catch (Exception ex)
            {
                await Log(CreateLogMessage(LogSeverity.Critical, $"{ex.Source}", $"Exception: {ex.Message}"));
            }
        }

        private bool GameExists(SocketGuildUser game)
        {
            var newGame = new TigerGame
            {
                GameName = game.Game.ToString()
            };

            if (_games.Get(newGame) != null)
                return true;

            return false;
        }

        private LogMessage CreateLogMessage(LogSeverity logSeverity, string source, string message)
        {
            return new LogMessage(logSeverity, source, message);
        }
    }
}
