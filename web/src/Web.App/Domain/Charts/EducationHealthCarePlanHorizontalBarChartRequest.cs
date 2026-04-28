using Microsoft.Azure.Cosmos.Linq;
using Web.App.Infrastructure.Apis;

// ReSharper disable ClassNeverInstantiated.Global

namespace Web.App.Domain.Charts;

public record EducationHealthCarePlanHorizontalBarChartRequest : PostHorizontalBarChartRequest<EducationHealthCarePlansComparisonDatum>
{
    public EducationHealthCarePlanHorizontalBarChartRequest(
        string uuid,
        string code,
        EducationHealthCarePlansComparisonDatum[] filteredData,
        Func<string, string?>? linkFormatter = null)
    {
        BarHeight = 22;
        Data = filteredData;
        HighlightKey = code;
        Id = uuid;
        KeyField = nameof(EducationHealthCarePlansComparisonDatum.Code).ToLower();
        LabelField = "name";
        LabelFormat = "%2$s";
        LinkFormat = linkFormatter == null
            ? string.Empty
            : linkFormatter.Invoke("%1$s");
        Sort = "desc";
        Width = 610;
        ValueField = nameof(EducationHealthCarePlansComparisonDatum.Plans).ToLower();
        ValueType = Web.App.Domain.Charts.ValueType.Numeric;
        XAxisLabel = EducationHealthCarePlanProperties.XAxisLabel;
    }
}

public class EducationHealthCarePlansComparisonDatum
{
    public string? Code { get; set; }
    public string? Name { get; set; }
    public decimal? Plans { get; set; }
    public decimal? TotalPupils { get; set; }
}