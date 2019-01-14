using System.Threading.Tasks;
using Autofac.Extras.Moq;
using LogViewer.Entities;
using LogViewer.Repositories.LogRepository;
using LogViewer.Repositories.LogRepository.Interfaces;
using LogViewer.Requests;
using NUnit.Framework;

namespace Tests
{
    [TestFixture]
    public class RepositoryTests
    {
        private AutoMock autoMockContext;
        private ILogReadRepository logReadRepository;

        [SetUp]
        public void Setup()
        {
            autoMockContext = AutoMock.GetLoose();
            logReadRepository = autoMockContext.Create<LogReadRepository>();
        }

        [TearDown]
        public void Close()
        {
            autoMockContext.Dispose();
        }

        [Test]
        public async Task GetTopOneLog()
        {
            var filter = new LogFilterRequest
            {
                Top = 1
            };

            var expectedResult = new LogModel()
            {

            };

            var result = await logReadRepository.GetAllLogs(filter);
            Assert.AreEqual(expectedResult, result);
        }
    }
}
