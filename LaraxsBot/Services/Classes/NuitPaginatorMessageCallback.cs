using Discord;
using Discord.Addons.Interactive;
using Discord.Commands;
using Discord.WebSocket;
using LaraxsBot.Database.Contexts;
using LaraxsBot.Database.Interfaces;
using LaraxsBot.Interfaces;
using LaraxsBot.Services.Interfaces;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.EntityFrameworkCore.ValueGeneration.Internal;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using System.Transactions;

namespace LaraxsBot.Services.Classes
{
    public class NuitPaginatorMessageCallback : IReactionCallback
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly IVoteContext _voteManager;
        private readonly IEmbedService _embedService;
        private readonly ISuggestionContext _suggestionManager;
        public ulong AnimeId;

        public NuitPaginatorMessageCallback(NuitInteractiveService service,
            IServiceProvider serviceProvider,
            IVoteContext voteManager,
            IEmbedService embedService,
            ISuggestionContext suggestionManager,
            IUserMessage message,
            ulong animeId,
            ICriterion<SocketReaction>? criterion = null)
        {
            _interactive = service;
            _serviceProvider = serviceProvider;
            _voteManager = voteManager;
            _embedService = embedService;
            _suggestionManager = suggestionManager;
            Message = message;
            AnimeId = animeId;
            Context = null;
            Criterion = criterion ?? new EmptyCriterion<SocketReaction>();
        }

        public NuitInteractiveService _interactive { get; private set; }
        public SocketCommandContext? Context { get; }
        public ICriterion<SocketReaction> Criterion { get; }
        public IUserMessage Message { get; private set; }

        public RunMode RunMode => RunMode.Sync;
        public TimeSpan? Timeout => new TimeSpan(0, 0, 0, 0, -1);
        public async Task DisplayAsync()
        {
            _interactive.AddReactionCallback(Message, this);
            // Reactions take a while to add, don't wait for them
            await Task.Run(async () =>
            {
                if (!Message.Reactions.ContainsKey(new Emoji("⬆")))
                {
                    await Message.AddReactionAsync(new Emoji("⬆"));
                    await Task.Delay(1000);
                }
                if (!Message.Reactions.ContainsKey(new Emoji("⬇")))
                {
                    await Message.AddReactionAsync(new Emoji("⬇"));
                    await Task.Delay(1000);
                }
                if (!Message.Reactions.ContainsKey(new Emoji("ℹ")))
                {
                    await Message.AddReactionAsync(new Emoji("ℹ"));
                    await Task.Delay(1000);
                }
                if (!Message.Reactions.ContainsKey(new Emoji("❌")))
                {
                    await Message.AddReactionAsync(new Emoji("❌"));
                    await Task.Delay(1000);
                }
            });
        }

        private string FormatMention(ulong userId)
               => $"<@{userId}>";

        public async Task<bool> HandleCallbackAsync(SocketReaction reaction)
        {
            var emote = reaction.Emote;
            var user = (SocketGuildUser)reaction.User.Value;
            bool wasMessageRemoved = false;
            var embedService = _serviceProvider.GetRequiredService<IEmbedService>();

            if (emote.Equals(new Emoji("⬆")))
            {
                await HandleVoteReaction(user);
            }
            else if (emote.Equals(new Emoji("⬇")))
            {
                await HandleUnvoteReaction(user);
            }
            else if (emote.Equals(new Emoji("ℹ")))
            {
            }
            else if (emote.Equals(new Emoji("❌")))
            {
                wasMessageRemoved = await HandleRemoveReaction(wasMessageRemoved);
            }

            if (!wasMessageRemoved)
            {
                await Message.RemoveReactionAsync(emote, reaction.User.Value);
            }
            return false;
        }

        private async Task HandleUnvoteReaction(SocketGuildUser user)
        {
            var currentSuggestion = await _embedService.GetVoteFromEmbedAsync(Message);

            if (currentSuggestion != null)
            {
                var vote = await _voteManager.GetVoteAsync(currentSuggestion.SuggestionModel.NuitId,
                currentSuggestion.SuggestionModel.AnimeId,
                user.Id);

                if (vote != null)
                {
                    await _voteManager.DeleteVoteAsync(vote);

                    var votes = await _voteManager.GetVotesAsync(currentSuggestion.SuggestionModel.NuitId, currentSuggestion.SuggestionModel.AnimeId);

                    var content = string.Join(" ", votes.Select(x => FormatMention(x.UserId)));
                    await Message.ModifyAsync(x =>
                    {
                        x.Content = string.IsNullOrWhiteSpace(content) ? string.Empty : content;
                        x.Embed = (Embed)Message.Embeds.Single();
                    });
                }
            }

            await SortChannelAsync();
        }

        private async Task HandleVoteReaction(SocketGuildUser user)
        {
            var currentSuggestion = await _embedService.GetVoteFromEmbedAsync(Message);

            if (currentSuggestion != null
                && !await _voteManager.VoteExistsAsync(currentSuggestion.SuggestionModel.AnimeId,
                        currentSuggestion.SuggestionModel.NuitId, user.Id))
            {
                await _voteManager.CreateVoteAsync(currentSuggestion.SuggestionModel.AnimeId,
                    currentSuggestion.SuggestionModel.NuitId,
                    user.Id);

                var votes = await _voteManager.GetVotesAsync(currentSuggestion.SuggestionModel.NuitId, currentSuggestion.SuggestionModel.AnimeId);

                await Message.ModifyAsync(x => x.Content = string.Join(" ", votes.Select(x => FormatMention(x.UserId))));
            }

            await SortChannelAsync();
        }

