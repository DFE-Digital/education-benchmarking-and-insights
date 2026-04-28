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
    [InlineData("Actuals", true)]
    [InlineData("PerHead", true)]
    [InlineData("invalid", false)]
    public void ShouldValidateHighNeedsPopulationDimensions(string dimension, bool expected)
    {
        var actual = Dimensions.HighNeeds.IsValidPopulation(dimension);
        Assert.Equal(expected, actual);
    }

    [Theory]
    [InlineData("PerPupil", true)]
    [InlineData("PerEhcp", true)]
    [InlineData("PerSenSupport", true)]
    [InlineData("PerTotalSupport", true)]
    [InlineData("invalid", false)]
    public void ShouldValidateHighNeedsSupportDimensions(string dimension, bool expected)
    {
        var actual = Dimensions.HighNeeds.IsValidSupport(dimension);
        Assert.Equal(expected, actual);
    }

    [Theory]
    [InlineData("Actuals", true)]
    [InlineData("Per1000", true)]
    [InlineData("invalid", false)]
    public void ShouldValidateEducationHealthCarePlansDimensions(string dimension, bool expected)
    {
        var actual = Dimensions.EducationHealthCarePlans.IsValid(dimension);
        Assert.Equal(expected, actual);
    }
}