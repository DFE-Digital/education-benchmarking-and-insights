using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.FeatureManagement.Mvc;
using Web.App.Attributes.RequestTelemetry;
using Web.App.Domain;
using Web.App.Infrastructure.Apis;
using Web.App.Infrastructure.Apis.Establishment;
using Web.App.Infrastructure.Apis.Insight;
using Web.App.Infrastructure.Extensions;
using Web.App.Services;
using Web.App.ViewModels;
namespace Web.App.Controllers;

[Controller]
[FeatureGate(FeatureFlags.BenchmarkingReportCards)]
[Route("school/{urn}/benchmarking-report-cards")]
[SchoolRequestTelemetry(TrackedRequestFeature.BenchmarkingReportCards)]
public class SchoolBenchmarkingReportCardsController(
    IEstablishmentApi establishmentApi,
    IFinanceService financeService,
    IBalanceApi balanceApi,
    ILogger<SchoolBenchmarkingReportCardsController> logger)
    : Controller
{
    [HttpGet]
    public async Task<IActionResult> Index(string urn)
    {
        using (logger.BeginScope(new
        {
            urn
        }))
        {
            try
            {
                var school = await establishmentApi.GetSchool(urn).GetResultOrThrow<School>();
                var isNonLeadFederation = !string.IsNullOrEmpty(school.FederationLeadURN) && school.FederationLeadURN != urn;
                if (isNonLeadFederation)
                {
                    return new NotFoundResult();
                }

                var years = await financeService.GetYears();
                var balance = await balanceApi
                    .School(urn)
                    .GetResultOrDefault<SchoolBalance>();

                var viewModel = new BenchmarkingReportCardsViewModel(school, years, balance);

                return View(viewModel);
            }
            catch (Exception e)
            {
                logger.LogError(e, "An error displaying school benchmarking report cards: {DisplayUrl}",
                    Request.GetDisplayUrl());
                return e is StatusCodeException s ? StatusCode((int)s.Status) : StatusCode(500);
            }
        }
    }
}