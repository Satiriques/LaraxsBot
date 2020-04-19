using LaraxsBot.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace LaraxsBot.Testing.Models
{
    public class AnimeVote : IAnimeVote
    {
        public ulong AnimeId { get; set; }

        public ulong VoteId { get; set; }

        public ulong DiscordId { get; set; }

        public DateTime DateAdded { get; set; }

        public ulong NuitId { get; set; }
    }
}
