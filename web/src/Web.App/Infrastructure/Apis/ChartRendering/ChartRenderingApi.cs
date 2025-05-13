namespace Web.App.Infrastructure.Apis.ChartRendering;

public class ChartRenderingApi(HttpClient httpClient, string? key = default) : ApiBase(httpClient, key), IChartRenderingApi
{
    public Task<ApiResult> PostVerticalBarChart<T>(PostVerticalBarChartRequest<T> request, CancellationToken cancellationToken = default)
    {
        var content = new JsonContent(request);
        content.Headers.Add("x-accept", "image/svg+xml");
        return PostAsync(Api.Charts.VerticalBarChart, content, cancellationToken);
    }

    public Task<ApiResult> PostVerticalBarCharts<T>(PostVerticalBarChartsRequest<T> request, CancellationToken cancellationToken = default)
    {
        return PostAsync(Api.Charts.VerticalBarChart, new JsonContent(request.ToArray()), cancellationToken);
    }
}

public interface IChartRenderingApi
{
    Task<ApiResult> PostVerticalBarChart<T>(PostVerticalBarChartRequest<T> request, CancellationToken cancellationToken = default);
    Task<ApiResult> PostVerticalBarCharts<T>(PostVerticalBarChartsRequest<T> request, CancellationToken cancellationToken = default);
}