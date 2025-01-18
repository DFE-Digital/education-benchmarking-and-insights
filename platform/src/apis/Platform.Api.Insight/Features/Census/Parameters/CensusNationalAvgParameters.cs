using System.Collections.Specialized;

namespace Platform.Api.Insight.Features.Census.Parameters;

public record CensusNationalAvgParameters : CensusParameters
{
    public string FinanceType { get; private set; } = string.Empty;
    public string OverallPhase { get; private set; } = string.Empty;

    public override void SetValues(NameValueCollection query)
    {
        base.SetValues(query);

        FinanceType = query["financeType"] ?? string.Empty;
        OverallPhase = query["phase"] ?? string.Empty;
    }
}