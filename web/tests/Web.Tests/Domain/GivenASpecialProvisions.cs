using Web.App.Domain;
using Xunit;

namespace Web.Tests.Domain;

public class GivenASpecialProvisions
{
    [Fact]
    public void ShouldReturnExpectedAllPhaseTypeFilters()
    {
        SpecialProvisions.SpecialProvisionFilter[] expected =
        [
            SpecialProvisions.SpecialProvisionFilter.HasSpecialClasses,
            SpecialProvisions.SpecialProvisionFilter.HasNoSpecialClasses,
            SpecialProvisions.SpecialProvisionFilter.NotApplicable,
            SpecialProvisions.SpecialProvisionFilter.NotRecorded
        ];

        var actual = SpecialProvisions.AllFilters;

        Assert.Equal(expected, actual);
    }

    [Theory]
    [InlineData(SpecialProvisions.SpecialProvisionFilter.HasSpecialClasses, "Has special classes")]
    [InlineData(SpecialProvisions.SpecialProvisionFilter.HasNoSpecialClasses, "Has no special classes")]
    [InlineData(SpecialProvisions.SpecialProvisionFilter.NotApplicable, "Not applicable")]
    [InlineData(SpecialProvisions.SpecialProvisionFilter.NotRecorded, "Not recorded")]
    public void ShouldReturnExpectedFilterDescriptions(SpecialProvisions.SpecialProvisionFilter filter, string expected)
    {
        var actual = filter.GetFilterDescription();

        Assert.Equal(expected, actual);
    }

    [Fact]
    public void ShouldThrowExceptionForOutOfRangeFilterDescription()
    {
        var exception = Assert.Throws<ArgumentOutOfRangeException>(() => ((SpecialProvisions.SpecialProvisionFilter)999).GetFilterDescription());

        Assert.NotNull(exception);
    }

    [Theory]
    [InlineData(SpecialProvisions.SpecialProvisionFilter.HasSpecialClasses, "Has Special Classes")]
    [InlineData(SpecialProvisions.SpecialProvisionFilter.HasNoSpecialClasses, "No Special Classes")]
    [InlineData(SpecialProvisions.SpecialProvisionFilter.NotApplicable, "Not applicable")]
    [InlineData(SpecialProvisions.SpecialProvisionFilter.NotRecorded, "Not recorded")]
    public void ShouldReturnExpectedQueryParam(SpecialProvisions.SpecialProvisionFilter filter, string expected)
    {
        var actual = filter.GetQueryParam();

        Assert.Equal(expected, actual);
    }

    [Fact]
    public void ShouldThrowExceptionForOutOfRangeQueryParam()
    {
        var exception = Assert.Throws<ArgumentOutOfRangeException>(() => ((SpecialProvisions.SpecialProvisionFilter)999).GetQueryParam());

        Assert.NotNull(exception);
    }
}