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
        public async Task<HtmlDocument?> GetHtmlDocAsync(string url)
        {
            using var client = new HttpClient();
            string text = string.Empty;

            try
            {
                text = await client.GetStringAsync(url).ConfigureAwait(false);
            }
            catch
            {
                return null;
            }

            var doc = new HtmlDocument();
            doc.LoadHtml(text);

            return doc;
        }
    }
}
