using LaraxsBot.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace LaraxsBot.Database.Models
{
    public class NuitModel : INuit
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public ulong NuitId { get; set; }
        public bool IsRunning { get; set; }
        [Required]
        public ulong CreatorId { get; set; }
        [Required]
        public DateTime CreationDate { get; set; }
        [Required]
        public DateTime StartTime { get; set; }
        [Required]
        public DateTime StopTime { get; set; }
        public ulong WinnerAnimeId { get; set; }
    }
}
