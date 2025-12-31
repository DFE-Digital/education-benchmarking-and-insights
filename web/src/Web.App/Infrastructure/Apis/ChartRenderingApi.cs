namespace Web.App.Infrastructure.Apis;

public class ChartRenderingApi(HttpClient httpClient, string? key = null) : ApiBase(httpClient, key), IChartRenderingApi
{
    public Task<ApiResult> PostHorizontalBarChart<T>(PostHorizontalBarChartRequest<T> request, CancellationToken cancellationToken = default)
    {
        var content = new JsonContent(request);
        content.Headers.Add("x-accept", "image/svg+xml");
        return PostAsync(Routes.HorizontalBarChart, content, cancellationToken);
    }

    public Task<ApiResult> PostHorizontalBarCharts<T>(PostHorizontalBarChartsRequest<T> request, CancellationToken cancellationToken = default) => PostAsync(Routes.HorizontalBarChart, new JsonContent(request.ToArray()), cancellationToken);

    public Task<ApiResult> PostVerticalBarChart<T>(PostVerticalBarChartRequest<T> request, CancellationToken cancellationToken = default)
    {
        var content = new JsonContent(request);
        content.Headers.Add("x-accept", "image/svg+xml");
        return PostAsync(Routes.VerticalBarChart, content, cancellationToken);
    }

    public Task<ApiResult> PostVerticalBarCharts<T>(PostVerticalBarChartsRequest<T> request, CancellationToken cancellationToken = default) => PostAsync(Routes.VerticalBarChart, new JsonContent(request.ToArray()), cancellationToken);

    private static class Routes
    {
        public static string HorizontalBarChart => "api/horizontalBarChart";
        public static string VerticalBarChart => "api/verticalBarChart";
    }
}

public interface IChartRenderingApi
{
    Task<ApiResult> PostHorizontalBarChart<T>(PostHorizontalBarChartRequest<T> request, CancellationToken cancellationToken = default);
    Task<ApiResult> PostHorizontalBarCharts<T>(PostHorizontalBarChartsRequest<T> request, CancellationToken cancellationToken = default);
    Task<ApiResult> PostVerticalBarChart<T>(PostVerticalBarChartRequest<T> request, CancellationToken cancellationToken = default);
    Task<ApiResult> PostVerticalBarCharts<T>(PostVerticalBarChartsRequest<T> request, CancellationToken cancellationToken = default);
}