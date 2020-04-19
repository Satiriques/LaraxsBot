using System;

namespace LaraxsBot.Interfaces
{
    public interface INuit
    {
        ulong NuitId { get; }
        bool IsRunning { get; }
        DateTime CreationDate { get; }
        ulong CreatorId { get; }
        DateTime StartTime { get; }
        DateTime StopTime { get; }
    }
}
