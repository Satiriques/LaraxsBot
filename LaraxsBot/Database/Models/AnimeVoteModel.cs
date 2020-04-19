using LaraxsBot.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace LaraxsBot.Database.Models
{
    public class AnimeVoteModel : IAnimeVote
    {

        public ulong VoteId { get; set; }
        public ulong AnimeId { get; set; }
        public ulong DiscordId { get; set; }
        public DateTime DateAdded { get; set; }
        public ulong NuitId { get; set; }
    }
}
