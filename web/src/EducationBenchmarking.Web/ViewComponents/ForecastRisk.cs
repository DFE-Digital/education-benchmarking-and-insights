using EducationBenchmarking.Web.ViewModels;
using EducationBenchmarking.Web.ViewModels.Components;
using Microsoft.AspNetCore.Mvc;

namespace EducationBenchmarking.Web.ViewComponents;

public class ForecastRisk : ViewComponent
{
    public IViewComponentResult Invoke(string identifier)
    {
        return View(new ForecastRiskViewModel(identifier));
    }
}