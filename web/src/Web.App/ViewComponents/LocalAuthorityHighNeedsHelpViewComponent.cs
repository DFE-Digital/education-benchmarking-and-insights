using Microsoft.AspNetCore.Mvc;

namespace Web.App.ViewComponents;

public class LocalAuthorityHighNeedsHelpViewComponent : ViewComponent
{
    public IViewComponentResult Invoke() => View();
}