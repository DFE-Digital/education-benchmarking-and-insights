using Microsoft.ApplicationInsights.DataContracts;
using Microsoft.AspNetCore.Mvc;
using Web.App.ViewModels.Components;
namespace Web.App.ViewComponents;

public class AnalyticsViewComponent : ViewComponent
{
    public IViewComponentResult Invoke()
    {
        var instrumentationKey = Environment.GetEnvironmentVariable("APPINSIGHTS_INSTRUMENTATIONKEY");
        if (string.IsNullOrWhiteSpace(instrumentationKey))
        {
            return new EmptyContentView();
        }

        var cookiePolicy = HttpContext.Request.Cookies[Constants.CookieSettingsName];
        var vm = new AnalyticsViewModel(instrumentationKey, cookiePolicy == "enabled");

        var telemetry = HttpContext.Features.Get<RequestTelemetry>();
        if (telemetry != null)
        {
            vm.OperationId = telemetry.Context.Operation.Id;
        }

        return View(vm);
    }
}