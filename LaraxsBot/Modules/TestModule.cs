using Discord;
using Discord.Commands;
using LaraxsBot.Services.Interfaces;
using MalParser;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace LaraxsBot.Modules
{
    public class TestModule : ModuleBase<SocketCommandContext>
    {
        private readonly IEmbedService _embedService;
        private readonly MalApi _malParser;
        private readonly IMessageService _messageService;

        public TestModule(IEmbedService embedService, MalApi malParser, IMessageService messageService)
        {
            _embedService = embedService;
            _malParser = malParser;
            _messageService = messageService;
        }

        [Command("test")]
        public async Task TestAsync()
        {
            await ReplyAsync("test");
            var channel = Context.Guild.GetChannel(1) as ITextChannel;
        }

        [Command("testanime")]
        public async Task TestAnimeAsync(ulong animeId)
        {
            var anime = await _malParser.GetAnimeAsync(animeId);

            if(anime != null)
            {
                await ReplyAsync(embed: _embedService.CreateEmbed(anime));
            }
            else
            {
                await ReplyAsync(_messageService.GetInvalidAnimeMessage(animeId));
            }
        }

        [Command("testvote")]
        public async Task TestVoteAsync(ulong animeId)
        {
            var anime = await _malParser.GetAnimeAsync(animeId);

            if(anime != null)
            {
                await ReplyAsync(embed: _embedService.CreateVoteEmbed(anime, 52, (IGuildUser)Context.User));
            }
            else
            {
                await ReplyAsync(_messageService.GetInvalidAnimeMessage(animeId));
            }
        }
    }
}
