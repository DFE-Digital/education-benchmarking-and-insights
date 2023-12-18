using EducationBenchmarking.Web.ViewModels.Components;
using Microsoft.AspNetCore.Mvc;

namespace EducationBenchmarking.Web.ViewComponents;

public class SchoolDetails : ViewComponent
{
    public IViewComponentResult Invoke(string identifier)
    {
        return View(new SchoolDetailsViewModel(identifier));
    }
}