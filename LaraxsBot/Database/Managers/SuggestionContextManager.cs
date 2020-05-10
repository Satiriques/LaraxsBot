using LaraxsBot.Database.Contexts;
using LaraxsBot.Database.Interfaces;
using LaraxsBot.Database.Models;
using LaraxsBot.Interfaces;
using Microsoft.EntityFrameworkCore;
using NLog;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace LaraxsBot.Database.Managers
{
    public class SuggestionContextManager : ISuggestionContext
    {
        private readonly Logger _logger = LogManager.GetCurrentClassLogger();
        public void BackupAndDrop()
        {
            using var db = new SuggestionContext();
            var path = Path.Combine(AppContext.BaseDirectory, "data", "suggestions.sqlite.db");

            if (File.Exists(path))
            {
                FileInfo fileInfo = new FileInfo(path);

                var fileName = DateTime.Now.ToShortDateString() + "_" + DateTime.Now.ToShortTimeString() + "_" + fileInfo.Name + ".bak";
                var newPath = Path.Combine(fileInfo.Directory.FullName, fileName);

                File.Copy(path, newPath);

                db.Database.EnsureDeleted();
            }
        }

        public async Task CreateSuggestionAsync(ulong animeId, ulong nuitId)
        {
            using var db = new SuggestionContext();
            var suggestion = new SuggestionModel()
            {
                AnimeId = animeId,
                CreationDate = DateTime.Now,
                NuitId = nuitId
            };

            db.Suggestions.Add(suggestion);
            await db.SaveChangesAsync();
            _logger.Info($"Suggestion INSERT: N{nuitId} A{animeId}");
        }

        public async Task DeleteSuggestionAsync(ulong suggestionId)
        {
            using var db = new SuggestionContext();
            var suggestion = await db.Suggestions.AsQueryable().SingleOrDefaultAsync(x => x.SuggestionId == suggestionId);

            if (suggestion != null)
            {
                db.Suggestions.Remove(suggestion);
                await db.SaveChangesAsync();
                _logger.Info($"Suggestion DELETE: N{suggestion.NuitId} A{suggestion.AnimeId}");
            }
        }

        public async Task<List<SuggestionModel>> GetAllSuggestionsAsync()
        {
            using var db = new SuggestionContext();
            return await db.Suggestions.AsQueryable().ToListAsync();
        }

        public async Task<List<SuggestionModel>> GetAllSuggestionsAsync(ulong nuitId)
        {
            using var db = new SuggestionContext();
            return await db.Suggestions.AsQueryable().Where(x => x.NuitId == nuitId).ToListAsync();
        }

        public async Task<ISuggestionModel?> GetSuggestionAsync(ulong animeId, ulong nuitId)
        {
            using var db = new SuggestionContext();
            return await db.Suggestions.AsQueryable().SingleOrDefaultAsync(x => x.AnimeId == animeId && x.NuitId == nuitId);
        }
    }
}
