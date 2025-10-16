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
        bool button = false,
        string? className = null)
    {
        // strip existing sort key/value pairs from query string
        var query = new Dictionary<string, StringValues>();
        foreach (var (key, value) in Request.Query)
        {
            if (key == sortKey)
            {
                continue;
            }

            foreach (var item in value.Where(item => !string.IsNullOrWhiteSpace(item)))
            {
                query.Add(key, item!);
            }
        }

        var currentSortKvp = Request.Query[sortKey].ToString().Split(sortDelimeter);
        var currentSortField = currentSortKvp.First();
        var currentSort = currentSortKvp.Last();

        if (button)
        {
            var formModel = new SortableTableHeaderCellButtonViewModel(label, sortField, sortDelimeter, sortKey, currentSortField, currentSort, className);
            return View("Button", formModel);
        }

        var baseUrl = $"{Request.Path}{QueryString.Create(query)}".TrimEnd('?').TrimEnd('&');
        var linkModel = new SortableTableHeaderCellLinkViewModel(label, sortField, sortKey, sortDelimeter, currentSortField, currentSort, baseUrl, className);
        return View("Link", linkModel);
    }
}