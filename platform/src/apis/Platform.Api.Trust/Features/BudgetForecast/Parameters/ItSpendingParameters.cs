using System.Collections.Specialized;
using Platform.Functions;
using Platform.Functions.Extensions;

namespace Platform.Api.Trust.Features.BudgetForecast.Parameters;

public record ItSpendingParameters : QueryParameters
{
    public string[] CompanyNumbers { get; private set; } = [];

    public override void SetValues(NameValueCollection query)
    {
        CompanyNumbers = query.ToStringArray("companyNumbers");
    }
}