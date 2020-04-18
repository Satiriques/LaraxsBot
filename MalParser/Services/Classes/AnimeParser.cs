using MalParser.Interfaces;
using MalParser.Models;
using MalParser.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MalParser.Services.Classes
{
    internal class AnimeParser : IAnimeParser
    {
        private IDownloaderService _downloaderService = new DownloaderService();
        private const string _animeUrlPrefix = "https://myanimelist.net/anime/";

        public async Task<IAnime> GetAnimeAsync(ulong id)
        {
            var doc = await _downloaderService.GetHtmlDocAsync(_animeUrlPrefix + id.ToString()).ConfigureAwait(false);

            return new Anime();
        }
    }
}
