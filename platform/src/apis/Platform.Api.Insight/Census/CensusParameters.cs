using Microsoft.AspNetCore.Http;
using Platform.Functions;
using Platform.Functions.Extensions;

namespace Platform.Api.Insight.Census;

public record CensusParameters : QueryParameters
{
    public string? Category { get; internal set; }
    public string Dimension { get; internal set; } = CensusDimensions.Total;

    public override void SetValues(IQueryCollection query)
    {
        if (query.TryGetValue("category", out var category) && !string.IsNullOrWhiteSpace(category))
        {
            Category = category.ToString();
        }

        if (query.TryGetValue("dimension", out var dimension) && !string.IsNullOrWhiteSpace(dimension))
        {
            Dimension = dimension.ToString();
        }
    }
}

public record CensusNationalAvgParameters : QueryParameters
{
    public string Dimension { get; internal set; } = CensusDimensions.Total;
    public string FinanceType { get; internal set; } = string.Empty;
    public string OverallPhase { get; internal set; } = string.Empty;

    public override void SetValues(IQueryCollection query)
    {
        if (query.TryGetValue("dimension", out var dimension) && !string.IsNullOrWhiteSpace(dimension))
        {
            Dimension = dimension.ToString();
        }

        if (query.TryGetValue("financeType", out var financeType) && !string.IsNullOrWhiteSpace(financeType))
        {
            FinanceType = financeType.ToString();
        }

        if (query.TryGetValue("phase", out var overallPhase) && !string.IsNullOrWhiteSpace(overallPhase))
        {
            OverallPhase = overallPhase.ToString();
        }
    }
}

public record QuerySchoolCensusParameters : CensusParameters
{
    public string[] Urns { get; internal set; } = [];
    public string? Phase { get; internal set; }
    public string? CompanyNumber { get; internal set; }
    public string? LaCode { get; internal set; }

    public override void SetValues(IQueryCollection query)
    {
        base.SetValues(query);
        Urns = query.ToStringArray("urns");
        CompanyNumber = query["companyNumber"].ToString();
        Phase = query["phase"].ToString();
        LaCode = query["laCode"].ToString();
    }
}