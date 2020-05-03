using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LaraxsBot.Database.Models
{
    public class NuitModel
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public ulong NuitId { get; set; }
        public bool IsRunning { get; set; }
        [Required]
        public ulong CreatorId { get; set; }
        [Required]
        public DateTime CreationDate { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime StopTime { get; set; }
        public ulong WinnerAnimeId { get; set; }
        public DateTime PlayTime { get; set; }
    }
}
