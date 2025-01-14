using Microsoft.AspNetCore.Http;
using Platform.Functions;
using Platform.Functions.Extensions;

namespace Platform.Api.Insight.Expenditure;

public record ExpenditureParameters : QueryParameters
{
    public bool ExcludeCentralServices { get; set; }
    public string? Category { get; set; }
    public string Dimension { get; set; } = ExpenditureDimensions.Actuals;

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
    public string[] Urns { get; set; } = [];
    public string? Phase { get; set; }
    public string? CompanyNumber { get; set; }
    public string? LaCode { get; set; }

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
    public string[] CompanyNumbers { get; set; } = [];

    public override void SetValues(IQueryCollection query)
    {
        base.SetValues(query);
        CompanyNumbers = query.ToStringArray("companyNumbers");
    }
}

public record ExpenditureNationalAvgParameters : QueryParameters
{
    public string Dimension { get; set; } = ExpenditureDimensions.Actuals;
    public string FinanceType { get; set; } = string.Empty;
    public string OverallPhase { get; set; } = string.Empty;

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