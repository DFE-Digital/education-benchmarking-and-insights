using Microsoft.AspNetCore.Http;
using Platform.Functions;
using Platform.Functions.Extensions;

namespace Platform.Api.Insight.MetricRagRatings;

public record MetricRagRatingParameters : QueryParameters
{
    public string DataContext { get; set; } = "default";

    public override void SetValues(IQueryCollection query)
    {
        DataContext = query.ToBool("useCustomData") ? "custom" : "default";
    }
}