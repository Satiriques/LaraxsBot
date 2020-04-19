using LaraxsBot.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace LaraxsBot.Database.Models
{
    public class AnimeVoteModel
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public ulong AnimeVoteId { get; set; }
        [Required]
        public ulong DiscordId { get; set; }
        [Required]
        public DateTime CreationDate { get; set; }
        [Required]
        public ulong AnimeId { get; set; }
        [Required]
        public ulong NuitId { get; set; }
    }
}
