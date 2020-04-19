using LaraxsBot.Database.Interfaces;
using LaraxsBot.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace LaraxsBot.Database.Contexts
{
    public class NuitContext : INuitContext
    {
        public Task CreateNuitAsync()
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<INuit>> GetAllNuitsAsync()
        {
            throw new NotImplementedException();
        }

        public Task<INuit> GetStillRunningNuitAsync()
        {
            throw new NotImplementedException();
        }

        public Task StopRunningNuitAsync()
        {
            throw new NotImplementedException();
        }
    }
}
