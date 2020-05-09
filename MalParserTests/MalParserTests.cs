using MalParser.Models;
using Newtonsoft.Json;
using NUnit.Framework;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;

namespace MalParser.Tests
{
    [TestFixture]
    public class MalParserTests
    {
        private readonly MalApi _malParser = new MalApi();

        [TestCase(1u)]
        [TestCase(35849u)]
        public async Task GetAnimeAsyncTest(ulong animeId)
        {
            var anime = await _malParser.GetAnimeAsync(animeId);
            Assert.IsNotNull(anime);
            var animeReference = JsonConvert.DeserializeObject<Anime>(File.ReadAllText($"References/GetAnimeAsyncTest_{animeId}.json"));

            Assert.AreEqual(animeReference, anime);
        }

        [Test]
        public async Task GetAnimeAsyncFailTest()
        {
            var anime = await _malParser.GetAnimeAsync(ulong.MaxValue);

            Assert.IsNull(anime);
        }
    }
}