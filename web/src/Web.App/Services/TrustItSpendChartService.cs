using Web.App.Domain.Charts;
using Web.App.Infrastructure.Apis;
using Web.App.Infrastructure.Extensions;
using Web.App.ViewModels;

namespace Web.App.Services;

public interface ITrustItSpendChartService
{
    Task<ChartResponse[]> BuildChartsAsync(
        string companyNumber,
        Dimensions.ResultAsOptions resultAs,
        TrustComparisonSubCategoriesViewModel subCategories,
        Func<string, string> buildUrl);

    Task<ChartResponse[]> BuildForecastChartsAsync(Dimensions.ResultAsOptions resultAs, TrustComparisonSubCategoriesViewModel subCategories);
}

public class TrustItSpendChartService(
    IChartRenderingApi chartRenderingApi,
    ILogger<TrustItSpendChartService> logger) : ITrustItSpendChartService
{
    public async Task<ChartResponse[]> BuildChartsAsync(
        string companyNumber,
        Dimensions.ResultAsOptions resultAs,
        TrustComparisonSubCategoriesViewModel subCategories,
        Func<string, string> buildUrl)
    {
        var requests = subCategories.Items.Select(c => new TrustComparisonItSpendHorizontalBarChartRequest(
            c.Uuid!,
            companyNumber,
            c.Data!,
            buildUrl,
            resultAs,
            c.ForecastData is not { Length: not 0 }
                ? null
                : Math.Min(c.Data?.Min(d => d.Expenditure) ?? 0, c.ForecastData?.Min(d => d.Expenditure) ?? 0),
            c.ForecastData is not { Length: not 0 }
                ? null
                : Math.Max(c.Data?.Max(d => d.Expenditure) ?? 0, c.ForecastData?.Max(d => d.Expenditure) ?? 0)
        ));

        try
        {
            return await chartRenderingApi
                .PostHorizontalBarCharts(new PostHorizontalBarChartsRequest<TrustComparisonDatum>(requests))
                .GetResultOrDefault<ChartResponse[]>() ?? [];
        }
        catch (Exception e)
        {
            logger.LogWarning(e, "Unable to load charts from API");
            return [];
        }
    }

    public async Task<ChartResponse[]> BuildForecastChartsAsync(Dimensions.ResultAsOptions resultAs, TrustComparisonSubCategoriesViewModel subCategories)
    {
        var requests = subCategories.Items
            .Where(c => c.ForecastData != null)
            .Select(c => new TrustForecastItSpendHorizontalBarChartRequest(
                c.Uuid!,
                c.ForecastData!,
                resultAs,
                Math.Min(c.Data?.Min(d => d.Expenditure) ?? 0, c.ForecastData?.Min(d => d.Expenditure) ?? 0),
                Math.Max(c.Data?.Max(d => d.Expenditure) ?? 0, c.ForecastData?.Max(d => d.Expenditure) ?? 0)
            ))
            .Where(r => r.Data != null && r.Data.Length != 0);

        try
        {
            return await chartRenderingApi
                .PostHorizontalBarCharts(new PostHorizontalBarChartsRequest<TrustForecastDatum>(requests))
                .GetResultOrDefault<ChartResponse[]>() ?? [];
        }
        catch (Exception e)
        {
            logger.LogWarning(e, "Unable to load forecast charts from API");
            return [];
        }
    }
}