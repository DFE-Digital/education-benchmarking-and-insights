using Microsoft.AspNetCore.Mvc;

namespace EducationBenchmarking.Web.ViewComponents;

public class SubmitAnEnquiry : ViewComponent
{
    public IViewComponentResult Invoke()
    {
        return View();
    }
}