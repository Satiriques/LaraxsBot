using Discord;
using Discord.Commands;
using Discord.WebSocket;
using LaraxsBot.Services.Classes;
using LaraxsBot.Services.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using System.Transactions;

namespace LaraxsBot
{
    class Program
    {
        private DiscordSocketClient? _client;
        private CommandService? _commandService;
        private IConfig? _config;
        private CommandHandler? _commandHandler;

        static async Task Main() 
            => await new Program().MainAsync();

        public async Task MainAsync()
        {
            _client = new DiscordSocketClient(new DiscordSocketConfig() { MessageCacheSize = 100000 });
            _commandService = new CommandService();
            _config = Config.EnsureExists("config.json");

            _commandHandler = new CommandHandler(_client, _commandService, _config);

            _client.Log += Log;
            _client.Ready += ReadyAsync;

            // Remember to keep token private or to read it from an 
            // external source! In this case, we are reading the token 
            // from an environment variable. If you do not know how to set-up
            // environment variables, you may find more information on the 
            // Internet or by using other methods such as reading from 
            // a configuration.
            var token = Environment.GetEnvironmentVariable("DiscordToken", EnvironmentVariableTarget.User);

            await _commandHandler.InstallCommandsAsync();
            await _client.LoginAsync(TokenType.Bot, token);
            await _client.StartAsync();

            // Block this task until the program is closed.
            await Task.Delay(-1);
        }

        private async Task ReadyAsync()
        {
            await _client!.SetGameAsync("prefix ;");

            var config = _commandHandler!.Services.GetRequiredService<IConfig>();
            var nuitService = _commandHandler.Services.GetRequiredService<INuitInteractiveService>();
            var embedService = _commandHandler.Services.GetRequiredService<IEmbedService>();

            var channel = _client.GetChannel(config.VoteChannelId);
            nuitService.ClearReactionCallbacks();

            if(channel is ITextChannel textChannel)
            {
                var messages = await textChannel.GetMessagesAsync(100000).FlattenAsync();
                foreach(var message in messages.OfType<IUserMessage>())
                {
                    var vote = await embedService.GetVoteFromEmbedAsync(message);

                    if(vote != null)
                    {
                        await nuitService.SetMessageReactionCallback(message, vote.AnimeId);
                    }
                }
            }

            _client.Ready -= ReadyAsync;
        }

        private Task Log(LogMessage arg)
        {
            Console.WriteLine(arg.ToString());
            return Task.CompletedTask;
        }
    }
}
