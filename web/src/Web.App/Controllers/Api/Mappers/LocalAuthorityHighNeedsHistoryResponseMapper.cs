using Web.App.Controllers.Api.Responses;
using Web.App.Domain.LocalAuthorities;

namespace Web.App.Controllers.Api.Mappers;

public static class LocalAuthorityHighNeedsHistoryResponseMapper
{
    public static IEnumerable<LocalAuthorityHighNeedsHistoryResponse> MapToApiResponse(
        this HighNeedsHistory<HighNeedsYear>? history,
        string code)
    {
        if (history?.StartYear == null || history.EndYear == null)
        {
            yield break;
        }

        for (var year = history.StartYear.Value; year <= history.EndYear; year++)
        {
            yield return new LocalAuthorityHighNeedsHistoryResponse
            {
                Year = year,
                Term = $"{year - 1} to {year}",
                Outturn = history.Outturn?.MapToApiResponse(code, year),
                Budget = history.Budget?.MapToApiResponse(code, year)
            };
        }
    }

    public static IEnumerable<LocalAuthorityHighNeedsHistoryDashboardResponse> MapToDashboardResponse(
        this HighNeedsHistory<HighNeedsYear>? history,
        string code)
    {
        if (history?.StartYear == null || history.EndYear == null)
        {
            yield break;
        }

        for (var year = history.StartYear.Value; year <= history.EndYear; year++)
        {
            var outturn = history.Outturn.MapToTotal(code, year);
            var budget = history.Budget.MapToTotal(code, year);
            yield return new LocalAuthorityHighNeedsHistoryDashboardResponse
            {
                Year = year,
                Outturn = outturn,
                Budget = budget,
                Balance = budget - outturn
            };
        }
    }

    private static LocalAuthorityHighNeedsApiResponse? MapToApiResponse(this HighNeedsYear[]? highNeedsYear, string code, int year)
    {
        var item = highNeedsYear?
            .Where(o => o.Year == year)
            .SingleOrDefault(o => o.Code == code);

        if (item == null)
        {
            return null;
        }

        return new LocalAuthorityHighNeedsApiResponse
        {
            HighNeedsAmountTotalPlaceFunding = item.HighNeedsAmount?.TotalPlaceFunding,
            HighNeedsAmountTopUpFundingMaintained = item.HighNeedsAmount?.TopUpFundingMaintained,
            HighNeedsAmountTopUpFundingNonMaintained = item.HighNeedsAmount?.TopUpFundingNonMaintained,
            HighNeedsAmountSenServices = item.HighNeedsAmount?.SenServices,
            HighNeedsAmountAlternativeProvisionServices = item.HighNeedsAmount?.AlternativeProvisionServices,
            HighNeedsAmountHospitalServices = item.HighNeedsAmount?.HospitalServices,
            HighNeedsAmountOtherHealthServices = item.HighNeedsAmount?.OtherHealthServices,

            MaintainedEarlyYears = item.Maintained?.EarlyYears,
            MaintainedPrimary = item.Maintained?.Primary,
            MaintainedSecondary = item.Maintained?.Secondary,
            MaintainedSpecial = item.Maintained?.Special,
            MaintainedAlternativeProvision = item.Maintained?.AlternativeProvision,
            MaintainedPostSchool = item.Maintained?.PostSchool,
            MaintainedIncome = item.Maintained?.Income,

            NonMaintainedEarlyYears = item.NonMaintained?.EarlyYears,
            NonMaintainedPrimary = item.NonMaintained?.Primary,
            NonMaintainedSecondary = item.NonMaintained?.Secondary,
            NonMaintainedSpecial = item.NonMaintained?.Special,
            NonMaintainedAlternativeProvision = item.NonMaintained?.AlternativeProvision,
            NonMaintainedPostSchool = item.NonMaintained?.PostSchool,
            NonMaintainedIncome = item.NonMaintained?.Income,

            PlaceFundingPrimary = item.PlaceFunding?.Primary,
            PlaceFundingSecondary = item.PlaceFunding?.Secondary,
            PlaceFundingSpecial = item.PlaceFunding?.Special,
            PlaceFundingAlternativeProvision = item.PlaceFunding?.AlternativeProvision
        };
    }

    private static decimal? MapToTotal(this HighNeedsYear[]? highNeedsYear, string code, int year)
    {
        return highNeedsYear?
            .Where(o => o.Year == year)
            .SingleOrDefault(o => o.Code == code)
            ?.Total;
    }
}