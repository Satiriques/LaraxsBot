using Discord;
using LaraxsBot.Database.Models;
using MalParser.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace LaraxsBot.Services.Interfaces
{
    /// <summary>
    /// Handles the creation and editing of embeds
    /// </summary>
    public interface IEmbedService
    {
        public Embed CreateEmbed(IAnime anime);
        public Task SwapEmbedAsync(IUserMessage message1, IUserMessage message2);
        Embed CreateVoteEmbed(IAnime anime, ulong id, IGuildUser user);
        Task<IEnumerable<IAnimeChannelVote>> GetChannelVotesAsync();
        Task<IAnimeChannelVote?> GetVoteFromEmbedAsync(IMessage message);
        Task<Embed> CreateEmbedAsync(NuitModel nuit);
        Embed CreateChoiceEmbed<TEmbed>();
    }
}
