using LaraxsBot.Database.Contexts;
using LaraxsBot.Database.Interfaces;
using LaraxsBot.Database.Models;
using Microsoft.EntityFrameworkCore;
using NLog;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;

namespace LaraxsBot.Database.Managers
{
    public class VoteContextManager : IVoteContext
    {
        private readonly Logger _logger = LogManager.GetCurrentClassLogger();
        public void BackupAndDrop()
        {
            using var db = new VoteContext();
            var path = Path.Combine(AppContext.BaseDirectory, "data", "votes.sqlite.db");

            if (File.Exists(path))
            {
                FileInfo fileInfo = new FileInfo(path);

                var fileName = DateTime.Now.ToShortDateString() + "_" + DateTime.Now.ToShortTimeString() + "_" + fileInfo.Name + ".bak";
                var newPath = Path.Combine(fileInfo.Directory.FullName, fileName);

                File.Copy(path, newPath);

                db.Database.EnsureDeleted();
            }
        }

        public async Task CreateVoteAsync(ulong animeId, ulong nuitId, ulong userId)
        {
            using var db = new VoteContext();
            var vote = new AnimeVoteModel()
            {
                AnimeId = animeId,
                CreationDate = DateTime.Now,
                NuitId = nuitId,
                UserId = userId,
            };

            db.Votes.Add(vote);
            await db.SaveChangesAsync();
            _logger.Info($"Vote INSERT: N{nuitId} A{animeId} U{userId}");
        }

        public async Task DeleteVoteAsync(ulong voteId)
        {
            using var db = new VoteContext();
            var vote = await db.Votes.AsQueryable().SingleOrDefaultAsync(x => x.AnimeVoteId == voteId);

            if (vote != null)
            {
                db.Votes.Remove(vote);
                await db.SaveChangesAsync();
                _logger.Info($"Vote DELETE: V{voteId}");
            }
        }

        public async Task DeleteVoteAsync(AnimeVoteModel model)
        {
            using var db = new VoteContext();
            db.Votes.Remove(model);
            await db.SaveChangesAsync();
            _logger.Info($"Vote DELETE: N{model.NuitId} A{model.AnimeId} U{model.UserId} V{model.AnimeVoteId}");
        }

        public async Task DeleteVotesAsync(IEnumerable<AnimeVoteModel> voteModels)
        {
            using var db = new VoteContext();
            db.Votes.RemoveRange(voteModels);
            await db.SaveChangesAsync();
            foreach (var model in voteModels)
            {
                _logger.Info($"Vote DELETE: N{model.NuitId} A{model.AnimeId} U{model.UserId} V{model.AnimeVoteId}");
            }
        }

        public async Task<List<AnimeVoteModel>> GetVotesAsync()
        {
            using var db = new VoteContext();
            return await db.Votes.AsQueryable().ToListAsync();
        }

        public async Task<List<AnimeVoteModel>> GetVotesAsync(ulong nuitId)
        {
            using var db = new VoteContext();
            return await db.Votes.AsQueryable().Where(x => x.NuitId == nuitId).ToListAsync();
        }

        public async Task<List<AnimeVoteModel>> GetVotesAsync(ulong nuidId, ulong animeId)
        {
            using var db = new VoteContext();
            return await db.Votes.AsQueryable().Where(x => x.NuitId == nuidId && x.AnimeId == animeId).ToListAsync();
        }

        public async Task<AnimeVoteModel?> GetVoteAsync(ulong nuitId, ulong animeId, ulong userId)
        {
            using var db = new VoteContext();
            return await db.Votes.AsQueryable().SingleOrDefaultAsync(x => x.AnimeId == animeId && x.NuitId == nuitId && x.UserId == userId);
        }

        public async Task<bool> VoteExistsAsync(ulong animeId, ulong nuitId, ulong userId)
        {
            using var db = new VoteContext();
            return await db.Votes.AsQueryable().AnyAsync(x => x.AnimeId == animeId && x.NuitId == nuitId && x.UserId == userId);
        }
    }
}
