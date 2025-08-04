using Web.App.Domain.Charts;
using Web.App.ViewModels;
using Xunit;

namespace Web.Tests.Domain.Charts;

public class GivenASchoolComparisonItSpendHorizontalBarChartRequest
{
    [Fact]
    public void SetsBasicValuesCorrectly()
    {
        var data = new[] { new SchoolComparisonDatum { Urn = "123456", SchoolName = "test", Expenditure = 1000m } };

        var request = new SchoolComparisonItSpendHorizontalBarChartRequest(
            uuid: "test-uuid",
            urn: "123456",
            filteredData: data,
            linkFormatter: s => $"url/{s}",
            dimension: ChartDimensions.Actuals
        );

        Assert.Equal("test-uuid", request.Id);
        Assert.Equal("123456", request.HighlightKey);
        Assert.Equal("expenditure", request.ValueField);
        Assert.Equal("$,~s", request.ValueFormat);
        Assert.Equal("actuals", request.XAxisLabel);
        Assert.Equal("url/%1$s", request.LinkFormat);
    }

    [Theory]
    [InlineData(ChartDimensions.Actuals, "$,~s", "actuals")]
    [InlineData(ChartDimensions.PerUnit, "$,~s", "£ per pupil")]
    [InlineData(ChartDimensions.PercentIncome, ".1%", "percentage of income")]
    [InlineData(ChartDimensions.PercentExpenditure, ".1%", "percentage of expenditure")]
    public void SetsFormatAndLabelCorrectlyForEachDimension(
        ChartDimensions dimension,
        string expectedFormat,
        string expectedLabel)
    {
        var data = new[] { new SchoolComparisonDatum { Urn = "123456", SchoolName = "test", Expenditure = 1000m } };

        var request = new SchoolComparisonItSpendHorizontalBarChartRequest(
            uuid: "test-uuid",
            urn: "123456",
            filteredData: data,
            linkFormatter: s => $"url/{s}",
            dimension: dimension
        );

        Assert.Equal(expectedFormat, request.ValueFormat);
        Assert.Equal(expectedLabel, request.XAxisLabel);
    }
}