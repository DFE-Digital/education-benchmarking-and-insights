using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.FeatureManagement;
using Web.App.ActionResults;
using Web.App.Attributes;
using Web.App.Attributes.RequestTelemetry;
using Web.App.Domain;
using Web.App.Domain.Charts;
using Web.App.Domain.Schools;
using Web.App.Infrastructure.Apis;
using Web.App.Infrastructure.Apis.Benchmark;
using Web.App.Infrastructure.Apis.Establishment;
using Web.App.Infrastructure.Apis.Insight;
using Web.App.Infrastructure.Extensions;
using Web.App.Services;
using Web.App.ViewModels;
// ReSharper disable UnusedAutoPropertyAccessor.Local

namespace Web.App.Controllers;

[Controller]
[Route("school/{urn}/comparison")]
[ValidateUrn]
public class SchoolComparisonController(
    IEstablishmentApi establishmentApi,
    IExpenditureApi expenditureApi,
    IComparatorSetApi comparatorSetApi,
    IChartRenderingApi chartRenderingApi,
    ILogger<SchoolComparisonController> logger,
    IUserDataService userDataService,
    ISchoolComparatorSetService schoolComparatorSetService,
    ICostCodesService costCodesService,
    IProgressBandingsService progressBandingsService,
    IComparatorSetRankingService attainment8ComparatorSetRankingService,
    IFeatureManager featureManager)
    : Controller
{
    [HttpGet]
    [SchoolRequestTelemetry(TrackedRequestFeature.BenchmarkCosts)]
    public async Task<IActionResult> Index(
        string urn,
        bool displayAttainment8,
        SchoolSpendingCategories.SubCategoryFilter[] selectedSubCategories,
        SchoolSpendingDimensions.BandingsAsOptions[] bandingsAs,
        Views.ViewAsOptions viewAs = Views.ViewAsOptions.Chart,
        SchoolSpendingDimensions.ResultAsOptions resultAs = SchoolSpendingDimensions.ResultAsOptions.SpendPerUnit,
        SchoolSpendingCategories.CategoryGroup? expandFilterGroup = null)
    {
        using (logger.BeginScope(new
        {
            urn
        }))
        {
            try
            {
                ViewData[ViewDataKeys.BreadcrumbNode] = BreadcrumbNodes.SchoolComparison(urn);

                var school = await establishmentApi.GetSchool(urn).GetResultOrThrow<School>();
                var expenditure = await expenditureApi.School(urn).GetResultOrDefault<SchoolExpenditure>();
                var defaultComparatorSet = await comparatorSetApi.GetDefaultSchoolAsync(urn).GetResultOrDefault<SchoolComparatorSet>();
                var userData = await userDataService.GetSchoolDataAsync(User, urn);
                var costCodes = await costCodesService.GetCostCodes(school.IsPartOfTrust);

                string[]? customComparatorSet = null;
                if (userData.ComparatorSet != null)
                {
                    var userDefinedSet = await comparatorSetApi.GetUserDefinedSchoolAsync(urn, userData.ComparatorSet)
                        .GetResultOrDefault<UserDefinedSchoolComparatorSet>();
                    customComparatorSet = userDefinedSet?.Set;
                }

                var showKs4Progress = await featureManager.IsEnabledAsync(FeatureFlags.KS4ProgressBanding);
                var showAttainment8 = await featureManager.IsEnabledAsync(FeatureFlags.Attainment8);

                // these two feature flags are mutually exclusive and therefore here we guard against incorrect config
                if (showKs4Progress && showAttainment8)
                {
                    showKs4Progress = false;
                    showAttainment8 = false;

                    logger.LogWarning(
                        "An error detected in feature management for school comparison: {KS4ProgressBanding} and {Attainment8} are both true",
                        nameof(FeatureFlags.KS4ProgressBanding),
                        nameof(FeatureFlags.Attainment8));
                    // TODO: alert as well as log
                }

                var bandings = showKs4Progress
                    ? await progressBandingsService.GetKS4ProgressBandings(customComparatorSet ?? defaultComparatorSet?.All ?? [])
                    : null;

                var topRankedBuilding = showAttainment8
                    ? await attainment8ComparatorSetRankingService.GetTopRankedAsync(
                        urn,
                        customComparatorSet ?? defaultComparatorSet?.Building ?? [])
                    : null;

                var topRankedPupil = showAttainment8
                    ? await attainment8ComparatorSetRankingService.GetTopRankedAsync(
                        urn,
                        customComparatorSet ?? defaultComparatorSet?.Pupil ?? [])
                    : null;

                SpendingComparisonSubCategoriesViewModel? subCategories = null;

                SchoolExpenditureWithPerformance[]? buildingResult = null;
                SchoolExpenditureWithPerformance[]? pupilResult = null;

                if (await featureManager.IsEnabledAsync(FeatureFlags.SchoolComparisonFilter))
                {
                    buildingResult = MergePerformance(await GetDefaultSchoolExpenditure(urn, true, resultAs), bandings, topRankedBuilding ?? []);
                    pupilResult = MergePerformance(await GetDefaultSchoolExpenditure(urn, false, resultAs), bandings, topRankedPupil ?? []);

                    subCategories = new SpendingComparisonSubCategoriesViewModel(
                        buildingResult,
                        pupilResult,
                        selectedSubCategories,
                        urn,
                        costCodes);

                    var charts = await BuildCharts(urn, subCategories, resultAs, bandingsAs, displayAttainment8);

                    subCategories.Groups.ForEach(group =>
                    {
                        group.Items.ForEach(item =>
                        {
                            var chart = charts.FirstOrDefault(c => c.Id != null && c.Id == item.Uuid);
                            if (chart != null)
                            {
                                item.ChartSvg = chart.Html;
                            }
                        });
                    });
                }

                var viewModel = new SchoolComparisonViewModel(
                    school,
                    costCodes,
                    userData.ComparatorSet,
                    userData.CustomData,
                    expenditure,
                    defaultComparatorSet,
                    bandings,
                    subCategories,
                    buildingResult,
                    pupilResult,
                    showAttainment8 ? topRankedBuilding : [],
                    showAttainment8 ? topRankedPupil : [])
                {
                    SelectedSubCategories = selectedSubCategories,
                    ViewAs = viewAs,
                    ResultAs = resultAs,
                    BandingsAs = bandingsAs,
                    ExpandFilterGroup = expandFilterGroup,
                    DisplayAttainment8 = displayAttainment8
                };

                var viewName = await GetViewName(nameof(Index));
                return View(viewName, viewModel);
            }
            catch (Exception e)
            {
                logger.LogError(e, "An error displaying school comparison: {DisplayUrl}", Request.GetDisplayUrl());
                return e is StatusCodeException s ? StatusCode((int)s.Status) : StatusCode(500);
            }
        }
    }

    private async Task<ChartResponse[]> BuildCharts(
        string urn,
        SpendingComparisonSubCategoriesViewModel subCategories,
        SchoolSpendingDimensions.ResultAsOptions resultAs,
        SchoolSpendingDimensions.BandingsAsOptions[] bandingsAs,
        bool displayAttainment8)
    {
        var requests = subCategories
            .Groups
            .SelectMany(group => group.Items.Select(item => new { group, item }))
            .Select(x => new SchoolComparisonHorizontalBarChartRequest(
                x.item.Uuid!,
                urn,
                x.item.Data!,
                format => Uri.UnescapeDataString(
                    Url.Action("Index", "School", new
                    {
                        urn = format
                    }) ?? string.Empty),
                resultAs,
                x.group.ComparatorSetType,
                bandingsAs,
                displayAttainment8));

        ChartResponse[] charts = [];
        try
        {
            charts = await chartRenderingApi
                .PostHorizontalBarCharts(new PostHorizontalBarChartsRequest<SchoolComparisonDatum>(requests))
                .GetResultOrDefault<ChartResponse[]>() ?? [];
        }
        catch (Exception e)
        {
            logger.LogWarning(e, "Unable to load charts from API");
        }

        return charts;
    }

    [HttpPost]
    public IActionResult Index(
        string urn,
        int[]? selectedSubCategories,
        int viewAs,
        int resultAs,
        int[]? bandingsAs,
        bool displayAttainment8 = false)
    {
        var routeValues = new RouteValueDictionary
        {
            { nameof(urn), urn },
        };

        if (displayAttainment8)
        {
            routeValues.Add(nameof(displayAttainment8), true);
        }

        if (selectedSubCategories is { Length: > 0 })
        {
            routeValues.Add(nameof(selectedSubCategories), selectedSubCategories);
        }

        routeValues.Add(nameof(viewAs), viewAs);
        routeValues.Add(nameof(resultAs), resultAs);

        if (bandingsAs is { Length: > 0 })
        {
            routeValues.Add(nameof(bandingsAs), bandingsAs);
        }

        return RedirectToAction("Index", routeValues);
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

                ViewData[ViewDataKeys.BreadcrumbNode] = BreadcrumbNodes.SchoolCustomisedDataComparison(urn);

                var school = await establishmentApi.GetSchool(urn).GetResultOrThrow<School>();
                var costCodes = await costCodesService.GetCostCodes(school.IsPartOfTrust);
                var viewModel = new SchoolComparisonViewModel(school, costCodes, customDataId: userCustomData.Id);

                return View(viewModel);
            }
            catch (Exception e)
            {
                logger.LogError(e, "An error displaying custom school comparison: {DisplayUrl}", Request.GetDisplayUrl());
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
                SchoolExpenditure[]? buildingResult;
                SchoolExpenditure[]? pupilResult;
                if (customDataId != null)
                {
                    buildingResult = await GetCustomSchoolExpenditure(urn, true, customDataId);
                    pupilResult = await GetCustomSchoolExpenditure(urn, false, customDataId);
                }
                else
                {
                    buildingResult = await GetDefaultSchoolExpenditure(urn, true);
                    pupilResult = await GetDefaultSchoolExpenditure(urn, false);
                }

                var showKs4Progress = await featureManager.IsEnabledAsync(FeatureFlags.KS4ProgressBanding);
                var showAttainment8 = await featureManager.IsEnabledAsync(FeatureFlags.Attainment8);

                // these two feature flags are mutually exclusive and therefore here we guard against incorrect config
                if (showKs4Progress && showAttainment8)
                {
                    showKs4Progress = false;
                    showAttainment8 = false;

                    logger.LogWarning(
                        "An error detected in feature management for school comparison: {KS4ProgressBanding} and {Attainment8} are both true",
                        nameof(FeatureFlags.KS4ProgressBanding),
                        nameof(FeatureFlags.Attainment8));
                    // TODO: alert as well as log
                }

                // TODO: refactor here move the comparator set out of the private methods
                var topRankedBuilding = showAttainment8
                    ? await attainment8ComparatorSetRankingService.GetTopRankedAsync(
                        urn,
                        buildingResult?.Select(x => x.URN).ToArray() ?? [])
                    : null;

                var topRankedPupil = showAttainment8
                    ? await attainment8ComparatorSetRankingService.GetTopRankedAsync(
                        urn,
                        pupilResult?.Select(x => x.URN).ToArray() ?? [])
                    : null;

                KS4ProgressBandings? bandings = null;
                if (showKs4Progress)
                {
                    var urns = buildingResult?
                        .Where(r => !string.IsNullOrWhiteSpace(r.URN))
                        .Select(r => r.URN!)
                        .Union(pupilResult?
                            .Where(r => !string.IsNullOrWhiteSpace(r.URN))
                            .Select(r => r.URN!) ?? []);
                    bandings = await progressBandingsService.GetKS4ProgressBandings(urns?.ToArray() ?? []);
                }

                List<string> exclude = [nameof(SchoolExpenditure.TotalInternalFloorArea)];

                if (!showKs4Progress)
                {
                    exclude.Add(nameof(SchoolExpenditureWithPerformance.ProgressBanding));
                }

                if (!showAttainment8)
                {
                    exclude.Add(nameof(SchoolExpenditureWithPerformance.TopRankedAttainment8));
                }

                IEnumerable<CsvResult> csvResults =
                [
                    new(MergePerformance(buildingResult, bandings, topRankedBuilding ?? []), $"comparison-{urn}-building.csv", [.. exclude]),
                    new(MergePerformance(pupilResult, bandings, topRankedPupil ?? []), $"comparison-{urn}-pupil.csv", [.. exclude])
                ];
                return new CsvResults(csvResults, $"comparison-{urn}.zip");
            }
            catch (Exception e)
            {
                logger.LogError(e, "An error downloading expenditure data: {DisplayUrl}", Request.GetDisplayUrl());
                return StatusCode(500);
            }
        }
    }

    private async Task<SchoolExpenditure[]?> GetCustomSchoolExpenditure(string urn, bool useBuildingSet, string customDataId)
    {
        var customSet = await schoolComparatorSetService.ReadComparatorSet(urn, customDataId);
        var set = useBuildingSet
            ? customSet?.Building
            : customSet?.Pupil;

        if (set == null || set.Length == 0)
        {
            return [];
        }

        var schools = set.Where(x => x != urn).ToArray();
        var customResult = await expenditureApi
            .SchoolCustom(urn, customDataId, BuildApiQuery())
            .GetResultOrDefault<SchoolExpenditure>();

        var defaultResult = await expenditureApi
            .QuerySchools(BuildApiQuery(schools))
            .GetResultOrDefault<SchoolExpenditure[]>();

        return customResult != null
            ? defaultResult?.Append(customResult).ToArray()
            : defaultResult;
    }

    private async Task<SchoolExpenditure[]> GetDefaultSchoolExpenditure(
        string urn,
        bool useBuildingSet,
        SchoolSpendingDimensions.ResultAsOptions resultAs = SchoolSpendingDimensions.ResultAsOptions.Actuals)
    {
        var userData = await userDataService.GetSchoolDataAsync(User, urn);
        if (string.IsNullOrEmpty(userData.ComparatorSet))
        {
            var defaultSet = await schoolComparatorSetService.ReadComparatorSet(urn);
            var set = useBuildingSet
                ? defaultSet?.Building
                : defaultSet?.Pupil;

            if (set == null || set.Length == 0)
            {
                return [];
            }

            var defaultResult = await expenditureApi
                .QuerySchools(BuildApiQuery(set, resultAs))
                .GetResultOrThrow<SchoolExpenditure[]>();

            return defaultResult;
        }

        var userDefinedSet = await schoolComparatorSetService.ReadUserDefinedComparatorSet(urn, userData.ComparatorSet);
        if (userDefinedSet == null || userDefinedSet.Set.Length == 0)
        {
            return [];
        }

        var userDefinedResult = await expenditureApi
            .QuerySchools(BuildApiQuery(userDefinedSet.Set, resultAs))
            .GetResultOrThrow<SchoolExpenditure[]>();

        return userDefinedResult;
    }

    private static ApiQuery BuildApiQuery(
        IEnumerable<string>? urns = null,
        SchoolSpendingDimensions.ResultAsOptions resultAs = SchoolSpendingDimensions.ResultAsOptions.Actuals)
    {
        var query = new ApiQuery()
            .AddIfNotNull("dimension", resultAs.GetQueryParam());

        foreach (var urn in urns ?? [])
        {
            query.AddIfNotNull("urns", urn);
        }

        return query;
    }

    private static SchoolExpenditureWithPerformance[] MergePerformance(
        SchoolExpenditure[]? expenditures,
        KS4ProgressBandings? bandings,
        IReadOnlyList<string> topRanked)
    {
        var expendituresWithBanding = MergeProgressBandings(expenditures, bandings);
        return MergeTopRanked(expendituresWithBanding, topRanked);
    }

    private static SchoolExpenditureWithPerformance[] MergeProgressBandings(
        SchoolExpenditure[]? expenditures,
        KS4ProgressBandings? bandings)
    {
        if (expenditures == null)
        {
            return [];
        }
        return bandings == null
            ? expenditures.Select(e => new SchoolExpenditureWithPerformance(e, null)).ToArray()
            : expenditures.Select(e => new SchoolExpenditureWithPerformance(e, bandings[e.URN])).ToArray();
    }

    private static SchoolExpenditureWithPerformance[] MergeTopRanked(
        SchoolExpenditureWithPerformance[]? expenditures,
        IReadOnlyList<string>? topRanked)
    {
        if (expenditures == null || expenditures.Length == 0)
        {
            return [];
        }

        if (topRanked == null || topRanked.Count == 0)
        {
            return expenditures;
        }

        var topSet = new HashSet<string>(topRanked, StringComparer.OrdinalIgnoreCase);

        foreach (var expenditure in expenditures)
        {
            if (topSet.Contains(expenditure.URN!))
            {
                expenditure.TopRankedAttainment8 = true;
            }
        }

        return expenditures;
    }

    private async Task<string> GetViewName(string baseViewName)
    {
        var useFilters = await featureManager.IsEnabledAsync(FeatureFlags.SchoolComparisonFilter);
        return useFilters ? $"{baseViewName}Filters" : baseViewName;
    }
}
