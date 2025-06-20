using Web.App.Controllers.Api.Mappers;
using Web.App.Controllers.Api.Responses;
using Web.App.Domain.LocalAuthorities;
using Xunit;

namespace Web.Tests.Controllers.Api.Mappers;

public class WhenLocalAuthorityHighNeedsHistoryResponseMapperMapsToDashboardWithCode : WhenLocalAuthorityHighNeedsHistoryResponseMapperMaps
{
    [Fact]
    public void ShouldMapToLocalAuthorityHighNeedsHistoryResponses()
    {
        var result = History.MapToDashboardResponse(Code).ToArray();

        Assert.Equal(EndYear - StartYear, result.Length);

        foreach (var actual in result)
        {
            Assert.NotNull(actual.Year);

            switch (actual.Year)
            {
                case 2021:
                    AssertValues(Outturn2021, Budget2021, Dsg2021, actual);
                    break;
                case 2022:
                    AssertValues(Outturn2022, Budget2022, Dsg2022, actual);
                    break;
                case 2023:
                    AssertValues(Outturn2023, Budget2023, Dsg2023, actual);
                    break;
                case 2024:
                    AssertValues(Outturn2024, Budget2024, Dsg2024, actual);
                    break;
                default:
                    throw new IndexOutOfRangeException();
            }
        }
    }

    [Fact]
    public void ShouldMapToLocalAuthorityHighNeedsHistoryResponsesWithMissingData()
    {
        var result = TruncatedHistory.MapToDashboardResponse(Code).ToArray();

        Assert.Equal(EndYear - StartYear - 1, result.Length);

        foreach (var actual in result)
        {
            Assert.NotNull(actual.Year);

            switch (actual.Year)
            {
                case 2021:
                    AssertValues(Outturn2021, Budget2021, Dsg2021, actual);
                    break;
                case 2022:
                    AssertNullValues(actual);
                    break;
                case 2023:
                    AssertValues(Outturn2023, Budget2023, Dsg2023, actual);
                    break;
                default:
                    throw new IndexOutOfRangeException();
            }
        }
    }

    [Fact]
    public void ShouldMapToLocalAuthorityHighNeedsHistoryResponsesWithPartialData()
    {
        var result = PartialHistory.MapToDashboardResponse(Code).ToArray();

        Assert.Equal(EndYear - StartYear - 2, result.Length);

        foreach (var actual in result)
        {
            Assert.NotNull(actual.Year);

            switch (actual.Year)
            {
                case 2022:
                    AssertValues(Outturn2022, Budget2022, Dsg2022, actual);
                    break;
                case 2023:
                    AssertValues(Outturn2023, Budget2023, Dsg2023, actual);
                    break;
                default:
                    throw new IndexOutOfRangeException();
            }
        }
    }

    private static void AssertValues(HighNeedsYear outturn, HighNeedsYear budget, HighNeedsDsgYear dsg, LocalAuthorityHighNeedsHistoryDashboardResponse actual)
    {
        Assert.Equal(outturn.Total + dsg.AcademyRecoupment, actual.Outturn);
        Assert.Equal(budget.Total, actual.Budget);
        Assert.Equal(budget.Total - (outturn.Total + dsg.AcademyRecoupment), actual.BudgetDifference);
        Assert.Equal(dsg.DsgFunding, actual.Funding);
        Assert.Equal(dsg.DsgFunding - (outturn.Total + dsg.AcademyRecoupment), actual.FundingDifference);
    }

    private static void AssertNullValues(LocalAuthorityHighNeedsHistoryDashboardResponse actual)
    {
        Assert.Null(actual.Outturn);
        Assert.Null(actual.Budget);
        Assert.Null(actual.BudgetDifference);
        Assert.Null(actual.Funding);
        Assert.Null(actual.FundingDifference);
    }
}