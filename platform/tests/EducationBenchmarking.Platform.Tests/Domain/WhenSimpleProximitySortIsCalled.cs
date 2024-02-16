using EducationBenchmarking.Platform.Domain;
using FluentAssertions;
using Xunit;

namespace EducationBenchmarking.Platform.Tests.Domain;

public class WhenSimpleProximitySortIsCalled
{
    [Theory]
    [InlineData(80, 110, 95, 100)]
    [InlineData(190, 120, 140, 150)]
    [InlineData(350, 325, 280, 300)]
    public void OrderIsCorrect(decimal value1, decimal value2, decimal value3, int baseline)
    {
        var proximitySort = new SimpleProximitySort
        {
            SortBy = "OtherIncome",
            Baseline = baseline
        };

        var schools = new List<SchoolTrustFinance>
        {
            new() { OtherIncome = value1 },
            new() { OtherIncome = value2 },
            new() { OtherIncome = value3 }
        };

        var result = proximitySort.Sort(schools).ToArray();

        proximitySort.Kind.Should().Be(ProximitySortKinds.Simple);

        Assert.Equal(value3, result.ElementAt(0).OtherIncome);
        Assert.Equal(value2, result.ElementAt(1).OtherIncome);
        Assert.Equal(value1, result.ElementAt(2).OtherIncome);
    }
}