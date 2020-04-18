using HtmlAgilityPack;
using System.Threading.Tasks;

namespace MalParser.Services.Interfaces
{
    internal interface IDownloaderService
    {
        public Task<HtmlDocument> GetHtmlDocAsync(string url);
    }
}