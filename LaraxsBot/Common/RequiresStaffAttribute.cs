using Discord.Commands;
using Discord.WebSocket;
using LaraxsBot.Services.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LaraxsBot.Common
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = false, Inherited = true)]
    public class RequiresStaffAttribute : PreconditionAttribute
    {

        public override async Task<PreconditionResult> CheckPermissionsAsync(ICommandContext context, CommandInfo command, IServiceProvider services)
        {
            var config = services.GetRequiredService<IConfig>();
            var role = context.Guild.GetRole(config.StaffRoleId) as SocketRole;
            var user = context.User as SocketGuildUser;
            var app = await context.Client.GetApplicationInfoAsync();
            var msg = services.GetRequiredService<IMessageService>();

            if (user != null && (user.Roles.Contains(role) || user.Id == app.Owner.Id))
            {
                return PreconditionResult.FromSuccess();
            }
            else
            {
                return PreconditionResult.FromError(msg.StaffOnlyCommand);
            }

        }
    }
}
