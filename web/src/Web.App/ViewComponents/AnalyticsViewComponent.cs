using Microsoft.ApplicationInsights.DataContracts;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewComponents;
using Web.App.ViewModels.Components;
namespace Web.App.ViewComponents;

public class AnalyticsViewComponent : ViewComponent
{
    public IViewComponentResult Invoke()
    {
        var instrumentationKey = Environment.GetEnvironmentVariable("APPINSIGHTS_INSTRUMENTATIONKEY");
        if (string.IsNullOrWhiteSpace(instrumentationKey))
        {
            return new HtmlContentViewComponentResult(new HtmlString(string.Empty));
        }

        var vm = new AnalyticsViewModel(instrumentationKey);

        var telemetry = HttpContext.Features.Get<RequestTelemetry>();
        if (telemetry != null)
        {
            vm.OperationId = telemetry.Context.Operation.Id;
        }

        return View(vm);
    }
}