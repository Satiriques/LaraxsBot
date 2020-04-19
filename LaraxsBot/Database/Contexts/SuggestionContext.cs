using LaraxsBot.Database.Interfaces;
using LaraxsBot.Database.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.IO;

namespace LaraxsBot.Database.Contexts
{
    public class SuggestionContext : DbContext, ISuggestionContext
    {
        public DbSet<SuggestionModel> Nuits { get; set; }

        public SuggestionContext()
        {
            Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            string baseDir = Path.Combine(AppContext.BaseDirectory, "data");
            if (!Directory.Exists(baseDir))
                Directory.CreateDirectory(baseDir);

            string datadir = Path.Combine(baseDir, "suggestions.sqlite.db");
            optionsBuilder.UseSqlite($"Filename={datadir}");
        }
    }
}
