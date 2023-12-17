using Microsoft.AspNetCore.Mvc;

namespace EducationBenchmarking.Web.ViewComponents;

public class ForecastRisk : ViewComponent
{
    public IViewComponentResult Invoke()
    {
        return View();
    }
}