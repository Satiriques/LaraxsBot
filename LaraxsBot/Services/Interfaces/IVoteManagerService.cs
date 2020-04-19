using Discord;
using LaraxsBot.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace LaraxsBot.Services.Interfaces
{
    public interface IVoteManagerService
    {
        Task<IManagerResult> VoteAsync(ulong animeId);
        Task<IManagerResult> UnvoteAsync(IAnimeVote vote);
        Task<IManagerResult> ProposeAsync(ulong animeId, IGuildUser user);
        Task<IManagerResult> VoteExistsAsync(ulong animeId);
        Task<IManagerResult> VoteExistsAsync(string animeName);
    }
}
