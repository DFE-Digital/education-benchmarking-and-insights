using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.FeatureManagement.Mvc;
using Web.App.Attributes;
using Web.App.Domain;
using Web.App.Infrastructure.Apis;
using Web.App.Infrastructure.Apis.Establishment;
using Web.App.Infrastructure.Apis.Insight;
using Web.App.Infrastructure.Extensions;
using Web.App.ViewModels;

namespace Web.App.Controllers;

[Controller]
[Route("trust/{companyNumber}/summary")]
[ValidateCompanyNumber]
[FeatureGate(FeatureFlags.FbisForTrust)]
public class TrustFinancialBenchmarkingInsightsSummaryController(
    IEstablishmentApi establishmentApi,
    IBalanceApi balanceApi,
    IMetricRagRatingApi metricRagRatingApi,
    ILogger<TrustFinancialBenchmarkingInsightsSummaryController> logger)
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
                var trust = await Trust(companyNumber);
                var balance = await TrustBalance(companyNumber);
                var ratings = await RagRatings(companyNumber);

                var viewModel = new TrustFinancialBenchmarkingInsightsSummaryViewModel(trust, balance, ratings);
                return View(viewModel);
            }
            catch (Exception e)
            {
                logger.LogError(e, "An error displaying financial benchmarking insights summary: {DisplayUrl}",
                    Request.GetDisplayUrl());
                return e is StatusCodeException s ? StatusCode((int)s.Status) : StatusCode(500);
            }
        }
    }

    private async Task<Trust> Trust(string companyNumber) => await establishmentApi
        .GetTrust(companyNumber)
        .GetResultOrThrow<Trust>();

    private async Task<TrustBalance?> TrustBalance(string companyNumber) => await balanceApi
        .Trust(companyNumber)
        .GetResultOrDefault<TrustBalance>();

    private async Task<RagRating[]?> RagRatings(string companyNumber) => await metricRagRatingApi
        .GetDefaultAsync(BuildQuery(companyNumber))
        .GetResultOrDefault<RagRating[]>();

    private static ApiQuery BuildQuery(string companyNumber)
    {
        var query = new ApiQuery();
        query.AddIfNotNull("companyNumber", companyNumber);
        return query;
    }
}