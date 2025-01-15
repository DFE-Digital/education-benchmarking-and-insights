using Microsoft.AspNetCore.Http;
using Platform.Domain;
using Platform.Functions;
using Platform.Functions.Extensions;

namespace Platform.Api.Insight.MetricRagRatings;

public record MetricRagRatingParameters : QueryParameters
{
    public string DataContext { get; set; } = Pipeline.RunType.Default;

    public override void SetValues(IQueryCollection query)
    {
        DataContext = query.ToBool("useCustomData") ? Pipeline.RunType.Custom : Pipeline.RunType.Default;
    }
}