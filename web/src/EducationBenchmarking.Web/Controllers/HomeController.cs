using Microsoft.AspNetCore.Mvc;

namespace EducationBenchmarking.Web.Controllers;

[Controller]
[Route("/")]
public class HomeController : Controller
{

    [HttpGet]
    public IActionResult Index()
    {
        return View();
    }
}