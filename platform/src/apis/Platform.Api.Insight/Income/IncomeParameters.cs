using System;
using Microsoft.AspNetCore.Http;

namespace Platform.Api.Insight.Income;

public record IncomeParameters
{
    public bool IncludeBreakdown { get; set; }

    public string? Category { get; set; }
    public string Dimension { get; set; } = IncomeDimensions.Actuals;
    public string[] Schools { get; set; } = Array.Empty<string>();
    public string[] Trusts { get; set; } = Array.Empty<string>();
}

public static class IncomeQueryParameters
{
    public static IncomeParameters Parameters(this IQueryCollection query)
    {
        var dimension = query["dimension"].ToString();
        if (!IncomeDimensions.IsValid(dimension) || string.IsNullOrWhiteSpace(dimension))
        {
            dimension = IncomeDimensions.Actuals;
        }

        var category = query["category"].ToString();
        if (!IncomeCategories.IsValid(category) || string.IsNullOrWhiteSpace(category))
        {
            category = null;
        }

        var includeBreakdown = bool.TryParse(query["includeBreakdown"].ToString(), out var val) && val;
        var urns = query["urns"].ToString().Split(",");
        var companyNumbers = query["companyNumbers"].ToString().Split(",");

        return new IncomeParameters
        {
            IncludeBreakdown = includeBreakdown,
            Category = category,
            Dimension = dimension,
            Schools = urns,
            Trusts = companyNumbers
        };
    }
}