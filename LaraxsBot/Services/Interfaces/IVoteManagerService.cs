using LaraxsBot.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace LaraxsBot.Services.Interfaces
{
    public interface IVoteManagerService
    {
        Task VoteAsync(IAnimeVote vote);
        Task UnvoteAsync(IAnimeVote vote);
        Task ProposeAsync(IAnimeVote vote);
        Task VoteExistsAsync(ulong animeId);
        Task VoteExistsAsync(string animeName);
    }
}
