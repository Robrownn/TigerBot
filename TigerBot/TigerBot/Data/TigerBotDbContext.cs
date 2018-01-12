using Microsoft.EntityFrameworkCore;
using TigerBot.Models;

namespace TigerBot.Data
{
    public class TigerBotDbContext : DbContext
    {
        public TigerBotDbContext(DbContextOptions options) : base(options)
        {

        }

        public DbSet<User> Users { get; set; }
        public DbSet<TigerGame> Games { get; set; }
        public DbSet<UserGame> UserGames { get; set; }
    }
}
