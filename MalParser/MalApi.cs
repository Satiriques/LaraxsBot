using MalParser.Interfaces;
using MalParser.Services.Classes;
using MalParser.Services.Interfaces;
using System;
using System.Threading.Tasks;

namespace MalParser
{
    /// <summary>
    /// Stateless mal parser
    /// </summary>
    public class MalApi
    {
        private readonly IAnimeParser _animeParser = new AnimeParser();

        public async Task<IAnime?> GetAnimeAsync(ulong id) => await _animeParser.GetAnimeAsync(id).ConfigureAwait(false);
    }
}
