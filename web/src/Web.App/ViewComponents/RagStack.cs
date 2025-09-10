using Microsoft.AspNetCore.Mvc;
using Web.App.ViewModels.Components;

namespace Web.App.ViewComponents;

public class RagStack : ViewComponent
{
    public IViewComponentResult Invoke(
        string identifier,
        int red,
        int amber,
        int green,
        bool small = false,
        string? redHref = null,
        string? amberHref = null,
        string? greenHref = null)
        => View(new RagStackViewModel(identifier, red, amber, green, small)
        {
            RedHref = redHref,
            AmberHref = amberHref,
            GreenHref = greenHref
        });
}