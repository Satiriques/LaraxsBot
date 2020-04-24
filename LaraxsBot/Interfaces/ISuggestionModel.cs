using System;
using System.Collections.Generic;
using System.Text;

namespace LaraxsBot.Interfaces
{
    public interface ISuggestionModel
    {
        ulong SuggestionId { get; }
        DateTime CreationDate { get; }
        ulong AnimeId { get; }
        ulong NuitId { get; }
    }
}
