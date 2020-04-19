using Discord;
using Discord.Commands;
using LaraxsBot.Services;
using LaraxsBot.Services.Interfaces;
using System.Threading.Tasks;

namespace LaraxsBot.Modules
{
    public class VoteModule : ModuleBase<SocketCommandContext>
    {
        private readonly IVoteManagerService _service;

        public VoteModule(IVoteManagerService service)
        {
            _service = service;
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
    }
}
