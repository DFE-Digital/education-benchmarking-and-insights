using Web.App.Infrastructure.Apis;

// ReSharper disable ClassNeverInstantiated.Global

namespace Web.App.Domain.Charts;

public record SchoolComparisonItSpendHorizontalBarChartRequest : PostHorizontalBarChartRequest<SchoolComparisonDatum>
{
    public SchoolComparisonItSpendHorizontalBarChartRequest(string uuid, string urn, SchoolComparisonDatum[] filteredData, Func<string, string?> linkFormatter)
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
        Width = 960;
        ValueField = nameof(SchoolComparisonDatum.Expenditure).ToLower();
        ValueFormat = "$,~s";
        XAxisLabel = "£ per pupil";
    }
}

public class SchoolComparisonDatum
{
    public string? Urn { get; init; }
    public string? SchoolName { get; init; }
    public decimal? Expenditure { get; init; }
}