using LaraxsBot.Database.Models;
using LaraxsBot.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace LaraxsBot.Services.Interfaces
{
    public interface IVoteChannelService
    {
        Task<IEnumerable<AnimeVoteModel>> ReadAsync();
        Task SwapVotes(AnimeVoteModel animeVote1, AnimeVoteModel animeVote2);
        Task RemoveVote(AnimeVoteModel animeVote);
        Task SyncVoteChannel();
    }
}
