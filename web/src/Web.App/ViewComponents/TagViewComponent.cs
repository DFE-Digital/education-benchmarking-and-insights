using Microsoft.AspNetCore.Mvc;
using Web.App.ViewModels;
using Web.App.ViewModels.Components;

namespace Web.App.ViewComponents;

public class TagViewComponent : ViewComponent
{
    public IViewComponentResult Invoke(RatingViewModel rating)
    {
        return View(new TagViewModel(rating.Colour, rating.DisplayText));
    }
}