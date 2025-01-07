using Microsoft.AspNetCore.Http;
using Platform.Functions;
using Platform.Functions.Extensions;

namespace Platform.Api.Insight.Census;

public record CensusParameters : QueryParameters
{
    public string? Category { get; private set; }
    public string Dimension { get; private set; } = CensusDimensions.Total;

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
    public string Dimension { get; private set; } = CensusDimensions.Total;
    public string FinanceType { get; private set; } = string.Empty;
    public string OverallPhase { get; private set; } = string.Empty;

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
    public string[] Urns { get; private set; } = [];
    public string? Phase { get; private set; }
    public string? CompanyNumber { get; private set; }
    public string? LaCode { get; private set; }

    public override void SetValues(IQueryCollection query)
    {
        base.SetValues(query);
        Urns = query.ToStringArray("urns");
        CompanyNumber = query["companyNumber"].ToString();
        Phase = query["phase"].ToString();
        LaCode = query["laCode"].ToString();
    }
}