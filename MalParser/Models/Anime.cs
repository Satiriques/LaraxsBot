using MalParser.Interfaces;

namespace MalParser.Models
{
    internal class Anime : IAnime
    {
        public string Background { get; set; } = string.Empty;

        public string[] Genres { get; set; } = new string[] { };

        public string[] Licensors { get; set; } = new string[] { };

        public int NumberOfEpisodes { get; set; }

        public int PopularityPosition { get; set; }

        public string[] Producers { get; set; } = new string[] { };

        public int RankedPosition { get; set; }

        public float Score { get; set; }

        public string Source { get; set; } = string.Empty;

        public string[] Studios { get; set; } = new string[] { };

        public string Synopsis { get; set; } = string.Empty;

        public string Title { get; set; } = string.Empty;

        public string Type { get; set; } = string.Empty;
        public string ImageUrl { get; set; } = string.Empty;
    }
}
