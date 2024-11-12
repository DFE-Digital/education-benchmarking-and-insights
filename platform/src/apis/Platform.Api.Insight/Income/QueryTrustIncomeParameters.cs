using Microsoft.AspNetCore.Http;
using Platform.Functions.Extensions;
namespace Platform.Api.Insight.Income;

public record QueryTrustIncomeParameters : IncomeParameters
{
    public string[] CompanyNumbers { get; internal set; } = [];

    public override void SetValues(IQueryCollection query)
    {
        base.SetValues(query);
        CompanyNumbers = query.ToStringArray("companyNumbers");
    }
}