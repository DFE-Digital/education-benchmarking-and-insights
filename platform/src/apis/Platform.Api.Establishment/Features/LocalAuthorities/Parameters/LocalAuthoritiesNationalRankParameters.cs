using System.Collections.Specialized;
using Platform.Functions;
using Platform.Functions.Extensions;

namespace Platform.Api.Establishment.Features.LocalAuthorities.Parameters;

public record LocalAuthoritiesNationalRankParameters : QueryParameters
{
    public string Ranking { get; private set; } = Domain.Ranking.LocalAuthorityNationalRanking.SpendAsPercentageOfBudget;
    public string? Sort { get; private set; }

    public override void SetValues(NameValueCollection query)
    {
        if (query.TryGetValue("ranking", out var ranking))
        {
            Ranking = ranking;
        }

        if (query.TryGetValue("sort", out var sort))
        {
            Sort = sort;
        }
    }
}