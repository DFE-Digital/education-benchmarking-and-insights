using EducationBenchmarking.Web.ViewModels.Components;
using Microsoft.AspNetCore.Mvc;

namespace EducationBenchmarking.Web.ViewComponents;

public class HistoricData : ViewComponent
{
    public IViewComponentResult Invoke(string identifier)
    {
        return View(new SchoolHistoryViewModel(identifier));
    }
}