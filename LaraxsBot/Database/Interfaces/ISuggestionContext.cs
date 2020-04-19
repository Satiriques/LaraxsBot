using System;
using System.Collections.Generic;
using System.Text;

namespace LaraxsBot.Database.Interfaces
{
    public interface ISuggestionContext
    {
        Task CreateSuggestionAsync(ulong animeId, ulong discordId, ulong nuitId);
        Task DeleteSuggestionAsync(ulong suggestionId);
        Task<List<SuggestionModel>> GetAllSuggestionsAsync();
        Task<List<SuggestionModel>> GetAllSuggestionsAsync(ulong nuitId);
    }
}
