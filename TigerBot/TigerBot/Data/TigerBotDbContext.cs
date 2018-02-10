using Microsoft.EntityFrameworkCore;
using TigerBot.Models;
using System;
using System.IO;
using System.Configuration;
using Microsoft.EntityFrameworkCore.Design;

namespace TigerBot.Data
{
    public class TigerBotDbContext : DbContext, IDesignTimeDbContextFactory<TigerBotDbContext>
    {

        public TigerBotDbContext(DbContextOptions options) : base(options)
        {

        }

        public DbSet<User> Users { get; set; }
        public DbSet<TigerGame> Games { get; set; }
        public DbSet<UserGame> UserGames { get; set; }

        public TigerBotDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<TigerBotDbContext>();
            optionsBuilder.UseSqlServer(ConfigurationManager.ConnectionStrings["TigerBot"].ConnectionString);

            return new TigerBotDbContext(optionsBuilder.Options);
        }
    }
}
