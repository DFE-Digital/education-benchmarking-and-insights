using Microsoft.AspNetCore.Mvc;

namespace Web.App.ViewComponents
{
    public class SubmitAnEnquiry : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}