using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using Web.App.Controllers.Api.Mappers;
using Web.App.Controllers.Api.Responses;
using Web.App.Domain.NonFinancial;
using Web.App.Infrastructure.Apis;
using Web.App.Infrastructure.Apis.NonFinancial;
using Web.App.Infrastructure.Extensions;
using Web.App.Services;

namespace Web.App.Controllers.Api;

[ApiController]
[Route("api/local-authorities/education-health-care-plans")]
public class EducationHealthCarePlansProxyController(
    ILogger<EducationHealthCarePlansProxyController> logger,
    IEducationHealthCarePlansApi educationHealthCarePlansApi,
    ILocalAuthorityComparatorSetService comparatorSetService) : Controller
{
    /// <param name="code" example="201"></param>
    /// <param name="cancellationToken"></param>
    [HttpGet]
    [Produces("application/json")]
    [ProducesResponseType<EducationHealthCarePlansComparisonResponse[]>(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [Route("comparison")]
    public async Task<IActionResult> Comparison([FromQuery] string code, CancellationToken cancellationToken)
    {
        try
        {
            var set = comparatorSetService.ReadUserDefinedComparatorSetFromSession(code).Set;
            if (set.Length == 0)
            {
                return NotFound();
            }

            var query = BuildQuery(new[] { code }.Concat(set).ToArray(), "Per1000");
            var plans = await educationHealthCarePlansApi
                .GetEducationHealthCarePlans(query, cancellationToken)
                .GetResultOrThrow<LocalAuthorityNumberOfPlans[]>();

            return new JsonResult(plans.MapToApiResponse());
        }
        catch (Exception e)
        {
            logger.LogError(e, "An error getting local authority education health care plans data: {DisplayUrl}", Request.GetDisplayUrl());
            return StatusCode(500);
        }
    }

    /// <param name="code" example="201"></param>
    /// <param name="cancellationToken"></param>
    [HttpGet]
    [Produces("application/json")]
    [ProducesResponseType<EducationHealthCarePlansHistoryResponse[]>(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [Route("history")]
    public async Task<IActionResult> History([FromQuery] string code, CancellationToken cancellationToken)
    {
        try
        {
            var query = BuildQuery([code]);
            var history = await educationHealthCarePlansApi
                .GetEducationHealthCarePlansHistory(query, cancellationToken)
                .GetResultOrThrow<EducationHealthCarePlansHistory<LocalAuthorityNumberOfPlansYear>>();

            return new JsonResult(history.MapToApiResponse(code));
        }
        catch (Exception e)
        {
            logger.LogError(e, "An error getting local authority education health care plans history data: {DisplayUrl}", Request.GetDisplayUrl());
            return StatusCode(500);
        }
    }

    private static ApiQuery BuildQuery(string[] codes, string? dimension = null)
    {
        var query = new ApiQuery();
        foreach (var c in codes)
        {
            query.AddIfNotNull("code", c);
        }

        query.AddIfNotNull("dimension", dimension);
        return query;
    }
}