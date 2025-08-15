using AutoFixture;
using Web.App.Domain.Charts;
using Xunit;

namespace Web.Tests.Domain.Charts;

public class GivenASchoolSpendingCostsVerticalBarChartRequest
{
    private readonly Fixture _fixture = new();

    [Fact]
    public void RequestShouldBeValid()
    {
        var uuid = _fixture.Create<Guid>().ToString();
        var urn = _fixture.Create<string>();
        var data = _fixture.Build<PriorityCostCategoryDatum>().CreateMany().ToArray();

        var actual = new SchoolSpendingCostsVerticalBarChartRequest(uuid, urn, data);

        Assert.Equal(data, actual.Data);
        Assert.Equal(200, actual.Height);
        Assert.Equal(urn, actual.HighlightKey);
        Assert.Equal(uuid, actual.Id);
        Assert.Equal("urn", actual.KeyField);
        Assert.Equal("asc", actual.Sort);
        Assert.Equal(630, actual.Width);
        Assert.Equal("amount", actual.ValueField);
    }
}