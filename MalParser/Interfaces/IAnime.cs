namespace MalParser.Interfaces
{
    public interface IAnime
    {
        public string Background { get; }
        public string[] Genres { get; }
        public string[] Licensors { get; }
        public int NumberOfEpisodes { get; }
        public int PopularityPosition { get; }
        public string[] Producers { get; }
        public int RankedPosition { get; }
        public float Score { get; }
        public string Source { get; }
        public string[] Studios { get; }
        public string Synopsis { get; }
        public string Title { get; }
        public string Type { get; }
        public string ImageUrl { get; }
    }
}
