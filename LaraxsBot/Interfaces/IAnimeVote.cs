using System;

namespace LaraxsBot.Interfaces
{
    public interface IAnimeVote
    {
        ulong AnimeId { get; }
        ulong VoteId { get; }
        public ulong DiscordId { get; }
        public DateTime DateAdded { get; }
        public ulong NuitId { get; }
    }
}
