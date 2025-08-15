using AutoFixture;
using Web.App.Domain.Charts;
using Web.App.Infrastructure.Apis;
using Xunit;

namespace Web.Tests.Domain.Charts;

public class GivenASchoolComparisonItSpendHorizontalBarChartRequest
{
    private readonly Fixture _fixture = new();
    private readonly Random _random = new();

    [Fact]
    public void RequestShouldBeValid()
    {
        var uuid = _fixture.Create<Guid>().ToString();
        var urn = _fixture.Create<string>();
        var data = _fixture
            .Build<SchoolComparisonDatum>()
            .With(s => s.PeriodCoveredByReturn, () => _random.Next(1, 12))
            .CreateMany().ToArray();
        const Dimensions.ResultAsOptions resultAs = Dimensions.ResultAsOptions.Actuals;

        var actual = new SchoolComparisonItSpendHorizontalBarChartRequest(uuid, urn, data, f => $"/{f}", resultAs);

        Assert.Equal(22, actual.BarHeight);
        Assert.Equal(data, actual.Data);
        Assert.Equal(urn, actual.HighlightKey);
        Assert.Equal(uuid, actual.Id);
        Assert.Equal("urn", actual.KeyField);
        Assert.Equal("schoolName", actual.LabelField);
        Assert.Equal("%2$s", actual.LabelFormat);
        Assert.Equal("/%1$s", actual.LinkFormat);
        Assert.Equal("desc", actual.Sort);
        Assert.Equal(610, actual.Width);
        Assert.Equal("expenditure", actual.ValueField);
        Assert.Equal("currency", actual.ValueType);
        Assert.Equal("actuals", actual.XAxisLabel);

        var partYear = data.Where(d => d.PeriodCoveredByReturn is not 12).ToList();
        if (partYear.Count > 0)
        {
            Assert.NotNull(actual.GroupedKeys);

            var expected = new ChartRequestGroupedKeys
            {
                ["part-year"] = partYear.Select(p => p.Urn!).ToArray()
            };
            Assert.Equal(expected, actual.GroupedKeys);
        }
        else
        {
            Assert.Null(actual.GroupedKeys);
        }
    }
}