using Discord;
using Discord.Addons.Interactive;
using Discord.Commands;
using LaraxsBot.Common;
using LaraxsBot.Services.Interfaces;
using System;
using System.Threading.Tasks;

namespace LaraxsBot.Modules.StaffModules
{
    [SummaryFromEnum(SummaryEnum.NuitStaffModule)]
    [RequireStaff]
    [Group("nuit")]
    [Name(nameof(NuitStaffModule))]
    public class NuitStaffModule : InteractiveBase<SocketCommandContext>
    {
        private readonly INuitService _nuitManagerService;
        private readonly IConfig _config;
        private readonly IMessageService _msg;
        private readonly IEmbedService _embedService;

        public NuitStaffModule(INuitService nuitManagerService,
            IConfig config,
            IMessageService messageService,
            IEmbedService embedService)
        {
            _nuitManagerService = nuitManagerService;
            _config = config;
            _msg = messageService;
            _embedService = embedService;
        }

        [SummaryFromEnum(SummaryEnum.NuitStaffList)]
        [Command("list")]
        public async Task ListNuitAsync()
        {
            var nuits = _nuitManagerService.GetAllNuitsAsync();
        }

        [SummaryFromEnum(SummaryEnum.NuitStaffEdit)]
        [Command("edit")]
        public async Task EditNuitAsync()
        {

        }

        [SummaryFromEnum(SummaryEnum.NuitStaffCreate)]
        [Command("create")]
        public async Task CreateNuitAsync(DateTime start, DateTime end)
        {
            var result = await _nuitManagerService.CreateNuitAsync(start, end, Context.User.Id);
            if (!result.Success)
            {
                await ReplyAsync(result.Message);
            }
            else
            {
                await Context.Message.AddReactionAsync(new Emoji("👍"));
            }
        }

        [SummaryFromEnum(SummaryEnum.NuitStaffCreate)]
        [Command("create")]
        public async Task CreateNuitAsync()
        {
            var result = await _nuitManagerService.CreateNuitAsync(Context.User.Id);
            if (!result.Success)
            {
                await ReplyAsync(result.Message);
            }
            else
            {
                await Context.Message.AddReactionAsync(new Emoji("👍"));
            }
        }

        [SummaryFromEnum(SummaryEnum.NuitStaffStart)]
        [Command("start")]
        public async Task StartNuitAsync(ulong nuitId)
        {
            var result = await _nuitManagerService.StartNuitAsync(nuitId);
            if (!result.Success)
            {
                await ReplyAsync(result.Message);
            }
            else
            {
                await Context.Message.AddReactionAsync(new Emoji("👍"));
            }
        }

        [SummaryFromEnum(SummaryEnum.NuitStaffStop)]
        [Command("stop")]
        public async Task StopNuitAsync(ulong animeId)
        {
            await StopAsync(animeId);
        }

        [SummaryFromEnum(SummaryEnum.NuitStaffStop)]
        [Command("stop")]
        public async Task StopNuitAsync(ulong animeId, [Remainder]DateTime playTime)
        {
            await StopAsync(animeId, playTime);
        }

        private async Task StopAsync(ulong animeId, DateTime playTime = default)
        {
            var result = playTime == default ? await _nuitManagerService.StopNuitAsync(animeId) : await _nuitManagerService.StopNuitAsync(animeId, playTime);
            if (result.Success)
            {
                var channel = Context.Guild.GetTextChannel(_config.VoteChannelId);
                var messages = await channel.GetMessagesAsync(100000).FlattenAsync();

                await channel.DeleteMessagesAsync(messages);

                await ReplyAsync(_msg.GetNuitStopped(animeId));
                await Context.Message.AddReactionAsync(new Emoji("👍"));
            }
            else
            {
                await ReplyAsync(result.Message);
            }
        }

        //[SummaryFromEnum(SummaryEnum.NuitStaffStatus)]
        //[Command("status")]
        //public async Task NuitStatusAsync()
        //{

        //}
    }
}
