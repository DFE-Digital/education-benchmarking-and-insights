using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using Web.App.Domain;
using Web.App.Infrastructure.Apis;
using Web.App.Infrastructure.Apis.Establishment;
using Web.App.Infrastructure.Extensions;

namespace Web.App.Controllers.Api;

[ApiController]
[Route("api/local-authorities/national-rank")]
public class NationalRankProxyController(
    ILogger<NationalRankProxyController> logger,
    IEstablishmentApi establishmentApi) : Controller
{
    /// <param name="ranking" example="SpendAsPercentageOfBudget"></param>
    /// <param name="sort" example="asc"></param>
    /// <param name="cancellationToken"></param>
    [HttpGet]
    [Produces("application/json")]
    [ProducesResponseType<LocalAuthorityRanking>(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Query([FromQuery] string ranking, [FromQuery] string? sort, CancellationToken cancellationToken = default)
    {
        try
        {
            var query = BuildQuery(ranking, sort);
            var result = await establishmentApi
                .GetLocalAuthoritiesNationalRank(query, cancellationToken)
                .GetResultOrThrow<LocalAuthorityRanking>();

            return new JsonResult(result);
        }
        catch (Exception e)
        {
            logger.LogError(e, "An error getting local authority national rank data: {DisplayUrl}", Request.GetDisplayUrl());
            return StatusCode(500);
        }
    }

    private static ApiQuery BuildQuery(string ranking, string? sort)
    {
        var query = new ApiQuery()
            .AddIfNotNull("ranking", ranking)
            .AddIfNotNull("sort", sort);

        return query;
    }
}