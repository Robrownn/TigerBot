using Discord.Commands;
using Discord.WebSocket;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TigerBot.Models;
using TigerBot.Services;

namespace TigerBot.Modules
{
    public class Summon : ModuleBase<SocketCommandContext>
    {
        private IUserService _users;
        private IUserGameService _userGames;

        public Summon(IUserGameService userGames,
                      IUserService users)
        {
            _users = users;
            _userGames = userGames;
        }

        [Command("summon")]
        public async Task SummonUsers([Remainder]string gameName)
        {
            var newGame = new TigerGame
            {
                GameName = gameName
            };

            var selectedUsers = _userGames.GetGameUsers(newGame);
            List<string> users = new List<string>();

            foreach(var m in selectedUsers)
            {
                var newUser = new User
                {
                    Id = m.UserID
                };

                var getUser = _users.GetById(newUser);
                string username = getUser.UserName;
                users.Add(username);
            }

            StringBuilder message = new StringBuilder($"Summoning everyone who has played `{gameName}` before...\n");

            users.ForEach(delegate (String name)
            {
                message.Append($"{name}, ");
            });
            await ReplyAsync(message.ToString());
        }
    }
}
