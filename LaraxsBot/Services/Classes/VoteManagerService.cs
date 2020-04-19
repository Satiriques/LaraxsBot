using Discord;
using Discord.WebSocket;
using LaraxsBot.Common;
using LaraxsBot.Database.Interfaces;
using LaraxsBot.Interfaces;
using LaraxsBot.Services.Interfaces;
using MalParser;
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
        private readonly DiscordSocketClient _client;
        private readonly NuitInteractiveService _nuitInteractiveService;
        private readonly IEmbedService _embedService;
        private readonly IConfig _config;
        private readonly MalApi _malApi;

        public VoteManagerService(IVoteContext voteContext, 
            ISuggestionContext suggestionContext, 
            INuitContext nuitContext,
            IMessageService messageService,
            DiscordSocketClient client,
            NuitInteractiveService nuitInteractiveService,
            IEmbedService embedService,
            IConfig config,
            MalApi malApi)
        {
            _voteDb = voteContext;
            _suggestionDb = suggestionContext;
            _nuitDb = nuitContext;
            _msg = messageService;
            _client = client;
            _nuitInteractiveService = nuitInteractiveService;
            _embedService = embedService;
            _config = config;
            _malApi = malApi;
        }

        public async Task<IManagerResult> ProposeAsync(ulong animeId, IGuildUser user)
        {
            var nuit = await _nuitDb.GetStillRunningNuitAsync();

            if(nuit != null)
            {
                var suggestions = await _suggestionDb.GetAllSuggestionsAsync();

                if(!suggestions.Any(x=>x.AnimeId == animeId && x.NuitId == nuit.NuitId))
                {
                    var anime = await _malApi.GetAnimeAsync(animeId);

                    if(anime != null)
                    {
                        await _suggestionDb.CreateSuggestionAsync(animeId, nuit.NuitId);
                        //TODO: handle the creation of the embed and such

                        if (_client.GetChannel(_config.VoteChannelId) is ITextChannel channel)
                        {
                            var msg = await channel.SendMessageAsync(embed: _embedService.CreateVoteEmbed(anime, animeId, user));
                            await _nuitInteractiveService.SetMessageReactionCallback(msg, animeId);
                        }
                        else
                        {

                        }
                    }
                    else
                    {
                        //TODO: anime not found
                    }
                    
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
