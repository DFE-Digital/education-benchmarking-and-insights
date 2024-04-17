using Platform.Domain;

namespace Platform.Api.Insight.Features.SchoolFinances;

public record SchoolHistoryQueryParameters
{
    public Dimension Dimension { get; set; } = Dimension.Actuals;
};