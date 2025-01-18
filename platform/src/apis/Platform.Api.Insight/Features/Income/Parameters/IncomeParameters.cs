using System.Collections.Specialized;
using Platform.Domain;
using Platform.Functions;

namespace Platform.Api.Insight.Features.Income.Parameters;

public record IncomeParameters : QueryParameters
{
    public string Dimension { get; private set; } = Dimensions.Finance.Actuals;

    public override void SetValues(NameValueCollection query)
    {
        Dimension = query["dimension"] ?? Dimensions.Finance.Actuals;
    }
}