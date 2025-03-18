using Web.App.Controllers.Api.Mappers;
using Web.App.Controllers.Api.Responses;
using Web.App.Domain.LocalAuthorities;
using Xunit;

namespace Web.Tests.Controllers.Api.Mappers;

public class WhenLocalAuthorityHighNeedsHistoryResponseMapperMapsToApiWithCode : WhenLocalAuthorityHighNeedsHistoryResponseMapperMaps
{
    [Fact]
    public void ShouldMapToLocalAuthorityHighNeedsHistoryResponses()
    {
        var result = History.MapToApiResponse(Code).ToArray();

        Assert.Equal(EndYear - StartYear + 1, result.Length);

        foreach (var actual in result)
        {
            Assert.NotNull(actual.Year);
            Assert.Equal($"{actual.Year - 1} to {actual.Year}", actual.Term);

            switch (actual.Year)
            {
                case 2020:
                    Assert.Null(actual.Outturn);
                    Assert.Null(actual.Budget);
                    break;
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

    private static void AssertFieldsMapped(HighNeedsYear expected, LocalAuthorityHighNeedsApiResponse? actual)
    {
        Assert.Equal(expected.HighNeedsAmount?.TotalPlaceFunding, actual?.HighNeedsAmountTotalPlaceFunding);
        Assert.Equal(expected.HighNeedsAmount?.TopUpFundingMaintained, actual?.HighNeedsAmountTopUpFundingMaintained);
        Assert.Equal(expected.HighNeedsAmount?.TopUpFundingNonMaintained, actual?.HighNeedsAmountTopUpFundingNonMaintained);
        Assert.Equal(expected.HighNeedsAmount?.SenServices, actual?.HighNeedsAmountSenServices);
        Assert.Equal(expected.HighNeedsAmount?.AlternativeProvisionServices, actual?.HighNeedsAmountAlternativeProvisionServices);
        Assert.Equal(expected.HighNeedsAmount?.HospitalServices, actual?.HighNeedsAmountHospitalServices);
        Assert.Equal(expected.HighNeedsAmount?.OtherHealthServices, actual?.HighNeedsAmountOtherHealthServices);

        Assert.Equal(expected.Maintained?.EarlyYears, actual?.MaintainedEarlyYears);
        Assert.Equal(expected.Maintained?.Primary, actual?.MaintainedPrimary);
        Assert.Equal(expected.Maintained?.Secondary, actual?.MaintainedSecondary);
        Assert.Equal(expected.Maintained?.Special, actual?.MaintainedSpecial);
        Assert.Equal(expected.Maintained?.AlternativeProvision, actual?.MaintainedAlternativeProvision);
        Assert.Equal(expected.Maintained?.PostSchool, actual?.MaintainedPostSchool);
        Assert.Equal(expected.Maintained?.Income, actual?.MaintainedIncome);

        Assert.Equal(expected.NonMaintained?.EarlyYears, actual?.NonMaintainedEarlyYears);
        Assert.Equal(expected.NonMaintained?.Primary, actual?.NonMaintainedPrimary);
        Assert.Equal(expected.NonMaintained?.Secondary, actual?.NonMaintainedSecondary);
        Assert.Equal(expected.NonMaintained?.Special, actual?.NonMaintainedSpecial);
        Assert.Equal(expected.NonMaintained?.AlternativeProvision, actual?.NonMaintainedAlternativeProvision);
        Assert.Equal(expected.NonMaintained?.PostSchool, actual?.NonMaintainedPostSchool);
        Assert.Equal(expected.NonMaintained?.Income, actual?.NonMaintainedIncome);

        Assert.Equal(expected.PlaceFunding?.Primary, actual?.PlaceFundingPrimary);
        Assert.Equal(expected.PlaceFunding?.Secondary, actual?.PlaceFundingSecondary);
        Assert.Equal(expected.PlaceFunding?.Special, actual?.PlaceFundingSpecial);
        Assert.Equal(expected.PlaceFunding?.AlternativeProvision, actual?.PlaceFundingAlternativeProvision);
    }
}