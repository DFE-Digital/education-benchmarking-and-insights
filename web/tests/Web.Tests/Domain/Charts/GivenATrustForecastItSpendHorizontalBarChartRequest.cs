using AutoFixture;
using Web.App.Domain.Charts;
using Web.App.Infrastructure.Apis;
using Xunit;

namespace Web.Tests.Domain.Charts;

public class GivenATrustForecastItSpendHorizontalBarChartRequest
{
    private readonly Fixture _fixture = new();

    [Fact]
    public void RequestShouldBeValid()
    {
        var uuid = _fixture.Create<Guid>().ToString();
        TrustForecastDatum[] data =
        [
            new()
            {
                Year = 2022,
                Expenditure = 123
            },
            new()
            {
                Year = 2024,
                Expenditure = 456
            },
            new()
            {
                Year = 2023,
                Expenditure = 789
            }
        ];
        const Dimensions.ResultAsOptions resultAs = Dimensions.ResultAsOptions.Actuals;

        var actual = new TrustForecastItSpendHorizontalBarChartRequest(uuid, data, resultAs);

        Assert.Equal(20, actual.BarHeight);
        Assert.Equal(data, actual.Data);
        Assert.Null(actual.HighlightKey);
        Assert.Equal(uuid, actual.Id);
        Assert.Equal("yearLabel", actual.KeyField);
        Assert.Null(actual.LabelField);
        Assert.Null(actual.LabelFormat);
        Assert.Null(actual.LinkFormat);
        Assert.Null(actual.MissingDataLabel);
        Assert.Null(actual.MissingDataLabelWidth);
        Assert.Null(actual.PaddingInner);
        Assert.Null(actual.PaddingOuter);
        Assert.Null(actual.Sort);
        Assert.Equal(610, actual.Width);
        Assert.Equal("expenditure", actual.ValueField);
        Assert.Equal("currency", actual.ValueType);
        Assert.Equal("actuals", actual.XAxisLabel);

        var expectedGroups = new ChartRequestGroupedKeys
        {
            { "previous", ["2021 – 2022"] },
            { "current", ["2022 – 2023"] },
            { "forecast", ["2023 – 2024"] }
        };
        Assert.Equal(expectedGroups, actual.GroupedKeys);
    }
}