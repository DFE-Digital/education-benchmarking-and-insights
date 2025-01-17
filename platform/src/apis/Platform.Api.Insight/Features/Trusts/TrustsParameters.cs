using Microsoft.AspNetCore.Http;
using Platform.Functions;
using Platform.Functions.Extensions;
namespace Platform.Api.Insight.Trusts;

public record TrustsParameters : QueryParameters
{
    public string[] Trusts { get; private set; } = [];

    public override void SetValues(IQueryCollection query)
    {
        Trusts = query.ToStringArray("companyNumbers");
    }
}