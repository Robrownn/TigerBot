using Microsoft.EntityFrameworkCore;
using TigerBot.Models;
using System.IO;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore.Design;
using System;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

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

        protected override void OnModelCreating(ModelBuilder builder)
        {
            // Define User key and properties
            builder.Entity<User>()
                .HasKey(x => x.Id);
            builder.Entity<User>()
                .Property(x => x.Id)
                .ValueGeneratedOnAdd()
                .IsRequired();
            builder.Entity<User>()
                .Property(x => x.UserName)
                .IsRequired();

            // Define TigerGame key and properties
            builder.Entity<TigerGame>()
                .HasKey(x => x.Id);
            builder.Entity<TigerGame>()
                .Property(x => x.Id)
                .ValueGeneratedOnAdd()
                .IsRequired();
            builder.Entity<TigerGame>()
                .Property(x => x.GameName)
                .IsRequired();

            // Define UserGame key and properties
            builder.Entity<UserGame>()
                .HasKey(x => x.Id);
            builder.Entity<UserGame>()
                .Property(x => x.Id)
                .ValueGeneratedOnAdd()
                .IsRequired();
            builder.Entity<UserGame>()
                .Property(x => x.UserID)
                .IsRequired();
            builder.Entity<UserGame>()
                .Property(x => x.GameID)
                .IsRequired();
        }

    }
}
