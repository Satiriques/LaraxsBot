using LaraxsBot.Database.Contexts;
using LaraxsBot.Database.Interfaces;
using LaraxsBot.Interfaces;
using LaraxsBot.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace LaraxsBot.Services.Classes
{
    public class VoteManagerService : IVoteManagerService
    {
        private readonly IVoteContext _voteContext;
        private readonly ISuggestionContext _suggestionContext;

        public VoteManagerService(IVoteContext voteContext, ISuggestionContext suggestionContext)
        {
            _voteContext = voteContext;
            _suggestionContext = suggestionContext;
        }

        public async Task ProposeAsync(ulong animeId)
        {
            
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
