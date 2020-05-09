using Discord;
using Discord.Commands;
using LaraxsBot.Common;
using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Threading.Tasks;

namespace LaraxsBot.Modules
{
    [SummaryFromEnum(SummaryEnum.InfoModule)]
    [Group("information")]
    [Name("InformationModule")]
    [Alias("info")]
    public sealed class InformationModule : ModuleBase<SocketCommandContext>
    {
        private readonly Process _process;

        private InformationModule()
        {
            _process = Process.GetCurrentProcess();
        }

        [Command]
        [SummaryFromEnum(SummaryEnum.Info)]
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

            builder.Description = $"**Temps de service:** {GetUptime()}\n" +
                                  $"**Librairie:** {GetLibrary()}\n" +
                                  $"**OS:** {GetOperatingSystem()}\n" +
                                  $"**Framework:** {GetFramework()}\n" +
                                  $"**Utilisation de mémoire:** {GetMemoryUsage()}\n" +
                                  $"**Latence:** {GetLatency()}\n" +
                                  $"**Github:** https://github.com/Satiriques/LaraxsBot\n" +
                                  $"**Propriétaire:** {app.Owner.Mention}";
            await ReplyAsync("", embed: builder.Build());
        }

        public string GetUptime()
        {
            var uptime = DateTime.Now - _process.StartTime;
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