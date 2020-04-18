using Discord.Commands;
using LaraxsBot.Services;
using LaraxsBot.Services.Interfaces;

namespace LaraxsBot.Modules
{
    public class VoteModule : ModuleBase<SocketCommandContext>
    {
        private readonly IVoteManagerService _service;

        public VoteModule(IVoteManagerService service)
        {
            _service = service;
        }
    }
}
