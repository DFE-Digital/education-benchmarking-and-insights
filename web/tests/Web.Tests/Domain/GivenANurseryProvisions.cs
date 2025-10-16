using Web.App.Domain;
using Xunit;

namespace Web.Tests.Domain;

public class GivenANurseryProvisions
{
    [Fact]
    public void ShouldReturnExpectedAllPhaseTypeFilters()
    {
        NurseryProvisions.NurseryProvisionFilter[] expected =
        [
            NurseryProvisions.NurseryProvisionFilter.HasNurseryClasses,
            NurseryProvisions.NurseryProvisionFilter.HasNoNurseryClasses,
            NurseryProvisions.NurseryProvisionFilter.NotApplicable,
            NurseryProvisions.NurseryProvisionFilter.NotRecorded
        ];

        var actual = NurseryProvisions.AllFilters;

        Assert.Equal(expected, actual);
    }

    [Theory]
    [InlineData(NurseryProvisions.NurseryProvisionFilter.HasNurseryClasses, "Has nursery classes")]
    [InlineData(NurseryProvisions.NurseryProvisionFilter.HasNoNurseryClasses, "Has no nursery classes")]
    [InlineData(NurseryProvisions.NurseryProvisionFilter.NotApplicable, "Not applicable")]
    [InlineData(NurseryProvisions.NurseryProvisionFilter.NotRecorded, "Not recorded")]
    public void ShouldReturnExpectedFilterDescriptions(NurseryProvisions.NurseryProvisionFilter filter, string expected)
    {
        var actual = filter.GetFilterDescription();

        Assert.Equal(expected, actual);
    }

    [Fact]
    public void ShouldThrowExceptionForOutOfRangeFilterDescription()
    {
        var exception = Assert.Throws<ArgumentException>(() => ((NurseryProvisions.NurseryProvisionFilter)999).GetFilterDescription());

        Assert.NotNull(exception);
    }
}