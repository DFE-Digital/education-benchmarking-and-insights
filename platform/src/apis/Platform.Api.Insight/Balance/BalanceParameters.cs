using System;
using Microsoft.AspNetCore.Http;
using Platform.Functions;
using Platform.Functions.Extensions;

namespace Platform.Api.Insight.Balance;

public record BalanceParameters : QueryParameters
{
    public bool ExcludeCentralServices { get; private set; }
    public string Dimension { get; private set; } = BalanceDimensions.Actuals;
    public string[] Schools { get; private set; } = Array.Empty<string>();
    public string[] Trusts { get; private set; } = Array.Empty<string>();

    public override void SetValues(IQueryCollection query)
    {
        var dimension = query["dimension"].ToString();
        if (!BalanceDimensions.IsValid(dimension) || string.IsNullOrWhiteSpace(dimension))
        {
            dimension = BalanceDimensions.Actuals;
        }

        ExcludeCentralServices = query.ToBool("excludeCentralServices");
        Dimension = dimension;
        Schools = query.ToStringArray("urns");
        Trusts = query.ToStringArray("companyNumbers");
    }
}