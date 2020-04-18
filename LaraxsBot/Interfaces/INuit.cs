using System;

namespace LaraxsBot.Interfaces
{
    public interface INuit
    {
        ulong NuitId { get; }
        bool IsRunning { get; }
        DateTime CreationDate { get; }
    }
}
