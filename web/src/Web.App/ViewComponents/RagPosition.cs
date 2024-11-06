using System.Collections.ObjectModel;
using Microsoft.AspNetCore.Mvc;
using Web.App.Domain;
using Web.App.ViewModels.Components;
namespace Web.App.ViewComponents;

public class RagPosition : ViewComponent
{
    public IViewComponentResult Invoke(
        ReadOnlyDictionary<string, Category> values,
        int itemWidth,
        int height,
        decimal itemSpacing = 1m,
        string? highlight = null,
        string? id = null,
        string? title = null)
        => View(new RagPositionViewModel(values, itemWidth, height, itemSpacing)
        {
            Highlight = highlight,
            Id = id,
            Title = title
        });
}