using LaraxsBot.Database.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LaraxsBot.Database.Interfaces
{
    public interface IVoteContext
    {
        Task CreateVoteAsync(ulong animeId, ulong nuitId, ulong userId);
        Task DeleteVoteAsync(ulong voteId);
        Task<List<AnimeVoteModel>> GetVotesAsync();
        Task<List<AnimeVoteModel>> GetVotesAsync(ulong nuitId);
        void BackupAndDrop();
        Task<AnimeVoteModel?> GetVoteAsync(ulong animeId, ulong nuitId, ulong userId);
        Task<bool> VoteExistsAsync(ulong animeId, ulong nuitId, ulong userId);
        Task<List<AnimeVoteModel>> GetVotesAsync(ulong nuidId, ulong animeId);
        Task DeleteVotesAsync(IEnumerable<AnimeVoteModel> voteModels);
        Task DeleteVoteAsync(AnimeVoteModel model);
    }
}
