using LaraxsBot.Common;
using LaraxsBot.Database.Contexts;
using LaraxsBot.Database.Interfaces;
using LaraxsBot.Interfaces;
using LaraxsBot.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LaraxsBot.Services.DatabaseFacade
{
    /// <summary>
    /// Serves as a facade between the user and the nuit database
    /// </summary>
    public class NuitService : INuitService
    {
        private readonly INuitContext _nuitContext;
        private readonly IMessageService _msg;

        public NuitService(INuitContext nuitContext,
            IMessageService messageService)
        {
            _nuitContext = nuitContext;
            _msg = messageService;
        }

        public async Task<IManagerResult> CreateNuitAsync(DateTime start, DateTime end, ulong creatorId)
        {
            var currentNuit = await _nuitContext.GetStillRunningNuitAsync();

            if (currentNuit == null)
            {
                await _nuitContext.CreateNuitAsync(start, end, creatorId);
            }
            else
            {
                return ManagerResult.FromErrorMessage(_msg.NuitAlreadyRunning);
            }

            return ManagerResult.Default;
        }

        public async Task<IManagerResult> StartNuitAsync(ulong nuitId)
        {
            var currentNuit = await _nuitContext.GetStillRunningNuitAsync();

            if (currentNuit == null)
            {
                var nuits = await _nuitContext.GetAllNuitsAsync();
                var nuit = nuits.FirstOrDefault(x => x.NuitId == nuitId);

                if(nuit != null)
                {
                    await _nuitContext.StartNuitAsync(nuit.NuitId);
                }
                else
                {
                    return ManagerResult.FromErrorMessage(_msg.GetNoNuitFoundWithId(nuitId));
                }
            }
            else
            {
                return ManagerResult.FromErrorMessage(_msg.NuitAlreadyRunning);
            }

            return ManagerResult.Default;
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

        public async Task<IManagerResult> StopNuitAsync(ulong animeId)
        {
            var currentNuit = await _nuitContext.GetStillRunningNuitAsync();

            if(currentNuit != null)
            {
                await _nuitContext.StopNuitAsync(animeId);
            }
            else
            {
                return ManagerResult.FromErrorMessage(_msg.NoRunningNuitFound);
            }

            return ManagerResult.Default;
        }
    }
}
