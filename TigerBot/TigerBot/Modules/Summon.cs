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
        private IGameService _games;

        // Summon will summon people who have played a specified game before.
        public Summon(IUserGameService userGames,   // userGames has a function to return all game user id's
                      IUserService users,           // we will use the user service to grab all the usernames of the corresponding users id
                      IGameService games)           // we will use the game service to grab all the gamenames of the corresponding game id           
        {
            _users = users;
            _userGames = userGames;
            _games = games;
        }

        [Command("summon")]
        public async Task SummonUsers([Remainder]string gameName)
        {
            // When someone specifies a game name, we need to create a TigerGame with that game name. Id is not necessary as we are searching by name
            var newGame = CreateNewGame(gameName);

            // Now that we have our ad-hoc game we are going to search the usergame table for the corresponding game and return all the users who have played that game
            var selectedUsers = _userGames.GetGameUsersLoose(newGame);
            List<string> users = new List<string>();

            // We have our list of users but we only have them by Id. We need to grab their usernames to add them to the message string.
            foreach(var m in selectedUsers)
            {
                // We create an ad-hoc user with the id of the user at the current index of selectedUsers
                var newUser = new User
                {
                    Id = m.UserID
                };

                // We search the user table with the ad-hoc user
                var getUser = _users.GetById(newUser);

                // We then save the username of the user we just got into a string
                string username = getUser.UserName;

                // We then add that username to our list of usernames
                users.Add(username);
            }

            // We create the base message. This has to be mutable so we can attach the list of usernames at the end.
            StringBuilder message = new StringBuilder($"Summoning everyone who has played `{gameName}` before...\n");

            // We then do a for each on the list of usernames that takes in a function as a parameter. 
            // This function adds the username at the current index to the message we just built
            users.ForEach(delegate (String name)
            {
                message.Append($"{name}, ");
            });

            // We then send the message as a string.
            await ReplyAsync(message.ToString());
        }

        [Command("games")]
        public async Task ListGames([Remainder]SocketGuildUser user)
        {
            var newUser = CreateNewUser(user.Mention);

            var selectedUserGames = _userGames.GetUsersGames(newUser);
            List<string> games = new List<string>();

            foreach (var ug in selectedUserGames)
            {
                var game = _games.GetGameById(ug.GameID);
                games.Add(game.GameName);
            }

            var listAnnounce = $"{user.Mention} has played...";
            var stringifiedGames = games.Aggregate(new StringBuilder(),
                (sb, g) => sb.AppendLine(String.Join(",", g)),
                sb => sb.ToString());
            await ReplyAsync($"{listAnnounce}\n`{stringifiedGames}`");
        }

        [Command("add")]
        public async Task AddGame([Remainder]string gameName)
        {
            await AddGame(Context.Message.Author as SocketGuildUser, gameName);
        }

        [Command("add")]
        public async Task AddGame(SocketGuildUser user,[Remainder]string gameName)
        {
            // Create new user and new game
            var newGame = CreateNewGame(gameName);
            var newUser = CreateNewUser(user.Mention);

            // Check user table for the specified user. Insert if not exist
            if (!CheckUserExists(newUser))
            {
                _users.Add(newUser);
            }
            // Check game table if the game already exists. Insert if not exist
            if (!CheckGameExists(newGame))
            {
                _games.Add(newGame);
            }

            // Insert combo into usergame table
            if (!CheckUserGameExists(newGame,newUser))
            {
                _userGames.Add(newUser, newGame);
            }

            await ReplyAsync($"`{gameName}` successfully added to {user.Mention}'s game list!");
        }

        private User CreateNewUser(string mention)
        {
            var newUser = new User
            {
                UserName = mention
            };

            return newUser;
        }

        private TigerGame CreateNewGame(string gameName)
        {
            var newGame = new TigerGame
            {
                GameName = gameName
            };

            return newGame;
        }

        private bool CheckUserExists(User user)
        {
            var selectedUser = _users.Get(user);
            if (selectedUser == null)
                return false;

            return true;
        }

        private bool CheckGameExists(TigerGame game)
        {
            var selectedGame = _games.Get(game);
            if (selectedGame == null)
                return false;

            return true;
        }

        private bool CheckUserGameExists(TigerGame game, User user)
        {
            var selectedUserGame = _userGames.Get(user, game);
            if (selectedUserGame == null)
                return false;

            return true;
        }

    }
}
