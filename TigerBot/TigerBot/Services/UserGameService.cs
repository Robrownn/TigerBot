using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TigerBot.Data;
using TigerBot.Models;

namespace TigerBot.Services
{
    public class UserGameService : IUserGameService
    {
        private TigerBotDbContext _context;

        public UserGameService(TigerBotDbContext context)
        {
            _context = context;
        }

        public UserGame Add(User user, TigerGame game)
        {
            var selectedUser = _context.Users.Where(u => u.UserName == user.UserName).FirstOrDefault();
            var selectedGame = _context.Games.Where(g => g.GameName == game.GameName).FirstOrDefault();

            if (selectedUser != null && selectedGame != null)
            {
                try
                {
                    var newUserGame = new UserGame
                    {
                        UserID = selectedUser.Id,
                        GameID = selectedGame.Id
                    };

                    _context.UserGames.Add(newUserGame);
                    _context.SaveChanges();
                    return newUserGame;
                }catch (Exception ex)
                {
                    Console.WriteLine($"{ex.Source}: {ex.Message}");
                }
            }

            return null;
        }

        public UserGame Get(User user, TigerGame game)
        {
            var selectedUser = _context.Users.Where(u => u.UserName == user.UserName).FirstOrDefault();
            var selectedGame = _context.Games.Where(g => g.GameName == game.GameName).FirstOrDefault();

            if (selectedUser != null && selectedGame != null)
            {
                try
                {
                    var newUserGame = new UserGame
                    {
                        UserID = selectedUser.Id,
                        GameID = selectedGame.Id
                    };

                    return _context.UserGames.FirstOrDefault(ug => ug.GameID == newUserGame.GameID && ug.UserID == newUserGame.UserID);
                }catch (Exception ex)
                {
                    Console.WriteLine($"{ex.Source}: {ex.Message}");
                }
            }

            return null;
        }

        public IQueryable<UserGame> GetGameUsers(TigerGame game)
        {
            var selectedGame = _context.Games.Where(g => g.GameName == game.GameName).FirstOrDefault();

            if (selectedGame != null)
            {
                var newUserGame = new UserGame
                {
                    GameID = selectedGame.Id
                };

                return _context.UserGames.Where(ug => ug.GameID == newUserGame.GameID).OrderBy(ug => ug.GameID == newUserGame.GameID);
            }

            return null;
        }

        public IQueryable<UserGame> GetGameUsersLoose(TigerGame game)
        {
            var selectedGame = _context.Games.Where(g => g.GameName.Contains(game.GameName)).FirstOrDefault();

            if (selectedGame != null)
            {
                var newUserGame = new UserGame
                {
                    GameID = selectedGame.Id
                };

                return _context.UserGames.Where(ug => ug.GameID == newUserGame.GameID).OrderBy(ug => ug.GameID == newUserGame.GameID);
            }

            return null;
        }

        public IQueryable<UserGame> GetUsersGames(User user)
        {
            var selectedUser = _context.Users.Where(u => u.UserName == user.UserName).FirstOrDefault();

            if (selectedUser != null)
            {
                var newUserGame = new UserGame
                {
                    UserID = selectedUser.Id
                };

                return _context.UserGames.Where(ug => ug.UserID == newUserGame.UserID).OrderBy(ug => ug.UserID == newUserGame.UserID);
            }

            return null;
        }
    }
}
