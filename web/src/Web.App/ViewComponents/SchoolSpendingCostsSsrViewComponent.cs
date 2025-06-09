using Microsoft.AspNetCore.Mvc;
using Web.App.Domain;
using Web.App.Infrastructure.Apis;
using Web.App.Infrastructure.Apis.ChartRendering;
using Web.App.Infrastructure.Extensions;
using Web.App.ViewModels.Components;

// ReSharper disable ClassNeverInstantiated.Global
// ReSharper disable PropertyCanBeMadeInitOnly.Global
// ReSharper disable UnusedAutoPropertyAccessor.Global

namespace Web.App.ViewComponents;

public class SchoolSpendingCostsSsrViewComponent(IChartRenderingApi chartRenderingApi, ILogger<SchoolSpendingCostsViewComponent> logger) : ViewComponent
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

        return View(new SchoolSpendingCostsViewModel(id, urn, isPartOfTrust, isCustomData, hasIncompleteData, categories, resources));
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

public class ChartResponse
{
    public string? Id { get; set; }
    public string? Html { get; set; }
}