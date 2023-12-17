using Microsoft.AspNetCore.Mvc;

namespace EducationBenchmarking.Web.ViewComponents;

public class HelpWithThisService : ViewComponent
{
    public IViewComponentResult Invoke()
    {
        return View();
    }
}