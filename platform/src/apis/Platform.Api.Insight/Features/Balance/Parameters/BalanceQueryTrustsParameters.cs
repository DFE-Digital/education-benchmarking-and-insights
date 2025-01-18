using System.Collections.Specialized;

namespace Platform.Api.Insight.Features.Balance.Parameters;

public record BalanceQueryTrustsParameters : BalanceParameters
{
    public string[] Trusts { get; private set; } = [];

    public override void SetValues(NameValueCollection query)
    {
        base.SetValues(query);

        Trusts = query["companyNumbers"]?.Split(',') ?? [];
    }
}