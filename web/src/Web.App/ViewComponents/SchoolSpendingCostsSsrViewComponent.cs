using Microsoft.AspNetCore.Mvc;
using Web.App.Domain;
using Web.App.Domain.Charts;
using Web.App.Domain.Content;
using Web.App.Infrastructure.Apis;
using Web.App.Infrastructure.Apis.ChartRendering;
using Web.App.Infrastructure.Extensions;
using Web.App.Services;
using Web.App.ViewModels.Components;

namespace Web.App.ViewComponents;

public class SchoolSpendingCostsSsrViewComponent(
    IChartRenderingApi chartRenderingApi,
    ILogger<SchoolSpendingCostsViewComponent> logger,
    ICostCodesService costCodesService) : ViewComponent
{
    public async Task<IViewComponentResult> InvokeAsync(
        IEnumerable<CostCategory> costs,
        string? id,
        string urn,
        bool hasIncompleteData,
        bool isCustomData,
        bool isPartOfTrust,
        Dictionary<string, CommercialResourceLink[]> resources)
    {
        var categories = new SchoolSpendingCostsViewModelCostCategories(urn, costs);
        var requests = categories
            .Select(c => new SchoolSpendingCostsVerticalBarChartRequest(c.Uuid!, urn, c.Data!));

        ChartResponse[] charts = [];
        try
        {
            charts = await chartRenderingApi.PostVerticalBarCharts(new PostVerticalBarChartsRequest<PriorityCostCategoryDatum>(requests)).GetResultOrDefault<ChartResponse[]>() ?? [];
        }
        catch (Exception e)
        {
            logger.LogWarning(e, "Unable to load charts from API");
        }

        foreach (var chart in charts)
        {
            var category = categories.FirstOrDefault(r => r.Uuid == chart.Id);
            if (category != null)
            {
                category.ChartSvg = chart.Html;
            }
        }

        var costCodes = await costCodesService.GetCostCodes(isPartOfTrust);
        return View(new SchoolSpendingCostsViewModel(id, urn, isPartOfTrust, isCustomData, hasIncompleteData, categories, resources, costCodes));
    }
}