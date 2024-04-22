using Platform.Domain;

namespace Platform.Api.Insight.Features.TrustFinances;

public record TrustHistoryQueryParameters
{
    public Dimension Dimension { get; set; } = Dimension.Actuals;
};