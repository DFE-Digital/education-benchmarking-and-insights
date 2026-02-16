using System.Collections.Specialized;
using Platform.Domain;
using Platform.Functions;

namespace Platform.Api.School.Features.Accounts.Parameters;

public record IncomeParameters : QueryParameters
{
    public string Dimension { get; private set; } = Dimensions.Finance.Actuals;

    public override void SetValues(NameValueCollection query)
    {
        Dimension = query["dimension"] ?? Dimensions.Finance.Actuals;
    }
}

public record IncomeNationalAvgParameters : IncomeParameters
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