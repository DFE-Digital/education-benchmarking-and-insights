// ReSharper disable UnusedAutoPropertyAccessor.Global
// ReSharper disable UnusedMember.Global

namespace Platform.ApiTests.Models;

public record PostHorizontalStackedBarChartRequest<T> : ChartRequest<T>
{
    public int? BarHeight { get; set; }
    public Dictionary<string, string[]>? GroupedKeys { get; set; }
    public string? LabelField { get; set; }
    public string? LabelFormat { get; set; }
    public string? LinkFormat { get; set; }
    public string? MissingDataLabel { get; set; }
    public int? MissingDataLabelWidth { get; set; }
    public decimal? PaddingInner { get; set; }
    public decimal? PaddingOuter { get; set; }
    public string[]? ValueField { get; set; }
    public string[]? LegendLabels { get; set; }
    public string? ValueType { get; set; }
    public string? XAxisLabel { get; set; }
}

public class PostHorizontalStackedBarChartsRequest<T>(IEnumerable<PostHorizontalStackedBarChartRequest<T>> collection) : List<PostHorizontalStackedBarChartRequest<T>>(collection);