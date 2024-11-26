using Microsoft.AspNetCore.Http;
using Platform.Functions;
using Platform.Functions.Extensions;
using Platform.Functions.Messages;
namespace Platform.Api.Insight.MetricRagRatings;

public record MetricRagRatingParameters : QueryParameters
{
    public string DataContext { get; set; } = PipelineRunType.Default;

    public override void SetValues(IQueryCollection query)
    {
        DataContext = query.ToBool("useCustomData") ? PipelineRunType.Custom : PipelineRunType.Default;
    }
}