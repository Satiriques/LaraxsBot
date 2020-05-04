namespace LaraxsBot.Services.Interfaces
{
    public interface IConfig
    {
        ulong VoteChannelId { get; }
        ulong StaffRoleId { get; }

        void SetVoteChannelId(ulong id);

        void SetPrefix(string prefix);

        string CommandPrefix { get; }

        void SetRole(ulong id);
    }
}