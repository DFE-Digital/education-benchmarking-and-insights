using Platform.Domain;

namespace Platform.Api.Insight.Features.Workforce;

public record WorkforceHistoryQueryParameters
{
    public WorkforceDimension Dimension { get; set; } = WorkforceDimension.Total;
};