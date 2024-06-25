using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.FeatureManagement.Mvc;
using Web.App.Attributes;
using Web.App.Domain;
using Web.App.Infrastructure.Apis;
using Web.App.Infrastructure.Extensions;
using Web.App.ViewModels;
namespace Web.App.Controllers;

[Controller]
[TrustAuthorization]
[FeatureGate(FeatureFlags.Trusts, FeatureFlags.ForecastRisk)]
[Route("trust/{companyNumber}/forecast")]
public class TrustForecastController(
    IEstablishmentApi establishmentApi,
    IBudgetForecastApi budgetForecastApi,
    ILogger<TrustForecastController> logger)
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
                ViewData[ViewDataKeys.BreadcrumbNode] = BreadcrumbNodes.TrustForecast(companyNumber);
                var trust = await establishmentApi.GetTrust(companyNumber).GetResultOrThrow<Trust>();
                var metrics = await GetBudgetForecastReturnMetrics(companyNumber);
                var returns = await GetCurrentBudgetForecastReturn(companyNumber);
                var viewModel = new TrustForecastViewModel(trust, metrics, returns);
                return View(viewModel);
            }
            catch (Exception e)
            {
                logger.LogError(e, "An error displaying trust forecast and risk: {DisplayUrl}",
                    Request.GetDisplayUrl());
                return e is StatusCodeException s ? StatusCode((int)s.Status) : StatusCode(500);
            }
        }
    }

    private async Task<BudgetForecastReturnMetric[]> GetBudgetForecastReturnMetrics(string companyNumber) => await budgetForecastApi
        .BudgetForecastReturnsMetrics(companyNumber)
        .GetResultOrDefault<BudgetForecastReturnMetric[]>() ?? [];

    private async Task<BudgetForecastReturn[]> GetCurrentBudgetForecastReturn(string companyNumber)
    {
        var query = new ApiQuery()
            .AddIfNotNull("runId", (Constants.CurrentYear - 1).ToString());

        return await budgetForecastApi
            .BudgetForecastReturns(companyNumber, query)
            .GetResultOrDefault<BudgetForecastReturn[]>() ?? [];
    }
}