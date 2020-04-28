using Discord;
using LaraxsBot.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace LaraxsBot.Services.Interfaces
{
    public interface IMessageService
    {
        string NoRunningNuitFound { get; }
        string NuitAlreadyRunning { get; }
        string StaffOnlyCommand { get; }

        public string GetInvalidAnimeMessage(ulong id);
        string GetVoteCreatorFooterNote(IGuildUser user);
        string GetSuggestionAlreadyExists(ulong animeId);
        string GetVoteChannelSet(ulong id);
        string GetVoteChannelGet(ulong id);
        string GetNoNuitFoundWithId(ulong nuitId);
        string GetNuitStopped(ulong animeId);
        string GetHelpInfo(ISelfUser currentUser);
        string GetModuleOrCommandNotExists(string moduleName);
        string GetSummaryFromEnum(SummaryEnum @enum);
    }
}
