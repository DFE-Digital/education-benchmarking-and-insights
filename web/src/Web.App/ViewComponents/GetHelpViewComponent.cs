using Microsoft.AspNetCore.Mvc;

namespace Web.App.ViewComponents;

public class GetHelpViewComponent : ViewComponent
{
    public IViewComponentResult Invoke()
    {
        return View();
    }
}