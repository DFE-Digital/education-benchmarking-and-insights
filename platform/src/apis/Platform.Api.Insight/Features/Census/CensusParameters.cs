using System.Collections.Specialized;
using Platform.Domain;
using Platform.Functions;

namespace Platform.Api.Insight.Features.Census;

public record CensusParameters : QueryParameters
{
    public string? Category { get; private set; }
    public string Dimension { get; private set; } = Dimensions.Census.Total;

    public override void SetValues(NameValueCollection query)
    {
        Dimension = query["dimension"] ?? Dimensions.Census.Total;
        Category = query["category"];
    }
}

public record CensusQuerySchoolsParameters : CensusParameters
{
    public string[] Urns { get; private set; } = [];
    public string? Phase { get; private set; }
    public string? CompanyNumber { get; private set; }
    public string? LaCode { get; private set; }

    public override void SetValues(NameValueCollection query)
    {
        base.SetValues(query);

        Urns = query["urns"]?.Split(',') ?? [];
        CompanyNumber = query["companyNumber"];
        Phase = query["phase"];
        LaCode = query["laCode"];
    }
}

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

