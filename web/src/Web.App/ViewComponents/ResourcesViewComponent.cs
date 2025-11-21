using Microsoft.AspNetCore.Mvc;
using Web.App.Builders;
using Web.App.ViewModels.Components;

namespace Web.App.ViewComponents;

public class ResourcesViewComponent(IUriBuilder uriBuilder) : ViewComponent
{
    public IViewComponentResult Invoke(string identifier, Resources[] resources)
    {
        var compareSchoolPerformanceUrl = uriBuilder.CompareSchoolPerformanceUrl(identifier);
        return View(new ResourcesViewModel(identifier, resources, compareSchoolPerformanceUrl));
    }
}