using LaraxsBot.Interfaces;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LaraxsBot.Database.Models
{
    public class SuggestionModel : ISuggestionModel
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public ulong SuggestionId { get; set; }
        [Required]
        public DateTime CreationDate { get; set; }
        [Required]
        public ulong AnimeId { get; set; }
        [Required]
        public ulong NuitId { get; set; }
    }
}
