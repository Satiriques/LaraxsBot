using Discord;
using Discord.Commands;
using LaraxsBot.Common;
using LaraxsBot.Services.Interfaces;
using System.Threading.Tasks;

namespace LaraxsBot.Modules.StaffModules
{
    [SummaryFromEnum(SummaryEnum.ConfigModule)]
    [Group("config")]
    [Name(nameof(ConfigModule))]
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

        [SummaryFromEnum(SummaryEnum.ConfigSetVoteChannel)]
        [Command("votechannel")]
        public async Task SetVoteChannelIdAsync(ITextChannel textChannel)
        {
            _config.SetVoteChannelId(textChannel.Id);
            await ReplyAsync(_msg.GetVoteChannelSet(textChannel.Id));
        }

        [SummaryFromEnum(SummaryEnum.ConfigGetVoteChannel)]
        [Command("votechannel")]
        public async Task GetVoteChannelIdAsync()
        {
            await ReplyAsync(_msg.GetVoteChannelGet(_config.VoteChannelId));
        }

        [SummaryFromEnum(SummaryEnum.ConfigSetCommandPrefix)]
        [Command("commandprefix")]
        public async Task CommandPrefixAsync(string prefix)
        {
            _config.SetPrefix(prefix);
            await ReplyAsync(_msg.GetCommandPrefixSet(prefix));
        }
        
        [SummaryFromEnum(SummaryEnum.ConfigGetCommandPrefix)]
        [Command("commandprefix")]
        public async Task CommandPrefixAsync()
        {
            await ReplyAsync(_msg.GetCommandPrefixGet(_config.CommandPrefix));
        }
    }
}
