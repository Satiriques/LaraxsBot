using Microsoft.VisualStudio.TestTools.UnitTesting;
using LaraxsBot.Services.Classes;
using System;
using System.Collections.Generic;
using System.Text;
using LaraxsBot.Services.Interfaces;
using LaraxsBot.Database.Testing.Contexts;
using System.Threading.Tasks;
using System.Threading;

namespace LaraxsBot.Services.Classes.Tests
{
    [TestClass]
    public class NuitManagerServiceTests
    {
        private readonly INuitManagerService _service = new NuitManagerService(new NuitContext());

        [TestMethod]
        public async Task CreateNuitAsyncTest()
        {
            await _service.CreateNuitAsync();
            await _service.CreateNuitAsync();

            var count = await _service.GetNumberOfNuitAsync();
            Assert.AreEqual(1, count);

            await _service.StopNuitAsync();
            await _service.CreateNuitAsync();

            count = await _service.GetNumberOfNuitAsync();

            Assert.AreEqual(2, count);
        }
    }
}