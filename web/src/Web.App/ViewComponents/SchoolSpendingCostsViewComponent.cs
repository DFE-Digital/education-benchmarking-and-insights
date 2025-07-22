using Microsoft.AspNetCore.Mvc;
using Web.App.Domain;
using Web.App.Domain.Content;
using Web.App.Services;
using Web.App.ViewModels.Components;

namespace Web.App.ViewComponents;

public class SchoolSpendingCostsViewComponent(ICostCodesService costCodesService) : ViewComponent
{
    public async Task<IViewComponentResult> InvokeAsync(
        IEnumerable<CostCategory> costs,
        Dictionary<string, CommercialResourceLink[]> resources,
        string? id,
        string urn,
        bool hasIncompleteData,
        bool isCustomData,
        bool isPartOfTrust)
    {
        var categories = new SchoolSpendingCostsViewModelCostCategories(urn, costs);
        var costCodes = await costCodesService.GetCostCodes(isPartOfTrust);
        return View(new SchoolSpendingCostsViewModel(id, urn, isPartOfTrust, isCustomData, hasIncompleteData, categories, resources, costCodes));
    }
}