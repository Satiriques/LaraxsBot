﻿using Discord;
using Discord.WebSocket;
using LaraxsBot.Common;
using System;

namespace LaraxsBot.Services.Interfaces
{
    public interface IMessageService
    {
        string NoRunningNuitFound { get; }
        string NuitAlreadyRunning { get; }
        string StaffOnlyCommand { get; }
        string NextNuitTitle { get; }
        string NoLastAnimeEndedFound { get; }
        string DateTimeEndMustBeAfterNow { get; }
        public string GetInvalidAnimeMessage(ulong id);
        string GetVoteCreatorFooterNote(IGuildUser user);
        string GetSuggestionAlreadyExists(ulong animeId);
        string GetVoteChannelSet(ulong id);
        string GetVoteChannelGet(ulong id);
        string GetNoNuitFoundWithId(ulong nuitId);
        string GetNuitStopped(ulong animeId);
        string GetHelpInfoCommand(ISelfUser currentUser);
        string GetModuleOrCommandNotExists(string moduleName);
        string GetSummaryFromEnum(SummaryEnum @enum);
        string GetCommandPrefixSet(string prefix);
        string GetCommandPrefixGet(string commandPrefix);
        string GetRoleSet(ulong id);
        string GetRoleGet(ulong voteChannelId);
        string GetHelpInfoModule(SocketSelfUser currentUser);
        string GetDefaultPlayTimeSet(DayOfWeek dayOfweek, TimeSpan timeOfDay);
        string GetDefaultPlayTimeGet(DayOfWeek defaultPlayDay, TimeSpan defaultPlayTime);
    }
}
