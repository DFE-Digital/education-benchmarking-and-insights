using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Primitives;
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
        var sort = Request.Query[sortKey];
        var currentSortKvp = (sort == StringValues.Empty ? string.Empty : sort.ToString()).Split(sortDelimeter);
        var currentSortField = currentSortKvp.First();
        var currentSort = currentSortKvp.Last();
        var formModel = new SortableTableHeaderCellViewModel(label, sortField, sortDelimeter, sortKey, currentSortField, currentSort, className, tableId);
        return View(formModel);
    }
}