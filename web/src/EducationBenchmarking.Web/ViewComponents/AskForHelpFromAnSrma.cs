using Microsoft.AspNetCore.Mvc;

namespace EducationBenchmarking.Web.ViewComponents;

public class AskForHelpFromAnSrma : ViewComponent
{
    public IViewComponentResult Invoke()
    {
        return View();
    }
}