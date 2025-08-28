using Web.App.Domain.Charts;
using Xunit;

namespace Web.Tests.Domain.Charts;

public class GivenDimensionsGetQueryParam
{
    [Theory]
    [InlineData(Dimensions.ResultAsOptions.Actuals, "Actuals")]
    [InlineData(Dimensions.ResultAsOptions.PercentExpenditure, "PercentExpenditure")]
    [InlineData(Dimensions.ResultAsOptions.PercentIncome, "PercentIncome")]
    [InlineData(Dimensions.ResultAsOptions.SpendPerPupil, "PerUnit")]
    public void WhenResultAsOptionsIs(Dimensions.ResultAsOptions option, string expected)
    {
        var actual = option.GetQueryParam();
        Assert.Equal(expected, actual);
    }

    [Fact]
    public void WhenResultAsOptionsIsOutOfRange()
    {
        var actual = Assert.Throws<ArgumentOutOfRangeException>(() => ((Dimensions.ResultAsOptions)999).GetQueryParam());
        Assert.NotNull(actual);
    }
}

public class GivenDimensionsGetValueType
{
    [Theory]
    [InlineData(Dimensions.ResultAsOptions.Actuals, "currency")]
    [InlineData(Dimensions.ResultAsOptions.PercentExpenditure, "percent")]
    [InlineData(Dimensions.ResultAsOptions.PercentIncome, "percent")]
    [InlineData(Dimensions.ResultAsOptions.SpendPerPupil, "currency")]
    public void WhenResultAsOptionsIs(Dimensions.ResultAsOptions option, string expected)
    {
        var actual = option.GetValueType();
        Assert.Equal(expected, actual);
    }

    [Fact]
    public void WhenResultAsOptionsIsOutOfRange()
    {
        var actual = Assert.Throws<ArgumentOutOfRangeException>(() => ((Dimensions.ResultAsOptions)999).GetValueType());
        Assert.NotNull(actual);
    }
}

public class GivenDimensionsGetXAxisLabel
{
    [Theory]
    [InlineData(Dimensions.ResultAsOptions.Actuals, "actuals")]
    [InlineData(Dimensions.ResultAsOptions.PercentExpenditure, "percentage of expenditure")]
    [InlineData(Dimensions.ResultAsOptions.PercentIncome, "percentage of income")]
    [InlineData(Dimensions.ResultAsOptions.SpendPerPupil, "£ per pupil")]
    public void WhenResultAsOptionsIs(Dimensions.ResultAsOptions option, string expected)
    {
        var actual = option.GetXAxisLabel();
        Assert.Equal(expected, actual);
    }

    [Fact]
    public void WhenResultAsOptionsIsOutOfRange()
    {
        var actual = Assert.Throws<ArgumentOutOfRangeException>(() => ((Dimensions.ResultAsOptions)999).GetXAxisLabel());
        Assert.NotNull(actual);
    }
}

public class GivenDimensionsGetFormattedValue
{
    [Theory]
    [InlineData(Dimensions.ResultAsOptions.Actuals, 123.45, "£123")]
    [InlineData(Dimensions.ResultAsOptions.PercentExpenditure, 123.45, "123.5%")]
    [InlineData(Dimensions.ResultAsOptions.PercentIncome, 123.45, "123.5%")]
    [InlineData(Dimensions.ResultAsOptions.SpendPerPupil, 123.45, "£123")]
    public void WhenResultAsOptionsIs(Dimensions.ResultAsOptions option, decimal value, string expected)
    {
        var actual = option.GetFormattedValue(value);
        Assert.Equal(expected, actual);
    }

    [Fact]
    public void WhenResultAsOptionsIsOutOfRange()
    {
        var actual = Assert.Throws<ArgumentOutOfRangeException>(() => ((Dimensions.ResultAsOptions)999).GetFormattedValue(123.45m));
        Assert.NotNull(actual);
    }
}

public class GivenDimensionsGetTableHeader
{
    [Theory]
    [InlineData(Dimensions.ResultAsOptions.Actuals, "Amount")]
    [InlineData(Dimensions.ResultAsOptions.PercentExpenditure, "Percentage")]
    [InlineData(Dimensions.ResultAsOptions.PercentIncome, "Percentage")]
    [InlineData(Dimensions.ResultAsOptions.SpendPerPupil, "Amount")]
    public void WhenResultAsOptionsIs(Dimensions.ResultAsOptions option, string expected)
    {
        var actual = option.GetTableHeader();
        Assert.Equal(expected, actual);
    }

    [Fact]
    public void WhenResultAsOptionsIsOutOfRange()
    {
        var actual = Assert.Throws<ArgumentOutOfRangeException>(() => ((Dimensions.ResultAsOptions)999).GetTableHeader());
        Assert.NotNull(actual);
    }
}