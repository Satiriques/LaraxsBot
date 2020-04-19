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
    public class NuitContext : INuitContext
    {
        private List<Nuit> _nuits = new List<Nuit>();

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
            return Task.CompletedTask;
        }
        public Task<IEnumerable<INuit>> GetAllNuitsAsync()
        {
            IEnumerable<INuit> nuits = _nuits;
            return Task.FromResult(nuits);
        }

        public Task StopRunningNuitAsync()
        {
            var nuit = _nuits.SingleOrDefault(x => x.IsRunning);
            if (nuit != null)
            {
                nuit.IsRunning = false;
            }

            return Task.CompletedTask;
        }

        public Task<INuit> GetStillRunningNuitAsync()
        {
            return Task.FromResult(_nuits.SingleOrDefault(x => x.IsRunning) as INuit);
        }
    }
}
