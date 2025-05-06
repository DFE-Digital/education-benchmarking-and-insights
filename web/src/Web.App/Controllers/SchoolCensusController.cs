using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.FeatureManagement.Mvc;
using Web.App.ActionResults;
using Web.App.Attributes;
using Web.App.Attributes.RequestTelemetry;
using Web.App.Domain;
using Web.App.Infrastructure.Apis;
using Web.App.Infrastructure.Apis.Benchmark;
using Web.App.Infrastructure.Apis.Establishment;
using Web.App.Infrastructure.Apis.Insight;
using Web.App.Infrastructure.Extensions;
using Web.App.Services;
using Web.App.ViewModels;

namespace Web.App.Controllers;

[Controller]
[Route("school/{urn}/census")]
[ValidateUrn]
public class SchoolCensusController(
    IEstablishmentApi establishmentApi,
    ICensusApi censusApi,
    IComparatorSetApi comparatorSetApi,
    ILogger<SchoolCensusController> logger,
    IUserDataService userDataService,
    ISchoolComparatorSetService schoolComparatorSetService)
    : Controller
{
    [HttpGet]
    [SchoolRequestTelemetry(TrackedRequestFeature.BenchmarkWorkforce)]
    public async Task<IActionResult> Index(string urn)
    {
        using (logger.BeginScope(new
        {
            urn
        }))
        {
            try
            {
                ViewData[ViewDataKeys.BreadcrumbNode] = BreadcrumbNodes.SchoolCensus(urn);

                var school = await School(urn);
                var census = await Census(urn);
                var userData = await UserData(urn);
                var defaultComparatorSet = await comparatorSetApi.GetDefaultSchoolAsync(urn).GetResultOrDefault<SchoolComparatorSet>();

                var viewModel = new SchoolCensusViewModel(school, userData.ComparatorSet, userData.CustomData, census, defaultComparatorSet);
                return View(viewModel);
            }
            catch (Exception e)
            {
                logger.LogError(e, "An error displaying school census: {DisplayUrl}", Request.GetDisplayUrl());
                return e is StatusCodeException s ? StatusCode((int)s.Status) : StatusCode(500);
            }
        }
    }

    [HttpGet]
    [Route("custom-data")]
    [SchoolAuthorization]
    [FeatureGate(FeatureFlags.CustomData)]
    [SchoolRequestTelemetry(TrackedRequestFeature.CustomisedData)]
    public async Task<IActionResult> CustomData(string urn)
    {
        using (logger.BeginScope(new
        {
            urn
        }))
        {
            try
            {
                var userCustomData = await userDataService.GetCustomDataActiveAsync(User, urn);
                if (userCustomData?.Status != Pipeline.JobStatus.Complete)
                {
                    return RedirectToAction("Index", "School", new
                    {
                        urn
                    });
                }

                ViewData[ViewDataKeys.BreadcrumbNode] = BreadcrumbNodes.SchoolCustomisedDataCensus(urn);

                var school = await School(urn);
                var viewModel = new SchoolCensusViewModel(school, customDataId: userCustomData.Id);

                return View(viewModel);
            }
            catch (Exception e)
            {
                logger.LogError(e, "An error displaying custom school census: {DisplayUrl}", Request.GetDisplayUrl());
                return e is StatusCodeException s ? StatusCode((int)s.Status) : StatusCode(500);
            }
        }
    }

    [HttpGet]
    [Produces("application/zip")]
    [ProducesResponseType<byte[]>(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [Route("download")]
    public async Task<IActionResult> Download(string urn, [FromQuery] string? customDataId)
    {
        using (logger.BeginScope(new
        {
            urn
        }))
        {
            try
            {
                var result = customDataId is not null
                    ? await GetCustomAsync(urn, customDataId)
                    : await GetDefaultAsync(urn);
                return new CsvResults([new CsvResult(result, $"census-{urn}.csv")], $"census-{urn}.zip");
            }
            catch (Exception e)
            {
                logger.LogError(e, "An error getting census data: {DisplayUrl}", Request.GetDisplayUrl());
                return StatusCode(500);
            }
        }
    }

    private async Task<School> School(string urn)
    {
        return await establishmentApi
            .GetSchool(urn)
            .GetResultOrThrow<School>();
    }

    private async Task<Census?> Census(string urn)
    {
        return await censusApi
            .Get(urn)
            .GetResultOrDefault<Census>();
    }

    private async Task<(string? CustomData, string? ComparatorSet)> UserData(string urn)
    {
        return await userDataService
            .GetSchoolDataAsync(User, urn);
    }

    private async Task<Census[]> GetCustomAsync(string urn, string customDataId)
    {
        var set = await schoolComparatorSetService.ReadComparatorSet(urn, customDataId);
        if (set == null || set.Pupil.Length == 0)
        {
            return [];
        }

        var schools = set.Pupil.Where(x => x != urn);
        var setQuery = BuildApiQuery(schools);
        var customQuery = BuildApiQuery();

        var defaultResults = await censusApi.Query(setQuery).GetResultOrDefault<Census[]>() ?? [];
        var customResult = await censusApi.GetCustom(urn, customDataId, customQuery).GetResultOrDefault<Census>();

        return customResult != null
            ? defaultResults.Append(customResult).ToArray()
            : defaultResults;
    }

    private async Task<Census[]> GetDefaultAsync(string urn)
    {
        var query = BuildApiQuery(await GetSchoolSet(urn));
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

    private static ApiQuery BuildApiQuery(IEnumerable<string>? urns = null, string? dimension = "Total")
    {
        var query = new ApiQuery()
            .AddIfNotNull("dimension", dimension);

        foreach (var urn in urns ?? [])
        {
            query.AddIfNotNull("urns", urn);
        }

        return query;
    }
}