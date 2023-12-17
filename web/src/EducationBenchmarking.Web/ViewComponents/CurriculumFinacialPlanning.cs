using Microsoft.AspNetCore.Mvc;

namespace EducationBenchmarking.Web.ViewComponents;

public class CurriculumFinancialPlanning : ViewComponent
{
    public IViewComponentResult Invoke()
    {
        return View();
    }
}