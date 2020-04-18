using Discord;
using LaraxsBot.Services.Interfaces;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace LaraxsBot.Services.Classes
{
    public sealed class Config : IConfig
    {
        private Config() { }
        public static Config EnsureExists(string path)
        {
            if (File.Exists(path))
            {
                var text = File.ReadAllText(path);
                return JsonConvert.DeserializeObject<Config>(text);
            }

            return new Config();
        }

        public ulong VoteChannelId { get; set; }

        public ITextChannel VoteChannel { get; set; }
    }
}
