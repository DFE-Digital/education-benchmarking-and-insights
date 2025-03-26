using Microsoft.AspNetCore.Mvc;
using Web.App.Domain;
using Web.App.Infrastructure.Apis;
using Web.App.Infrastructure.Apis.ChartRendering;
using Web.App.Infrastructure.Extensions;
using Web.App.ViewModels.Components;

namespace Web.App.ViewComponents;

public class PriorityCostCategoryViewComponent(IChartRenderingApi chartRenderingApi) : ViewComponent
{
    public async Task<IViewComponentResult> InvokeAsync(
        string? id,
        string urn,
        bool isPartOfTrust,
        bool isCustomData,
        bool hasIncompleteData,
        CostCategory? costCategory,
        bool isFirstItem)
    {
        var data = costCategory?.Values.Select(x => new PriorityCostCategoryDatum { Urn = x.Key, Amount = x.Value.Value }).ToArray() ?? [];
        var filteredData = data.Where(x => x.Urn == urn || x.Amount > 0).ToArray();
        var hasNegativeOrZeroValues = data.Length > filteredData.Length;
        var uuid = Guid.NewGuid().ToString();
        string? chart = null;

        if (filteredData.Length > 0)
        {
            // multiple API calls on first pass
            chart = await chartRenderingApi.PostVerticalBarChart(new PostVerticalBarChartRequest<PriorityCostCategoryDatum>
            {
                Data = filteredData,
                Height = 200,
                HighlightKey = urn,
                Id = uuid,
                KeyField = nameof(PriorityCostCategoryDatum.Urn).ToLower(),
                Sort = "asc",
                Width = 630,
                ValueField = nameof(PriorityCostCategoryDatum.Amount).ToLower()
            }).GetString(_ => string.Empty);
        }

        return View(new PriorityCostCategoryViewModel(id, uuid, urn, isPartOfTrust, isCustomData, hasIncompleteData, hasNegativeOrZeroValues, costCategory, isFirstItem, chart));
    }
}

public class PriorityCostCategoryDatum
{
    public string Urn { get; set; }
    public decimal? Amount { get; set; }
}