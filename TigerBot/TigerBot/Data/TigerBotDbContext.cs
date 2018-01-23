using Microsoft.EntityFrameworkCore;
using TigerBot.Models;
using System;
using System.IO;
using System.Configuration;

namespace TigerBot.Data
{
    public class TigerBotDbContext : DbContext
    {
        private string _db;

        public TigerBotDbContext()
        {
            Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            _db = ConfigurationManager.ConnectionStrings["TigerBot"].ToString();

            options.UseSqlServer(_db);
        }

        public DbSet<User> Users { get; set; }
        public DbSet<TigerGame> Games { get; set; }
        public DbSet<UserGame> UserGames { get; set; }
    }
}
