using Microsoft.AspNetCore.Mvc;
using Web.App.Domain;
using Web.App.Infrastructure.Apis;
using Web.App.Infrastructure.Apis.ChartRendering;
using Web.App.Infrastructure.Extensions;
using Web.App.ViewModels.Components;
// ReSharper disable UnusedAutoPropertyAccessor.Global

namespace Web.App.ViewComponents;

public class SchoolSpendingCostsViewComponent(IChartRenderingApi chartRenderingApi, ILogger<SchoolSpendingCostsViewComponent> logger) : ViewComponent
{
    public async Task<IViewComponentResult> InvokeAsync(
        IEnumerable<CostCategory> costs,
        string? id,
        string urn,
        bool hasIncompleteData,
        bool isCustomData,
        bool isPartOfTrust,
        bool renderSsrCharts)
    {
        var categories = new List<SchoolSpendingCostsViewModelCostCategory<PriorityCostCategoryDatum>>();
        var requests = new List<PostVerticalBarChartRequest<PriorityCostCategoryDatum>>();
        foreach (var costCategory in costs)
        {
            var data = costCategory.Values.Select(x => new PriorityCostCategoryDatum { Urn = x.Key, Amount = x.Value.Value }).ToArray();
            var filteredData = data.Where(x => x.Urn == urn || x.Amount > 0).ToArray();
            var hasNegativeOrZeroValues = data.Length > filteredData.Length;
            var uuid = Guid.NewGuid().ToString();

            categories.Add(new SchoolSpendingCostsViewModelCostCategory<PriorityCostCategoryDatum>
            {
                Uuid = uuid,
                Category = costCategory,
                HasNegativeOrZeroValues = hasNegativeOrZeroValues,
                Data = filteredData
            });

            if (renderSsrCharts)
            {
                // build collection of chart definitions to be resolved in a single API call
                requests.Add(new SchoolSpendingCostsVerticalBarChartRequest(uuid, urn, filteredData));
            }
        }

        ChartResponse[] charts = [];
        if (renderSsrCharts)
        {
            try
            {
                charts = await chartRenderingApi.PostVerticalBarCharts(new PostVerticalBarChartsRequest<PriorityCostCategoryDatum>(requests)).GetResultOrDefault<ChartResponse[]>() ?? [];
            }
            catch (Exception e)
            {
                logger.LogWarning(e, "Unable to load charts from API");
            }
        }

        foreach (var chart in charts)
        {
            var category = categories.FirstOrDefault(r => r.Uuid == chart.Id);
            if (category != null)
            {
                category.ChartSvg = chart.Html;
            }
        }

        return View(new SchoolSpendingCostsViewModel(id, urn, isPartOfTrust, isCustomData, hasIncompleteData, categories));
    }
}

public record SchoolSpendingCostsVerticalBarChartRequest : PostVerticalBarChartRequest<PriorityCostCategoryDatum>
{
    public SchoolSpendingCostsVerticalBarChartRequest(string uuid, string urn, PriorityCostCategoryDatum[] filteredData)
    {
        Data = filteredData;
        Height = 200;
        HighlightKey = urn;
        Id = uuid;
        KeyField = nameof(PriorityCostCategoryDatum.Urn).ToLower();
        Sort = "asc";
        Width = 630;
        ValueField = nameof(PriorityCostCategoryDatum.Amount).ToLower();
    }
}

public class PriorityCostCategoryDatum
{
    public string? Urn { get; init; }
    public decimal? Amount { get; init; }
}

public abstract class ChartResponse
{
    public string? Id { get; set; }
    public string? Html { get; set; }
}