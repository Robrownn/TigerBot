using Microsoft.EntityFrameworkCore;
using TigerBot.Models;
using System;
using System.IO;
using System.Configuration;
using Microsoft.EntityFrameworkCore.Design;

namespace TigerBot.Data
{
    public class TigerBotDbContext : DbContext
    {

        public TigerBotDbContext(DbContextOptions options) : base(options)
        {
            
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<User>().HasKey(x => x.Id);
            builder.Entity<User>().Property(x => x.Id)
                                  .ValueGeneratedOnAdd()
                                  .IsRequired();

            builder.Entity<TigerGame>().HasKey(x => x.Id);
            builder.Entity<TigerGame>().Property(x => x.Id)
                                       .ValueGeneratedOnAdd()
                                       .IsRequired();

            builder.Entity<UserGame>().HasKey(x => x.Id);
            builder.Entity<UserGame>().Property(x => x.Id)
                                      .ValueGeneratedOnAdd()
                                      .IsRequired();
        }

        public DbSet<User> Users { get; set; }
        public DbSet<TigerGame> Games { get; set; }
        public DbSet<UserGame> UserGames { get; set; }
    }
}
