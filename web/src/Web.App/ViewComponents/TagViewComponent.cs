using Microsoft.AspNetCore.Mvc;
using Web.App.ViewModels.Components;

namespace Web.App.ViewComponents;

public class TagViewComponent : ViewComponent
{
    public IViewComponentResult Invoke(TagColour colour, string displayText)
    {
        return View(new TagViewModel(colour, displayText));
    }
}