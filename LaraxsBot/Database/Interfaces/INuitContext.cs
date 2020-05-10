using LaraxsBot.Database.Models;
using LaraxsBot.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace LaraxsBot.Database.Interfaces
{
    public interface INuitContextManager
    {
        Task CreateNuitAsync(DateTime start, DateTime end, ulong creatorId, DateTime playTime);
        Task CreateNuitAsync(ulong creatorId, DateTime playTime);
        Task<List<NuitModel>> GetAllNuitsAsync();
        Task<NuitModel?> GetStillRunningNuitAsync();
        Task StopNuitAsync(ulong animeId);
        Task StartNuitAsync(ulong id);
        void BackupAndDrop();
        void EnsureDeleted();
        Task<NuitModel?> GetLastEndedAnimeAsync();
        Task StopNuitAsync(ulong animeId, DateTime playTime);
        Task<bool> DoesNuitExistsAsync(ulong id);
        Task<NuitModel?> GetNuitAsync(ulong nuitId);
        Task ReplaceNuitAsync(NuitModel model);
        Task<NuitModel?> GetLastCreatedNuitAsync();
    }
}
