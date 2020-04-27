using LaraxsBot.Database.Interfaces;
using LaraxsBot.Database.Models;
using LaraxsBot.Interfaces;
using LaraxsBot.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace LaraxsBot.Database.Contexts
{
    public class VoteContext : DbContext
    {
        public DbSet<AnimeVoteModel> Votes { get; set; }

#pragma warning disable CS8618
        public VoteContext()
#pragma warning restore CS8618
        {
            Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            string baseDir = Path.Combine(AppContext.BaseDirectory, "data");
            if (!Directory.Exists(baseDir))
                Directory.CreateDirectory(baseDir);

            string datadir = Path.Combine(baseDir, "votes.sqlite.db");
            optionsBuilder.UseSqlite($"Filename={datadir}");
        }

        
    }
}
