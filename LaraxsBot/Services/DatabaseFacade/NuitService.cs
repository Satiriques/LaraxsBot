using LaraxsBot.Common;
using LaraxsBot.Database.Contexts;
using LaraxsBot.Database.Interfaces;
using LaraxsBot.Database.Models;
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
        private readonly IMessageService _msg;
        private readonly INuitContextManager _nuitContext;
        public NuitService(INuitContextManager nuitContext,
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

        public async Task<IManagerResult> CreateNuitAsync(ulong creatorId)
        {
            var currentNuit = await _nuitContext.GetStillRunningNuitAsync();

            if (currentNuit == null)
            {
                await _nuitContext.CreateNuitAsync(creatorId);
            }
            else
            {
                return ManagerResult.FromErrorMessage(_msg.NuitAlreadyRunning);
            }

            return ManagerResult.Default;
        }

        public async Task ReplaceAsync(NuitModel nuit)
            => await _nuitContext.ReplaceNuitAsync(nuit);

        public async Task<bool> DoesNuitExistsAsync(ulong id)
            => await _nuitContext.DoesNuitExistsAsync(id);

        public async Task<List<NuitModel>> GetAllNuitsAsync()
            => await _nuitContext.GetAllNuitsAsync();

        public async Task<NuitModel?> GetNuitAsync(ulong nuitId)
            => await _nuitContext.GetNuitAsync(nuitId);

        public async Task<int> GetNumberOfNuitAsync()
        {
            var nuits = await _nuitContext.GetAllNuitsAsync();
            return nuits.Count;
        }

        public async Task<NuitModel?> GetRunningNuitAsync()
            => await _nuitContext.GetStillRunningNuitAsync();

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

        public async Task<IManagerResult> StopNuitAsync(ulong animeId, DateTime playTime)
        {
            var currentNuit = await _nuitContext.GetStillRunningNuitAsync();

            if (currentNuit != null)
            {
                await _nuitContext.StopNuitAsync(animeId, playTime);
            }
            else
            {
                return ManagerResult.FromErrorMessage(_msg.NoRunningNuitFound);
            }

            return ManagerResult.Default;
        }
    }
}
