using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.FeatureManagement;
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
// ReSharper disable UnusedAutoPropertyAccessor.Local
// ReSharper disable InvertIf

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
    ISchoolComparatorSetService schoolComparatorSetService,
    IProgressBandingsService progressBandingsService,
    IFeatureManager featureManager)
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

                string[]? customComparatorSet = null;
                if (userData.ComparatorSet != null)
                {
                    var userDefinedSet = await comparatorSetApi.GetUserDefinedSchoolAsync(urn, userData.ComparatorSet)
                        .GetResultOrDefault<UserDefinedSchoolComparatorSet>();
                    customComparatorSet = userDefinedSet?.Set;
                }

                var bandings = await featureManager.IsEnabledAsync(FeatureFlags.KS4ProgressBanding)
                    ? await progressBandingsService.GetKS4ProgressBandings(customComparatorSet ?? defaultComparatorSet?.Pupil ?? [])
                    : null;
                var viewModel = new SchoolCensusViewModel(school, userData.ComparatorSet, userData.CustomData, census, defaultComparatorSet, bandings);
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

                KS4ProgressBandings? bandings = null;
                if (await featureManager.IsEnabledAsync(FeatureFlags.KS4ProgressBanding))
                {
                    var urns = result
                        .Where(r => !string.IsNullOrWhiteSpace(r.URN))
                        .Select(r => r.URN!);
                    bandings = await progressBandingsService.GetKS4ProgressBandings(urns?.ToArray() ?? []);
                }

                return new CsvResults([new CsvResult(MergeProgressBandings(result, bandings), $"census-{urn}.csv")], $"census-{urn}.zip");
            }
            catch (Exception e)
            {
                logger.LogError(e, "An error getting census data: {DisplayUrl}", Request.GetDisplayUrl());
                return StatusCode(500);
            }
        }
    }

    private async Task<School> School(string urn) => await establishmentApi
        .GetSchool(urn)
        .GetResultOrThrow<School>();

    private async Task<Census?> Census(string urn) => await censusApi
        .Get(urn)
        .GetResultOrDefault<Census>();

    private async Task<(string? CustomData, string? ComparatorSet)> UserData(string urn) => await userDataService
        .GetSchoolDataAsync(User, urn);

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

    private static IEnumerable<object>? MergeProgressBandings(Census[]? censuses, KS4ProgressBandings? bandings)
    {
        if (censuses == null || bandings == null)
        {
            return censuses;
        }

        return censuses.Select(e => new CensusWithProgress(e, bandings[e.URN]));
    }

    private record CensusWithProgress : Census
    {
        public CensusWithProgress(Census census, KS4ProgressBanding? banding) : base(census)
        {
            Workforce = census.Workforce;
            WorkforceHeadcount = census.WorkforceHeadcount;
            Teachers = census.Teachers;
            SeniorLeadership = census.SeniorLeadership;
            TeachingAssistant = census.TeachingAssistant;
            TeachingAssistant = census.TeachingAssistant;
            NonClassroomSupportStaff = census.NonClassroomSupportStaff;
            AuxiliaryStaff = census.AuxiliaryStaff;
            PercentTeacherWithQualifiedStatus = census.PercentTeacherWithQualifiedStatus;

            URN = census.URN;
            SchoolName = census.SchoolName;
            SchoolType = census.SchoolType;
            LAName = census.LAName;
            TotalPupils = census.TotalPupils;

            if (banding?.Banding is KS4ProgressBandings.Banding.AboveAverage or KS4ProgressBandings.Banding.WellAboveAverage)
            {
                ProgressBanding = banding.Banding.ToStringValue();
            }
        }

        [PropertyOrder(int.MaxValue)]
        public string? ProgressBanding { get; set; }
    }
}