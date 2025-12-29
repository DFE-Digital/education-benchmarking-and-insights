using System.Collections.Specialized;
using Platform.Functions;
using Platform.Functions.Extensions;

namespace Platform.Api.School.Features.MetricRagRatings.Parameters;

public record DetailParameters : QueryParameters
{
    public string[] Categories { get; set; } = [];
    public string[] Statuses { get; set; } = [];
    public string[] Urns { get; set; } = [];
    public string? CompanyNumber { get; set; }

    public override void SetValues(NameValueCollection query)
    {
        Statuses = query.ToStringArray("statuses");
        Categories = query.ToStringArray("categories");
        Urns = query.ToStringArray("urns");
        CompanyNumber = query["companyNumber"];
    }
}