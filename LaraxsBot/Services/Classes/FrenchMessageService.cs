using Discord;
using LaraxsBot.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace LaraxsBot.Services.Classes
{
    public class FrenchMessageService : IMessageService
    {
        public string GetInvalidAnimeMessage(ulong id) 
            => $"Aucune anime trouvé avec id: {id}";

        public string GetVoteCreatorFooterNote(IGuildUser user) 
            => $"Proposé par: {user.Nickname ?? user.Username}#{user.Discriminator} | {DateTime.Now.ToShortDateString()}";
    }
}