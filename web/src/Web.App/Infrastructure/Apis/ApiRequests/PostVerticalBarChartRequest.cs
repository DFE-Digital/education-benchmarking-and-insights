// ReSharper disable ClassNeverInstantiated.Global
// ReSharper disable UnusedAutoPropertyAccessor.Global
// ReSharper disable UnusedMember.Global

namespace Web.App.Infrastructure.Apis;

public record PostVerticalBarChartRequest<T> : ChartRequest<T>
{
    public int? Height { get; set; }
}

public class PostVerticalBarChartsRequest<T>(IEnumerable<PostVerticalBarChartRequest<T>> collection) : List<PostVerticalBarChartRequest<T>>(collection);