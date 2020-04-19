using LaraxsBot.Database.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace LaraxsBot.Database.Interfaces
{
    public interface IVoteContext
    {
        Task CreateVoteAsync(ulong animeId, ulong nuitId);
        Task DeleteVoteAsync(ulong voteId);
        Task<List<AnimeVoteModel>> GetAllVotesAsync();
        Task<List<AnimeVoteModel>> GetAllVotesAsync(ulong nuitId);
        void BackupAndDrop();
    }
}
