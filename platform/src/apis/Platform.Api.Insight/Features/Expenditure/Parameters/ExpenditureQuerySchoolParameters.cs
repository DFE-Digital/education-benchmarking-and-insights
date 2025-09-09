using System.Collections.Specialized;

namespace Platform.Api.Insight.Features.Expenditure.Parameters;

public record ExpenditureQuerySchoolParameters : ExpenditureParameters
{
    public string[] Urns { get; private set; } = [];
    public string? Phase { get; private set; }
    public string? CompanyNumber { get; private set; }
    public string? LaCode { get; private set; }

    public override void SetValues(NameValueCollection query)
    {
        base.SetValues(query);

        Urns = query["urns"]?.Split(',') ?? [];
        CompanyNumber = query["companyNumber"];
        Phase = query["phase"];
        LaCode = query["laCode"];
    }
}