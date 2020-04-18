using HtmlAgilityPack;
using MalParser.Interfaces;
using MalParser.Models;
using MalParser.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text;
using System.Threading.Tasks;

namespace MalParser.Services.Classes
{
    internal class AnimeParser : IAnimeParser
    {
        private IDownloaderService _downloaderService = new DownloaderService();
        private const string _animeUrlPrefix = "https://myanimelist.net/anime/";

        public async Task<IAnime?> GetAnimeAsync(ulong id)
        {
            var anime = new Anime();
            var doc = await _downloaderService.GetHtmlDocAsync(_animeUrlPrefix + id.ToString()).ConfigureAwait(false);

            if(doc == null)
            {
                return null;
            }

            var rootNode = doc.DocumentNode;

            // search all the elements span with attribute names itemprop with value name
            anime.Title = GetTitle(rootNode);
            anime.Synopsis = GetSynopsis(rootNode);
            anime.Type = GetType(rootNode);
            anime.NumberOfEpisodes = GetNumberOfEpisodes(rootNode);
            anime.Producers = GetProducers(rootNode);
            anime.Licensors = GetLicensors(rootNode);
            anime.Studios = GetStudios(rootNode);
            anime.Source = GetSource(rootNode);
            anime.Genres = GetGenres(rootNode);
            anime.ImageUrl = GetAnimeUrl(rootNode);

            return anime;
        }

        private string GetAnimeUrl(HtmlNode rootNode) 
            => rootNode.SelectSingleNode("//div/div/a/img[@itemprop='image']")?.Attributes["data-src"].Value ?? string.Empty;

        private string GetTitle(HtmlNode rootNode)
            => rootNode.SelectSingleNode("//span[@itemprop='name']")?.InnerText ?? string.Empty;

        private string GetSynopsis(HtmlNode rootNode)
            => rootNode.SelectSingleNode("//span[@itemprop='description']")?.InnerText ?? string.Empty;

        private string GetType(HtmlNode rootNode)
            => rootNode.SelectSingleNode("//span[@class='dark_text' and text() = 'Type:']")?.NextSibling?.NextSibling?.InnerText ?? string.Empty;

        private int GetNumberOfEpisodes(HtmlNode rootNode)
            => int.Parse(rootNode.SelectSingleNode("//span[@class='dark_text' and text() = 'Episodes:']")?.NextSibling?.InnerText ?? "0");

        private string[] GetProducers(HtmlNode rootNode)
            => rootNode.SelectSingleNode("//span[@class='dark_text' and text() = 'Producers:']")?.NextSibling?.NextSibling?.InnerText?.Split(",") ?? new string[] { };

        private string[] GetLicensors(HtmlNode rootNode)
            => rootNode.SelectSingleNode("//span[@class='dark_text' and text() = 'Licensors:']")?.NextSibling?.NextSibling?.InnerText?.Split(",") ?? new string[] { };

        private string[] GetStudios(HtmlNode rootNode)
            => rootNode.SelectSingleNode("//span[@class='dark_text' and text() = 'Studios:']")?.NextSibling?.NextSibling?.InnerText?.Split(",") ?? new string[] { };

        private string GetSource(HtmlNode rootNode)
            => rootNode.SelectSingleNode("//span[@class='dark_text' and text() = 'Source:']")?.NextSibling?.InnerText ?? string.Empty;

        private string[] GetGenres(HtmlNode rootNode)
            => rootNode.SelectSingleNode("//span[@class='dark_text' and text() = 'Genres:']")?.NextSibling?.NextSibling?.InnerText?.Split(",") ?? new string[] { };
    }
}
