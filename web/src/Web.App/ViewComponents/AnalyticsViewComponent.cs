using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Web.App.ViewModels.Components;

namespace Web.App.ViewComponents;

public class AnalyticsViewComponent : ViewComponent
{
    public IViewComponentResult Invoke()
    {
        var connectionString = Environment.GetEnvironmentVariable("APPLICATIONINSIGHTS_CONNECTION_STRING");
        if (string.IsNullOrWhiteSpace(connectionString))
        {
            return new EmptyContentView();
        }

        var cookiePolicy = HttpContext.Request.Cookies[Constants.CookieSettingsName];
        var vm = new AnalyticsViewModel(connectionString, cookiePolicy == "enabled");

        var activity = Activity.Current;
        if (activity != null)
        {
            vm.OperationId = activity.TraceId.ToString();
        }

        return View(vm);
    }
}