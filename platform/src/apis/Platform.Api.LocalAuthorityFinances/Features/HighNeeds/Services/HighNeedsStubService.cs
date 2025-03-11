using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Platform.Api.LocalAuthorityFinances.Features.HighNeeds.Models;

namespace Platform.Api.LocalAuthorityFinances.Features.HighNeeds.Services;

// stubbed, untested implementation until backend ready
[ExcludeFromCodeCoverage]
[SuppressMessage("Performance", "CA1822:Mark members as static")]
public class HighNeedsStubService : IHighNeedsService
{
    public Task<LocalAuthority<Models.HighNeeds>[]> Get(string[] codes, string dimension, CancellationToken cancellationToken = default)
    {
        return Task.FromResult(codes.Select(c => GetLocalAuthority(c, dimension)).ToArray());
    }

    public Task<History<HighNeedsYear>?> GetHistory(string[] codes, CancellationToken cancellationToken = default)
    {
        var code = codes.First();
        const int startYear = 2021;
        const int endYear = 2024;
        var history = new History<HighNeedsYear>
        {
            StartYear = startYear,
            EndYear = endYear,
            Budget = GetBudget(code, startYear, endYear).ToArray(),
            Outturn = GetOutturn(code, startYear, endYear).ToArray()
        };

        return Task.FromResult<History<HighNeedsYear>?>(history);
    }

    private static IEnumerable<HighNeedsYear> GetBudget(string code, int startYear, int endYear)
    {
        for (var year = startYear; year <= endYear; year++)
        {
            yield return GetStubbedRow(code, year, 1_100_000, 1_110_000 + year % 2);
        }
    }

    private static IEnumerable<HighNeedsYear> GetOutturn(string code, int startYear, int endYear)
    {
        for (var year = startYear; year <= endYear; year++)
        {
            yield return GetStubbedRow(code, year, 1_000_000, 1_010_000 + year * 2);
        }
    }

    private static HighNeedsYear GetStubbedRow(string code, int year, decimal baseValue, decimal total) => new()
    {
        Code = code,
        Year = year,
        Total = total,
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

    private static LocalAuthority<Models.HighNeeds> GetLocalAuthority(string code, string dimension)
    {
        if (!int.TryParse(code, out var baseValue))
        {
            baseValue = code.Length;
        }

        var baseMultiplier = dimension == "Actuals" ? 1_000 : 1;
        return new LocalAuthority<Models.HighNeeds>
        {
            Code = code,
            Name = $"Local authority {code}",
            Budget = GetStubbedRow(code, 2024, 1_100 * baseMultiplier + baseValue, 1_110 * baseMultiplier + baseValue % 2),
            Outturn = GetStubbedRow(code, 2024, 1_000 * baseMultiplier + baseValue, 1_010 * baseMultiplier + baseValue % 2)
        };
    }
}