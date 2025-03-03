using Web.App.Controllers.Api.Mappers;
using Xunit;

namespace Web.Tests.Controllers.Api.Mappers;

public class WhenLocalAuthorityHighNeedsHistoryResponseMapperMapsToDashboardWithCode : WhenLocalAuthorityHighNeedsHistoryResponseMapperMaps
{
    [Fact]
    public void ShouldMapToLocalAuthorityHighNeedsHistoryResponses()
    {
        var actual = History.MapToDashboardResponse(Code).ToArray();

        Assert.Equal(EndYear - StartYear + 1, actual.Length);

        var startYearResponse = actual.FirstOrDefault();
        Assert.NotNull(startYearResponse);
        Assert.Equal(OutturnStartYear.Total, startYearResponse.Outturn);
        Assert.Equal(BudgetStartYear.Total, startYearResponse.Budget);
        Assert.Equal(BudgetStartYear.Total - OutturnStartYear.Total, startYearResponse.Balance);

        var endYearResponse = actual.LastOrDefault();
        Assert.NotNull(endYearResponse);
        Assert.Equal(OutturnEndYear.Total, endYearResponse.Outturn);
        Assert.Equal(BudgetEndYear.Total, endYearResponse.Budget);
        Assert.Equal(BudgetEndYear.Total - OutturnEndYear.Total, endYearResponse.Balance);
    }
}