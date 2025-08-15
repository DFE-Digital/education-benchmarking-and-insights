using Web.App.Infrastructure.Apis;

// ReSharper disable ClassNeverInstantiated.Global

namespace Web.App.Domain.Charts;

public record SchoolComparisonItSpendHorizontalBarChartRequest : PostHorizontalBarChartRequest<SchoolComparisonDatum>
{
    public SchoolComparisonItSpendHorizontalBarChartRequest(
        string uuid,
        string urn,
        SchoolComparisonDatum[] filteredData,
        Func<string, string?> linkFormatter,
        Dimensions.ResultAsOptions resultsAs)
    {
        BarHeight = 22;
        Data = filteredData;
        HighlightKey = urn;
        Id = uuid;
        KeyField = nameof(SchoolComparisonDatum.Urn).ToLower();
        LabelField = "schoolName";
        LabelFormat = "%2$s";
        LinkFormat = linkFormatter("%1$s");
        Sort = "desc";
        Width = 610;
        ValueField = nameof(SchoolComparisonDatum.Expenditure).ToLower();
        ValueType = resultsAs.GetValueType();
        XAxisLabel = resultsAs.GetXAxisLabel();

        var partYear = filteredData
            .Where(x => x.PeriodCoveredByReturn is not 12)
            .Select(x => x.Urn!)
            .ToArray();
        if (partYear.Length > 0)
        {
            GroupedKeys = new ChartRequestGroupedKeys
            {
                [GroupType.PartYear] = partYear
            };
        }
    }
}

public class SchoolComparisonDatum
{
    public string? Urn { get; init; }
    public string? SchoolName { get; init; }
    public decimal? Expenditure { get; init; }
    public string? LAName { get; init; }
    public string? SchoolType { get; init; }
    public decimal? TotalPupils { get; init; }
    public int? PeriodCoveredByReturn { get; init; }
}