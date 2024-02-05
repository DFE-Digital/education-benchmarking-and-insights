using EducationBenchmarking.Web.ViewModels.Components;
using Microsoft.AspNetCore.Mvc;

namespace EducationBenchmarking.Web.ViewComponents;

public class FinanceToolsViewComponent : ViewComponent
{
    public IViewComponentResult Invoke(string identifier, FinanceTools[] tools)
    {
        return View(new FinanceToolsViewModel(identifier, tools));
    }
}