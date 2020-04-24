using LaraxsBot.Database.Interfaces;
using LaraxsBot.Database.Models;
using LaraxsBot.Interfaces;
using LaraxsBot.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace LaraxsBot.Database.Contexts
{
    public class VoteContext : DbContext, IVoteContext
    {
        public DbSet<AnimeVoteModel> Votes { get; set; }

#pragma warning disable CS8618
        public VoteContext()
#pragma warning restore CS8618
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

        public void BackupAndDrop()
        {
            var path = Path.Combine(AppContext.BaseDirectory, "data", "votes.sqlite.db");

            if (File.Exists(path))
            {
                FileInfo fileInfo = new FileInfo(path);

                var fileName = DateTime.Now.ToShortDateString() + "_" + DateTime.Now.ToShortTimeString() + "_" + fileInfo.Name + ".bak";
                var newPath = Path.Combine(fileInfo.Directory.FullName, fileName);

                File.Copy(path, newPath);

                Database.EnsureDeleted();
            }
        }

        public async Task CreateVoteAsync(ulong animeId, ulong nuitId, ulong userId)
        {
            var vote = new AnimeVoteModel()
            {
                AnimeId = animeId,
                CreationDate = DateTime.Now,
                NuitId = nuitId,
                UserId = userId,
            };

            Votes.Add(vote);
            await SaveChangesAsync();
        }

        public async Task DeleteVoteAsync(ulong voteId)
        {
            var vote = await Votes.AsQueryable().SingleOrDefaultAsync(x => x.AnimeVoteId == voteId);

            if (vote != null)
            {
                Votes.Remove(vote);
                await SaveChangesAsync();
            }
        }

        public async Task DeleteVoteAsync(AnimeVoteModel model)
        {
            Votes.Remove(model);
            await SaveChangesAsync();
        }

        public async Task DeleteVotesAsync(IEnumerable<AnimeVoteModel> voteModels)
        {
            Votes.RemoveRange(voteModels);
            await SaveChangesAsync();
        }

        public async Task<List<AnimeVoteModel>> GetVotesAsync()
            => await Votes.AsQueryable().ToListAsync();
        public async Task<List<AnimeVoteModel>> GetVotesAsync(ulong nuitId)
            => await Votes.AsQueryable().Where(x => x.NuitId == nuitId).ToListAsync();
        public async Task<List<AnimeVoteModel>> GetVotesAsync(ulong nuidId, ulong animeId)
            => await Votes.AsQueryable().Where(x => x.NuitId == nuidId && x.AnimeId == animeId).ToListAsync();
        public async Task<AnimeVoteModel?> GetVoteAsync(ulong nuitId, ulong animeId, ulong userId)
            => await Votes.AsQueryable().SingleOrDefaultAsync(x => x.AnimeId == animeId && x.NuitId == nuitId && x.UserId == userId);
        public async Task<bool> VoteExistsAsync(ulong animeId, ulong nuitId, ulong userId)
            => await Votes.AsQueryable().AnyAsync(x => x.AnimeId == animeId && x.NuitId == nuitId && x.UserId == userId);
    }
}
