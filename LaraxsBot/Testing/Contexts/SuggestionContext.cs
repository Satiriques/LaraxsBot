using LaraxsBot.Database.Interfaces;
using LaraxsBot.Database.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace LaraxsBot.Database.Testing.Contexts
{
    public class SuggestionContext : ISuggestionContext
    {
        public Task CreateSuggestionAsync(ulong animeId, ulong discordId, ulong nuitId)
        {
            throw new NotImplementedException();
        }

        public Task DeleteSuggestionAsync(ulong suggestionId)
        {
            throw new NotImplementedException();
        }

        public Task<List<SuggestionModel>> GetAllSuggestionsAsync()
        {
            throw new NotImplementedException();
        }

        public Task<List<SuggestionModel>> GetAllSuggestionsAsync(ulong nuitId)
        {
            throw new NotImplementedException();
        }
    }
}
