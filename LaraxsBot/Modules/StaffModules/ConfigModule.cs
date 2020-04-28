using Discord;
using Discord.Commands;
using LaraxsBot.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace LaraxsBot.Modules.StaffModules
{
    [Group("config")]
    [Name("Config")]
    public class ConfigModule : ModuleBase
    {
        private readonly IMessageService _msg;
        private readonly IConfig _config;

        public ConfigModule(IMessageService msg,
            IConfig config)
        {
            _msg = msg;
            _config = config;
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

        [Command("commandprefix")]
        public async Task CommandPrefixAsync(string prefix)
        {
            _config.SetPrefix(prefix);
            await ReplyAsync(_msg.GetCommandPrefixSet(prefix));
        }

        [Command("commandprefix")]
        public async Task CommandPrefixAsync()
        {
            await ReplyAsync(_msg.GetCommandPrefixGet(_config.CommandPrefix));
        }
    }
}
