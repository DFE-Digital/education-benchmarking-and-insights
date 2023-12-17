using Microsoft.AspNetCore.Mvc;

namespace EducationBenchmarking.Web.ViewComponents;

public class SchoolDetails : ViewComponent
{
    public IViewComponentResult Invoke()
    {
        return View();
    }
}