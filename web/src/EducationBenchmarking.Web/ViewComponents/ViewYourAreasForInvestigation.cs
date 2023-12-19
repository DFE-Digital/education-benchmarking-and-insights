using EducationBenchmarking.Web.ViewModels.Components;
using Microsoft.AspNetCore.Mvc;

namespace EducationBenchmarking.Web.ViewComponents;

public class ViewYourAreasForInvestigation : ViewComponent
{
    public IViewComponentResult Invoke(string identifier)
    {
        return View(new ViewYourAreasForInvestigationViewModel(identifier));
    }
}