using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TigerBot.Models;

namespace TigerBot.Services
{
    public interface IUserGameService
    {
        UserGame Add(User user, TigerGame game);
        IQueryable<UserGame> GetUsersGames(User user);
        IQueryable<UserGame> GetGameUsers(TigerGame game);
        UserGame Get(User user, TigerGame game);
    }
}
