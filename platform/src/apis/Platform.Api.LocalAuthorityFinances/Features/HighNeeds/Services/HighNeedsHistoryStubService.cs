using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Platform.Api.LocalAuthorityFinances.Features.HighNeeds.Models;

namespace Platform.Api.LocalAuthorityFinances.Features.HighNeeds.Services;

// stubbed, untested implementation until backend ready
[ExcludeFromCodeCoverage]
public class HighNeedsHistoryStubService : IHighNeedsHistoryService
{
    public Task<History<LocalAuthorityHighNeedsYear>?> GetHistory(string[] codes, CancellationToken cancellationToken = default)
    {
        var code = codes.First();
        const int startYear = 2021;
        const int endYear = 2024;
        var history = new History<LocalAuthorityHighNeedsYear>
        {
            StartYear = startYear,
            EndYear = endYear,
            Budget = GetBudget(code, startYear, endYear).ToArray(),
            Outturn = GetOutturn(code, startYear, endYear).ToArray()
        };

        return Task.FromResult<History<LocalAuthorityHighNeedsYear>?>(history);
    }

    private static IEnumerable<LocalAuthorityHighNeedsYear> GetBudget(string code, int startYear, int endYear)
    {
        for (var year = startYear; year <= endYear; year++)
        {
            yield return GetStubbedRow(code, year, 1_100_000);
        }
    }

    private static IEnumerable<LocalAuthorityHighNeedsYear> GetOutturn(string code, int startYear, int endYear)
    {
        for (var year = startYear; year <= endYear; year++)
        {
            yield return GetStubbedRow(code, year, 1_000_000);
        }
    }

    private static LocalAuthorityHighNeedsYear GetStubbedRow(string code, int year, decimal baseValue) => new()
    {
        Code = code,
        Year = year,
        HighNeedsAmount = new HighNeedsAmount
        {
            TotalPlaceFunding = baseValue + 1 + year,
            TopUpFundingMaintained = baseValue + 2 + year,
            TopUpFundingNonMaintained = baseValue + 3 + year,
            SenServices = baseValue + 4 + year,
            AlternativeProvisionServices = baseValue + 5 + year,
            HospitalServices = baseValue + 6 + year,
            OtherHealthServices = baseValue + 7 + year
        },
        Maintained = new TopFunding
        {
            EarlyYears = baseValue + 8 + year,
            Primary = baseValue + 9 + year,
            Secondary = baseValue + 10 + year,
            Special = baseValue + 11 + year,
            AlternativeProvision = baseValue + 12 + year,
            PostSchool = baseValue + 13 + year,
            Income = baseValue + 14 + year
        },
        NonMaintained = new TopFunding
        {
            EarlyYears = baseValue + 15 + year,
            Primary = baseValue + 16 + year,
            Secondary = baseValue + 17 + year,
            Special = baseValue + 18 + year,
            AlternativeProvision = baseValue + 19 + year,
            PostSchool = baseValue + 20 + year,
            Income = baseValue + 21 + year
        },
        PlaceFunding = new PlaceFunding
        {
            Primary = baseValue + 22 + year,
            Secondary = baseValue + 23 + year,
            Special = baseValue + 24 + year,
            AlternativeProvision = baseValue + 25 + year
        }
    };
}