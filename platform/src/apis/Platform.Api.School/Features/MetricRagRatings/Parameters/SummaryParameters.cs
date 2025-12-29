using System.Collections.Specialized;
using Platform.Functions;
using Platform.Functions.Extensions;

namespace Platform.Api.School.Features.MetricRagRatings.Parameters;

public record SummaryParameters : QueryParameters
{
    public string[] Urns { get; set; } = [];
    public string? CompanyNumber { get; set; }
    public string? LaCode { get; set; }
    public string? OverallPhase { get; set; }

    public override void SetValues(NameValueCollection query)
    {
        Urns = query.ToStringArray("urns");
        CompanyNumber = query["companyNumber"];
        LaCode = query["laCode"];
        OverallPhase = query["overallPhase"];
    }
}