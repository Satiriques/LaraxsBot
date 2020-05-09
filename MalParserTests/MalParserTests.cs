using MalParser.Models;
using Newtonsoft.Json;
using NUnit.Framework;
using System.IO;
using System.Threading.Tasks;

namespace MalParser.Tests
{
    [TestFixture]
    public class MalParserTests
    {
        private MalApi _malParser = new MalApi();

        [TestCase(1u)]
        public async Task GetAnimeAsyncTest(ulong animeId)
        {
            var anime = await _malParser.GetAnimeAsync(animeId);
            Assert.IsNotNull(anime);
            var animeReference = JsonConvert.DeserializeObject<Anime>(File.ReadAllText($"References/GetAnimeAsyncTest_{animeId}.json"));

            Assert.AreEqual(animeReference, anime);
//            Assert.AreEqual("Cowboy Bebop", anime.Title);
//            Assert.AreEqual(@"In the year 2071, humanity has colonized several of the planets and moons of the solar system leaving the now uninhabitable surface of planet Earth behind. The Inter Solar System Police attempts to keep peace in the galaxy, aided in part by outlaw bounty hunters, referred to as &quot;Cowboys.&quot; The ragtag team aboard the spaceship Bebop are two such individuals.
 
//Mellow and carefree Spike Spiegel is balanced by his boisterous, pragmatic partner Jet Black as the pair makes a living chasing bounties and collecting rewards. Thrown off course by the addition of new members that they meet in their travels&mdash;Ein, a genetically engineered, highly intelligent Welsh Corgi; femme fatale Faye Valentine, an enigmatic trickster with memory loss; and the strange computer whiz kid Edward Wong&mdash;the crew embarks on thrilling adventures that unravel each member&#039;s dark and mysterious past little by little.

//Well-balanced with high density action and light-hearted comedy, Cowboy Bebop is a space Western classic and an homage to the smooth and improvised music it is named after.

//[Written by MAL Rewrite]", anime.Synopsis);

//            /*
//            Assert.AreEqual(@"When Cowboy Bebop first aired in spring of 1998 on TV Tokyo, only episodes 2, 3, 7-15, and 18 were broadcast, it was concluded with a recap special known as Yose Atsume Blues. This was due to anime censorship having increased following the big controversies over Evangelion, as a result most of the series was pulled from the air due to violent content. Satellite channel WOWOW picked up the series in the fall of that year and aired it in its entirety uncensored. Cowboy Bebop was not a ratings hit in Japan, but sold over 19,000 DVD units in the initial release run, and 81,000 overall. Protagonist Spike Spiegel won Best Male Character, and Megumi Hayashibara won Best Voice Actor for her role as Faye Valentine in the 1999 and 2000 Anime Grand Prix, respectively.

//Cowboy Bebop's biggest influence has been in the United States, where it premiered on Adult Swim in 2001 with many reruns since. The show's heavy Western influence struck a chord with American viewers, where it became a ""gateway drug"" to anime aimed at adult audiences.", anime.Background);

//            */
//            Assert.AreEqual(anime.Type, "TV");
//            Assert.AreEqual("Action, Adventure, Comedy, Drama, Sci-Fi, Space", string.Join(", ", anime.Genres));
//            Assert.AreEqual("Sunrise", string.Join(", ", anime.Studios));
            
        }

        [Test]
        public async Task GetAnimeAsyncFailTest()
        {
            var anime = await _malParser.GetAnimeAsync(ulong.MaxValue);

            Assert.IsNull(anime);
        }
    }
}