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
        var actual = History.MapToApiResponse(Code).ToArray();

        Assert.Equal(EndYear - StartYear + 1, actual.Length);

        var startYearResponse = actual.FirstOrDefault();
        Assert.NotNull(startYearResponse);
        Assert.Equal("2020 to 2021", startYearResponse.Term);
        AssertFieldsMapped(OutturnStartYear, startYearResponse.Outturn);
        AssertFieldsMapped(BudgetStartYear, startYearResponse.Budget);

        var endYearResponse = actual.LastOrDefault();
        Assert.NotNull(endYearResponse);
        Assert.Equal("2023 to 2024", endYearResponse.Term);
        AssertFieldsMapped(OutturnEndYear, endYearResponse.Outturn);
        AssertFieldsMapped(BudgetEndYear, endYearResponse.Budget);
    }

    private static void AssertFieldsMapped(LocalAuthorityHighNeedsYear expected, LocalAuthorityHighNeedsApiResponse? actual)
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