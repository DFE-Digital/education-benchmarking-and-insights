using AutoFixture;
using Web.App.Domain.Charts;
using Xunit;

namespace Web.Tests.Domain.Charts;

public class GivenATrustComparisonItSpendHorizontalBarChartRequest
{
    private readonly Fixture _fixture = new();

    [Fact]
    public void RequestShouldBeValid()
    {
        var uuid = _fixture.Create<Guid>().ToString();
        var companyNumber = _fixture.Create<string>();
        var data = _fixture.Build<TrustComparisonDatum>().CreateMany().ToArray();
        const Dimensions.ResultAsOptions resultAs = Dimensions.ResultAsOptions.Actuals;

        var actual = new TrustComparisonItSpendHorizontalBarChartRequest(uuid, companyNumber, data, f => $"/{f}", resultAs);

        Assert.Equal(24, actual.BarHeight);
        Assert.Equal(data, actual.Data);
        Assert.Null(actual.GroupedKeys);
        Assert.Equal(companyNumber, actual.HighlightKey);
        Assert.Equal(uuid, actual.Id);
        Assert.Equal("companyNumber", actual.KeyField);
        Assert.Equal("trustName", actual.LabelField);
        Assert.Equal("%2$s", actual.LabelFormat);
        Assert.Equal("/%1$s", actual.LinkFormat);
        Assert.Equal("No data submitted", actual.MissingDataLabel);
        Assert.Equal(115, actual.MissingDataLabelWidth);
        Assert.Equal(0.6m, actual.PaddingInner);
        Assert.Equal(0.2m, actual.PaddingOuter);
        Assert.Equal("desc", actual.Sort);
        Assert.Equal(610, actual.Width);
        Assert.Equal("expenditure", actual.ValueField);
        Assert.Equal("currency", actual.ValueType);
        Assert.Equal("actuals", actual.XAxisLabel);
    }
}