using Web.App.Domain;
using Xunit;

namespace Web.Tests.Domain;

public class GivenASixthFormProvisions
{
    [Fact]
    public void ShouldReturnExpectedAllPhaseTypeFilters()
    {
        SixthFormProvisions.SixthFormProvisionFilter[] expected =
        [
            SixthFormProvisions.SixthFormProvisionFilter.HasSixthFormClasses,
            SixthFormProvisions.SixthFormProvisionFilter.HasNoSixthFormClasses,
            SixthFormProvisions.SixthFormProvisionFilter.NotApplicable,
            SixthFormProvisions.SixthFormProvisionFilter.NotRecorded
        ];

        var actual = SixthFormProvisions.AllFilters;

        Assert.Equal(expected, actual);
    }

    [Theory]
    [InlineData(SixthFormProvisions.SixthFormProvisionFilter.HasSixthFormClasses, "Has a sixth form")]
    [InlineData(SixthFormProvisions.SixthFormProvisionFilter.HasNoSixthFormClasses, "Does not have a sixth form")]
    [InlineData(SixthFormProvisions.SixthFormProvisionFilter.NotApplicable, "Not applicable")]
    [InlineData(SixthFormProvisions.SixthFormProvisionFilter.NotRecorded, "Not recorded")]
    public void ShouldReturnExpectedFilterDescriptions(SixthFormProvisions.SixthFormProvisionFilter filter, string expected)
    {
        var actual = filter.GetFilterDescription();

        Assert.Equal(expected, actual);
    }

    [Fact]
    public void ShouldThrowExceptionForOutOfRangeFilterDescription()
    {
        var exception = Assert.Throws<ArgumentOutOfRangeException>(() => ((SixthFormProvisions.SixthFormProvisionFilter)999).GetFilterDescription());

        Assert.NotNull(exception);
    }

    [Theory]
    [InlineData(SixthFormProvisions.SixthFormProvisionFilter.HasSixthFormClasses, "Has a sixth form")]
    [InlineData(SixthFormProvisions.SixthFormProvisionFilter.HasNoSixthFormClasses, "Does not have a sixth form")]
    [InlineData(SixthFormProvisions.SixthFormProvisionFilter.NotApplicable, "Not applicable")]
    [InlineData(SixthFormProvisions.SixthFormProvisionFilter.NotRecorded, "Not recorded")]
    public void ShouldReturnExpectedQueryParam(SixthFormProvisions.SixthFormProvisionFilter filter, string expected)
    {
        var actual = filter.GetQueryParam();

        Assert.Equal(expected, actual);
    }

    [Fact]
    public void ShouldThrowExceptionForOutOfRangeQueryParam()
    {
        var exception = Assert.Throws<ArgumentOutOfRangeException>(() => ((SixthFormProvisions.SixthFormProvisionFilter)999).GetQueryParam());

        Assert.NotNull(exception);
    }
}