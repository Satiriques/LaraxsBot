using Discord;
using Discord.Addons.Interactive;
using Discord.Commands;
using Discord.WebSocket;
using LaraxsBot.Database.Interfaces;
using LaraxsBot.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace LaraxsBot.Services.Classes
{
    public class NuitPaginatorMessageCallback : IReactionCallback
    {
        public SocketCommandContext Context { get; }
        public NuitInteractiveService _interactive { get; private set; }
        public IUserMessage Message { get; private set; }

        public RunMode RunMode => RunMode.Sync;
        public ICriterion<SocketReaction> Criterion => _criterion;

        public ulong AnimeId;
        private readonly IVoteContext _voteDb;

        public TimeSpan? Timeout => new TimeSpan(0, 0, 0, 0, -1);

        private readonly ICriterion<SocketReaction> _criterion;


        public NuitPaginatorMessageCallback(NuitInteractiveService service,
            IUserMessage message,
            ulong animeId,
            IVoteContext voteContext,
            ICriterion<SocketReaction> criterion = null)
        {
            _interactive = service;
            Message = message;
            AnimeId = animeId;
            _voteDb = voteContext;
            Context = null;
            _criterion = criterion ?? new EmptyCriterion<SocketReaction>();
        }

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

        public void RemoveCallBack(IMessage message)
        {
            _interactive.RemoveReactionCallback(message);
        }

        public async Task<bool> HandleCallbackAsync(SocketReaction reaction)
        {
            var emote = reaction.Emote;
            var user = reaction.User.Value as SocketGuildUser;

            if (emote.Equals(new Emoji("⬆")))
            {
                //if (!await _service.VoteExistsAsync(NuitActive.Key, AnimeId, reaction.UserId))
                //{
                //    await _service.AddVoteAsync(NuitActive.Key, AnimeId, reaction.UserId);
                //    var votes = await _service.GetVotesAsync(NuitActive.Key);
                //    //await _service.OrganiseDisplayAsync(votes, reaction.Channel as ITextChannel);
                //}

            }
            else if (emote.Equals(new Emoji("⬇")))
            {
                //if (await _service.VoteExistsAsync(NuitActive.Key, AnimeId, reaction.UserId))
                //{
                //    await _service.RemVoteAsync(NuitActive.Key, AnimeId, reaction.UserId);
                //    var votes = await _service.GetVotesAsync(NuitActive.Key);

                //    if (await _service.VoteExistsAsync(NuitActive.Key, AnimeId))
                //    {
                //        await _service.OrganiseDisplayAsync(votes, reaction.Channel as ITextChannel);
                //    }
                //    else
                //    {
                //        await _service.RemoveSuggestion(NuitActive.Key, AnimeId, reaction.User.Value);
                //        await _service.RemoveSuggestionDisplay(AnimeId, reaction.Channel as ITextChannel);
                //    }
                //}
            }
            else if (emote.Equals(new Emoji("ℹ")))
            {
                //if ((reaction.User.Value as SocketGuildUser).Roles.Any(x => x.Id.Equals(config.StaffRoleId)))
                //{

                //}
            }
            else if (emote.Equals(new Emoji("❌")))
            {
                //if ((reaction.User.Value as SocketGuildUser).Roles.Any(x => x.Id.Equals(config.StaffRoleId)))
                //{
                //    await _service.RemoveSuggestion(NuitActive.Key, AnimeId, reaction.User.Value);
                //    RemoveCallBack(Message);
                //    await Message.DeleteAsync();
                //}
            }

            await Message.RemoveReactionAsync(emote, reaction.User.Value);
            return false;

        }
    }
}
