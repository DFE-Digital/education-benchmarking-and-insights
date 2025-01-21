using System.Collections.Specialized;
using Platform.Functions;

namespace Platform.Api.Insight.Features.Trusts.Parameters;

public record TrustsParameters : QueryParameters
{
    public string[] Trusts { get; private set; } = [];

    public override void SetValues(NameValueCollection query)
    {
        Trusts = query["companyNumbers"]?.Split(',') ?? [];
    }
}