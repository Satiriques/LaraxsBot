using Discord;
using LaraxsBot.Services.Classes;
using System;
using System.Collections.Generic;
using System.Text;

namespace LaraxsBot.Services.Interfaces
{
    public interface IConfig
    {
        ulong VoteChannelId { get; }
        ITextChannel VoteChannel { get; }
    }
}
