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
                    break;
                case 2022:
                    AssertFieldsMapped(Outturn2022, actual.Outturn);
                    AssertFieldsMapped(Budget2022, actual.Budget);
                    break;
                case 2023:
                    AssertFieldsMapped(Outturn2023, actual.Outturn);
                    AssertFieldsMapped(Budget2023, actual.Budget);
                    break;
                case 2024:
                    AssertFieldsMapped(Outturn2024, actual.Outturn);
                    AssertFieldsMapped(Budget2024, actual.Budget);
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
                    break;
                case 2022:
                    AssertFieldsMapped(null, actual.Outturn);
                    AssertFieldsMapped(null, actual.Budget);
                    break;
                case 2023:
                    AssertFieldsMapped(Outturn2023, actual.Outturn);
                    AssertFieldsMapped(Budget2023, actual.Budget);
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
                    break;
                case 2023:
                    AssertFieldsMapped(Outturn2023, actual.Outturn);
                    AssertFieldsMapped(Budget2023, actual.Budget);
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