using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.FeatureManagement.Mvc;
using Web.App.Attributes;
using Web.App.Attributes.RequestTelemetry;
using Web.App.Infrastructure.Apis;
using Web.App.ViewModels;

namespace Web.App.Controllers;

[Controller]
[Route("school/{urn}/comparison/it")]
[ValidateUrn]
[FeatureGate(FeatureFlags.CfrItSpendBreakdown)]
public class SchoolComparisonItSpendController(ILogger<SchoolComparisonController> logger) : Controller
{
    [HttpGet]
    [SchoolRequestTelemetry(TrackedRequestFeature.BenchmarkItSpend)]
    public async Task<IActionResult> Index(string urn)
    {
        using (logger.BeginScope(new
        {
            urn
        }))
        {
            try
            {
                ViewData[ViewDataKeys.BreadcrumbNode] = BreadcrumbNodes.SchoolComparisonItSpend(urn);
                var viewModel = new SchoolComparisonItSpendViewModel();
                return await Task.FromResult(View(viewModel));
            }
            catch (Exception e)
            {
                logger.LogError(e, "An error displaying school IT spending comparison: {DisplayUrl}", Request.GetDisplayUrl());
                return e is StatusCodeException s ? StatusCode((int)s.Status) : StatusCode(500);
            }
        }
    }
}