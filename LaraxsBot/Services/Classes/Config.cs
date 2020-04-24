﻿using Discord;
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
        private readonly string? _path;
        private Config(string path) { _path = path; }

        [JsonConstructor]
        private Config() { }

        public static Config EnsureExists(string path)
        {
            if (File.Exists(path))
            {
                var text = File.ReadAllText(path);
                return JsonConvert.DeserializeObject<Config>(text);
            }
            return new Config(path);
        }

        public void Save()
        {
            File.WriteAllText(_path, JsonConvert.SerializeObject(this, Formatting.Indented));
        }

        public void SetVoteChannelId(ulong id)
        {
            VoteChannelId = id;
            Save();
        }

        public ulong VoteChannelId { get; set; }
    }
}
