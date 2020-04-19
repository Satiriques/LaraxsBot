using LaraxsBot.Database.Interfaces;
using LaraxsBot.Database.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace LaraxsBot.Database.Testing.Contexts
{
    public class VoteContext : IVoteContext
    {
        public Task CreateVoteAsync(ulong animeId, ulong discordId, ulong nuitId)
        {
            throw new NotImplementedException();
        }

        public Task DeleteVoteAsync(ulong voteId)
        {
            throw new NotImplementedException();
        }

        public Task<List<AnimeVoteModel>> GetAllVotesAsync()
        {
            throw new NotImplementedException();
        }

        public Task<List<AnimeVoteModel>> GetAllVotesAsync(ulong nuitId)
        {
            throw new NotImplementedException();
        }
    }
}
