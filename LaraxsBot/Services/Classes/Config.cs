using LaraxsBot.Services.Interfaces;
using Newtonsoft.Json;
using System;
using System.IO;

namespace LaraxsBot.Services.Classes
{
    public sealed class Config : IConfig
    {
        private string? _path;

        private Config(string path)
        {
            _path = path;
        }

        [JsonConstructor]
        private Config() { }

        public static Config EnsureExists(string path)
        {
            if (File.Exists(path))
            {
                var text = File.ReadAllText(path);
                var config = JsonConvert.DeserializeObject<Config>(text);
                config.SetPath(path);

                return config;
            }
            return new Config(path);
        }

        internal void SetPath(string path)
        {
            _path = path;
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

        public void SetPrefix(string prefix)
        {
            CommandPrefix = prefix;
            Save();
        }

        public void SetRole(ulong id)
        {
            StaffRoleId = id;
            Save();
        }

        public void SetDefaultPlayTime(DayOfWeek dayOfweek, TimeSpan timeOfDay)
        {
            DefaultPlayDay = dayOfweek;
            DefaultPlayTime = timeOfDay;
            Save();
        }

        public ulong VoteChannelId { get; set; }

        public ulong StaffRoleId { get; set; }

        public string CommandPrefix { get; set; } = "!";
        public DayOfWeek DefaultPlayDay { get; set; }
        public TimeSpan DefaultPlayTime { get; set; }
    }
}