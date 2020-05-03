using Discord;
using Discord.Addons.Interactive;
using Discord.Commands;
using Discord.WebSocket;
using LaraxsBot.Database.Contexts;
using LaraxsBot.Database.Interfaces;
using LaraxsBot.Database.Managers;
using LaraxsBot.Services;
using LaraxsBot.Services.Classes;
using LaraxsBot.Services.DatabaseFacade;
using LaraxsBot.Services.Interfaces;
using MalParser;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace LaraxsBot
{
    public class CommandHandler
    {
        private readonly DiscordSocketClient _client;
        private readonly CommandService _commands;
        private readonly IConfig _config;
        public readonly IServiceProvider Services;
        private readonly Regex _magicRegex = new Regex(@"\[\[(.*?)\]\]");

        public CommandHandler(DiscordSocketClient client, 
            CommandService commands, 
            IConfig config)
        {
            _commands = commands;
            _config = config;
            _client = client;
            Services = BuildServiceProvider();
        }

        private IServiceProvider BuildServiceProvider()
        {
            return new ServiceCollection()
                        .AddSingleton(_client)
                        .AddSingleton(_commands)
                        .AddSingleton(_config)
                        .AddSingleton<CommandHandler>()
                        .AddSingleton<INuitInteractiveService, NuitInteractiveService>()
                        .AddSingleton<InteractiveService>()
                        .AddTransient<IVoteService, VoteService>()
                        .AddTransient<IMessageService, FrenchMessageService>()
                        .AddTransient<INuitService, NuitService>()
                        .AddTransient<INuitContextManager, NuitContextManager>()
                        .AddTransient<IVoteContext, VoteContextManager>()
                        .AddTransient<ISuggestionContext, SuggestionContextManager>()
                        .AddTransient<IEmbedService, EmbedService>()
                        .AddTransient<MalApi>()
                        .BuildServiceProvider();
        }

        public async Task InstallCommandsAsync()
        {
            // Hook the MessageReceived event into our command handler
            _client.MessageReceived += HandleCommandAsync;

            // Here we discover all of the command modules in the entry 
            // assembly and load them. Starting from Discord.NET 2.0, a
            // service provider is required to be passed into the
            // module registration method to inject the 
            // required dependencies.
            //
            // If you do not use Dependency Injection, pass null.
            // See Dependency Injection guide for more information.
            await _commands.AddModulesAsync(assembly: Assembly.GetEntryAssembly(),
                                            services: Services);
        }

        private async Task HandleCommandAsync(SocketMessage messageParam)
        {
            // Don't process the command if it was a system message
            var message = messageParam as SocketUserMessage;
            if (message == null) return;

            // Create a number to track where the prefix ends and the command begins
            int argPos = 0;

            // Determine if the message is a command based on the prefix and make sure no bots trigger commands
            if (!(message.HasStringPrefix(_config.CommandPrefix, ref argPos) ||
                message.HasMentionPrefix(_client.CurrentUser, ref argPos) ||
                _magicRegex.IsMatch(message.Content)) ||
                message.Author.IsBot)
            {
                return;
            }

            // Create a WebSocket-based command context based on the message
            var context = new SocketCommandContext(_client, message);

            // Execute the command with the command context we just
            // created, along with the service provider for precondition checks.
            IResult result = await _commands.ExecuteAsync(
                context: context,
                argPos: argPos,
                services: Services);

            if (!result.IsSuccess)
            {
                if(result.Error == CommandError.UnknownCommand)
                {
                    await context.Message.AddReactionAsync(new Emoji("❓"));
                }
                else
                {
                    await context.Channel.SendMessageAsync(result.ErrorReason);
                }
            }
            // Keep in mind that result does not indicate a return value
            // rather an object stating if the command executed successfully.

            // Optionally, we may inform the user if the command fails
            // to be executed; however, this may not always be desired,
            // as it may clog up the request queue should a user spam a
            // command.

        }
    }
}
