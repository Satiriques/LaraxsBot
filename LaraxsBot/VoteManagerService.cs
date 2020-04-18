using LaraxsBot.Interfaces;
using LaraxsBot.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace LaraxsBot
{
    public class VoteManagerService : IVoteManagerService
    {
        public Task ProposeAsync(IAnimeVote vote)
        {
            throw new NotImplementedException();
        }

        public Task UnvoteAsync(IAnimeVote vote)
        {
            throw new NotImplementedException();
        }

        public Task VoteAsync(IAnimeVote vote)
        {
            throw new NotImplementedException();
        }

        public Task VoteExistsAsync(ulong animeId)
        {
            throw new NotImplementedException();
        }

        public Task VoteExistsAsync(string animeName)
        {
            throw new NotImplementedException();
        }
    }
}
