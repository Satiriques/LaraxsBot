using Discord;
using LaraxsBot.Interfaces;

namespace LaraxsBot.Services.Interfaces
{
    /// <summary>
    /// Entity representing a vote on the vote channel
    /// </summary>
    public interface IAnimeChannelVote
    {
        ulong AnimeId { get; }
        IMessage Message { get; }
        ISuggestionModel SuggestionModel { get; }
    }
}