using LaraxsBot.Database.Interfaces;
using LaraxsBot.Interfaces;
using LaraxsBot.Services.Interfaces;
using LaraxsBot.Testing.Models;
using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Linq;
using System.Threading.Tasks;

namespace LaraxsBot.Database.Testing.Contexts
{
    class NuitContext : INuitContext
    {
        private List<INuit> _nuits = new List<INuit>();

        public NuitContext()
        {
        }

        public Task CreateNuitAsync()
        {
            var nuit = new Nuit
            {
                CreationDate = DateTime.Now,
                IsRunning = true,
            };

            _nuits.Add(nuit);
            return null;
        }
        public Task<IEnumerable<INuit>> GetAllNuitsAsync()
        {
            IEnumerable<INuit> nuits = _nuits;
            return Task.FromResult(nuits);
        }

        public Task<INuit> GetStillRunningNuitAsync()
        {
            throw new NotImplementedException();
        }

        private bool IsAnyNuitRunning()
        {
            return _nuits.Any(x => x.IsRunning);
        }
    }
}
