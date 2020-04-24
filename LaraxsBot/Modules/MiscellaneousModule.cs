using Discord;
using Discord.Commands;
using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Threading.Tasks;

namespace LaraxsBot.Modules
{
    public class MiscellaneousModule : ModuleBase
    {

        [Group("information"), Name("Information")]
        [Alias("info")]
        public class InformationModule : ModuleBase<SocketCommandContext>
        {
            private Process _process;

            InformationModule()
            {
                _process = Process.GetCurrentProcess();
            }
            [Command]
            [Summary("Donne les informations de performances.")]
            public async Task PerformanceAsync()
            {
                var builder = new EmbedBuilder();
                var app = await Context.Client.GetApplicationInfoAsync();
                builder.ThumbnailUrl = Context.Client.CurrentUser.GetAvatarUrl();
                builder.Title = "Sommaire des performances";
                builder.Author = new EmbedAuthorBuilder()
                {
                    Name = app.Owner.Username,
                    IconUrl = app.Owner.GetAvatarUrl(),
                };


                var uptime = (DateTime.Now - _process.StartTime);

                var desc = $"**Temps de service:** {GetUptime()}\n" +
                           $"**Librairie:** {GetLibrary()}\n" +
                           $"**OS:** {GetOperatingSystem()}\n" +
                           $"**Framework:** {GetFramework()}\n" +
                           $"**Utilisation de mémoire:** {GetMemoryUsage()}\n" +
                           $"**Latence:** {GetLatency()}\n" + 
                           $"**Github:** https://github.com/Satiriques/LaraxsBot";

                builder.Description = desc;
                await ReplyAsync("", embed: builder.Build());
            }

            public string GetUptime()
            {
                var uptime = (DateTime.Now - _process.StartTime);
                return $"{uptime.Days} jour {uptime.Hours} hr {uptime.Minutes} min {uptime.Seconds} sec";
            }

            public string GetLibrary()
                => $"Discord.Net ({DiscordConfig.Version})";

            public string GetOperatingSystem()
                => $"{RuntimeInformation.OSDescription} {RuntimeInformation.OSArchitecture}";

            public string GetFramework()
                => RuntimeInformation.FrameworkDescription;

            public string GetMemoryUsage()
                => $"{Math.Round(GC.GetTotalMemory(true) / (1024.0 * 1024.0), 2)}mb";

            public string GetLatency()
                => $"{Context.Client.Latency}ms";

        }
    }
}