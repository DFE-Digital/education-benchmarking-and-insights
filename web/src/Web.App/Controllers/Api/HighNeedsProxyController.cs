using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using Web.App.Controllers.Api.Mappers;
using Web.App.Controllers.Api.Responses;
using Web.App.Domain.LocalAuthorities;
using Web.App.Infrastructure.Apis;
using Web.App.Infrastructure.Apis.LocalAuthorities;
using Web.App.Infrastructure.Extensions;

namespace Web.App.Controllers.Api;

[ApiController]
[Route("api/local-authorities/high-needs")]
public class HighNeedsProxyController(
    ILogger<HighNeedsProxyController> logger,
    ILocalAuthoritiesApi localAuthoritiesApi) : Controller
{
    /// <param name="code" example="201"></param>
    /// <param name="set" example="202,203,204"></param>
    /// <param name="cancellationToken"></param>
    [HttpGet]
    [Produces("application/json")]
    [ProducesResponseType<LocalAuthorityHighNeedsComparisonResponse[]>(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [Route("comparison")]
    public async Task<IActionResult> Comparison([FromQuery] string code, [FromQuery] string[]? set = null, CancellationToken cancellationToken = default)
    {
        try
        {
            if (set == null || set.All(string.IsNullOrWhiteSpace))
            {
                return NotFound();
            }

            var query = BuildQuery(new[] { code }.Concat(set).ToArray(), "PerHead");
            var localAuthorities = await localAuthoritiesApi
                .GetHighNeeds(query, cancellationToken)
                .GetResultOrThrow<LocalAuthority<HighNeeds>[]>();

            return new JsonResult(localAuthorities.MapToApiResponse());
        }
        catch (Exception e)
        {
            logger.LogError(e, "An error getting local authority high needs data: {DisplayUrl}", Request.GetDisplayUrl());
            return StatusCode(500);
        }
    }

    /// <param name="code" example="201"></param>
    /// <param name="cancellationToken"></param>
    [HttpGet]
    [Produces("application/json")]
    [ProducesResponseType<LocalAuthorityHighNeedsHistoryResponse[]>(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [Route("history")]
    public async Task<IActionResult> History([FromQuery] string code, CancellationToken cancellationToken = default)
    {
        try
        {
            var query = BuildQuery([code], "PerHead");
            var history = await localAuthoritiesApi
                .GetHighNeedsHistory(query, cancellationToken)
                .GetResultOrThrow<HighNeedsHistory<HighNeedsYear>>();

            return new JsonResult(history.MapToApiResponse(code));
        }
        catch (Exception e)
        {
            logger.LogError(e, "An error getting local authority high needs history data: {DisplayUrl}", Request.GetDisplayUrl());
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