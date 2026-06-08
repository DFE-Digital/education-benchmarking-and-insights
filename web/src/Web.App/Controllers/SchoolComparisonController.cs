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
    IFeatureManager featureManager)
    : Controller
{
    [HttpGet]
    [SchoolRequestTelemetry(TrackedRequestFeature.BenchmarkCosts)]
    public async Task<IActionResult> Index(
        string urn,
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

                var bandings = await featureManager.IsEnabledAsync(FeatureFlags.KS4ProgressBanding)
                    ? await progressBandingsService.GetKS4ProgressBandings(customComparatorSet ?? defaultComparatorSet?.All ?? [])
                    : null;

                SpendingComparisonSubCategoriesViewModel? subCategories = null;

                if (await featureManager.IsEnabledAsync(FeatureFlags.SchoolComparisonFilter))
                {
                    var buildingResult = MergeProgressBandings(await GetDefaultSchoolExpenditure(urn, true, resultAs), bandings);
                    var pupilResult = MergeProgressBandings(await GetDefaultSchoolExpenditure(urn, false, resultAs), bandings);

                    subCategories = new SpendingComparisonSubCategoriesViewModel(
                        buildingResult,
                        pupilResult,
                        selectedSubCategories,
                        urn,
                        costCodes);

                    var charts = await BuildCharts(urn, subCategories, resultAs, bandingsAs);

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
                    subCategories)
                {
                    SelectedSubCategories = selectedSubCategories,
                    ViewAs = viewAs,
                    ResultAs = resultAs,
                    BandingsAs = bandingsAs,
                    ExpandFilterGroup = expandFilterGroup
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
        SchoolSpendingDimensions.BandingsAsOptions[] bandingsAs)
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
                bandingsAs));

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
    public IActionResult Index(string urn, int[]? selectedSubCategories, int viewAs, int resultAs, int[]? bandingsAs) => RedirectToAction("Index", new
    {
        urn,
        selectedSubCategories,
        viewAs,
        resultAs,
        bandingsAs
    });

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

                KS4ProgressBandings? bandings = null;
                if (await featureManager.IsEnabledAsync(FeatureFlags.KS4ProgressBanding))
                {
                    var urns = buildingResult?
                        .Where(r => !string.IsNullOrWhiteSpace(r.URN))
                        .Select(r => r.URN!)
                        .Union(pupilResult?
                            .Where(r => !string.IsNullOrWhiteSpace(r.URN))
                            .Select(r => r.URN!) ?? []);
                    bandings = await progressBandingsService.GetKS4ProgressBandings(urns?.ToArray() ?? []);
                }

                string[] exclude = bandings == null || bandings.Items.Length == 0
                    ? [nameof(SchoolExpenditure.TotalInternalFloorArea), nameof(SchoolExpenditureWithProgress.ProgressBanding)]
                    : [nameof(SchoolExpenditure.TotalInternalFloorArea)];

                IEnumerable<CsvResult> csvResults =
                [
                    new(MergeProgressBandings(buildingResult, bandings), $"comparison-{urn}-building.csv", exclude),
                    new(MergeProgressBandings(pupilResult, bandings), $"comparison-{urn}-pupil.csv", exclude)
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
            .QuerySchools(BuildApiQuery(userDefinedSet.Set))
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

    private static SchoolExpenditureWithProgress[] MergeProgressBandings(SchoolExpenditure[]? expenditures, KS4ProgressBandings? bandings)
    {
        if (expenditures == null)
        {
            return [];
        }
        return bandings == null
            ? expenditures.Select(e => new SchoolExpenditureWithProgress(e, null)).ToArray()
            : expenditures.Select(e => new SchoolExpenditureWithProgress(e, bandings[e.URN])).ToArray();
    }

    private async Task<string> GetViewName(string baseViewName)
    {
        var useFilters = await featureManager.IsEnabledAsync(FeatureFlags.SchoolComparisonFilter);
        return useFilters ? $"{baseViewName}Filters" : baseViewName;
    }
}
