using HtmlAgilityPack;
using MalParser.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace MalParser.Services.Classes
{
    internal class DownloaderService : IDownloaderService
    {
        public async Task<HtmlDocument> GetHtmlDocAsync(string url)
        {
            using var client = new HttpClient();

            var text = await client.GetStringAsync(url).ConfigureAwait(false);

            var doc = new HtmlDocument();
            doc.LoadHtml(text);

            return doc;
        }
    }
}
