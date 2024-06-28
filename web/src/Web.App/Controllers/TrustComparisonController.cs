using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.FeatureManagement.Mvc;
using Web.App.Attributes.RequestTelemetry;
using Web.App.Domain;
using Web.App.Infrastructure.Apis;
using Web.App.Infrastructure.Extensions;
using Web.App.ViewModels;
namespace Web.App.Controllers;

[Controller]
[FeatureGate(FeatureFlags.Trusts)]
[Route("trust/{companyNumber}/comparison")]
[TrustRequestTelemetry(TrackedRequestFeature.Comparators)]
public class TrustComparisonController(
    IEstablishmentApi establishmentApi,
    ILogger<TrustComparisonController> logger)
    : Controller
{
    [HttpGet]
    public async Task<IActionResult> Index(string companyNumber)
    {
        using (logger.BeginScope(new
        {
            companyNumber
        }))
        {
            try
            {
                ViewData[ViewDataKeys.BreadcrumbNode] = BreadcrumbNodes.TrustComparison(companyNumber);

                var trust = await establishmentApi.GetTrust(companyNumber).GetResultOrThrow<Trust>();

                var query = new ApiQuery().AddIfNotNull("companyNumber", companyNumber);
                var schools = await establishmentApi.QuerySchools(query).GetResultOrThrow<School[]>();

                var phases = schools.GroupBy(x => x.OverallPhase).OrderByDescending(x => x.Count()).Select(x => x.Key).OfType<string>().ToArray();

                var viewModel = new TrustComparisonViewModel(trust, phases);


                return View(viewModel);
            }
            catch (Exception e)
            {
                logger.LogError(e, "An error displaying trust comparison: {DisplayUrl}", Request.GetDisplayUrl());
                return e is StatusCodeException s ? StatusCode((int)s.Status) : StatusCode(500);
            }
        }
    }
}