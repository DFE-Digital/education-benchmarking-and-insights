using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using Web.App.Domain;
using Web.App.Infrastructure.Apis;
using Web.App.Infrastructure.Apis.Insight;
using Web.App.Infrastructure.Extensions;

namespace Web.App.Controllers.Api;

[ApiController]
[Authorize]
[Route("api/budget-forecast")]
public class BudgetForecastProxyController(ILogger<BudgetForecastProxyController> logger, IBudgetForecastApi budgetForecastApi) : Controller
{
    /// <param name="companyNumber" example="07465701"></param>
    /// <param name="cancellationToken"></param>
    [HttpGet]
    [Produces("application/json")]
    [ProducesResponseType<BudgetForecastReturn[]>(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Index([FromQuery] string companyNumber, CancellationToken cancellationToken = default)
    {
        using (logger.BeginScope(new
        {
            companyNumber
        }))
        {
            try
            {
                var metrics = await budgetForecastApi
                    .BudgetForecastReturnsMetrics(companyNumber, cancellationToken: cancellationToken)
                    .GetResultOrDefault<BudgetForecastReturnMetric[]>() ?? [];
                var metricsYear = metrics
                    .Select(x => x.Year)
                    .OrderDescending()
                    .FirstOrDefault() ?? Constants.CurrentYear - 1;

                var query = new ApiQuery().AddIfNotNull("runId", metricsYear.ToString());
                var result = await budgetForecastApi
                    .BudgetForecastReturns(companyNumber, query, cancellationToken)
                    .GetResultOrDefault<BudgetForecastReturn[]>();

                return new JsonResult(result);
            }
            catch (Exception e)
            {
                logger.LogError(e, "An error getting forecast data: {DisplayUrl}", Request.GetDisplayUrl());
                return StatusCode(500);
            }
        }
    }
}