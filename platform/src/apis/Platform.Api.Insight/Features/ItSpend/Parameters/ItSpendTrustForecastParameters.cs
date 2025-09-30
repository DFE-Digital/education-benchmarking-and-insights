using System.Collections.Specialized;
using Platform.Functions;

namespace Platform.Api.Insight.Features.ItSpend.Parameters;

public record ItSpendTrustForecastParameters : QueryParameters
{
    public string? CompanyNumber { get; private set; }
    public string? Year { get; private set; }

    public override void SetValues(NameValueCollection query)
    {
        CompanyNumber = query["companyNumber"];
        Year = query["year"];
    }
}