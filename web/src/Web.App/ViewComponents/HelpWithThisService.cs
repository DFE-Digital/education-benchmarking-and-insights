using Microsoft.AspNetCore.Mvc;

namespace Web.App.ViewComponents
{
    public class HelpWithThisService : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}