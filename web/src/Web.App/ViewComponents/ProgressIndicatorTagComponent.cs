using Microsoft.AspNetCore.Mvc;
using Web.App.Domain;
using Web.App.ViewModels.Components;

namespace Web.App.ViewComponents;

public class ProgressBandingTag : ViewComponent
{
    public IViewComponentResult Invoke(KS4ProgressBandings.Banding? banding)
    {
        if (!banding.HasValue)
        {
            return new EmptyContentView();
        }

        var vm = new ProgressBandingTagViewModel(banding.Value);
        return View(vm);
    }
}