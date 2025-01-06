using Microsoft.AspNetCore.Http;
using Platform.Functions;
using Platform.Functions.Extensions;

namespace Platform.Api.Insight.Balance;

public record BalanceParameters : QueryParameters
{
    public string Dimension { get; internal set; } = BalanceDimensions.Actuals;
    public string[] Trusts { get; private set; } = [];

    public override void SetValues(IQueryCollection query)
    {
        var dimension = query["dimension"].ToString();
        if (!BalanceDimensions.IsValid(dimension) || string.IsNullOrWhiteSpace(dimension))
        {
            dimension = BalanceDimensions.Actuals;
        }

        Dimension = dimension;
        Trusts = query.ToStringArray("companyNumbers");
    }
}