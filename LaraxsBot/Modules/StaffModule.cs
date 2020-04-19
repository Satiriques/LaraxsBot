using Discord.Commands;
using LaraxsBot.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace LaraxsBot.Modules
{
    public class StaffModule : ModuleBase<SocketCommandContext>
    {
        private readonly INuitManagerService _nuitManagerService;

        public StaffModule(INuitManagerService nuitManagerService)
        {
            _nuitManagerService = nuitManagerService;
        }

        //[Command("create")]
        //public async Task CreateNuitAsync()
        //{
        //    await _nuitManagerService.CreateNuitAsync();
        //}
    }
}
