using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Primitives;
using Web.App.ViewModels.Components;

namespace Web.App.ViewComponents;

public class SortableTableHeaderCellViewComponent : ViewComponent
{
    public IViewComponentResult Invoke(
        string label,
        string sortField,
        string sortFieldKey = "sortField",
        string sortOrderKey = "sortOrder",
        string? className = null)
    {
        // strip existing sort key/value pairs from query string
        var query = new Dictionary<string, StringValues>();
        foreach (var (key, value) in Request.Query)
        {
            if (key == sortFieldKey || key == sortOrderKey)
            {
                continue;
            }

            foreach (var item in value.Where(item => !string.IsNullOrWhiteSpace(item)))
            {
                query.Add(key, item!);
            }
        }

        var baseUrl = $"{Request.Path}{QueryString.Create(query)}".TrimEnd('?').TrimEnd('&');
        var currentSortField = Request.Query[sortFieldKey];
        var currentSort = Request.Query[sortOrderKey];
        var vm = new SortableTableHeaderCellViewModel(label, sortField, sortFieldKey, sortOrderKey, currentSortField, currentSort, baseUrl, className);
        return View(vm);
    }
}