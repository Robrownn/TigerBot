using Microsoft.EntityFrameworkCore;
using TigerBot.Models;
using System.IO;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore.Design;
using System;

namespace TigerBot.Data
{
    public class TigerBotDbContext : DbContext, IDesignTimeDbContextFactory<TigerBotDbContext>
    {
        public DbSet<User> Users { get; set; }
        public DbSet<TigerGame> Games { get; set; }
        public DbSet<UserGame> UserGames { get; set; }

        public TigerBotDbContext()
        {

        }

        public TigerBotDbContext(DbContextOptions options) : base(options)
        {

        }

        public TigerBotDbContext CreateDbContext(string[] args)
        {
            IConfigurationRoot config;
            var builder = new ConfigurationBuilder()
                .SetBasePath(Environment.CurrentDirectory)
                .AddJsonFile("_configuration.json");
            config = builder.Build();

            var optionsBuilder = new DbContextOptionsBuilder<TigerBotDbContext>();
            optionsBuilder.UseSqlServer(config.GetConnectionString("TigerBot"));

            return new TigerBotDbContext(optionsBuilder.Options);
        }
    }
}
