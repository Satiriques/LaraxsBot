using Discord;
using Discord.Commands;
using LaraxsBot.Common;
using LaraxsBot.Services;
using LaraxsBot.Services.Interfaces;
using System.ComponentModel;
using System.Threading.Tasks;

namespace LaraxsBot.Modules
{
    [SummaryFromEnum(SummaryEnum.VoteModule)]
    public class VoteModule : ModuleBase<SocketCommandContext>
    {
        private readonly IVoteService _service;
        private readonly IMessageService _msg;

        public VoteModule(IVoteService service,
            IMessageService msg)
        {
            _service = service;
            _msg = msg;
        }

        [Command("propose")]
        public async Task ProposeAsync(ulong animeId)
        {
            var result = await _service.ProposeAsync(animeId, (IGuildUser)Context.User);
            if (!result.Success)
            {
                await ReplyAsync(result.Message);
            }
        }

        [Command("vote")]
        public async Task VoteAsync(ulong animeId)
        {
            var result = await _service.VoteAsync(animeId);
            if (!result.Success)
            {
                await ReplyAsync(result.Message);
            }
        }

        [SummaryFromEnum(SummaryEnum.Nuit)]
        [Command("nuit")]
        public async Task NuitAsync()
        {

        }
    }
}
