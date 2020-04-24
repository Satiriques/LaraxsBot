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
    public class SuggestionContext : DbContext, ISuggestionContext
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

        public void BackupAndDrop()
        {
            var path = Path.Combine(AppContext.BaseDirectory, "data", "suggestions.sqlite.db");

            if (File.Exists(path))
            {
                FileInfo fileInfo = new FileInfo(path);

                var fileName = DateTime.Now.ToShortDateString() + "_" + DateTime.Now.ToShortTimeString() + "_" + fileInfo.Name + ".bak";
                var newPath = Path.Combine(fileInfo.Directory.FullName, fileName);

                File.Copy(path, newPath);

                Database.EnsureDeleted();
            }
        }

        public async Task CreateSuggestionAsync(ulong animeId, ulong nuitId)
        {
            var suggestion = new SuggestionModel()
            {
                AnimeId = animeId,
                CreationDate = DateTime.Now,
                NuitId = nuitId
            };

            Suggestions.Add(suggestion);
            await SaveChangesAsync();
        }

        public async Task DeleteSuggestionAsync(ulong suggestionId)
        {
            var suggestion = await Suggestions.AsQueryable().SingleOrDefaultAsync(x => x.SuggestionId == suggestionId);

            if (suggestion != null)
            {
                Suggestions.Remove(suggestion);
                await SaveChangesAsync();
            }
        }

        public async Task<List<SuggestionModel>> GetAllSuggestionsAsync()
            => await Suggestions.AsQueryable().ToListAsync();

        public async Task<List<SuggestionModel>> GetAllSuggestionsAsync(ulong nuitId)
            => await Suggestions.AsQueryable().Where(x => x.NuitId == nuitId).ToListAsync();

        public async Task<ISuggestionModel?> GetSuggestionAsync(ulong animeId, ulong nuitId)
            => await Suggestions.AsQueryable().SingleOrDefaultAsync(x => x.AnimeId == animeId && x.NuitId == nuitId);
    }
}
