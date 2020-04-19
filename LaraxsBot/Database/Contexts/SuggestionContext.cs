using LaraxsBot.Database.Interfaces;
using LaraxsBot.Database.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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

        public async Task CreateSuggestionAsync(ulong animeId, ulong discordId, ulong nuitId)
        {
            var suggestion = new SuggestionModel()
            {
                AnimeId = animeId,
                CreationDate = DateTime.Now,
                DiscordId = discordId,
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

        public Task<List<SuggestionModel>> GetAllSuggestionsAsync()
            => Suggestions.AsQueryable().ToListAsync();

        public Task<List<SuggestionModel>> GetAllSuggestionsAsync(ulong nuitId)
            => Suggestions.AsQueryable().Where(x => x.NuitId == nuitId).ToListAsync();
    }
}
