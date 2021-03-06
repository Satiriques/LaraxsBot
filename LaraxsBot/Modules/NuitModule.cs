﻿using Discord;
using Discord.Commands;
using LaraxsBot.Common;
using LaraxsBot.Database.Interfaces;
using LaraxsBot.Services.Interfaces;
using System.Threading.Tasks;

namespace LaraxsBot.Modules
{
    [SummaryFromEnum(SummaryEnum.VoteModule)]
    [Group("nuit")]
    [Name(nameof(NuitModule))]
    public class NuitModule : ModuleBase<SocketCommandContext>
    {
        private readonly IVoteService _service;
        private readonly IEmbedService _embedService;
        private readonly INuitContextManager _nuitDb;
        private readonly IMessageService _messageService;

        public NuitModule(IVoteService service,
            IEmbedService embedService,
            INuitContextManager nuitDb,
            IMessageService messageService)
        {
            _service = service;
            _embedService = embedService;
            _nuitDb = nuitDb;
            _messageService = messageService;
        }

        [SummaryFromEnum(SummaryEnum.Propose)]
        [Command("propose")]
        [Alias("p")]
        public async Task ProposeAsync(ulong animeId)
        {
            var result = await _service.ProposeAsync(animeId, (IGuildUser)Context.User);
            if (!result.Success)
            {
                await ReplyAsync(result.Message);
            }
            else
            {
                await Context.Message.AddReactionAsync(new Emoji("👍"));
            }
        }

        [SummaryFromEnum(SummaryEnum.Nuit)]
        [Command("")]
        public async Task NuitAsync()
        {
            var nuit = await _nuitDb.GetLastEndedAnimeAsync();

            if(nuit != null)
            {
                var embed = await _embedService.CreateEmbedAsync(nuit);
                await ReplyAsync(embed: embed);
            }
            else
            {
                await ReplyAsync(_messageService.NoLastAnimeEndedFound);
            }
        }
    }
}
