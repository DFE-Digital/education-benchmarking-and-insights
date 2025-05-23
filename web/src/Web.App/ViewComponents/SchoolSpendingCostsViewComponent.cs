using Microsoft.AspNetCore.Mvc;
using Web.App.Domain;
using Web.App.ViewModels.Components;

namespace Web.App.ViewComponents;

public class SchoolSpendingCostsViewComponent : ViewComponent
{
    public IViewComponentResult Invoke(
        IEnumerable<CostCategory> costs,
        string? id,
        string urn,
        bool hasIncompleteData,
        bool isCustomData,
        bool isPartOfTrust)
    {
        var categories = new List<SchoolSpendingCostsViewModelCostCategory<PriorityCostCategoryDatum>>();
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
        }

        return View(new SchoolSpendingCostsViewModel(id, urn, isPartOfTrust, isCustomData, hasIncompleteData, categories));
    }
}