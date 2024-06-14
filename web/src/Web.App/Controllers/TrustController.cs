using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.FeatureManagement.Mvc;
using Web.App.Domain;
using Web.App.Infrastructure.Apis;
using Web.App.Infrastructure.Extensions;
using Web.App.ViewModels;
namespace Web.App.Controllers;

[Controller]
[FeatureGate(FeatureFlags.Trusts)]
[Route("trust/{companyNumber}")]
public class TrustController(ILogger<TrustController> logger, IEstablishmentApi establishmentApi, IBalanceApi balanceApi, IMetricRagRatingApi metricRagRatingApi)
    : Controller
{
    [HttpGet]
    public async Task<IActionResult> Index(
        string companyNumber)
    {
        using (logger.BeginScope(new
        {
            companyNumber
        }))
        {
            try
            {
                ViewData[ViewDataKeys.BreadcrumbNode] = BreadcrumbNodes.TrustHome(companyNumber);

                var trust = await establishmentApi.GetTrust(companyNumber).GetResultOrThrow<Trust>();
                var balance = await balanceApi.Trust(companyNumber).GetResultOrThrow<TrustBalance>();
                var trustQuery = new ApiQuery().AddIfNotNull("companyNumber", companyNumber);
                var schools = await establishmentApi.QuerySchools(trustQuery).GetResultOrDefault<School[]>() ?? [];

                var schoolsQuery = new ApiQuery();
                foreach (var school in schools)
                {
                    schoolsQuery.AddIfNotNull("urns", school.URN);
                }

                var ratings = await metricRagRatingApi.GetDefaultAsync(schoolsQuery).GetResultOrThrow<RagRating[]>();
                var viewModel = new TrustViewModel(trust, balance, schools, ratings);
                return View(viewModel);
            }
            catch (Exception e)
            {
                logger.LogError(e, "An error displaying trust details: {DisplayUrl}", Request.GetDisplayUrl());
                return e is StatusCodeException s ? StatusCode((int)s.Status) : StatusCode(500);
            }
        }
    }
}