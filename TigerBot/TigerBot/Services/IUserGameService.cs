﻿using System.Linq;
using TigerBot.Models;

namespace TigerBot.Services
{
    public interface IUserGameService
    {
        UserGame Add(User user, TigerGame game);
        IQueryable<UserGame> GetUsersGames(User user);
        IQueryable<UserGame> GetGameUsers(TigerGame game);
        IQueryable<UserGame> GetGameUsersLoose(TigerGame game);
        UserGame Get(User user, TigerGame game);
    }
}
