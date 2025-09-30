using System.Collections.Specialized;
using Platform.Functions;

namespace Platform.Api.Insight.Features.ItSpend.Parameters;

public record ItSpendTrustsParameters : QueryParameters
{
    public string[] CompanyNumbers { get; private set; } = [];

    public override void SetValues(NameValueCollection query)
    {
        CompanyNumbers = query["companyNumbers"]?.Split(',') ?? [];
    }
}