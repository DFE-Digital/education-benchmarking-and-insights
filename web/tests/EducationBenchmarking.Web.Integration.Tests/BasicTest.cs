using Xunit;

namespace EducationBenchmarking.Web.Integration.Tests
{
    public class BasicTest : IClassFixture<CustomWebApplicationFactory<Program>>
    {

        [Fact]
        public void Test1()
        {
            Assert.True(true);
        }
    }
}