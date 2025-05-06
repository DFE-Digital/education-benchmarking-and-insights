using System.Collections.Specialized;
using Platform.Functions;
using Platform.Functions.Extensions;

namespace Platform.Api.Insight.Features.MetricRagRatings.Parameters;

public record MetricRagRatingsParameters : QueryParameters
{
    public string[] Categories { get; set; } = [];
    public string[] Statuses { get; set; } = [];
    public string[] Urns { get; set; } = [];
    public string? Phase { get; set; }
    public string? CompanyNumber { get; set; }
    public string? LaCode { get; set; }

    public override void SetValues(NameValueCollection query)
    {
        Statuses = query.ToStringArray("statuses");
        Categories = query.ToStringArray("categories");
        Urns = query.ToStringArray("urns");
        CompanyNumber = query["companyNumber"];
        Phase = query["phase"];
        LaCode = query["laCode"];
    }
}