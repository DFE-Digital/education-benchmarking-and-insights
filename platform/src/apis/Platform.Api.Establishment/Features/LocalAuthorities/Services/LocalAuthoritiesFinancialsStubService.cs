using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;
using Platform.Api.Establishment.Features.LocalAuthorities.Models;

namespace Platform.Api.Establishment.Features.LocalAuthorities.Services;

// stubbed implementation until backend ready
[ExcludeFromCodeCoverage]
public class LocalAuthoritiesFinancialsStubService : ILocalAuthoritiesFinancialsService
{
    public Task<LocalAuthorityRanking> GetRanking(string sort)
    {
        var values = new List<LocalAuthorityRank>();
        for (var i = 0; i < 155; i++)
        {
            var code = (100 + i).ToString();
            var outturn = 1_000_000 * Convert.ToDecimal(i + 0.1);
            var budget = 1_111_111 * Convert.ToDecimal(Math.Pow(i + 0.1 + i % 3, 1.1));
            var value = new LocalAuthorityRank
            {
                Code = code,
                Name = $"LA {code}",
                Value = Math.Round(outturn / budget * 100, 10)
            };

            values.Add(value);
        }

        var ordered = values.OrderBy(v => v.Value);
        if (sort == "desc")
        {
            ordered = values.OrderByDescending(v => v.Value);
        }

        var ranking = ordered
            .Select((v, i) =>
            {
                v.Rank = i;
                return v;
            })
            .ToArray();
        return Task.FromResult(new LocalAuthorityRanking
        {
            Ranking = ranking
        });
    }
}