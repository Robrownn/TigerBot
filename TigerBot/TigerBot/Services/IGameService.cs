using Discord;
using System;
using System.Collections.Generic;
using System.Text;

namespace TigerBot.Services
{
    public interface IGameService
    {
        IEnumerable<Game> GetAll();
        Game Add(Game newGame);
        Game Update(Game game);
    }
}
