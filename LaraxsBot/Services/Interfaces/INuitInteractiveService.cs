using Discord;
using Discord.Addons.Interactive;
using Discord.WebSocket;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace LaraxsBot.Services.Interfaces
{
    public interface INuitInteractiveService
    {
        Task<IUserMessage> SetMessageReactionCallback(IUserMessage message, ulong animeId, ICriterion<SocketReaction>? criterion = null);
        void ClearReactionCallbacks();
    }
}
