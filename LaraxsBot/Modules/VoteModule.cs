﻿using Discord;
using Discord.Commands;
using LaraxsBot.Common;
using LaraxsBot.Database.Interfaces;
using LaraxsBot.Services;
using LaraxsBot.Services.Interfaces;
using System.ComponentModel;
using System.Threading.Tasks;

namespace LaraxsBot.Modules
{
    [SummaryFromEnum(SummaryEnum.VoteModule)]
    [Group("nuit")]
    public class VoteModule : ModuleBase<SocketCommandContext>
    {
        private readonly IVoteService _service;
        private readonly IEmbedService _embedService;

        public VoteModule(IVoteService service, 
            IEmbedService embedService,
            INuitContextManager nuitContext)
        {
            _service = service;
            _embedService = embedService;
        }

        [Command("propose")]
        [Alias("p")]
        [SummaryFromEnum(SummaryEnum.Propose)]
        public async Task ProposeAsync(ulong animeId)
        {
            var result = await _service.ProposeAsync(animeId, (IGuildUser)Context.User);
            if (!result.Success)
            {
                await ReplyAsync(result.Message);
            }
        }

        [SummaryFromEnum(SummaryEnum.Nuit)]
        [Command("")]
        public async Task NuitAsync()
        {
            

            //_embedService.CreateEmbed()
        }
    }
}
