using LaraxsBot.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace LaraxsBot.Testing.Models
{
    public class Nuit : INuit
    {
        public ulong NuitId { get; set; }

        public bool IsRunning { get; set; }

        public DateTime CreationDate { get; set; }
    }
}
