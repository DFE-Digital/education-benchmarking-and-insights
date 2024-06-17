using System;
using Microsoft.AspNetCore.Http;
using Platform.Functions;
using Platform.Functions.Extensions;

namespace Platform.Api.Insight.Census;

public record CensusParameters : QueryParameters
{
    public string? Category { get; private set; }
    public string Dimension { get; private set; } = CensusDimensions.Total;
    public string[] Schools { get; private set; } = Array.Empty<string>();

    public override void SetValues(IQueryCollection query)
    {
        var category = query["category"].ToString();
        if (!CensusCategories.IsValid(category) || string.IsNullOrWhiteSpace(category))
        {
            category = null;
        }

        var dimension = query["dimension"].ToString();
        if (!CensusDimensions.IsValid(dimension) || string.IsNullOrWhiteSpace(dimension))
        {
            dimension = CensusDimensions.Total;
        }

        Category = category;
        Dimension = dimension;
        Schools = query.ToStringArray("urns");
    }
}