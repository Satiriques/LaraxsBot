using LaraxsBot.Common;
using LaraxsBot.Database.Interfaces;
using LaraxsBot.Interfaces;
using LaraxsBot.Services.Interfaces;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace LaraxsBot.Services.Classes
{
    public class VoteManagerService : IVoteManagerService
    {
        private readonly IVoteContext _voteDb;
        private readonly ISuggestionContext _suggestionDb;
        private readonly INuitContext _nuitDb;
        private readonly IMessageService _msg;

        public VoteManagerService(IVoteContext voteContext, 
            ISuggestionContext suggestionContext, 
            INuitContext nuitContext,
            IMessageService messageService)
        {
            _voteDb = voteContext;
            _suggestionDb = suggestionContext;
            _nuitDb = nuitContext;
            _msg = messageService;
        }

        public async Task<IManagerResult> ProposeAsync(ulong animeId)
        {
            var nuit = await _nuitDb.GetStillRunningNuitAsync();

            if(nuit != null)
            {
                var suggestions = await _suggestionDb.GetAllSuggestionsAsync();

                if(!suggestions.Any(x=>x.AnimeId == animeId && x.NuitId == nuit.NuitId))
                {
                    await _suggestionDb.CreateSuggestionAsync(animeId, nuit.NuitId);
                    //TODO: handle the creation of the embed and such
                }
                else
                {
                    return ManagerResult.FromErrorMessage(_msg.GetSuggestionAlreadyExists(animeId));
                }
            }
            else
            {
                return ManagerResult.FromErrorMessage(_msg.NoRunningNuitFound);
            }

            return ManagerResult.Default;
        }

        public Task<IManagerResult> UnvoteAsync(IAnimeVote vote)
        {
            throw new NotImplementedException();
        }

        public Task<IManagerResult> VoteAsync(ulong animeId)
        {
            throw new NotImplementedException();
        }

        public Task<IManagerResult> VoteExistsAsync(ulong animeId)
        {
            throw new NotImplementedException();
        }

        public Task<IManagerResult> VoteExistsAsync(string animeName)
        {
            throw new NotImplementedException();
        }
    }
}
