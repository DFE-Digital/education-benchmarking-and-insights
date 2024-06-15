using System;
using Microsoft.AspNetCore.Http;

namespace Platform.Api.Insight.Balance;

public record BalanceParameters
{
    public bool IncludeBreakdown { get; set; }
    public string Dimension { get; set; } = BalanceDimensions.Actuals;
    public string[] Schools { get; set; } = Array.Empty<string>();
    public string[] Trusts { get; set; } = Array.Empty<string>();
}

public static class BalanceQueryParameters
{
    public static BalanceParameters Parameters(this IQueryCollection query)
    {
        var dimension = query["dimension"].ToString();
        if (!BalanceDimensions.IsValid(dimension) || string.IsNullOrWhiteSpace(dimension))
        {
            dimension = BalanceDimensions.Actuals;
        }

        var includeBreakdown = bool.TryParse(query["includeBreakdown"].ToString(), out var val) && val;
        var urns = query["urns"].ToString().Split(",");
        var companyNumbers = query["companyNumbers"].ToString().Split(",");

        return new BalanceParameters
        {
            IncludeBreakdown = includeBreakdown,
            Dimension = dimension,
            Schools = urns,
            Trusts = companyNumbers
        };
    }
}