using Discord;
using Discord.WebSocket;
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

        public string NextNuitTitle 
            => "Prochaine nuit de l'animé.";

        public string NoLastAnimeEndedFound
            => "Aucune ancienne nuit terminé trouvé.";

        public string GetCommandPrefixGet(string commandPrefix)
            => $"Le préfix de commande est: {commandPrefix}";

        public string GetCommandPrefixSet(string prefix)
            => $"Le nouveau préfix est: {prefix}";

        public string GetHelpInfoCommand(ISelfUser currentUser)
            => $"Écrire @{currentUser} help <commande> pour plus d'information";

        public string GetHelpInfoModule(SocketSelfUser currentUser)
            => $"Écrire @{currentUser} help <module> pour plus d'information";

        public string GetInvalidAnimeMessage(ulong id)
            => $"Aucune anime trouvé avec id: {id}.";

        public string GetModuleOrCommandNotExists(string moduleName)
            => $"Le module ou la commande `{moduleName}` n'existe pas.";

        public string GetNoNuitFoundWithId(ulong nuitId)
            => $"Aucune nuit trouvé avec id: {nuitId}";

        public string GetNuitStopped(ulong animeId)
            => $"Le nuit a été fermé avec comme anime gagnant: {animeId}";

        public string GetRoleGet(ulong voteChannelId)
            => $"Le rôle de staff a comme id: {voteChannelId}";

        public string GetRoleSet(ulong id)
            => $"Le role de staff a été défini sur l'id: {id}.";

        public string GetSuggestionAlreadyExists(ulong animeId)
            => $"L'anime avec id {animeId} a déjà été proposé.";

        public string GetSummaryFromEnum(SummaryEnum @enum)
        {
            return @enum switch
            {
                SummaryEnum.Nuit => "Affiche la prochaine nuit de l'animé",
                SummaryEnum.VoteModule => "Commandes pour un utilisateur normal",
                SummaryEnum.Info => "Donne les informations de performances",
                SummaryEnum.Propose => "Propose un animé par id pour la nuit de l'animé",
                SummaryEnum.NuitStaffModule => "Commands pour controler les nuits de l'animé",
                SummaryEnum.NuitStaffCreate => "Crée une nuit de l'animé",
                SummaryEnum.NuitStaffStart => "Permet au utilisateur de proposer et voter des animés",
                SummaryEnum.NuitStaffStop => "Termine une nuit de l'animé",
                SummaryEnum.NuitStaffHelper => "Permet de gérer les nuits avec une commande interactive",
                SummaryEnum.ConfigModule => "Module qui permet de changer les configuration",
                SummaryEnum.ConfigSetVoteChannel => "Défine le canal de vote",
                SummaryEnum.ConfigGetVoteChannel => "Obtient le canal de vote courant",
                SummaryEnum.ConfigSetCommandPrefix => "Défine le préfixe des commandes",
                SummaryEnum.ConfigGetCommandPrefix => "Obtient le préfixe des commandes",
                SummaryEnum.NuitStaffStatus => "Affiche le status de la nuit courante",
                SummaryEnum.NuitStaffEdit => "Permet d'éditer une nuit",
                SummaryEnum.NuitStaffList => "Liste toutes les nuits",
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