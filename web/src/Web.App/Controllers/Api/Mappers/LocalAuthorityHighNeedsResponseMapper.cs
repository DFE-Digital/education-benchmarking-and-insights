using Web.App.Controllers.Api.Responses;
using Web.App.Domain.LocalAuthorities;

namespace Web.App.Controllers.Api.Mappers;

public static class LocalAuthorityHighNeedsResponseMapper
{
    public static IEnumerable<LocalAuthorityHighNeedsComparisonResponse> MapToApiResponse(this LocalAuthority<HighNeeds>[] localAuthorities)
    {
        return localAuthorities.Select(l => new LocalAuthorityHighNeedsComparisonResponse
        {
            Code = l.Code,
            Name = l.Name,
            Population2To18 = l.Population2To18,
            Outturn = l.Outturn?.MapToApiResponse(),
            Budget = l.Budget?.MapToApiResponse()
        });
    }

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
        var results = new List<LocalAuthorityHighNeedsHistoryDashboardResponse>();
        if (history?.StartYear == null || history.EndYear == null)
        {
            return results;
        }

        for (var year = history.StartYear.Value; year <= history.EndYear; year++)
        {
            var outturn = history.Outturn.MapToTotal(code, year);
            var budget = history.Budget.MapToTotal(code, year);
            var (dsgFunding, academyRecoupment) = history.Dsg.MapToValues(code, year);

            // exclude part or missing years at start of range
            if (outturn == null || budget == null)
            {
                if (results.Count == 0)
                {
                    continue;
                }
            }

            outturn += academyRecoupment ?? 0;
            results.Add(new LocalAuthorityHighNeedsHistoryDashboardResponse
            {
                Year = year,
                Outturn = outturn,
                Budget = budget,
                Funding = dsgFunding,
                BudgetDifference = budget - outturn,
                FundingDifference = dsgFunding - outturn
            });
        }

        for (var i = results.Count - 1; i >= 0; i--)
        {
            var previousResult = results.ElementAtOrDefault(i + 1);
            if (previousResult?.Year != null)
            {
                continue;
            }

            // mark part or missing years to exclude from end of range
            var result = results.ElementAt(i);
            if (result.Outturn == null || result.Budget == null)
            {
                result.Year = null;
            }
        }

        return results.Where(r => r.Year != null);
    }

    private static LocalAuthorityHighNeedsApiResponse? MapToApiResponse(this HighNeedsYear[]? highNeedsYear, string code, int year)
    {
        var item = highNeedsYear?
            .Where(o => o.Year == year)
            .SingleOrDefault(o => o.Code == code);

        return item == null ? null : MapToApiResponse(item);
    }

    private static LocalAuthorityHighNeedsApiResponse? MapToApiResponse(this HighNeeds? item)
    {
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

    private static (decimal? dsgFunding, decimal? academyRecoupment) MapToValues(this HighNeedsDsgYear[]? highNeedsDsg, string code, int year)
    {
        var dsg = highNeedsDsg?
            .Where(o => o.Year == year)
            .SingleOrDefault(o => o.Code == code);

        return (dsg?.DsgFunding, dsg?.AcademyRecoupment);
    }
}