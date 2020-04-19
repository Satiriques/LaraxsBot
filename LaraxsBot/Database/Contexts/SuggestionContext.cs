using LaraxsBot.Database.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace LaraxsBot.Database.Contexts
{
    public class SuggestionContext : ISuggestionContext
    {
        public DbSet<> Nuits { get; set; }

        public NuitContext()
        {
            Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            string baseDir = Path.Combine(AppContext.BaseDirectory, "data");
            if (!Directory.Exists(baseDir))
                Directory.CreateDirectory(baseDir);

            string datadir = Path.Combine(baseDir, "nuits.sqlite.db");
            optionsBuilder.UseSqlite($"Filename={datadir}");
        }
    }
}
