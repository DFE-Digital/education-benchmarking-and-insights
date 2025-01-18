using System.Collections.Specialized;

namespace Platform.Api.Insight.Features.Expenditure.Parameters;

public record ExpenditureQueryTrustParameters : ExpenditureParameters
{
    public bool ExcludeCentralServices { get; private set; }
    public string[] CompanyNumbers { get; private set; } = [];

    public override void SetValues(NameValueCollection query)
    {
        base.SetValues(query);

        CompanyNumbers = query["companyNumbers"]?.Split(',') ?? [];
        ExcludeCentralServices = bool.TryParse(query["excludeCentralServices"], out var result) && result;
    }
}