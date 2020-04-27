using LaraxsBot.Database.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.IO;

namespace LaraxsBot.Database.Contexts
{
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Design", "RCS1090:Call 'ConfigureAwait(false)'.", Justification = "<Pending>")]
    public class NuitContext : DbContext
    {
        public DbSet<NuitModel> Nuits { get; set; }

#pragma warning disable CS8618
        public NuitContext()
#pragma warning restore CS8618
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
