using LaraxsBot.Database.Interfaces;
using LaraxsBot.Database.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace LaraxsBot.Database.Contexts
{
    public class VoteContext : DbContext, IVoteContext
    {
        public DbSet<AnimeVoteModel> Nuits { get; set; }

        public VoteContext()
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
