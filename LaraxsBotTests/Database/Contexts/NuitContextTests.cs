using Microsoft.VisualStudio.TestTools.UnitTesting;
using LaraxsBot.Database.Contexts;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using NCrunch.Framework;

namespace LaraxsBot.Database.Contexts.Tests
{
    [ExclusivelyUses]
    [TestClass()]
    public class NuitContextTests
    {
        private NuitContext _context = new NuitContext();

        [TestCleanup]
        public void TestCleanup()
        {
            _context.Database.EnsureDeleted();
        }

        [TestMethod()]
        public async Task NuitContextTest()
        {
            await _context.CreateNuitAsync(DateTime.Now, DateTime.Now + TimeSpan.FromDays(7), 0);
            await _context.CreateNuitAsync(DateTime.Now, DateTime.Now + TimeSpan.FromDays(7), 0);
        }

        [TestMethod()]
        public async Task CreateNuitAsyncTest()
        {
            await _context.CreateNuitAsync(DateTime.Now, DateTime.Now + TimeSpan.FromDays(7), 0);
        }

        [TestMethod()]
        public async Task GetAllNuitsAsyncTest()
        {
            await _context.CreateNuitAsync(DateTime.Now, DateTime.Now + TimeSpan.FromDays(7), 0);
        }

        [TestMethod()]
        public async Task GetStillRunningNuitAsyncTest()
        {
            await _context.CreateNuitAsync(DateTime.Now, DateTime.Now + TimeSpan.FromDays(7), 0);
        }

        [TestMethod()]
        public async Task StopRunningNuitAsyncTest()
        {
            await _context.CreateNuitAsync(DateTime.Now, DateTime.Now + TimeSpan.FromDays(7), 0);
        }
    }
}