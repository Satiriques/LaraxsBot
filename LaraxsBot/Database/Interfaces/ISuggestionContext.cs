using LaraxsBot.Database.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace LaraxsBot.Database.Interfaces
{
    public interface ISuggestionContext
    {
        Task CreateSuggestionAsync(ulong animeId, ulong nuitId);
        Task DeleteSuggestionAsync(ulong suggestionId);
        Task<List<SuggestionModel>> GetAllSuggestionsAsync();
        Task<List<SuggestionModel>> GetAllSuggestionsAsync(ulong nuitId);
        void BackupAndDrop();
    }
}
