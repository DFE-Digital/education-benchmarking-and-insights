using Platform.Domain;

namespace Platform.Api.Insight.Features.Workforce;

public record WorkforceQueryParameters
{
    public WorkforceDimension Dimension { get; set; } = WorkforceDimension.Total;
    public string Category { get; set; } = WorkforceCategory.WorkforceFte;
    public string[] Urns { get; set; } = [];
};