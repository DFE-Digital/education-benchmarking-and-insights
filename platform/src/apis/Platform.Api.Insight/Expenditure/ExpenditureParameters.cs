using System;
using Microsoft.AspNetCore.Http;

namespace Platform.Api.Insight.Expenditure;

public record ExpenditureParameters
{
    public bool IncludeBreakdown { get; set; }
    public string? Category { get; set; }
    public string Dimension { get; set; } = ExpenditureDimensions.Actuals;
    public string[] Schools { get; set; } = Array.Empty<string>();
    public string[] Trusts { get; set; } = Array.Empty<string>();
}

public static class ExpenditureQueryParameters
{
    public static ExpenditureParameters Parameters(this IQueryCollection query)
    {
        var dimension = query["dimension"].ToString();
        if (!ExpenditureDimensions.IsValid(dimension) || string.IsNullOrWhiteSpace(dimension))
        {
            dimension = ExpenditureDimensions.Actuals;
        }

        var category = query["category"].ToString();
        if (!ExpenditureCategories.IsValid(category) || string.IsNullOrWhiteSpace(category))
        {
            category = null;
        }

        var includeBreakdown = bool.TryParse(query["includeBreakdown"].ToString(), out var val) && val;
        var urns = query["urns"].ToString().Split(",");
        var companyNumbers = query["companyNumbers"].ToString().Split(",");

        return new ExpenditureParameters
        {
            IncludeBreakdown = includeBreakdown,
            Category = category,
            Dimension = dimension,
            Schools = urns,
            Trusts = companyNumbers
        };
    }
}