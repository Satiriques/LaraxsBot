using LaraxsBot.Database.Contexts;
using LaraxsBot.Database.Interfaces;
using LaraxsBot.Interfaces;
using LaraxsBot.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LaraxsBot.Services.Classes
{
    public class NuitManagerService : INuitManagerService
    {
        private readonly INuitContext _nuitContext;

        public NuitManagerService(INuitContext nuitContext)
        {
            _nuitContext = nuitContext;
        }

        public async Task CreateNuitAsync(DateTime start, DateTime end, ulong creatorId)
        {
            var currentNuit = await _nuitContext.GetStillRunningNuitAsync();

            if (currentNuit == null)
            {
                await _nuitContext.CreateNuitAsync(start, end, creatorId);
            }
        }

        public async Task<int> GetNumberOfNuitAsync()
        {
            var nuits = await _nuitContext.GetAllNuitsAsync();
            return nuits.Count();
        }

        public Task InitializeAsync()
        {
            throw new NotImplementedException();
        }

        public async Task StopNuitAsync()
        {
            await _nuitContext.StopNuitAsync();
        }
    }
}
