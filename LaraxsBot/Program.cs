using Discord;
using Discord.Commands;
using Discord.WebSocket;
using System;
using System.Threading.Tasks;
using System.Transactions;

namespace LaraxBot
{
    class Program
    {
        private DiscordSocketClient _client;
        private CommandService _commandService;
        private CommandHandler _commandHandler;

        static async Task Main() 
            => await new Program().MainAsync();

        public async Task MainAsync()
        {
            _client = new DiscordSocketClient(new DiscordSocketConfig() { MessageCacheSize = 100000 });
            _commandService = new CommandService();
            _commandHandler = new CommandHandler(_client, _commandService);

            _client.Log += Log;
            _client.Ready += Ready;

            // Remember to keep token private or to read it from an 
            // external source! In this case, we are reading the token 
            // from an environment variable. If you do not know how to set-up
            // environment variables, you may find more information on the 
            // Internet or by using other methods such as reading from 
            // a configuration.
            var token = Environment.GetEnvironmentVariable("DiscordToken", EnvironmentVariableTarget.User);
            await _client.LoginAsync(TokenType.Bot, token);
            await _client.StartAsync();

            await _commandHandler.InstallCommandsAsync();


            // Block this task until the program is closed.
            await Task.Delay(-1);
        }

        private Task Ready()
        {
            _client.SetGameAsync("prefix ;");
            return Task.CompletedTask;
        }

        private Task Log(LogMessage arg)
        {
            Console.WriteLine(arg.ToString());
            return Task.CompletedTask;
        }
    }
}
