using Discord;
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
        public void EditEmbed();
        public void RemovedEmbed();
        public void SwapEmbed();
        Embed CreateVoteEmbed(IAnime anime, ulong id, IGuildUser user);
    }
}
