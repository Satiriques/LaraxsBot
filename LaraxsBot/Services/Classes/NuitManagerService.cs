using LaraxsBot.Database.Contexts;
using LaraxsBot.Database.Interfaces;
using LaraxsBot.Interfaces;
using LaraxsBot.Services.Interfaces;
using System;
using System.Collections.Generic;
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

        public Task<INuit> CreateNuitAsync()
        {
            throw new NotImplementedException();
        }

        public Task<INuit> GetNuitAsync(ulong nuitId)
        {
            throw new NotImplementedException();
        }

        public Task<INuit> GetRunningNuitAsync()
        {
            throw new NotImplementedException();
        }

        public Task InitializeAsync()
        {
            throw new NotImplementedException();
        }
    }
}
