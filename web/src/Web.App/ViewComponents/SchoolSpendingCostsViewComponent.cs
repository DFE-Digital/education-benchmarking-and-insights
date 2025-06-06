using Microsoft.AspNetCore.Mvc;
using Web.App.Domain;
using Web.App.ViewModels.Components;

namespace Web.App.ViewComponents;

public class SchoolSpendingCostsViewComponent : ViewComponent
{
    public IViewComponentResult Invoke(
        IEnumerable<CostCategory> costs,
        Dictionary<string, CommercialResourceLink[]> resources,
        string? id,
        string urn,
        bool hasIncompleteData,
        bool isCustomData,
        bool isPartOfTrust,
        bool isMat)
    {
        var categories = new SchoolSpendingCostsViewModelCostCategories(urn, costs);
        return View(new SchoolSpendingCostsViewModel(id, urn, isPartOfTrust, isCustomData, hasIncompleteData, categories, resources));
    }
}
