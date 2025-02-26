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
    /// <param name="cancellationToken"></param>
    [HttpGet]
    [Produces("application/json")]
    [ProducesResponseType<LocalAuthorityHighNeedsHistoryResponse[]>(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [Route("history")]
    public async Task<IActionResult> History([FromQuery] string code, CancellationToken cancellationToken)
    {
        try
        {
            var query = BuildQuery(code);
            var history = await localAuthoritiesApi
                .GetHighNeedsHistory(query, cancellationToken)
                .GetResultOrThrow<History<LocalAuthorityHighNeedsYear>>();

            return new JsonResult(history.MapToApiResponse(code));
        }
        catch (Exception e)
        {
            logger.LogError(e, "An error getting local authority high needs history data: {DisplayUrl}", Request.GetDisplayUrl());
            return StatusCode(500);
        }
    }

    private static ApiQuery BuildQuery(params string[] code)
    {
        var query = new ApiQuery();
        foreach (var c in code)
        {
            query.AddIfNotNull(nameof(code), c);
        }

        return query;
    }
}