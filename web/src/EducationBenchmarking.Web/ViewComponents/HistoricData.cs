using Microsoft.AspNetCore.Mvc;

namespace EducationBenchmarking.Web.ViewComponents;

public class HistoricData : ViewComponent
{
    public IViewComponentResult Invoke()
    {
        return View();
    }
}