using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Platform.Api.NonFinancial.Features.EducationHealthCarePlans.Models;

namespace Platform.Api.NonFinancial.Features.EducationHealthCarePlans.Services;

public class EducationHealthCarePlansStubService : IEducationHealthCarePlansService
{
    // TODO: remove and replace once db / data ingestion changes are in place to provide actual data
    // this generates stubbed data for usage in development prior to db updates
    public Task<LocalAuthorityNumberOfPlans[]> Get(string[] codes, CancellationToken cancellationToken = default)
    {
        const int year = 2024;
        List<LocalAuthorityNumberOfPlans> plans = [];

        for (var i = 0; i < codes.Length; i++)
        {
            var code = codes[i];

            var parsedCode = int.TryParse(code, out var parsedValue) ? parsedValue : 200;
            var baseNumber = parsedCode + i + year;
            var yearData = GetStubbedYear(year, code, baseNumber);
            plans.Add(yearData);
        }

        return Task.FromResult(plans.ToArray());
    }

    public Task<History<LocalAuthorityNumberOfPlansYear>> GetHistory(string[] codes, CancellationToken cancellationToken = default)
    {
        const int startYear = 2020;
        const int endYear = 2024;
        List<LocalAuthorityNumberOfPlansYear> plans = [];

        for (var i = 0; i < codes.Length; i++)
        {
            var code = codes[i];

            for (var year = startYear; year <= endYear; year++)
            {
                var parsedCode = int.TryParse(code, out var parsedValue) ? parsedValue : 200;
                var baseNumber = parsedCode + i + year;
                plans.Add(GetStubbedYear(year, code, baseNumber));
            }
        }

        var result = new History<LocalAuthorityNumberOfPlansYear>
        {
            StartYear = startYear,
            EndYear = endYear,
            Plans = plans.ToArray()
        };

        return Task.FromResult(result);
    }

    private static LocalAuthorityNumberOfPlansYear GetStubbedYear(int year, string code, int baseNumber)
    {
        // some variation based on input but still repeatable for testing
        var mainstream = baseNumber % 40;
        var resourced = baseNumber % 10;
        var special = baseNumber % 7;
        var independent = baseNumber % 15;
        var hospital = baseNumber % 25;
        var post16 = baseNumber % 30;
        var other = baseNumber % 8;
        var total = mainstream + resourced + special + independent + hospital + post16 + other;

        return new LocalAuthorityNumberOfPlansYear
        {
            Year = year,
            Code = code,
            Name = $"Local authority {code}",
            Mainstream = mainstream,
            Resourced = resourced,
            Special = special,
            Independent = independent,
            Hospital = hospital,
            Post16 = post16,
            Other = other,
            Total = total
        };
    }
}