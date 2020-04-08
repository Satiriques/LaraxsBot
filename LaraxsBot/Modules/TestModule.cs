using Discord.Commands;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace LaraxBot.Modules
{
    public class TestModule : ModuleBase<SocketCommandContext>
    {
        [Command("test")]
        public async Task TestAsync()
        {
            await ReplyAsync("test");
        }
    }
}
