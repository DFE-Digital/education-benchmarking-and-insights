using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.FeatureManagement.Mvc;
using Web.App.Attributes;
using Web.App.Attributes.RequestTelemetry;
using Web.App.Domain;
using Web.App.Infrastructure.Apis;
using Web.App.Infrastructure.Apis.Benchmark;
using Web.App.Infrastructure.Apis.Establishment;
using Web.App.Infrastructure.Extensions;
using Web.App.Services;
using Web.App.ViewModels;

namespace Web.App.Controllers;

[Controller]
[Authorize]
[Route("trust/{companyNumber}/benchmark-it-spending")]
[ValidateCompanyNumber]
[FeatureGate(FeatureFlags.TrustItSpendBreakdown)]
public class TrustComparisonItSpendController(
    ILogger<TrustComparisonItSpendController> logger,
    IEstablishmentApi establishmentApi,
    IComparatorSetApi comparatorSetApi,
    IUserDataService userDataService) : Controller
{
    [HttpGet]
    [TrustRequestTelemetry(TrackedRequestFeature.BenchmarkItSpend)]
    public async Task<IActionResult> Index(string companyNumber,
        [FromQuery(Name = "comparator-generated")] bool? comparatorGenerated)
    {
        using (logger.BeginScope(new
        {
            companyNumber
        }))
        {
            try
            {
                var trust = await establishmentApi.GetTrust(companyNumber).GetResultOrThrow<Trust>();
                var redirectUri = Url.Action("Index", new
                {
                    companyNumber
                });
                var userData = await userDataService.GetTrustDataAsync(User, companyNumber);
                if (userData.ComparatorSet == null)
                {
                    return RedirectToAction("Index", "TrustComparatorsCreateBy", new
                    {
                        companyNumber,
                        redirectUri
                    });
                }

                var userDefinedSet = await comparatorSetApi.GetUserDefinedTrustAsync(companyNumber, userData.ComparatorSet)
                    .GetResultOrDefault<UserDefinedSchoolComparatorSet>();

                var viewModel = new TrustComparisonItSpendViewModel(trust, comparatorGenerated, redirectUri, userDefinedSet?.Set);
                return View(viewModel);
            }
            catch (Exception e)
            {
                logger.LogError(e, "An error displaying trust IT spend landing: {DisplayUrl}", Request.GetDisplayUrl());
                return e is StatusCodeException s ? StatusCode((int)s.Status) : StatusCode(500);
            }
        }
    }
}