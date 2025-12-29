using System.Collections.Specialized;
using Platform.Domain;
using Platform.Functions;

namespace Platform.Api.School.Features.Accounts.Parameters;

public record ItSpendingParameters : QueryParameters
{
    public string[] Urns { get; private set; } = [];
    public string Dimension { get; private set; } = Dimensions.Finance.Actuals;

    public override void SetValues(NameValueCollection query)
    {
        Urns = query["urns"]?.Split(',') ?? [];
        Dimension = query["dimension"] ?? Dimensions.Finance.Actuals;
    }
}