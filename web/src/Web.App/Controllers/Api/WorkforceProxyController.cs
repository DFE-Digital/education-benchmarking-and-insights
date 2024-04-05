using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using Web.App.Domain;
using Web.App.Infrastructure.Apis;
using Web.App.Infrastructure.Extensions;
using Web.App.Services;

namespace Web.App.Controllers.Api;

[ApiController]
[Route("api/workforce")]
public class WorkforceProxyController(
    ILogger<ProxyController> logger,
    IEstablishmentApi establishmentApi,
    IWorkforceApi workforceApi,
    IComparatorSetService comparatorSetService)
    : Controller
{
    [HttpGet]
    [Produces("application/json")]
    public async Task<IActionResult> Query([FromQuery] string type, [FromQuery] string id, [FromQuery] string category, [FromQuery] string dimension)
    {
        using (logger.BeginScope(new { type, id }))
        {
            try
            {
                var set = type.ToLower() switch
                {
                    OrganisationTypes.School => (await comparatorSetService.ReadComparatorSet(id)).DefaultPupil,
                    OrganisationTypes.Trust => (await establishmentApi.GetTrustSchools(id)
                        .GetResultOrThrow<IEnumerable<School>>()).Select(x => x.Urn).OfType<string>(),
                    _ => throw new ArgumentOutOfRangeException(nameof(type))
                };

                var query = BuildApiQuery(set, category, dimension);
                var result = await workforceApi.Query(query).GetResultOrDefault<Workforce[]>();
                return new JsonResult(result);
            }
            catch (Exception e)
            {
                logger.LogError(e, "An error getting workforce data: {DisplayUrl}", Request.GetDisplayUrl());
                return StatusCode(500);
            }
        }
    }

    [HttpGet]
    [Produces("application/json")]
    [Route("history")]
    public async Task<IActionResult> History([FromQuery] string id, [FromQuery] string dimension)
    {
        using (logger.BeginScope(new { id }))
        {
            try
            {
                var query = BuildApiQuery(dimension);
                var result = await workforceApi.History(id, query).GetResultOrDefault<Workforce[]>();
                return new JsonResult(result);
            }
            catch (Exception e)
            {
                logger.LogError(e, "An error getting workforce history data: {DisplayUrl}", Request.GetDisplayUrl());
                return StatusCode(500);
            }
        }
    }

    private static ApiQuery BuildApiQuery(IEnumerable<string> urns, string category, string dimension)
    {
        var query = BuildApiQuery(dimension)
            .AddIfNotNull("category", category);

        foreach (var urn in urns)
        {
            query.AddIfNotNull("urns", urn);
        }

        return query;
    }

    private static ApiQuery BuildApiQuery(string dimension)
    {
        var query = new ApiQuery()
            .AddIfNotNull("dimension", dimension);

        return query;
    }
}