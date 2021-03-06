﻿using Discord;
using Discord.Addons.Interactive;
using Discord.Commands;
using LaraxsBot.Common;
using LaraxsBot.Database.Interfaces;
using LaraxsBot.Database.Models;
using LaraxsBot.Services.Classes;
using LaraxsBot.Services.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Newtonsoft.Json;
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;

namespace LaraxsBot.Modules.StaffModules
{
    [SummaryFromEnum(SummaryEnum.NuitStaffModule)]
    [RequireStaff]
    [Group("nuit")]
    [Name(nameof(NuitStaffModule))]
    public class NuitStaffModule : InteractiveBase<SocketCommandContext>
    {
        private enum NuitProperties
        {
            PlayTime,
            WinnerAnimeId
        }
        private readonly INuitService _nuitManagerService;
        private readonly IConfig _config;
        private readonly IMessageService _msg;
        private readonly IEmbedService _embedService;
        private readonly IServiceProvider _serviceProvider;
        private readonly IVoteContext _voteDb;
        private readonly ISuggestionContext _suggestionDb;

        public NuitStaffModule(INuitService nuitManagerService,
            IConfig config,
            IMessageService messageService,
            IEmbedService embedService, 
            IServiceProvider serviceProvider,
            IVoteContext voteDb,
            ISuggestionContext suggestionDb)
        {
            _nuitManagerService = nuitManagerService;
            _config = config;
            _msg = messageService;
            _embedService = embedService;
            _serviceProvider = serviceProvider;
            _voteDb = voteDb;
            _suggestionDb = suggestionDb;
        }

        [SummaryFromEnum(SummaryEnum.NuitStaffList)]
        [Command("list")]
        public async Task ListNuitAsync()
        {
            var nuits = await _nuitManagerService.GetAllNuitsAsync();
            var json = JsonConvert.SerializeObject(nuits);
            var file = Path.GetTempFileName();
            File.WriteAllText(file, json);
            await Context.Channel.SendFileAsync(file);
            File.Delete(file);
        }

        [SummaryFromEnum(SummaryEnum.NuitStaffEdit)]
        [Command("edit", RunMode = RunMode.Async)]
        public async Task EditNuitAsync(ulong nuitId)
        {
            if (await _nuitManagerService.GetNuitAsync(nuitId) is NuitModel nuit)
            {
                var embed = _embedService.CreateChoiceEmbed<NuitProperties>();
                await ReplyAsync(embed: embed);
                var response = await NextMessageAsync(timeout: TimeSpan.FromMinutes(2));
                if (int.TryParse(response.Content, out int index) && Enum.IsDefined(typeof(NuitProperties), index))
                {
                    var property = (NuitProperties)index;
                    await ReplyAsync("Entrer la nouvelle valeur:");
                    response = await NextMessageAsync(timeout: TimeSpan.FromMinutes(2));
                    var prop = typeof(NuitModel).GetProperty(property.ToString());

                    if (prop != null)
                    {
                        var type = prop.PropertyType;
                        var converter = TypeDescriptor.GetConverter(type);
                        try
                        {
                            var convertedValue = converter.ConvertFromString(response.Content);
                            prop.SetValue(nuit, convertedValue);
                            await _nuitManagerService.ReplaceAsync(nuit);
                            await response.AddReactionAsync(new Emoji("👍"));
                        }
                        catch
                        {
                            await response.AddReactionAsync(new Emoji("👎"));
                        }
                    }
                }
            }
        }

        [SummaryFromEnum(SummaryEnum.NuitStaffCreate)]
        [Command("create")]
        public async Task CreateNuitAsync(DateTime start, DateTime end, DateTime playTime = default)
        {
            var result = await _nuitManagerService.CreateNuitAsync(start, end, Context.User.Id, playTime);
            if (!result.Success)
            {
                await ReplyAsync(result.Message);
            }
            else
            {
                await Context.Message.AddReactionAsync(new Emoji("👍"));
            }
        }

        [SummaryFromEnum(SummaryEnum.NuitStaffCreate)]
        [Command("create")]
        public async Task CreateNuitAsync(DateTime playTime = default)
        {
            var result = await _nuitManagerService.CreateNuitAsync(Context.User.Id, playTime);
            if (!result.Success)
            {
                await ReplyAsync(result.Message);
            }
            else
            {
                await Context.Message.AddReactionAsync(new Emoji("👍"));
            }
        }

        [SummaryFromEnum(SummaryEnum.NuitStaffStart)]
        [Command("start")]
        public async Task StartNuitAsync(ulong nuitId)
        {
            var result = await _nuitManagerService.StartNuitAsync(nuitId);
            if (!result.Success)
            {
                await ReplyAsync(result.Message);
            }
            else
            {
                await Context.Message.AddReactionAsync(new Emoji("👍"));
            }
        }

        [SummaryFromEnum(SummaryEnum.NuitStaffStop)]
        [Command("stop")]
        public async Task StopNuitAsync(ulong animeId)
        {
            await StopAsync(animeId);
        }

        [SummaryFromEnum(SummaryEnum.NuitStaffStop)]
        [Command("stop")]
        public async Task StopNuitAsync(ulong animeId, [Remainder]DateTime playTime)
        {
            await StopAsync(animeId, playTime);
        }

        private async Task StopAsync(ulong animeId, DateTime playTime = default)
        {
            var result = playTime == default ? await _nuitManagerService.StopNuitAsync(animeId) : await _nuitManagerService.StopNuitAsync(animeId, playTime);
            if (result.Success)
            {
                var channel = Context.Guild.GetTextChannel(_config.VoteChannelId);
                var messages = await channel.GetMessagesAsync(100000).FlattenAsync();

                await channel.DeleteMessagesAsync(messages);

                await ReplyAsync(_msg.GetNuitStopped(animeId));
                await Context.Message.AddReactionAsync(new Emoji("👍"));
            }
            else
            {
                await ReplyAsync(result.Message);
            }
        }

        [SummaryFromEnum(SummaryEnum.NuitStaffStatus)]
        [Command("status")]
        public async Task NuitStatusAsync()
        {
            var runningNuit = await _nuitManagerService.GetRunningNuitAsync();
            var runningNuitString = runningNuit != null ? $"{Format.Bold("Id:")} {runningNuit.NuitId}" : "N/A";

            var lastNuit = await _nuitManagerService.GetLastCreatedNuitAsync();
            var lastNuitString = lastNuit != null ? $"{Format.Bold("Id:")} {lastNuit.NuitId}" : "N/A";

            var embed = new EmbedBuilder()
                                .WithTitle("Status")
                                .WithColor(new Color(0xE3B6EB))
                                .WithThumbnailUrl("https://thumbs.gfycat.com/ColorlessSpryBigmouthbass-small.gif")
                                .WithFields(new EmbedFieldBuilder()
                                                   .WithName("Nuit en cours:")
                                                   .WithValue(runningNuitString)
                                                   .WithIsInline(true),
                                            new EmbedFieldBuilder()
                                                   .WithName("Dernière nuit créée:")
                                                   .WithValue(lastNuitString)
                                                   .WithIsInline(true));

            if(runningNuit != null)
            {
                var suggestions = await _suggestionDb.GetAllSuggestionsAsync(runningNuit.NuitId);
                var votes = await _voteDb.GetVotesAsync(runningNuit.NuitId);

                embed.AddField("Nombre de Suggestions: ", suggestions.Count, true);
                embed.AddField("Nombre de Votes: ", votes.Count, true);
            }

            await ReplyAsync(embed: embed.Build());
        }
    }
}
