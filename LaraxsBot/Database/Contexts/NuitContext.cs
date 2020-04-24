using LaraxsBot.Database.Interfaces;
using LaraxsBot.Database.Models;
using LaraxsBot.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using System.Security.Cryptography.X509Certificates;

namespace LaraxsBot.Database.Contexts
{
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Design", "RCS1090:Call 'ConfigureAwait(false)'.", Justification = "<Pending>")]
    public class NuitContext : DbContext, INuitContext
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

        public void BackupAndDrop()
        {
            var path = Path.Combine(AppContext.BaseDirectory, "data", "nuits.sqlite.db");

            if (File.Exists(path))
            {
                FileInfo fileInfo = new FileInfo(path);

                var fileName = DateTime.Now.ToShortDateString() + "_" + DateTime.Now.ToShortTimeString() + "_" + fileInfo.Name + ".bak";
                var newPath = Path.Combine(fileInfo.Directory.FullName, fileName);

                File.Copy(path, newPath);

                Database.EnsureDeleted();
            }
        }

        public async Task CreateNuitAsync(DateTime start, DateTime end, ulong creatorId)
        {
            var nuit = new NuitModel()
            {
                StartTime = start,
                StopTime = end,
                CreatorId = creatorId,
                CreationDate = DateTime.Now,
            };

            Nuits.Add(nuit);
            await SaveChangesAsync();
        }

       
        public async Task<List<NuitModel>> GetAllNuitsAsync()
            => await Nuits.AsQueryable().ToListAsync();

        public async Task<NuitModel?> GetStillRunningNuitAsync()
            => await Nuits.AsQueryable().SingleOrDefaultAsync(x => x.IsRunning);

        public async Task StopNuitAsync(ulong animeId)
        {
            var nuit = await Nuits.AsQueryable().SingleOrDefaultAsync(x => x.IsRunning);
            if(nuit != null)
            {
                nuit.IsRunning = false;
                nuit.WinnerAnimeId = animeId;
                await SaveChangesAsync();
            }
        }

        public async Task StartNuitAsync(ulong id)
        {
            if(!await Nuits.AsQueryable().AnyAsync(x => x.IsRunning))
            {
                var nuit = Nuits.AsQueryable().SingleOrDefault(x => x.NuitId == id);
                if(nuit != null)
                {
                    nuit.IsRunning = true;
                    await SaveChangesAsync();
                }
            }
        }
    }
}
