using Discord;
using Discord.Commands;
using LaraxsBot.Database.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace LaraxsBot.Modules
{
    public class OwnerModule : ModuleBase
    {
        private readonly IVoteContext _voteContext;
        private readonly INuitContext _nuitContext;
        private readonly ISuggestionContext _suggestionContext;

        public OwnerModule(IVoteContext voteContext,
            INuitContext nuitContext,
            ISuggestionContext suggestionContext)
        {
            _voteContext = voteContext;
            _nuitContext = nuitContext;
            _suggestionContext = suggestionContext;
        }

        [Command("dropdb")]
        public async Task DropDatabase(string database)
        {
            switch (database.ToUpper())
            {
                case "VOTES":
                    _voteContext.BackupAndDrop();
                    break;

                case "NUITS":
                    _nuitContext.BackupAndDrop();
                    break;

                case "SUGGESTIONS":
                    _suggestionContext.BackupAndDrop();
                    break;
                default:
                    break;
            }
        }

        [Command("listdb")]
        public async Task ListDatabase(string database)
        {
            switch (database.ToUpper())
            {
                case "VOTES":
                    var votes = await _voteContext.GetAllVotesAsync();
                    var msg = string.Join(Environment.NewLine, votes.Select(x => $"{x.NuitId} {x.AnimeVoteId}"));
                    msg = "NuitId AnimeVoteId" + Environment.NewLine + msg;
                    await ReplyAsync(Format.BlockQuote(msg));
                    break;

                case "NUITS":
                    var nuits = await _nuitContext.GetAllNuitsAsync();
                    msg = string.Join(Environment.NewLine, nuits.Select(x => $"{x.NuitId} {x.CreatorId} {x.IsRunning}"));
                    msg = "NuitId CreatorId IsRunning" + Environment.NewLine + msg;
                    await ReplyAsync(Format.BlockQuote(msg));
                    break;

                case "SUGGESTIONS":
                    var suggestions = await _suggestionContext.GetAllSuggestionsAsync();
                    msg = string.Join(Environment.NewLine, suggestions.Select(x => $"{x.NuitId} {x.SuggestionId}"));
                    msg = "NuitId SuggestionId" + Environment.NewLine + msg;
                    await ReplyAsync(Format.BlockQuote(msg));
                    break;
                default:
                    break;
            }
        }
    }
}
