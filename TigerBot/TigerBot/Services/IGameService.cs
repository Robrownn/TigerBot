using Discord;
using System;
using System.Collections.Generic;
using System.Text;
using TigerBot.Models;

namespace TigerBot.Services
{
    public interface IGameService
    {
        IEnumerable<TigerGame> GetAll();
        TigerGame Add(TigerGame newGame);
        TigerGame Update(TigerGame game);
        TigerGame Get(TigerGame game);
        TigerGame GetGameById(TigerGame game);
    }
}
