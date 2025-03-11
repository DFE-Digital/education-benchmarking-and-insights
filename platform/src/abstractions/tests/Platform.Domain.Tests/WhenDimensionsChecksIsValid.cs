using Xunit;

namespace Platform.Domain.Tests;

public class WhenDimensionsChecksIsValid
{
    [Theory]
    [InlineData("HeadcountPerFte", true)]
    [InlineData("Total", true)]
    [InlineData("PercentWorkforce", true)]
    [InlineData("PupilsPerStaffRole", true)]
    [InlineData("invalid", false)]
    public void ShouldValidateCensusDimensions(string dimension, bool expected)
    {
        var actual = Dimensions.Census.IsValid(dimension);
        Assert.Equal(expected, actual);
    }

    [Theory]
    [InlineData("Actuals", true)]
    [InlineData("PerUnit", true)]
    [InlineData("PercentExpenditure", true)]
    [InlineData("PercentIncome", true)]
    [InlineData("invalid", false)]
    public void ShouldValidateFinanceDimensions(string dimension, bool expected)
    {
        var actual = Dimensions.Finance.IsValid(dimension);
        Assert.Equal(expected, actual);
    }

    [Theory]
    [InlineData("Per1000", true)]
    [InlineData("invalid", false)]
    public void ShouldValidateHighNeedsDimensions(string dimension, bool expected)
    {
        var actual = Dimensions.HighNeeds.IsValid(dimension);
        Assert.Equal(expected, actual);
    }
}