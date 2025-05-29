using Microsoft.AspNetCore.Mvc;
using Web.App.Domain;
using Web.App.ViewModels.Components;

namespace Web.App.ViewComponents;

public class CommercialResourceViewComponent : ViewComponent
{
    public IViewComponentResult Invoke(string section, IEnumerable<CommercialResourceLink> links, bool displayHeading = true)
    {
        return View(new CommercialResourceViewModel(section, links, displayHeading));
    }
}