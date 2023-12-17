using EducationBenchmarking.Web.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace EducationBenchmarking.Web.ViewComponents;

public class CurriculumFinancialPlanning : ViewComponent
{
    public IViewComponentResult Invoke(string identifier)
    {
        return View(new CurriculumFinancialPlanningViewModel(identifier));
    }
}