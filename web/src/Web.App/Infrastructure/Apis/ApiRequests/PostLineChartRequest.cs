// ReSharper disable UnusedAutoPropertyAccessor.Global

namespace Web.App.Infrastructure.Apis;

public record PostLineChartRequest<T> : ChartRequest<T>
{
    public int? Height { get; set; }
    public string? XAxisLabel { get; set; }
    public bool ShowValueDots { get; set; }
    public bool ShowValueLabels { get; set; }
}

public class PostLineChartsRequest<T>(IEnumerable<PostLineChartRequest<T>> collection) : List<PostLineChartRequest<T>>(collection);
