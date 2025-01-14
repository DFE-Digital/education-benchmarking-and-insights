using Microsoft.AspNetCore.Http;
using Platform.Domain.Messages;
using Platform.Functions;
using Platform.Functions.Extensions;

namespace Platform.Api.Insight.MetricRagRatings;

public record MetricRagRatingParameters : QueryParameters
{
    public string DataContext { get; set; } = PipelineRunType.Default;

    public override void SetValues(IQueryCollection query)
    {
        DataContext = query.ToBool("useCustomData") ? PipelineRunType.Custom : PipelineRunType.Default;
    }
}