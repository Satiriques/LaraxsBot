using LaraxsBot.Database.Testing.Contexts;
using LaraxsBot.Services.Interfaces;
using Microsoft.VisualStudio.TestTools.UnitTesting;
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