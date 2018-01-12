using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
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
    }
}
