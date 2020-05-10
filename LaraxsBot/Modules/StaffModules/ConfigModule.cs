using Discord;
using Discord.Commands;
using Discord.WebSocket;
using LaraxsBot.Common;
using LaraxsBot.Services.Interfaces;
using System;
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
        private readonly DiscordSocketClient _client;

        public ConfigModule(IMessageService msg,
            IConfig config,
            DiscordSocketClient client)
        {
            _msg = msg;
            _config = config;
            _client = client;
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

        [SummaryFromEnum(SummaryEnum.ConfigSetVoteChannel)]
        [Command("staffrole")]
        public async Task SetStaffRoleAsync(IRole role)
        {
            _config.SetRole(role.Id);
            await ReplyAsync(_msg.GetRoleSet(role.Id));
        }

        [SummaryFromEnum(SummaryEnum.ConfigGetVoteChannel)]
        [Command("staffrole")]
        public async Task GetStaffRoleAsync()
        {
            await ReplyAsync(_msg.GetRoleGet(_config.StaffRoleId));
        }

        [SummaryFromEnum(SummaryEnum.ConfigSetCommandPrefix)]
        [Command("commandprefix")]
        public async Task CommandPrefixAsync(string prefix)
        {
            _config.SetPrefix(prefix);
            await ReplyAsync(_msg.GetCommandPrefixSet(prefix));
            await _client!.SetGameAsync($"{_config!.CommandPrefix ?? _client.CurrentUser.Mention}help");
        }

        [SummaryFromEnum(SummaryEnum.ConfigGetCommandPrefix)]
        [Command("commandprefix")]
        public async Task CommandPrefixAsync()
        {
            await ReplyAsync(_msg.GetCommandPrefixGet(_config.CommandPrefix));
        }

        [SummaryFromEnum(SummaryEnum.ConfigDefaultPlayTimeSet)]
        [Command("defaultplaytime")]
        public async Task DefaultPlayTimeAsync(DayOfWeek dayOfweek, DateTime time)
        {
            _config.SetDefaultPlayTime(dayOfweek, time.TimeOfDay);
            await ReplyAsync(_msg.GetDefaultPlayTimeSet(dayOfweek, time.TimeOfDay));
        }

        [SummaryFromEnum(SummaryEnum.ConfigDefaultPlayTimeGet)]
        [Command("defaultplaytime")]
        public async Task DefaultPlayTimeAsync()
        {
            await ReplyAsync(_msg.GetDefaultPlayTimeGet(_config.DefaultPlayDay, _config.DefaultPlayTime));
        }
    }
}
