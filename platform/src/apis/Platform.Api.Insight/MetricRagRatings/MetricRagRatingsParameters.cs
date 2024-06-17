using System;
using System.Linq;
using Microsoft.AspNetCore.Http;
using Platform.Functions;
using Platform.Functions.Extensions;

namespace Platform.Api.Insight.MetricRagRatings;

public record MetricRagRatingsParameters : QueryParameters
{
    public string[] Categories { get; private set; } = Array.Empty<string>();
    public string[] Schools { get; private set; } = Array.Empty<string>();
    public string[] Statuses { get; private set; } = Array.Empty<string>();

    public override void SetValues(IQueryCollection query)
    {
        Statuses = query.ToStringArray("statuses");
        Categories = query.ToStringArray("categories");
        Schools = query.ToStringArray("urns");
    }
}