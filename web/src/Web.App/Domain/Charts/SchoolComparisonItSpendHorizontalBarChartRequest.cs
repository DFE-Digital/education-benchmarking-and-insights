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
        ChartDimensions dimension)
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
        Width = 600;
        ValueField = nameof(SchoolComparisonDatum.Expenditure).ToLower();
        (ValueFormat, XAxisLabel) = dimension switch
        {
            ChartDimensions.PerUnit => ("$,~s", "£ per pupil"),
            ChartDimensions.Actuals => ("$,~s", "actuals"),
            ChartDimensions.PercentExpenditure => (".1%", "percentage of expenditure"),
            ChartDimensions.PercentIncome => (".1%", "percentage of income"),
            _ => throw new ArgumentOutOfRangeException(nameof(dimension))
        };
    }
}

public class SchoolComparisonDatum
{
    public string? Urn { get; init; }
    public string? SchoolName { get; init; }
    public decimal? Expenditure { get; init; }
}