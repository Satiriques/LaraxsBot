using Discord;
using LaraxsBot.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace LaraxsBot.Services.Interfaces
{
    public interface IVoteService
    {
        Task<IManagerResult> ProposeAsync(ulong animeId, IGuildUser user);
    }
}
