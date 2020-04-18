using Discord;
using System;
using System.Collections.Generic;
using System.Text;

namespace LaraxsBot.Services.Interfaces
{
    public interface IMessageService
    {
        public string GetInvalidAnimeMessage(ulong id);
        string GetVoteCreatorFooterNote(IGuildUser user);
    }
}
