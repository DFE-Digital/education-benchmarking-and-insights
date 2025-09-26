// ReSharper disable UnusedAutoPropertyAccessor.Global
// ReSharper disable UnusedMember.Global

namespace Platform.ApiTests.Models;

public record PostHorizontalBarChartRequest<T> : ChartRequest<T>
{
    public int? BarHeight { get; set; }
    public string? LabelField { get; set; }
    public string? LabelFormat { get; set; }
    public string? LinkFormat { get; set; }
    public string? MissingDataLabel { get; set; }
    public int? MissingDataLabelWidth { get; set; }
    public decimal? PaddingInner { get; set; }
    public decimal? PaddingOuter { get; set; }
    public string? ValueType { get; set; }
    public string? XAxisLabel { get; set; }
}

public class PostHorizontalBarChartsRequest<T>(IEnumerable<PostHorizontalBarChartRequest<T>> collection) : List<PostHorizontalBarChartRequest<T>>(collection);