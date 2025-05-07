using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using Web.App.Attributes;
using Web.App.Controllers.Api.Mappers;
using Web.App.Controllers.Api.Responses;
using Web.App.Domain.NonFinancial;
using Web.App.Infrastructure.Apis;
using Web.App.Infrastructure.Apis.NonFinancial;
using Web.App.Infrastructure.Extensions;

namespace Web.App.Controllers.Api;

[ApiController]
[Route("api/local-authorities/education-health-care-plans")]
public class EducationHealthCarePlansProxyController(
    ILogger<EducationHealthCarePlansProxyController> logger,
    IEducationHealthCarePlansApi educationHealthCarePlansApi) : Controller
{
    /// <param name="code" example="201"></param>
    /// <param name="set" example="202,203,204"></param>
    /// <param name="cancellationToken"></param>
    [HttpGet]
    [Produces("application/json")]
    [ProducesResponseType<EducationHealthCarePlansComparisonResponse[]>(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [Route("comparison")]
    [ValidateLaCode]
    public async Task<IActionResult> Comparison([FromQuery] string code, [FromQuery] string[]? set = null, CancellationToken cancellationToken = default)
    {
        try
        {
            if (set == null || set.All(string.IsNullOrWhiteSpace))
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
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [Route("history")]
    [ValidateLaCode]
    public async Task<IActionResult> History([FromQuery] string code, CancellationToken cancellationToken = default)
    {
        try
        {
            var query = BuildQuery([code], "Per1000");
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

    private static ApiQuery BuildQuery(string[] codes, string dimension)
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