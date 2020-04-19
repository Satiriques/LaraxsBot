using LaraxsBot.Database.Models;
using LaraxsBot.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace LaraxsBot.Database.Interfaces
{
    public interface INuitContext
    {
        Task CreateNuitAsync(DateTime start, DateTime end, ulong creatorId);
        Task<List<NuitModel>> GetAllNuitsAsync();
        Task<NuitModel?> GetStillRunningNuitAsync();
        Task StopNuitAsync(ulong animeId);
        Task StartNuitAsync(ulong id);
        void BackupAndDrop();
    }
}
