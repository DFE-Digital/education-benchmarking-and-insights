using Web.App.Controllers.Api.Mappers;
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
                    AssertFieldsMapped(Outturn2021, actual.Outturn);
                    AssertFieldsMapped(Budget2021, actual.Budget);
                    Assert.Equal(Budget2021.Total - (Outturn2021.Total + Dsg2021.AcademyRecoupment), actual.BudgetDifference);
                    Assert.Equal(Dsg2021.DsgFunding, actual.Funding);
                    Assert.Equal(Dsg2021.DsgFunding - (Outturn2021.Total + Dsg2021.AcademyRecoupment), actual.FundingDifference);
                    break;
                case 2022:
                    AssertFieldsMapped(Outturn2022, actual.Outturn);
                    AssertFieldsMapped(Budget2022, actual.Budget);
                    Assert.Equal(Budget2022.Total - (Outturn2022.Total + Dsg2022.AcademyRecoupment), actual.BudgetDifference);
                    Assert.Equal(Dsg2022.DsgFunding, actual.Funding);
                    Assert.Equal(Dsg2022.DsgFunding - (Outturn2022.Total + Dsg2022.AcademyRecoupment), actual.FundingDifference);
                    break;
                case 2023:
                    AssertFieldsMapped(Outturn2023, actual.Outturn);
                    AssertFieldsMapped(Budget2023, actual.Budget);
                    Assert.Equal(Budget2023.Total - (Outturn2023.Total + Dsg2023.AcademyRecoupment), actual.BudgetDifference);
                    Assert.Equal(Dsg2023.DsgFunding, actual.Funding);
                    Assert.Equal(Dsg2023.DsgFunding - (Outturn2023.Total + Dsg2023.AcademyRecoupment), actual.FundingDifference);
                    break;
                case 2024:
                    AssertFieldsMapped(Outturn2024, actual.Outturn);
                    AssertFieldsMapped(Budget2024, actual.Budget);
                    Assert.Equal(Budget2024.Total - (Outturn2024.Total + Dsg2024.AcademyRecoupment), actual.BudgetDifference);
                    Assert.Equal(Dsg2024.DsgFunding, actual.Funding);
                    Assert.Equal(Dsg2024.DsgFunding - (Outturn2024.Total + Dsg2024.AcademyRecoupment), actual.FundingDifference);
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
                    AssertFieldsMapped(Outturn2021, actual.Outturn);
                    AssertFieldsMapped(Budget2021, actual.Budget);
                    Assert.Equal(Budget2021.Total - (Outturn2021.Total + Dsg2021.AcademyRecoupment), actual.BudgetDifference);
                    Assert.Equal(Dsg2021.DsgFunding, actual.Funding);
                    Assert.Equal(Dsg2021.DsgFunding - (Outturn2021.Total + Dsg2021.AcademyRecoupment), actual.FundingDifference);
                    break;
                case 2022:
                    AssertFieldsMapped(null, actual.Outturn);
                    AssertFieldsMapped(null, actual.Budget);
                    Assert.Null(actual.BudgetDifference);
                    Assert.Null(actual.Funding);
                    Assert.Null(actual.FundingDifference);
                    break;
                case 2023:
                    AssertFieldsMapped(Outturn2023, actual.Outturn);
                    AssertFieldsMapped(Budget2023, actual.Budget);
                    Assert.Equal(Budget2023.Total - (Outturn2023.Total + Dsg2023.AcademyRecoupment), actual.BudgetDifference);
                    Assert.Equal(Dsg2023.DsgFunding, actual.Funding);
                    Assert.Equal(Dsg2023.DsgFunding - (Outturn2023.Total + Dsg2023.AcademyRecoupment), actual.FundingDifference);
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
                    AssertFieldsMapped(Outturn2022, actual.Outturn);
                    AssertFieldsMapped(Budget2022, actual.Budget);
                    Assert.Equal(Budget2022.Total - (Outturn2022.Total + Dsg2022.AcademyRecoupment), actual.BudgetDifference);
                    Assert.Equal(Dsg2022.DsgFunding, actual.Funding);
                    Assert.Equal(Dsg2022.DsgFunding - (Outturn2022.Total + Dsg2022.AcademyRecoupment), actual.FundingDifference);
                    break;
                case 2023:
                    AssertFieldsMapped(Outturn2023, actual.Outturn);
                    AssertFieldsMapped(Budget2023, actual.Budget);
                    Assert.Equal(Budget2023.Total - (Outturn2023.Total + Dsg2023.AcademyRecoupment), actual.BudgetDifference);
                    Assert.Equal(Dsg2023.DsgFunding, actual.Funding);
                    Assert.Equal(Dsg2023.DsgFunding - (Outturn2023.Total + Dsg2023.AcademyRecoupment), actual.FundingDifference);
                    break;
                default:
                    throw new IndexOutOfRangeException();
            }
        }
    }

    private static void AssertFieldsMapped(HighNeedsYear? expected, decimal? actual)
    {
        Assert.Equal(expected?.Total, actual);
    }
}