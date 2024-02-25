using EducationBenchmarking.Web.ViewModels.Components;
using Microsoft.AspNetCore.Mvc;

namespace EducationBenchmarking.Web.ViewComponents;

public class TrustFinanceToolsViewComponent : ViewComponent
{
    public IViewComponentResult Invoke(string identifier, FinanceTools[] tools)
    {
        return View(new FinanceToolsViewModel(identifier, tools));
    }
}