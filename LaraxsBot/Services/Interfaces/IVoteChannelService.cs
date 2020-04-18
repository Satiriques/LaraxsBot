using LaraxsBot.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace LaraxsBot.Services.Interfaces
{
    public interface IVoteChannelService
    {
        Task<IEnumerable<IAnimeVote>> ReadAsync();
        Task SwapVotes(IAnimeVote animeVote1, IAnimeVote animeVote2);
        Task RemoveVote(IAnimeVote animeVote);
        Task SyncVoteChannel();
    }
}
