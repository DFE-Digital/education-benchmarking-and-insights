using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.FeatureManagement.Mvc;
using Web.App.Attributes;
using Web.App.Attributes.RequestTelemetry;

namespace Web.App.Controllers;

[Controller]
[Authorize]
[Route("trust/{companyNumber}/benchmark-it-spending")]
[ValidateCompanyNumber]
[FeatureGate(FeatureFlags.BfrItSpendBreakdown)]
public class TrustComparisonItSpendController : Controller
{
    [HttpGet]
    [TrustRequestTelemetry(TrackedRequestFeature.BenchmarkItSpend)]
    public IActionResult Index(string companyNumber)
    {
        return new NotFoundObjectResult(new { companyNumber });
    }
}