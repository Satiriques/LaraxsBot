using Discord;
using Discord.WebSocket;
using LaraxsBot.Services.Interfaces;
using MalParser.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using LaraxsBot.Database.Interfaces;
using LaraxsBot.Interfaces;
using LaraxsBot.Database.Models;
using MalParser;

namespace LaraxsBot.Services.Classes
{
    public class EmbedService : IEmbedService
    {
        private readonly IMessageService _messageService;
        private readonly IConfig _config;
        private readonly DiscordSocketClient _client;
        private readonly INuitContextManager _nuitContext;
        private readonly IVoteContext _voteContext;
        private readonly ISuggestionContext _suggestionContext;
        private readonly MalApi _malApi;

        public EmbedService(IMessageService messageService,
            IConfig config, 
            DiscordSocketClient client,
            INuitContextManager nuitContext,
            IVoteContext voteContext,
            ISuggestionContext suggestionContext,
            MalApi malApi)
        {
            _messageService = messageService;
            _config = config;
            _client = client;
            _nuitContext = nuitContext;
            _voteContext = voteContext;
            _suggestionContext = suggestionContext;
            _malApi = malApi;
        }

        public Embed CreateEmbed(IAnime anime)
        {
            return new EmbedBuilder()
            {
                ImageUrl = anime.ImageUrl,
                Description = anime.Title,
                Fields = new List<EmbedFieldBuilder>()
                {
                    new EmbedFieldBuilder()
                    {
                        Name = "Synopsis",
                        Value = anime.Synopsis.Substring(0, 1024),
                    }
                }
            }.Build();
        }

        public async Task<IAnimeChannelVote?> GetVoteFromEmbedAsync(IMessage message)
        {
            if (message.Embeds.Count == 1)
            {
                var embed = message.Embeds.Single();
                var fields = embed.Fields;
                if(fields.Length == 1)
                {
                    var field = fields[0];

                    if (field.Name == "Anime Id" && ulong.TryParse(field.Value, out ulong animeId))
                    {
                        var nuit = await _nuitContext.GetStillRunningNuitAsync();
                        if(nuit != null)
                        {
                            var suggestionModel = await _suggestionContext.GetSuggestionAsync(animeId, nuit.NuitId);
                            if(suggestionModel != null)
                            {
                                return new AnimeChannelVote(animeId, suggestionModel, message);
                            }
                        }
                    }
                }
            }

            return null;
        }

        public async Task<IEnumerable<IAnimeChannelVote>> GetChannelVotesAsync()
        {
            var list = new List<IAnimeChannelVote>();

            if (_client.GetChannel(_config.VoteChannelId) is ITextChannel channel)
            {
                var messages = await channel.GetMessagesAsync(100000).FlattenAsync();
                foreach (var message in messages)
                {
                    if (message.Embeds.Count == 1)
                    {
                        var vote = await GetVoteFromEmbedAsync(message);
                        if(vote != null)
                        {
                            list.Add(vote);
                        }
                    }
                }
            }

            return list;
        }

        public Embed CreateVoteEmbed(IAnime anime, ulong animeId, IGuildUser user)
        {
            return new EmbedBuilder()
            {
                ThumbnailUrl = anime.ImageUrl,
                Description = anime.Title,
                Url = $"https://myanimelist.net/anime/{animeId}",
                Footer = new EmbedFooterBuilder()
                {
                    IconUrl = user.GetAvatarUrl(),
                    Text = _messageService.GetVoteCreatorFooterNote(user),
                },
                Fields = new List<EmbedFieldBuilder>()
                {
                    new EmbedFieldBuilder()
                    {
                        Name = "Anime Id",
                        Value = animeId,
                    },
                },
                Color = new Color(0x64FFDA),
            }.Build();
        }

        public async Task SwapEmbedAsync(IUserMessage message1, IUserMessage message2)
        {
            var tempEmbed = message1.Embeds.Single();
            var tempContent = message1.Content;

            await message1.ModifyAsync(x =>
            {
                x.Embed = (Embed)message2.Embeds.Single();
                x.Content = message2.Content;
            });

            await message2.ModifyAsync(x =>
            {
                x.Embed = (Embed)tempEmbed;
                x.Content = tempContent;
            });
        }

        public async Task<Embed> CreateEmbedAsync(NuitModel nuit)
        {
            var animeInfo = await _malApi.GetAnimeAsync(nuit.WinnerAnimeId);

            var embed = new EmbedBuilder()
            {
                Description = _messageService.NextNuitTitle,
                Fields = new List<EmbedFieldBuilder>()
                {
                    new EmbedFieldBuilder()
                    {
                        Name = animeInfo?.Title ?? $"Anime Id = {nuit.WinnerAnimeId}",
                        Value = $"https://myanimelist.net/anime/{nuit.WinnerAnimeId}",
                    },
                }
            };

            if(animeInfo != null)
            {
                embed.Fields.Add(new EmbedFieldBuilder()
                {
                    Name = "Genres:",
                    Value = string.Join(", ", animeInfo.Genres),
                });
                embed.ThumbnailUrl = animeInfo.ImageUrl;
            }

            if(nuit.PlayTime != default)
            {
                embed.Footer = new EmbedFooterBuilder()
                {
                    Text = $"Date: {nuit.PlayTime:dd/MM/yy}, Heure: {nuit.PlayTime:H:mm}",
                };
            }

            return embed.Build();
        }

        public Embed CreateChoiceEmbed<TEnum>()
        {
            var enums = Enum.GetValues(typeof(TEnum)).Cast<TEnum>().ToArray();

            var enumLine = enums.Select(x => $"{Array.IndexOf(enums, x)}. {x}");

            var embed = new EmbedBuilder()
                                .WithTitle("Écrire le numéro qui correspond à l'action désiré.")
                                .WithDescription(string.Join(Environment.NewLine, enumLine));

            return embed.Build();
        }
    }
}
