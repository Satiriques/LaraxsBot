using System;
using System.Collections.Generic;
using System.Text;

namespace LaraxsBot.Interfaces
{
    public interface IAnimeVote
    {
        ulong AnimeId { get; }
        string Name { get; }
        string Url { get; }
        ulong VoteId { get; }
        string Description { get; }
    }
}
