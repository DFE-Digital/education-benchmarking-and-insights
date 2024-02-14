using EducationBenchmarking.Web.ViewModels.Components;
using Microsoft.AspNetCore.Mvc;

namespace EducationBenchmarking.Web.ViewComponents;

public class ResourcesViewComponent : ViewComponent
{
    public IViewComponentResult Invoke(string identifier, Resources[] resources)
    {
        return View(new ResourcesViewModel(identifier, resources));
    }
}