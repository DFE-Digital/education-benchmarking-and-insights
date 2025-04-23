using AutoFixture;
using Web.App.Controllers.Api.Mappers;
using Web.App.Controllers.Api.Responses;
using Web.App.Domain.LocalAuthorities;
using Xunit;

namespace Web.Tests.Controllers.Api.Mappers;

public class WhenLocalAuthorityHighNeedsComparisonResponseMapperMapsToApi
{
    private static readonly Fixture Fixture = new();

    [Fact]
    public void ShouldMapToLocalAuthorityHighNeedsComparisonResponses()
    {
        var codes = new[] { "code1", "code2", "code3" };

        var plans = codes.Select(c => Fixture
                .Build<LocalAuthority<HighNeeds>>()
                .With(p => p.Code, c)
                .Create())
            .ToArray();

        var results = plans.ToArray().MapToApiResponse().ToArray();

        foreach (var actual in results)
        {
            var expected = plans.FirstOrDefault(p => p.Code == actual.Code);
            Assert.NotNull(expected);

            Assert.Equal(expected.Code, actual.Code);
            Assert.Equal(expected.Name, actual.Name);
            Assert.Equal(expected.Population2To18, actual.Population2To18);
            AssertFieldsMapped(expected.Outturn!, actual.Outturn);
            AssertFieldsMapped(expected.Budget!, actual.Budget);
        }
    }

    private static void AssertFieldsMapped(HighNeeds expected, LocalAuthorityHighNeedsApiResponse? actual)
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