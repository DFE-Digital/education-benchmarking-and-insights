using Microsoft.AspNetCore.Http;
using Platform.Functions.Extensions;
namespace Platform.Api.Insight.Expenditure;

public record QuerySchoolExpenditureParameters : ExpenditureParameters
{
    public string[] Urns { get; internal set; } = [];
    public string[] CompanyNumbers { get; internal set; } = [];
    public string? Phase { get; internal set; }
    public string? CompanyNumber { get; internal set; }
    public string? LaCode { get; internal set; }

    public override void SetValues(IQueryCollection query)
    {
        base.SetValues(query);
        Urns = query.ToStringArray("urns");
        CompanyNumbers = query.ToStringArray("companyNumbers");
        CompanyNumber = query["companyNumber"].ToString();
        Phase = query["phase"].ToString();
        LaCode = query["laCode"].ToString();
    }
}