        private async Task<bool> HandleRemoveReaction(bool wasMessageRemoved)
        {
            var vote = await _embedService.GetVoteFromEmbedAsync(Message);
            if (vote != null)
            {
                var suggestionModel = await _suggestionManager.GetSuggestionAsync(vote.SuggestionModel.AnimeId, vote.SuggestionModel.NuitId);
                if (suggestionModel != null)
                {
                    await _suggestionManager.DeleteSuggestionAsync(suggestionModel.SuggestionId);
                    var votes = await _voteManager.GetVotesAsync(suggestionModel.NuitId, suggestionModel.AnimeId);
                    await _voteManager.DeleteVotesAsync(votes);
                }
            }
            RemoveCallBack(Message);
            await Message.DeleteAsync();
            wasMessageRemoved = true;
            return wasMessageRemoved;
        }

        private async Task SortChannelAsync()
        {
            var votes = (await _embedService.GetChannelVotesAsync()).Reverse();

            var messages = votes.Select(x => x.Message).ToArray();
            var index = messages.FindIndex(x => x.Id == Message.Id);

            if (index >= 0)
            {
                var previous = index > 0 ? messages[index - 1] as IUserMessage : null;
                var next = index < messages.Length - 1 ? messages[index + 1] as IUserMessage : null;

                var previousModel = votes.FirstOrDefault(x => x.Message == previous);
                var nextModel = votes.FirstOrDefault(x => x.Message == next);
                var currentModel = votes.First(x => x.Message.Id == Message.Id);

                var currentVoteNumber = await GetNumberOfVotesAsync(currentModel);
                var previousVoteNumber = previousModel != null ? await GetNumberOfVotesAsync(previousModel) : 0;
                var nextVoteNumber = nextModel != null ? await GetNumberOfVotesAsync(nextModel) : 0;

                if (previous != null && currentVoteNumber > previousVoteNumber)
                {
                    var messageToSwap = await GetFirstMessageWithCount(votes, previousVoteNumber);

                    _interactive.RemoveReactionCallback(Message);
                    _interactive.RemoveReactionCallback(messageToSwap);

                    await _embedService.SwapEmbedAsync(Message, messageToSwap);

                    var swapModel = await _embedService.GetVoteFromEmbedAsync(messageToSwap);

                    await _interactive.SetMessageReactionCallback(Message, swapModel!.AnimeId);
                    await _interactive.SetMessageReactionCallback(messageToSwap, currentModel!.AnimeId);
                }
                else if (next != null && currentVoteNumber < nextVoteNumber)
                {
                    var messageToSwap = await GetLastMessageWithCount(votes, previousVoteNumber);

                    _interactive.RemoveReactionCallback(Message);
                    _interactive.RemoveReactionCallback(messageToSwap);

                    await _embedService.SwapEmbedAsync(Message, messageToSwap);

                    var swapModel = await _embedService.GetVoteFromEmbedAsync(messageToSwap);

                    await _interactive.SetMessageReactionCallback(Message, swapModel!.AnimeId);
                    await _interactive.SetMessageReactionCallback(messageToSwap, currentModel!.AnimeId);
                }
            }
        }

        private async Task<IUserMessage> GetLastMessageWithCount(IEnumerable<IAnimeChannelVote> votes, int count)
        {
            List<VoteWithCount> voteWithCounts = new List<VoteWithCount>();
            foreach (var vote in votes)
            {
                voteWithCounts.Add(new VoteWithCount(vote, await GetNumberOfVotesAsync(vote)));
            }

            return (IUserMessage)voteWithCounts.Last(x => x.Count == count).Vote.Message;
        }

        private async Task<IUserMessage> GetFirstMessageWithCount(IEnumerable<IAnimeChannelVote> votes, int count)
        {
            List<VoteWithCount> voteWithCounts = new List<VoteWithCount>();
            foreach (var vote in votes)
            {
                voteWithCounts.Add(new VoteWithCount(vote, await GetNumberOfVotesAsync(vote)));
            }

            return (IUserMessage)voteWithCounts.First(x => x.Count == count).Vote.Message;
        }

        private class VoteWithCount
        {
            public VoteWithCount(IAnimeChannelVote vote, int count)
            {
                Vote = vote;
                Count = count;
            }

            public IAnimeChannelVote Vote { get; }
            public int Count { get; }
        }

        private async Task<int> GetNumberOfVotesAsync(IAnimeChannelVote animeChannelVote)
        {
            var votes = await _voteManager.GetVotesAsync(animeChannelVote.SuggestionModel.NuitId, animeChannelVote.SuggestionModel.AnimeId);
            return votes.Count;
        }

        public void RemoveCallBack(IMessage message)
        {
            _interactive.RemoveReactionCallback(message);
        }
    }
}
