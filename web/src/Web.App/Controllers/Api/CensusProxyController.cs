using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using Web.App.Domain;
using Web.App.Extensions;
using Web.App.Infrastructure.Apis;
using Web.App.Infrastructure.Extensions;
using Web.App.Services;
namespace Web.App.Controllers.Api;

[ApiController]
[Route("api/census")]
public class CensusProxyController(
    ILogger<CensusProxyController> logger,
    IEstablishmentApi establishmentApi,
    ICensusApi censusApi,
    ISchoolComparatorSetService schoolComparatorSetService,
    IUserDataService userDataService)
    : Controller
{
    [HttpGet]
    [Produces("application/json")]
    public async Task<IActionResult> Query([FromQuery] string type, [FromQuery] string id, [FromQuery] string category, [FromQuery] string dimension, [FromQuery] string? phase)
    {
        using (logger.BeginScope(new { type, id }))
        {
            try
            {
                var set = type.ToLower() switch
                {
                    OrganisationTypes.School => await GetSchoolSet(id),
                    OrganisationTypes.Trust => await GetTrustSet(id, phase),
                    OrganisationTypes.LocalAuthority => await GetLocalAuthoritySet(id, phase),
                    _ => throw new ArgumentOutOfRangeException(nameof(type))
                };

                var query = BuildApiQuery(set, category, dimension);
                var result = await censusApi.Query(query).GetResultOrDefault<Census[]>();
                return new JsonResult(result);
            }
            catch (Exception e)
            {
                logger.LogError(e, "An error getting census data: {DisplayUrl}", Request.GetDisplayUrl());
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
                var query = BuildApiQuery(dimension: dimension);
                var result = await censusApi.History(id, query).GetResultOrDefault<CensusHistory[]>();
                return new JsonResult(result);
            }
            catch (Exception e)
            {
                logger.LogError(e, "An error getting census history data: {DisplayUrl}", Request.GetDisplayUrl());
                return StatusCode(500);
            }
        }
    }

    private async Task<IEnumerable<string>> GetSchoolSet(string id)
    {
        var userData = await userDataService.GetSchoolDataAsync(User.UserId(), id);
        if (string.IsNullOrEmpty(userData.ComparatorSet))
        {
            var defaultSet = await schoolComparatorSetService.ReadComparatorSet(id);
            return defaultSet.Pupil;
        }

        var userDefinedSet = await schoolComparatorSetService.ReadUserDefinedComparatorSet(id, userData.ComparatorSet);
        return userDefinedSet.Set;
    }

    private async Task<IEnumerable<string>> GetTrustSet(string id, string? phase)
    {
        var query = new ApiQuery()
            .AddIfNotNull("companyNumber", id)
            .AddIfNotNull("phase", phase);

        var result = await establishmentApi.QuerySchools(query).GetResultOrThrow<IEnumerable<School>>();
        return result.Select(x => x.URN).OfType<string>();
    }

    private async Task<IEnumerable<string>> GetLocalAuthoritySet(string id, string? phase)
    {
        var query = new ApiQuery()
            .AddIfNotNull("laCode", id)
            .AddIfNotNull("phase", phase);

        var result = await establishmentApi.QuerySchools(query).GetResultOrThrow<IEnumerable<School>>();
        return result.Select(x => x.URN).OfType<string>();
    }

    private static ApiQuery BuildApiQuery(IEnumerable<string>? urns = null, string? category = null, string? dimension = null)
    {
        var query = new ApiQuery()
            .AddIfNotNull("dimension", dimension)
            .AddIfNotNull("category", category);

        if (urns != null)
        {
            foreach (var urn in urns)
            {
                query.AddIfNotNull("urns", urn);

            }
        }

        return query;
    }
}