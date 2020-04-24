﻿using Discord;
using Discord.Commands;
using LaraxsBot.Services.Interfaces;
using System;
using System.Threading.Tasks;

namespace LaraxsBot.Modules
{
    public class StaffModule : ModuleBase<SocketCommandContext>
    {
        private readonly INuitService _nuitManagerService;
        private readonly IConfig _config;
        private readonly IMessageService _msg;

        public StaffModule(INuitService nuitManagerService,
            IConfig config,
            IMessageService messageService)
        {
            _nuitManagerService = nuitManagerService;
            _config = config;
            _msg = messageService;
        }

        [Command("votechannel")]
        public async Task SetVoteChannelIdAsync(ITextChannel textChannel)
        {
            _config.SetVoteChannelId(textChannel.Id);
            await ReplyAsync(_msg.GetVoteChannelSet(textChannel.Id));
        }

        [Command("votechannel")]
        public async Task GetVoteChannelIdAsync()
        {
            await ReplyAsync(_msg.GetVoteChannelGet(_config.VoteChannelId));
        }

        [Command("createnuit")]
        public async Task CreateNuitAsync(DateTime start, DateTime end)
        {
            var result = await _nuitManagerService.CreateNuitAsync(start, end, Context.User.Id);
            if (!result.Success)
            {
                await ReplyAsync(result.Message);
            }
        }

        [Command("startnuit")]
        public async Task StartNuitAsync(ulong nuitId)
        {
            var result = await _nuitManagerService.StartNuitAsync(nuitId);
            if (!result.Success)
            {
                await ReplyAsync(result.Message);
            }
        }

        [Command("stopnuit")]
        public async Task StopNuitAsync(ulong animeId)
        {
            var result = await _nuitManagerService.StopNuitAsync(animeId);
            if (!result.Success)
            {
                await ReplyAsync(result.Message);
            }
            else
            {
                await ReplyAsync(_msg.GetNuitStopped(animeId));
            }
        }
    }
}
