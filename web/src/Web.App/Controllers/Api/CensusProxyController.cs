using Microsoft.AspNetCore.Http.Extensions;
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
                var result = await SchoolCensusHistory(id, dimension);
                return new JsonResult(result);
            }
            catch (Exception e)
            {
                logger.LogError(e, "An error getting census history data: {DisplayUrl}", Request.GetDisplayUrl());
                return StatusCode(500);
            }
        }
    }

    /// <param name="id" example="140565"></param>
    /// <param name="dimension" example="HeadcountPerFte"></param>
    /// <param name="phase" example="Secondary"></param>
    /// <param name="financeType" example="Academy"></param>
    [HttpGet]
    [Produces("application/json")]
    [ProducesResponseType<HistoryComparison<CensusHistory>>(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [Route("history/comparison")]
    public async Task<IActionResult> HistoryComparison(
        [FromQuery] string id,
        [FromQuery] string dimension,
        [FromQuery] string? phase,
        [FromQuery] string? financeType)
    {
        using (logger.BeginScope(new
        {
            id
        }))
        {
            try
            {
                var result = await GetSchoolHistoryComparison(id, dimension, phase, financeType);
                return new JsonResult(result);
            }
            catch (Exception e)
            {
                logger.LogError(e, "An error getting census history trends data: {DisplayUrl}", Request.GetDisplayUrl());
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
        var query = type.ToLower() switch
        {
            OrganisationTypes.School => BuildApiQuery(category, dimension, await GetSchoolSet(id)),
            OrganisationTypes.Trust => BuildApiQuery(category, dimension, null, phase, id),
            OrganisationTypes.LocalAuthority => BuildApiQuery(category, dimension, null, phase, null, id),
            _ => throw new ArgumentOutOfRangeException(nameof(type))
        };

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

    private static ApiQuery BuildApiQuery(
        string? category = null,
        string? dimension = null,
        IEnumerable<string>? urns = null,
        string? phase = null,
        string? companyNumber = null,
        string? laCode = null,
        string? financeType = null)
    {
        var query = new ApiQuery()
            .AddIfNotNull("category", category)
            .AddIfNotNull("dimension", dimension)
            .AddIfNotNull("financeType", financeType)
            .AddIfNotNull("phase", phase)
            .AddIfNotNull("companyNumber", companyNumber)
            .AddIfNotNull("laCode", laCode);

        foreach (var urn in urns ?? [])
        {
            query.AddIfNotNull("urns", urn);
        }

        return query;
    }

    private async Task<CensusHistory[]?> SchoolCensusHistory(string urn, string dimension) => await censusApi
        .SchoolHistory(urn, BuildApiQuery(dimension: dimension))
        .GetResultOrDefault<CensusHistory[]>();

    private async Task<CensusHistory[]?> SchoolCensusHistoryComparatorSetAverage(string urn, string dimension) => await censusApi
        .SchoolHistoryComparatorSetAverage(urn, BuildApiQuery(dimension: dimension))
        .GetResultOrDefault<CensusHistory[]>();

    private async Task<CensusHistory[]?> SchoolCensusHistoryNationalAverage(string urn, string dimension)
    {
        var school = await establishmentApi.GetSchool(urn).GetResultOrThrow<School>();
        return await SchoolCensusHistoryNationalAverage(school.OverallPhase, school.FinanceType, dimension);
    }

    private async Task<CensusHistory[]?> SchoolCensusHistoryNationalAverage(string? phase, string? financeType, string dimension)
    {
        var query = BuildApiQuery(null, dimension, phase: phase, financeType: financeType);
        return await censusApi
            .SchoolHistoryNationalAverage(query)
            .GetResultOrDefault<CensusHistory[]>();
    }

    private async Task<HistoryComparison<CensusHistory>> GetSchoolHistoryComparison(string urn, string dimension, string? phase, string? financeType) => new()
    {
        School = await SchoolCensusHistory(urn, dimension),
        ComparatorSetAverage = await SchoolCensusHistoryComparatorSetAverage(urn, dimension),
        NationalAverage = string.IsNullOrWhiteSpace(phase) || string.IsNullOrWhiteSpace(financeType)
            ? await SchoolCensusHistoryNationalAverage(urn, dimension)
            : await SchoolCensusHistoryNationalAverage(phase, financeType, dimension)
    };
}