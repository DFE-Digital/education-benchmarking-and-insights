using System.Collections.Specialized;
using Platform.Domain;
using Platform.Functions;
using Platform.Functions.Extensions;

namespace Platform.Api.Insight.Features.MetricRagRatings.Parameters;

public record MetricRagRatingParameters : QueryParameters
{
    public string DataContext { get; set; } = Pipeline.RunType.Default;

    public override void SetValues(NameValueCollection query)
    {
        DataContext = query.ToBool("useCustomData") ? Pipeline.RunType.Custom : Pipeline.RunType.Default;
    }
}