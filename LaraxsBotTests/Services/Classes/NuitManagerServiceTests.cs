using LaraxsBot.Database.Contexts;
using LaraxsBot.Services.Interfaces;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;

namespace LaraxsBot.Services.Classes.Tests
{
    [TestClass]
    [SuppressMessage("Design", "RCS1090:Call 'ConfigureAwait(false)'.", Justification = "<Pending>")]
    public class NuitManagerServiceTests
    {
        private readonly INuitManagerService _service = new NuitManagerService(new NuitContext());

        [TestMethod]
        public async Task CreateNuitAsyncTest()
        {
            await _service.CreateNuitAsync(DateTime.Now, DateTime.Now + TimeSpan.FromDays(7), 0);
            await _service.CreateNuitAsync(DateTime.Now, DateTime.Now + TimeSpan.FromDays(7), 0);

            var count = await _service.GetNumberOfNuitAsync();
            Assert.AreEqual(1, count);

            await _service.StopNuitAsync();
            await _service.CreateNuitAsync(DateTime.Now, DateTime.Now + TimeSpan.FromDays(7), 0);

            count = await _service.GetNumberOfNuitAsync();

            Assert.AreEqual(2, count);
        }
    }
}