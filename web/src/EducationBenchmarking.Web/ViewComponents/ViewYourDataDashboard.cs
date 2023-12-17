using Microsoft.AspNetCore.Mvc;

namespace EducationBenchmarking.Web.ViewComponents;

public class ViewYourDataDashboard : ViewComponent
{
    public IViewComponentResult Invoke()
    {
        return View();
    }
}