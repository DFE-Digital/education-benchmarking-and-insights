using EducationBenchmarking.Web.Domain;
using EducationBenchmarking.Web.ViewModels.Components;
using Microsoft.AspNetCore.Mvc;

namespace EducationBenchmarking.Web.ViewComponents;

public class TagViewComponent : ViewComponent
{
    public IViewComponentResult Invoke(TagColour colour, string displayText)
    {
        return View(new TagViewModel(colour, displayText));
    }
}