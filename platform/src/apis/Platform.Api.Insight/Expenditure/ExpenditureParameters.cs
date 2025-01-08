using Microsoft.AspNetCore.Http;
using Platform.Functions;
using Platform.Functions.Extensions;

namespace Platform.Api.Insight.Expenditure;

public record ExpenditureParameters : QueryParameters
{
    public bool ExcludeCentralServices { get; internal set; }
    public string? Category { get; internal set; }
    public string Dimension { get; internal set; } = ExpenditureDimensions.Actuals;

    public override void SetValues(IQueryCollection query)
    {
        ExcludeCentralServices = query.ToBool("excludeCentralServices");

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

public record QuerySchoolExpenditureParameters : ExpenditureParameters
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

public record QueryTrustExpenditureParameters : ExpenditureParameters
{
    public string[] CompanyNumbers { get; internal set; } = [];

    public override void SetValues(IQueryCollection query)
    {
        base.SetValues(query);
        CompanyNumbers = query.ToStringArray("companyNumbers");
    }
}

public record ExpenditureNationalAvgParameters : QueryParameters
{
    public string Dimension { get; internal set; } = ExpenditureDimensions.Actuals;
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