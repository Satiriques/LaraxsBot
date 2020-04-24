using Discord;
using LaraxsBot.Database.Models;
using LaraxsBot.Interfaces;
using LaraxsBot.Services.Interfaces;

namespace LaraxsBot.Services.Classes
{
    public class AnimeChannelVote : IAnimeChannelVote
    {
        public AnimeChannelVote(ulong animeId,
            ISuggestionModel suggestionModel,
            IMessage message)
        {
            AnimeId = animeId;
            SuggestionModel = suggestionModel;
            Message = message;
        }
        public ulong AnimeId { get; }
        public ISuggestionModel SuggestionModel { get; }

        public IMessage Message { get; }
    }
}
