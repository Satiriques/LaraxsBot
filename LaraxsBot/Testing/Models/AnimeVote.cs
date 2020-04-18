using LaraxsBot.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace LaraxsBot.Testing.Models
{
    public class AnimeVote : IAnimeVote
    {
        public ulong AnimeId { get; set; }

        public string Name { get; set; }

        public string Url { get; set; }

        public ulong VoteId { get; set; }

        public string Description { get; set; }
    }
}
