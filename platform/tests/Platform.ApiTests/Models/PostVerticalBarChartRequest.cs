// ReSharper disable UnusedAutoPropertyAccessor.Global

namespace Platform.ApiTests.Models;

public record PostVerticalBarChartRequest<T> : ChartRequest<T>
{
    public int? Height { get; set; }
}

public class PostVerticalBarChartsRequest<T>(IEnumerable<PostVerticalBarChartRequest<T>> collection) : List<PostVerticalBarChartRequest<T>>(collection);