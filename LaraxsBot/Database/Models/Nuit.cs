using LaraxsBot.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace LaraxsBot.Database.Models
{
    public class Nuit : INuit
    {
        public ulong NuitId { get; set; }

        public bool IsRunning { get; set; }
        public ulong CreatorId { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime StopTime { get; set; }
    }
}
