using Discord;
using LaraxsBot.Common;
using LaraxsBot.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace LaraxsBot.Services.Classes
{
    public class FrenchMessageService : IMessageService
    {
        public string NoRunningNuitFound
            => "Il n'y a pas de nuit en cours.";

        public string NuitAlreadyRunning
            => "Il y a déjà une nuit en cours.";

        public string StaffOnlyCommand
            => "Cette commande peut seulement être utilisé par un membre du staff.";

        public string GetHelpInfo(ISelfUser currentUser)
            => $"Écrire @{currentUser} help <module> pour plus d'information";

        public string GetInvalidAnimeMessage(ulong id)
            => $"Aucune anime trouvé avec id: {id}.";

        public string GetModuleOrCommandNotExists(string moduleName)
            => $"Le module ou la commande `{moduleName}` n'existe pas.";

        public string GetNoNuitFoundWithId(ulong nuitId)
            => $"Aucune nuit trouvé avec id: {nuitId}";

        public string GetNuitStopped(ulong animeId)
            => $"Le nuit a été fermé avec comme anime gagnant: {animeId}";

        public string GetSuggestionAlreadyExists(ulong animeId)
            => $"L'anime avec id {animeId} a déjà été proposé.";

        public string GetSummaryFromEnum(SummaryEnum @enum)
        {
            return @enum switch
            {
                SummaryEnum.Nuit => "Affiche la prochaine nuit de l'animé",
                SummaryEnum.VoteModule => "Commandes pour un utilisateur normal",
                _ => "N/A",
            };
        }

        public string GetVoteChannelGet(ulong id)
            => $"Le canal de vote a comme id: {id}";

        public string GetVoteChannelSet(ulong id)
            => $"Le canal de vote a été défini sur l'id: {id}.";

        public string GetVoteCreatorFooterNote(IGuildUser user)
            => $"Proposé par: {user.Nickname ?? user.Username}#{user.Discriminator} | {DateTime.Now.ToShortDateString()}.";
    }
}