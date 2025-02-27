using AutoFixture;
using Web.App.Controllers.Api.Mappers;
using Web.App.Controllers.Api.Responses;
using Web.App.Domain.LocalAuthorities;
using Xunit;

namespace Web.Tests.Controllers.Api.Mappers;

public class WhenLocalAuthorityHighNeedsHistoryResponseMapperMapsWithCode
{
    private static readonly Fixture Fixture = new();

    [Fact]
    public void ShouldMapToLocalAuthorityHighNeedsHistoryResponses()
    {
        const string code = nameof(code);
        const int startYear = 2021;
        const int endYear = 2024;

        var outturnStartYear = Fixture
            .Build<LocalAuthorityHighNeedsYear>()
            .With(o => o.Year, startYear)
            .With(o => o.Code, code)
            .Create();

        var outturnEndYear = Fixture
            .Build<LocalAuthorityHighNeedsYear>()
            .With(o => o.Year, endYear)
            .With(o => o.Code, code)
            .Create();

        var budgetStartYear = Fixture
            .Build<LocalAuthorityHighNeedsYear>()
            .With(o => o.Year, startYear)
            .With(o => o.Code, code)
            .Create();

        var budgetEndYear = Fixture
            .Build<LocalAuthorityHighNeedsYear>()
            .With(o => o.Year, endYear)
            .With(o => o.Code, code)
            .Create();

        var history = new HighNeedsHistory<LocalAuthorityHighNeedsYear>
        {
            StartYear = startYear,
            EndYear = endYear,
            Outturn =
            [
                outturnStartYear,
                outturnEndYear,
                new LocalAuthorityHighNeedsYear
                {
                    Year = endYear,
                },
                new LocalAuthorityHighNeedsYear
                {
                    Code = code
                }
            ],
            Budget =
            [
                budgetStartYear,
                budgetEndYear,
                new LocalAuthorityHighNeedsYear
                {
                    Year = endYear,
                },
                new LocalAuthorityHighNeedsYear
                {
                    Code = code
                }
            ]
        };

        var actual = history.MapToApiResponse(code).ToArray();

        Assert.Equal(endYear - startYear + 1, actual.Length);

        var startYearResponse = actual.FirstOrDefault();
        Assert.NotNull(startYearResponse);
        Assert.Equal("2020 to 2021", startYearResponse.Term);
        AssertFieldsMapped(outturnStartYear, startYearResponse.Outturn);
        AssertFieldsMapped(budgetStartYear, startYearResponse.Budget);

        var endYearResponse = actual.LastOrDefault();
        Assert.NotNull(endYearResponse);
        Assert.Equal("2023 to 2024", endYearResponse.Term);
        AssertFieldsMapped(outturnEndYear, endYearResponse.Outturn);
        AssertFieldsMapped(budgetEndYear, endYearResponse.Budget);
    }

    private static void AssertFieldsMapped(LocalAuthorityHighNeedsYear? expected, LocalAuthorityHighNeedsResponse? actual)
    {
        Assert.Equal(expected?.HighNeedsAmount?.TotalPlaceFunding, actual?.HighNeedsAmountTotalPlaceFunding);
        Assert.Equal(expected?.HighNeedsAmount?.TopUpFundingMaintained, actual?.HighNeedsAmountTopUpFundingMaintained);
        Assert.Equal(expected?.HighNeedsAmount?.TopUpFundingNonMaintained, actual?.HighNeedsAmountTopUpFundingNonMaintained);
        Assert.Equal(expected?.HighNeedsAmount?.SenServices, actual?.HighNeedsAmountSenServices);
        Assert.Equal(expected?.HighNeedsAmount?.AlternativeProvisionServices, actual?.HighNeedsAmountAlternativeProvisionServices);
        Assert.Equal(expected?.HighNeedsAmount?.HospitalServices, actual?.HighNeedsAmountHospitalServices);
        Assert.Equal(expected?.HighNeedsAmount?.OtherHealthServices, actual?.HighNeedsAmountOtherHealthServices);

        Assert.Equal(expected?.Maintained?.EarlyYears, actual?.MaintainedEarlyYears);
        Assert.Equal(expected?.Maintained?.Primary, actual?.MaintainedPrimary);
        Assert.Equal(expected?.Maintained?.Secondary, actual?.MaintainedSecondary);
        Assert.Equal(expected?.Maintained?.Special, actual?.MaintainedSpecial);
        Assert.Equal(expected?.Maintained?.AlternativeProvision, actual?.MaintainedAlternativeProvision);
        Assert.Equal(expected?.Maintained?.PostSchool, actual?.MaintainedPostSchool);
        Assert.Equal(expected?.Maintained?.Income, actual?.MaintainedIncome);

        Assert.Equal(expected?.NonMaintained?.EarlyYears, actual?.NonMaintainedEarlyYears);
        Assert.Equal(expected?.NonMaintained?.Primary, actual?.NonMaintainedPrimary);
        Assert.Equal(expected?.NonMaintained?.Secondary, actual?.NonMaintainedSecondary);
        Assert.Equal(expected?.NonMaintained?.Special, actual?.NonMaintainedSpecial);
        Assert.Equal(expected?.NonMaintained?.AlternativeProvision, actual?.NonMaintainedAlternativeProvision);
        Assert.Equal(expected?.NonMaintained?.PostSchool, actual?.NonMaintainedPostSchool);
        Assert.Equal(expected?.NonMaintained?.Income, actual?.NonMaintainedIncome);

        Assert.Equal(expected?.PlaceFunding?.Primary, actual?.PlaceFundingPrimary);
        Assert.Equal(expected?.PlaceFunding?.Secondary, actual?.PlaceFundingSecondary);
        Assert.Equal(expected?.PlaceFunding?.Special, actual?.PlaceFundingSpecial);
        Assert.Equal(expected?.PlaceFunding?.AlternativeProvision, actual?.PlaceFundingAlternativeProvision);
    }
}