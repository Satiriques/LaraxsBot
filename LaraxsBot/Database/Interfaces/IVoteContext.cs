using System;
using System.Collections.Generic;
using System.Text;

namespace LaraxsBot.Database.Interfaces
{
    public interface IVoteContext
    {
        Task CreateVoteAsync(ulong animeId, ulong discordId, ulong nuitId);
        Task DeleteVoteAsync(ulong voteId);
        Task<List<AnimeVoteModel>> GetAllVotesAsync();
        Task<List<AnimeVoteModel>> GetAllVotesAsync(ulong nuitId);
    }
}
