using Discord;
using LaraxsBot.Services.Interfaces;
using MalParser.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace LaraxsBot.Services.Classes
{
    public class EmbedService : IEmbedService
    {
        private readonly IMessageService _messageService;

        public EmbedService(IMessageService messageService)
        {
            _messageService = messageService;
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

        public Embed CreateVoteEmbed(IAnime anime, ulong id, IGuildUser user)
        {
            return new EmbedBuilder()
            {
                ThumbnailUrl = anime.ImageUrl,
                Description = anime.Title,
                Url = $"https://myanimelist.net/anime/{id}",
                Footer = new EmbedFooterBuilder()
                {
                    IconUrl = user.GetAvatarUrl(),
                    Text = _messageService.GetVoteCreatorFooterNote(user),
                }
            }.Build();
        }

        public void EditEmbed()
        {
            throw new NotImplementedException();
        }

        public void RemovedEmbed()
        {
            throw new NotImplementedException();
        }

        public void SwapEmbed()
        {
            throw new NotImplementedException();
        }
    }
}
