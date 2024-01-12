using EducationBenchmarking.Platform.Domain;
using Xunit;

namespace EducationBenchmarking.Platform.Shared.Tests
{
    public class WhenUnknownProximitySortIsCalled
    {
        [Fact]
        public void SortingWithEmptyKindThrows()
        {
            Assert.Throws<NotImplementedException>(
                () => new UnknownProximitySort()
                    .Sort(Array.Empty<SchoolTrustFinance>()));
        }
    }
}
