using Microsoft.AspNetCore.Mvc;

namespace EducationBenchmarking.Web.ViewComponents;

public class FindCommercialResources : ViewComponent
{
    public IViewComponentResult Invoke()
    {
        return View();
    }
}