using Microsoft.AspNetCore.Mvc;
using Web.App.ViewModels.Components;

namespace Web.App.ViewComponents;

public class SchoolHeadingViewComponent : ViewComponent
{
    public IViewComponentResult Invoke(string pageTitle, string schoolName, string urn)
    {
        return View(new SchoolHeadingViewModel(pageTitle, schoolName, urn));
    }
}

