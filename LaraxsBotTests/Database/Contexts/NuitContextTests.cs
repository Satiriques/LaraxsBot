using LaraxsBot.Database.Contexts;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MoreLinq;
using NCrunch.Framework;
using System;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace LaraxsBot.Database.Contexts.Tests
{
    [ExclusivelyUses]
    [TestClass]
    [SuppressMessage("Design", "RCS1090:Call 'ConfigureAwait(false)'.", Justification = "<Pending>")]
    public class NuitContextTests
    {
        private readonly NuitContext _context = new NuitContext();

        [TestCleanup]
        public void TestCleanup()
        {
            _context.Database.EnsureDeleted();
            Directory.Delete(Path.Combine(AppContext.BaseDirectory, "data"));
        }

        [TestMethod]
        public void NuitContextTest()
        {
            var context = new NuitContext();
            context.Database.EnsureDeleted();
        }

        [TestMethod]
        public async Task CreateNuitAsyncTest()
        {
            await _context.CreateNuitAsync(DateTime.Now, DateTime.Now + TimeSpan.FromDays(7), 0);
            await _context.CreateNuitAsync(DateTime.Now, DateTime.Now + TimeSpan.FromDays(7), 0);

            var nuits = await _context.GetAllNuitsAsync();
            Assert.AreEqual(2, nuits.Count);
            Assert.AreEqual(2, nuits.DistinctBy(x => x.NuitId).Count());
        }

        [TestMethod]
        public async Task GetAllNuitsAsyncTest()
        {
            await _context.CreateNuitAsync(DateTime.Now, DateTime.Now + TimeSpan.FromDays(7), 0);

            var nuits = await _context.GetAllNuitsAsync();
            Assert.AreEqual(1, nuits.Count);

            await _context.CreateNuitAsync(DateTime.Now, DateTime.Now + TimeSpan.FromDays(7), 0);

            nuits = await _context.GetAllNuitsAsync();
            Assert.AreEqual(2, nuits.Count);

            await _context.CreateNuitAsync(DateTime.Now, DateTime.Now + TimeSpan.FromDays(7), 0);

            nuits = await _context.GetAllNuitsAsync();
            Assert.AreEqual(3, nuits.Count);
        }

        [TestMethod]
        public async Task GetStillRunningNuitAsyncTest()
        {
            await _context.CreateNuitAsync(DateTime.Now, DateTime.Now + TimeSpan.FromDays(7), 0);

            await _context.StartNuitAsync(1);

            var nuit = await _context.GetStillRunningNuitAsync();
            Assert.IsNotNull(nuit);
            Assert.IsTrue(nuit.IsRunning);
        }

        [TestMethod]
        public async Task StopNuitAsyncTest()
        {
            await _context.CreateNuitAsync(DateTime.Now, DateTime.Now + TimeSpan.FromDays(7), 0);

            await _context.StartNuitAsync(1);

            var nuits = await _context.GetAllNuitsAsync();
            Assert.AreEqual(1, nuits.Count);

            var nuit = nuits[0];
            Assert.IsTrue(nuit.IsRunning);

            await _context.StopNuitAsync(0);

            nuits = await _context.GetAllNuitsAsync();
            Assert.AreEqual(1, nuits.Count);

            nuit = nuits[0];
            Assert.IsFalse(nuit.IsRunning);
        }

        [TestMethod]
        public async Task StartNuitAsyncTest()
        {
            await _context.CreateNuitAsync(DateTime.Now, DateTime.Now + TimeSpan.FromDays(7), 0);

            await _context.StartNuitAsync(1);

            var nuits = await _context.GetAllNuitsAsync();
            Assert.AreEqual(1, nuits.Count);

            var nuit = nuits[0];
            Assert.IsTrue(nuit.IsRunning);
        }
    }
}