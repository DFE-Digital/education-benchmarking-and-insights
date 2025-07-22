// ReSharper disable UnusedAutoPropertyAccessor.Global
namespace Platform.ApiTests.Models;

public record PostHorizontalBarChartRequest<T> : ChartRequest<T>
{
    public int? BarHeight { get; set; }
    public string? LabelField { get; set; }
    public string? LabelFormat { get; set; }
    public string? LinkFormat { get; set; }
    public string? ValueFormat { get; set; }
    public string? XAxisLabel { get; set; }
}

public class PostHorizontalBarChartsRequest<T>(IEnumerable<PostHorizontalBarChartRequest<T>> collection) : List<PostHorizontalBarChartRequest<T>>(collection);