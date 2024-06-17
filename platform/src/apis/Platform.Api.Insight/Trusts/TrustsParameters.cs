using System;
using Microsoft.AspNetCore.Http;
using Platform.Functions;
using Platform.Functions.Extensions;

namespace Platform.Api.Insight.Trusts;

public record TrustsParameters : QueryParameters
{
    public string[] Truts { get; private set; } = Array.Empty<string>();

    public override void SetValues(IQueryCollection query)
    {
        Truts = query.ToStringArray("companyNumbers");
    }
}