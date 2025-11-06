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
        Dimensions.ResultAsOptions resultsAs,
        decimal? domainMin,
        decimal? domainMax)
    {
        BarHeight = 24;
        Data = filteredData;
        DomainMax = domainMax == 0 ? null : domainMax;
        DomainMin = domainMin == 0 ? null : domainMin;
        HighlightKey = companyNumber;
        Id = uuid;
        KeyField = nameof(companyNumber);
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

public record TrustForecastItSpendHorizontalBarChartRequest : PostHorizontalBarChartRequest<TrustForecastDatum>
{
    public TrustForecastItSpendHorizontalBarChartRequest(
        string uuid,
        TrustForecastDatum[] forecastData,
        Dimensions.ResultAsOptions resultsAs,
        decimal? domainMin,
        decimal? domainMax)
    {
        BarHeight = 24;
        Data = forecastData;
        DomainMax = domainMax == 0 ? null : domainMax;
        DomainMin = domainMin == 0 ? null : domainMin;
        Id = uuid;
        KeyField = "yearLabel";
        PaddingInner = 0.6m;
        PaddingOuter = 0.2m;
        Width = 610;
        ValueField = nameof(TrustForecastDatum.Expenditure).ToLower();
        ValueType = resultsAs.GetValueType();
        XAxisLabel = resultsAs.GetXAxisLabel();

        var years = forecastData
            .Where(f => f.Year != null)
            .Select(f => f.YearLabel!)
            .Distinct()
            .OrderBy(y => y)
            .ToArray();
        if (years.Length == 3)
        {
            GroupedKeys = new ChartRequestGroupedKeys
            {
                [GroupType.Previous] = [years[0]],
                [GroupType.Current] = [years[1]],
                [GroupType.Forecast] = [years[2]]
            };
        }
    }
}

public class TrustComparisonDatum
{
    public string? CompanyNumber { get; init; }
    public string? TrustName { get; init; }
    public decimal? Expenditure { get; init; }
}

public class TrustForecastDatum
{
    public int? Year { get; init; }
    public string? YearLabel => Year == null ? null : $"{Year - 1} – {Year}";
    public decimal? Expenditure { get; init; }
}