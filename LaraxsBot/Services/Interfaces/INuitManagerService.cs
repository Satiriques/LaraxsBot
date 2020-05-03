using LaraxsBot.Database.Models;
using LaraxsBot.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace LaraxsBot.Services.Interfaces
{
    public interface INuitService
    {
        /// <summary>
        /// Creates a new nuit, throws if a nuit is already running.
        /// </summary>
        /// <returns></returns>
        Task<IManagerResult> CreateNuitAsync(DateTime start, DateTime end, ulong creatorId);
        Task<int> GetNumberOfNuitAsync();
        Task<IManagerResult> StopNuitAsync(ulong animeId);
        Task<IManagerResult> StopNuitAsync(ulong animeId, DateTime playTime);
        Task<IManagerResult> StartNuitAsync(ulong nuitId);
        Task<NuitModel?> GetRunningNuitAsync();
        Task<IManagerResult> CreateNuitAsync(ulong id);
    }
}
