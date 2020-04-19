using System;
using System.Collections.Generic;
using System.Text;

namespace LaraxsBot.Interfaces
{
    public interface IManagerResult
    {
        string Message { get; }
        bool Success { get; }
    }
}
