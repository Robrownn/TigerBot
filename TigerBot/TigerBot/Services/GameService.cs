using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Discord;
using TigerBot.Data;
using TigerBot.Models;

namespace TigerBot.Services
{
    public class GameService : IGameService
    {
        private TigerBotDbContext _context;

        public GameService(TigerBotDbContext context)
        {
            _context = context;
        }

        public TigerGame Add(TigerGame newGame)
        {
            _context.Games.Add(newGame);
            _context.SaveChanges();

            return newGame;
        }

        public TigerGame Get(TigerGame game)
        {
            return _context.Games.FirstOrDefault(g => g.GameName.Equals(game.GameName));
        }

        public TigerGame Update(TigerGame game)
        {
            throw new NotImplementedException();
        }

        IEnumerable<TigerGame> IGameService.GetAll()
        {
            return _context.Games.OrderBy(g => g.GameName);
        }
    }
}
