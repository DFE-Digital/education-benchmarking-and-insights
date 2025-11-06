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
        string? className,
        string? redHref = null,
        string? amberHref = null,
        string? greenHref = null)
        => View(new RagStackViewModel(identifier, red, amber, green, className)
        {
            RedHref = redHref,
            AmberHref = amberHref,
            GreenHref = greenHref
        });
}