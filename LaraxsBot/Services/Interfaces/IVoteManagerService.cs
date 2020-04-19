using LaraxsBot.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace LaraxsBot.Services.Interfaces
{
    public interface IVoteManagerService
    {
        Task VoteAsync(ulong animeId);
        Task UnvoteAsync(IAnimeVote vote);
        Task ProposeAsync(ulong animeId);
        Task VoteExistsAsync(ulong animeId);
        Task VoteExistsAsync(string animeName);
    }
}
