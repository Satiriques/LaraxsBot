using LaraxsBot.Database.Interfaces;
using LaraxsBot.Interfaces;
using LaraxsBot.Services.Interfaces;
using System;
using System.Threading.Tasks;

namespace LaraxsBot.Services.Classes
{
    public class VoteManagerService : IVoteManagerService
    {
        private readonly IVoteContext _voteDb;
        private readonly ISuggestionContext _suggestionDb;
        private readonly INuitContext _nuitDb;

        public VoteManagerService(IVoteContext voteContext, ISuggestionContext suggestionContext, INuitContext nuitContext)
        {
            _voteDb = voteContext;
            _suggestionDb = suggestionContext;
            _nuitDb = nuitContext;
        }

        public async Task ProposeAsync(ulong animeId)
        {
            var nuit = await _nuitDb.GetStillRunningNuitAsync();
            var suggestions = _suggestionDb.GetAllSuggestionsAsync();
        }

        public Task UnvoteAsync(IAnimeVote vote)
        {
            throw new NotImplementedException();
        }

        public Task VoteAsync(ulong animeId)
        {
            throw new NotImplementedException();
        }

        public Task VoteExistsAsync(ulong animeId)
        {
            throw new NotImplementedException();
        }

        public Task VoteExistsAsync(string animeName)
        {
            throw new NotImplementedException();
        }
    }
}
