using LaraxsBot.Database.Interfaces;
using LaraxsBot.Database.Models;
using LaraxsBot.Testing.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Linq;

namespace LaraxsBot.Database.Contexts
{
    public class VoteContext : DbContext, IVoteContext
    {
        public DbSet<AnimeVoteModel> Votes { get; set; }

        public VoteContext()
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

        public async Task CreateVoteAsync(ulong animeId, ulong discordId, ulong nuitId)
        {
            var vote = new AnimeVoteModel()
            {
                AnimeId = animeId,
                CreationDate = DateTime.Now,
                DiscordId = discordId,
                NuitId = nuitId
            };

            Votes.Add(vote);
            await SaveChangesAsync();
        }

        public async Task DeleteVoteAsync(ulong voteId)
        {
            var vote = await Votes.AsQueryable().SingleOrDefaultAsync(x => x.AnimeVoteId == voteId);

            if(vote != null)
            {
                Votes.Remove(vote);
                await SaveChangesAsync();
            }
        }

        public Task<List<AnimeVoteModel>> GetAllVotesAsync()
            => Votes.AsQueryable().ToListAsync();

        public Task<List<AnimeVoteModel>> GetAllVotesAsync(ulong nuitId)
            => Votes.AsQueryable().Where(x => x.NuitId == nuitId).ToListAsync();
    }
}
