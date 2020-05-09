using System;

namespace LaraxsBot.Services.Interfaces
{
    public interface IConfig
    {
        ulong VoteChannelId { get; }
        ulong StaffRoleId { get; }
        void SetVoteChannelId(ulong id);
        void SetPrefix(string prefix);
        string CommandPrefix { get; }
        public DayOfWeek DefaultPlayDay { get; }
        public TimeSpan DefaultPlayTime { get; }
        void SetRole(ulong id);
        void SetDefaultPlayTime(DayOfWeek dayOfweek, TimeSpan timeOfDay);
    }
}