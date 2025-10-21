using Microsoft.AspNetCore.Mvc;
using Web.App.ViewModels.Components;

namespace Web.App.ViewComponents;

public class SortableTableHeaderCellViewComponent : ViewComponent
{
    public IViewComponentResult Invoke(
        string label,
        string sortField,
        string sortKey = "sort",
        string sortDelimeter = "~",
        string? className = null,
        string? tableId = null)
    {
        var currentSortKvp = Request.Query[sortKey].ToString().Split(sortDelimeter);
        var currentSortField = currentSortKvp.First();
        var currentSort = currentSortKvp.Last();
        var formModel = new SortableTableHeaderCellViewModel(label, sortField, sortDelimeter, sortKey, currentSortField, currentSort, className, tableId);
        return View(formModel);
    }
}