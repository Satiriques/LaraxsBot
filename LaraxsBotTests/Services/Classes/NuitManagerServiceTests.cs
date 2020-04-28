using LaraxsBot.Database.Contexts;
using LaraxsBot.Database.Interfaces;
using LaraxsBot.Database.Managers;
using LaraxsBot.Services.DatabaseFacade;
using LaraxsBot.Services.Interfaces;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NCrunch.Framework;
using System;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Threading.Tasks;

namespace LaraxsBot.Services.Classes.Tests
{
    [ExclusivelyUses]
    [TestClass]
    [SuppressMessage("Design", "RCS1090:Call 'ConfigureAwait(false)'.", Justification = "<Pending>")]
    public class NuitManagerServiceTests
    {
        private INuitContextManager _nuitContext;
        private IVoteContext _voteContext;
        private INuitService _service;

        [TestInitialize]
        public void TestInitialize()
        {
            _nuitContext = new NuitContextManager();
            _voteContext = new VoteContextManager();
            _service = new NuitService(_nuitContext, new FrenchMessageService());
        }

        [TestCleanup]
        public void TestCleanup()
        {
            _nuitContext.EnsureDeleted();
            Directory.Delete(Path.Combine(AppContext.BaseDirectory, "data"), true);
        }

        [TestMethod]
        public async Task CreateNuitAsyncTest()
        {
            await _service.CreateNuitAsync(DateTime.Now, DateTime.Now + TimeSpan.FromDays(7), 0);
            await _service.CreateNuitAsync(DateTime.Now, DateTime.Now + TimeSpan.FromDays(7), 0);

            var count = await _service.GetNumberOfNuitAsync();
            Assert.AreEqual(2, count);

            await _service.StopNuitAsync(0);
            await _service.CreateNuitAsync(DateTime.Now, DateTime.Now + TimeSpan.FromDays(7), 0);

            count = await _service.GetNumberOfNuitAsync();

            Assert.AreEqual(3, count);
        }
    }
}