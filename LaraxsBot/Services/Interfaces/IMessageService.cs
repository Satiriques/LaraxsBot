﻿using Discord;
using System;
using System.Collections.Generic;
using System.Text;

namespace LaraxsBot.Services.Interfaces
{
    public interface IMessageService
    {
        string NoRunningNuitFound { get; }
        string NuitAlreadyRunning { get; }

        public string GetInvalidAnimeMessage(ulong id);
        string GetVoteCreatorFooterNote(IGuildUser user);
        string GetSuggestionAlreadyExists(ulong animeId);
        string GetVoteChannelSet(ulong id);
        string GetVoteChannelGet(ulong id);
        string GetNoNuitFoundWithId(ulong nuitId);
        string GetNuitStopped(ulong animeId);
    }
}
