using System.Collections.Specialized;
using Platform.Domain;
using Platform.Functions;

namespace Platform.Api.School.Features.Accounts.Parameters;

public record ExpenditureParameters : QueryParameters
{
    public string? Category { get; private set; }
    public string Dimension { get; private set; } = Dimensions.Finance.Actuals;

    public override void SetValues(NameValueCollection query)
    {
        Dimension = query["dimension"] ?? Dimensions.Finance.Actuals;
        Category = query["category"];
    }
}

public record ExpenditureNationalAvgParameters : ExpenditureParameters
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

public record ExpenditureQueryParameters : ExpenditureParameters
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