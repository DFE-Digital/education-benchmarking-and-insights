using EducationBenchmarking.Platform.Domain;
using FluentAssertions;
using Xunit;

namespace EducationBenchmarking.Platform.Tests.Domain;

public class WhenUnknownProximitySortIsCalled
{
    [Fact]
    public void SortingWithEmptyKindThrows()
    {
        var sortType = new UnknownProximitySort();

        sortType.Kind.Should().Be("");
            
        Assert.Throws<NotImplementedException>(
            () => new UnknownProximitySort()
                .Sort(Array.Empty<SchoolTrustFinance>()));
    }
}