using System.Collections.Specialized;
using Platform.Api.LocalAuthority.Features.Details.Models;
using Platform.Domain;
using Platform.Functions;
using Platform.Functions.Extensions;

namespace Platform.Api.LocalAuthority.Features.Details.Parameters;

public record WorkforceSummaryParameters : QueryParameters
{
    public string Dimension { get; private set; } = Dimensions.SchoolsSummaryWorkforce.Actuals;
    public string SortField { get; private set; } = WorkforceSummarySortFields.SchoolName;
    public string SortOrder { get; private set; } = SortDirection.Asc;
    public string[] OverallPhase { get; private set; } = [];
    public string[] NurseryProvision { get; private set; } = [];
    public string[] SixthFormProvision { get; private set; } = [];
    public string[] SpecialClassesProvision { get; private set; } = [];
    public string? Limit { get; private set; }

    public override void SetValues(NameValueCollection query)
    {
        Dimension = query["dimension"] ?? Dimensions.SchoolsSummaryWorkforce.Actuals;
        SortField = query["sortField"] ?? WorkforceSummarySortFields.SchoolName;
        SortOrder = query["sortOrder"] ?? SortDirection.Asc;
        OverallPhase = query.ToStringArray("overallPhase");
        NurseryProvision = query.ToStringArray("nurseryProvision");
        SixthFormProvision = query.ToStringArray("sixthFormProvision");
        SpecialClassesProvision = query.ToStringArray("specialClassesProvision");
        Limit = query["limit"];
    }
}