using Discord;
using Discord.Commands;
using LaraxsBot.Common;
using LaraxsBot.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace LaraxsBot.Modules
{
    //code taken and slighty modified from aux on github : 
    // https://github.com/Aux/Dogey/blob/master/src/Dogey/Modules/HelpModule.cs
    [Group("help"), Name("Help")]
    public class HelpModule : ModuleBase<SocketCommandContext>
    {
        private readonly CommandService _commandService;
        private readonly IConfig _config;
        private readonly IMessageService _msg;
        private readonly IServiceProvider _provider;

        public HelpModule(IServiceProvider provider,
            CommandService commands,
            IConfig config,
            IMessageService msg)
        {
            _commandService = commands;
            _config = config;
            _msg = msg;
            _provider = provider;
        }

        private string GetSummary(ModuleInfo moduleInfo)
        {
            var attribute = moduleInfo.Attributes.OfType<SummaryFromEnum>().FirstOrDefault();

            return attribute != null ? _msg.GetSummaryFromEnum(attribute.Enum) : string.Empty;
        }

        private string GetSummary(CommandInfo commandInfo)
        {
            var attribute = commandInfo.Attributes.OfType<SummaryFromEnum>().FirstOrDefault();

            return attribute != null ? _msg.GetSummaryFromEnum(attribute.Enum) : string.Empty;
        }

        [Command]
        public async Task HelpAsync()
        {
            string prefix = _config.CommandPrefix ?? $"@{Context.Client.CurrentUser} ";
            var modules = _commandService.Modules.Where(x => !string.IsNullOrWhiteSpace(GetSummary(x)));

            _msg.GetHelpInfo(Context.Client.CurrentUser);

            var builder = new EmbedBuilder()
                .WithFooter(x => x.Text = _msg.GetHelpInfo(Context.Client.CurrentUser));

            foreach (var module in modules)
            {
                bool success = false;
                foreach (var command in module.Commands)
                {
                    var result = await command.CheckPreconditionsAsync(Context, _provider);
                    if (result.IsSuccess)
                    {
                        success = true;
                        break;
                    }
                }

                if (!success)
                    continue;

                builder.AddField(module.Name, GetSummary(module));
            }

            await ReplyAsync("", embed: builder.Build());
        }

        [Command]
        public async Task HelpAsync([Remainder] string moduleOrCommandName)
        {
            if (!await CheckForModulesAsync(moduleOrCommandName) && !await CheckForCommandsAsync(moduleOrCommandName))
            {

            }
        }

        private async Task<bool> CheckForModulesAsync(string moduleName)
        {
            string prefix = _config.CommandPrefix ?? $"@{Context.Client.CurrentUser.Username}";
            var modules = _commandService.Modules.Where(x => string.Equals(x.Name, moduleName, StringComparison.OrdinalIgnoreCase));

            var commands = modules.SelectMany(x => x.Commands).Where(x => !string.IsNullOrWhiteSpace(GetSummary(x)))
                                 .GroupBy(x => x.Name)
                                 .Select(x => x.First());

            if (!commands.Any())
            {
                return false;
            }

            var builder = new EmbedBuilder()
                .WithFooter(x => x.Text = _msg.GetHelpInfo(Context.Client.CurrentUser));

            foreach (var command in commands)
            {
                var result = await command.CheckPreconditionsAsync(Context, _provider);
                if (result.IsSuccess)
                {
                    var parameters = string.Join(" ", command.Parameters.Select(x => $"<{x.Name}>"));

                    builder.AddField(prefix + command.Aliases[0] + " " + parameters, GetSummary(command));
                }
            }

            await ReplyAsync("", embed: builder.Build());
            return true;
        }

        private async Task<bool> CheckForCommandsAsync(string commandName)
        {
            string prefix = _config.CommandPrefix ?? $"@{Context.Client.CurrentUser.Username}";

            var commands = _commandService.Commands.Where(x => x.Aliases.Contains(commandName));

            if (!commands.Any())
            {
                return false;
            }

            var builder = new EmbedBuilder();
            var aliases = new List<string>();

            foreach (var command in commands)
            {
                var result = await command.CheckPreconditionsAsync(Context, _provider);


                if (result.IsSuccess)
                {
                    var sbuilder = new StringBuilder()
                        .Append(prefix + command.Aliases.First());

                    foreach (var parameter in command.Parameters)
                    {
                        string p = parameter.Name;

                        if (parameter.IsRemainder)
                            p += "...";
                        if (parameter.IsOptional)
                            p = $"[{p}]";
                        else
                            p = $"<{p}>";

                        sbuilder.Append(" " + p);
                    }

                    builder.AddField(sbuilder.ToString(), command.Remarks ?? GetSummary(command));
                }
                aliases.AddRange(command.Aliases);

            }
            builder.WithFooter(x => x.Text = $"Alias: {string.Join(", ", aliases)}");
            await ReplyAsync("", embed: builder.Build());
            return true;
        }
    }
}
