﻿using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using Web.App.Domain;
using Web.App.Infrastructure.Apis;
using Web.App.Infrastructure.Apis.Establishment;
using Web.App.Infrastructure.Apis.Insight;
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
    [ProducesResponseType<Census[]>(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Query([FromQuery] string type, [FromQuery] string id, [FromQuery] string category, [FromQuery] string dimension, [FromQuery] string? phase, [FromQuery] string? customDataId)
    {
        using (logger.BeginScope(new
        {
            type,
            id
        }))
        {
            try
            {
                var result = customDataId is not null
                    ? await GetCustomAsync(id, category, dimension, customDataId)
                    : await GetDefaultAsync(type, id, category, dimension, phase);
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
    [ProducesResponseType<CensusHistory[]>(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [Route("history")]
    public async Task<IActionResult> History([FromQuery] string id, [FromQuery] string dimension)
    {
        using (logger.BeginScope(new
        {
            id
        }))
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

    private async Task<Census[]> GetCustomAsync(string id, string category, string dimension, string customDataId)
    {

        var set = await schoolComparatorSetService.ReadComparatorSet(id, customDataId);
        if (set == null || set.Pupil.Length == 0)
        {
            return [];
        }

        var schools = set.Pupil.Where(x => x != id);
        var setQuery = BuildApiQuery(category, dimension, schools);
        var customQuery = BuildApiQuery(category, dimension);

        var defaultResults = await censusApi.Query(setQuery).GetResultOrDefault<Census[]>() ?? [];
        var customResult = await censusApi.GetCustom(id, customDataId, customQuery).GetResultOrDefault<Census>();

        return customResult != null
            ? defaultResults.Append(customResult).ToArray()
            : defaultResults;
    }

    private async Task<Census[]> GetDefaultAsync(string type, string id, string category, string dimension, string? phase)
    {
        var set = type.ToLower() switch
        {
            OrganisationTypes.School => await GetSchoolSet(id),
            OrganisationTypes.Trust => await GetTrustSet(id, phase),
            OrganisationTypes.LocalAuthority => await GetLocalAuthoritySet(id, phase),
            _ => throw new ArgumentOutOfRangeException(nameof(type))
        };
        if (set.Length == 0)
        {
            return [];
        }

        var query = BuildApiQuery(category, dimension, set);
        var result = await censusApi.Query(query).GetResultOrDefault<Census[]>();
        return result ?? [];
    }


    private async Task<string[]> GetSchoolSet(string id)
    {
        var userData = await userDataService.GetSchoolDataAsync(User, id);
        if (string.IsNullOrEmpty(userData.ComparatorSet))
        {
            var defaultSet = await schoolComparatorSetService.ReadComparatorSet(id);
            return defaultSet?.Pupil ?? [];
        }

        var userDefinedSet = await schoolComparatorSetService.ReadUserDefinedComparatorSet(id, userData.ComparatorSet);
        return userDefinedSet?.Set ?? [];
    }

    private async Task<string[]> GetTrustSet(string id, string? phase)
    {
        var trust = await establishmentApi.GetTrust(id).GetResultOrThrow<Trust>();
        var schools = trust.Schools.Where(x => x.OverallPhase == phase);
        return schools.Select(x => x.URN).OfType<string>().ToArray();
    }

    private async Task<string[]> GetLocalAuthoritySet(string id, string? phase)
    {
        var la = await establishmentApi.GetLocalAuthority(id).GetResultOrThrow<LocalAuthority>();
        var schools = la.Schools.Where(x => x.OverallPhase == phase);
        return schools.Select(x => x.URN).OfType<string>().ToArray();
    }

    private static ApiQuery BuildApiQuery(string? category = null, string? dimension = null, IEnumerable<string>? urns = null)
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