using Microsoft.AspNetCore.Http;
using Platform.Functions;
using Platform.Functions.Extensions;
namespace Platform.Api.Insight.MetricRagRatings;

public record MetricRagRatingsParameters : QueryParameters
{
    public string[] Categories { get; internal set; } = [];
    public string[] Statuses { get; internal set; } = [];
    public string[] Urns { get; internal set; } = [];
    public string? Phase { get; internal set; }
    public string? CompanyNumber { get; internal set; }
    public string? LaCode { get; internal set; }

    public override void SetValues(IQueryCollection query)
    {
        Statuses = query.ToStringArray("statuses");
        Categories = query.ToStringArray("categories");
        Urns = query.ToStringArray("urns");
        CompanyNumber = query["companyNumber"].ToString();
        Phase = query["phase"].ToString();
        LaCode = query["laCode"].ToString();
    }
}