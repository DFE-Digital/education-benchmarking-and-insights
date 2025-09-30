using System.Collections.Specialized;
using Platform.Functions;
using Platform.Functions.Extensions;

namespace Platform.Api.Insight.Features.ItSpend.Parameters;

public record ItSpendTrustsParameters : QueryParameters
{
    public string[] CompanyNumbers { get; private set; } = [];

    public override void SetValues(NameValueCollection query)
    {
        CompanyNumbers = query.ToStringArray("companyNumbers");
    }
}