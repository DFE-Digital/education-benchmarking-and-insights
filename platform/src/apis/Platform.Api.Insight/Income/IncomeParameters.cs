using System;
using Microsoft.AspNetCore.Http;
using Platform.Functions;
using Platform.Functions.Extensions;

namespace Platform.Api.Insight.Income;

public record IncomeParameters : QueryParameters
{
    public bool IncludeBreakdown { get; private set; }
    public string? Category { get; private set; }
    public string Dimension { get; private set; } = IncomeDimensions.Actuals;
    public string[] Schools { get; private set; } = Array.Empty<string>();
    public string[] Trusts { get; private set; } = Array.Empty<string>();

    public override void SetValues(IQueryCollection query)
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

        IncludeBreakdown = query.ToBool("includeBreakdown");
        Category = category;
        Dimension = dimension;
        Schools = query.ToStringArray("urns");
        Trusts = query.ToStringArray("companyNumbers");
    }
}