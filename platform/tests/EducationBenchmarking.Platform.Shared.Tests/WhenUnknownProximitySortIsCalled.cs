using Xunit;

namespace EducationBenchmarking.Platform.Shared.Tests
{
    public class WhenUnknownProximitySortIsCalled
    {
        [Fact]
        public void SortingWithEmptyKindThrowsNotImplementedException()
        {
            Assert.Throws<NotImplementedException>(
                () => new UnknownProximitySort()
                    .Sort(Array.Empty<SchoolTrustFinance>()));
        }
    }
}
