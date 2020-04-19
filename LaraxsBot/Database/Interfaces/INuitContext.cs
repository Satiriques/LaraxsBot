using LaraxsBot.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace LaraxsBot.Database.Interfaces
{
    public interface INuitContext
    {
        Task CreateNuitAsync();
        Task<IEnumerable<INuit>> GetAllNuitsAsync();
        Task<INuit> GetStillRunningNuitAsync();
        Task StopRunningNuitAsync();
    }
}
