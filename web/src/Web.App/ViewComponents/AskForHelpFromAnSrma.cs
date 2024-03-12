using Microsoft.AspNetCore.Mvc;

namespace Web.App.ViewComponents
{
    public class AskForHelpFromAnSrma : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}