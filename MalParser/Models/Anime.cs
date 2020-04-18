using MalParser.Interfaces;

namespace MalParser.Models
{
    internal class Anime : IAnime
    {
        public string Background { get; set; }

        public string[] Genres { get; set; }

        public string[] Licensors { get; set; }

        public int NumberOfEpisodes { get; set; }

        public int PopularityPosition { get; set; }

        public string[] Producers { get; set; }

        public int RankedPosition { get; set; }

        public float Score { get; set; }

        public string Source { get; set; }

        public string[] Studios { get; set; }

        public string Synopsis { get; set; }

        public string Title { get; set; }

        public string Type { get; set; }
    }
}
