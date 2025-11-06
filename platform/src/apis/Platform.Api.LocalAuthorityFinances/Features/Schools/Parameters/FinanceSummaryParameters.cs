using System.Collections.Specialized;
using Platform.Api.LocalAuthorityFinances.Features.Schools.Models;
using Platform.Domain;
using Platform.Functions;
using Platform.Functions.Extensions;

namespace Platform.Api.LocalAuthorityFinances.Features.Schools.Parameters;

public record FinanceSummaryParameters : QueryParameters
{
    public string Dimension { get; private set; } = Dimensions.Finance.Actuals;
    public string SortField { get; private set; } = FinanceSummarySortFields.SchoolName;
    public string SortOrder { get; private set; } = SortDirection.Asc;
    public string[] OverallPhase { get; private set; } = [];
    public string[] NurseryProvision { get; private set; } = [];
    public string[] SixthFormProvision { get; private set; } = [];
    public string[] SpecialClassesProvision { get; private set; } = [];
    public string? Limit { get; private set; }

    public override void SetValues(NameValueCollection query)
    {
        Dimension = query["dimension"] ?? Dimensions.Finance.Actuals;
        SortField = query["sortField"] ?? FinanceSummarySortFields.SchoolName;
        SortOrder = query["sortOrder"] ?? SortDirection.Asc;
        OverallPhase = query.ToStringArray("overallPhase");
        NurseryProvision = query.ToStringArray("nurseryProvision");
        SixthFormProvision = query.ToStringArray("sixthFormProvision");
        SpecialClassesProvision = query.ToStringArray("specialClassesProvision");
        Limit = query["limit"];
    }
}