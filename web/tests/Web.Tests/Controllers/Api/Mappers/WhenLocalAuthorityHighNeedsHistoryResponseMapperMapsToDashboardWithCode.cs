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
        Assert.Equal(OutturnStartYear.HighNeedsAmount?.TotalPlaceFunding, startYearResponse.Outturn);
        Assert.Equal(BudgetStartYear.HighNeedsAmount?.TotalPlaceFunding, startYearResponse.Budget);
        Assert.Equal(BudgetStartYear.HighNeedsAmount?.TotalPlaceFunding - OutturnStartYear.HighNeedsAmount?.TotalPlaceFunding, startYearResponse.Balance);

        var endYearResponse = actual.LastOrDefault();
        Assert.NotNull(endYearResponse);
        Assert.Equal(OutturnEndYear.HighNeedsAmount?.TotalPlaceFunding, endYearResponse.Outturn);
        Assert.Equal(BudgetEndYear.HighNeedsAmount?.TotalPlaceFunding, endYearResponse.Budget);
        Assert.Equal(BudgetEndYear.HighNeedsAmount?.TotalPlaceFunding - OutturnEndYear.HighNeedsAmount?.TotalPlaceFunding, endYearResponse.Balance);
    }
}