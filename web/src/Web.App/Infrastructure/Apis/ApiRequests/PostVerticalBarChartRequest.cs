// ReSharper disable ClassNeverInstantiated.Global
// ReSharper disable UnusedAutoPropertyAccessor.Global
// ReSharper disable UnusedMember.Global
namespace Web.App.Infrastructure.Apis;

public record PostVerticalBarChartRequest<T>
{
    public string? KeyField { get; set; }
    public string? ValueField { get; set; }
    public string? HighlightKey { get; set; }
    public string? Sort { get; set; }
    public int? Height { get; set; }
    public string? Id { get; set; }
    public int? Width { get; set; }
    public T[]? Data { get; set; }
}

public class PostVerticalBarChartsRequest<T>(IEnumerable<PostVerticalBarChartRequest<T>> collection) : List<PostVerticalBarChartRequest<T>>(collection);