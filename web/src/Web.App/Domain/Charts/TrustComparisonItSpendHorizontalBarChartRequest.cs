using Web.App.Infrastructure.Apis;

// ReSharper disable ClassNeverInstantiated.Global

namespace Web.App.Domain.Charts;

public record TrustComparisonItSpendHorizontalBarChartRequest : PostHorizontalBarChartRequest<TrustComparisonDatum>
{
    public TrustComparisonItSpendHorizontalBarChartRequest(
        string uuid,
        string companyNumber,
        TrustComparisonDatum[] filteredData,
        Func<string, string?> linkFormatter,
        Dimensions.ResultAsOptions resultsAs)
    {
        BarHeight = 24;
        Data = filteredData;
        HighlightKey = companyNumber;
        Id = uuid;
        KeyField = nameof(TrustComparisonDatum.CompanyNumber).ToLower();
        LabelField = "trustName";
        LabelFormat = "%2$s";
        LinkFormat = linkFormatter("%1$s");
        MissingDataLabel = "No data submitted";
        MissingDataLabelWidth = 115;
        PaddingInner = 0.6m;
        PaddingOuter = 0.2m;
        Sort = "desc";
        Width = 610;
        ValueField = nameof(TrustComparisonDatum.Expenditure).ToLower();
        ValueType = resultsAs.GetValueType();
        XAxisLabel = resultsAs.GetXAxisLabel();
    }
}

public class TrustComparisonDatum
{
    public string? CompanyNumber { get; init; }
    public string? TrustName { get; init; }
    public decimal? Expenditure { get; init; }
}