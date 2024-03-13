using Microsoft.AspNetCore.Mvc;
using SmartBreadcrumbs.Attributes;

namespace Web.App.Controllers;

[Controller]
[Route("/")]
public class HomeController : Controller
{

    [HttpGet]
    [DefaultBreadcrumb(PageTitles.ServiceHome)]
    public IActionResult Index()
    {
        return View();
    }
}