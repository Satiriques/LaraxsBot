using LaraxsBot.Database.Interfaces;
using LaraxsBot.Database.Models;
using LaraxsBot.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;

namespace LaraxsBot.Database.Contexts
{
    public class SuggestionContext : DbContext
    {
        public DbSet<SuggestionModel> Suggestions { get; set; }

#pragma warning disable CS8618 // Non-nullable field is uninitialized. Consider declaring as nullable.
        public SuggestionContext()
#pragma warning restore CS8618 // Non-nullable field is uninitialized. Consider declaring as nullable.
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
