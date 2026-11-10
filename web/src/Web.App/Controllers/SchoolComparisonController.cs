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

namespace Web.App.Controllers;

[Controller]
[Route("school/{urn}/comparison")]
[ValidateUrn]
public class SchoolComparisonController(
    IEstablishmentApi establishmentApi,
    IExpenditureApi expenditureApi,
    IComparatorSetApi comparatorSetApi,
    IChartRenderingApi chartRenderingApi,
    IComparisonExpenditureService comparisonExpenditureService,
    ISchoolPerformanceDataService performanceDataService,
    ICostCodesService costCodesService,
    IUserDataService userDataService,
    IFeatureManager featureManager,
    ILogger<SchoolComparisonController> logger) : Controller
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
        using (logger.BeginScope(new { urn }))
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

                var perf = await performanceDataService.GetPerformanceDataAsync(
                    urn,
                    customComparatorSet ?? defaultComparatorSet?.Building ?? [],
                    customComparatorSet ?? defaultComparatorSet?.Pupil ?? []);

                SpendingComparisonSubCategoriesViewModel? subCategories = null;
                SchoolExpenditureWithPerformance[]? buildingResult = null;
                SchoolExpenditureWithPerformance[]? pupilResult = null;

                var useFilters = await featureManager.IsEnabledAsync(FeatureFlags.SchoolComparisonFilter);

                var hasTopRankedAttainment8 = perf.ShowAttainment8 && (perf.TopRankedBuilding.Count > 0 || perf.TopRankedPupil.Count > 0);
                var effectiveDisplayAttainment8 = displayAttainment8 && hasTopRankedAttainment8;

                if (useFilters)
                {
                    var expenditures = await comparisonExpenditureService.GetExpendituresAsync(User, urn, resultAs);
                    buildingResult = performanceDataService.Merge(expenditures.BuildingExpenditure, perf.Bandings, perf.TopRankedBuilding);
                    pupilResult = performanceDataService.Merge(expenditures.PupilExpenditure, perf.Bandings, perf.TopRankedPupil);

                    subCategories = new SpendingComparisonSubCategoriesViewModel(
                        buildingResult,
                        pupilResult,
                        selectedSubCategories,
                        urn,
                        costCodes);

                    await PopulateChartsAsync(urn, subCategories, resultAs, bandingsAs, displayAttainment8);
                }

                var viewModel = new SchoolComparisonViewModel(
                    school,
                    costCodes,
                    userData.ComparatorSet,
                    userData.CustomData,
                    expenditure,
                    defaultComparatorSet,
                    perf.Bandings,
                    subCategories,
                    buildingResult,
                    pupilResult,
                    perf.ShowAttainment8 ? perf.TopRankedBuilding : [],
                    perf.ShowAttainment8 ? perf.TopRankedPupil : [])
                {
                    SelectedSubCategories = selectedSubCategories,
                    ViewAs = viewAs,
                    ResultAs = resultAs,
                    BandingsAs = bandingsAs,
                    ExpandFilterGroup = expandFilterGroup,
                    DisplayAttainment8 = effectiveDisplayAttainment8
                };

                return View(useFilters ? $"{nameof(Index)}Filters" : nameof(Index), viewModel);
            }
            catch (Exception e)
            {
                logger.LogError(e, "An error displaying school comparison: {DisplayUrl}", Request.GetDisplayUrl());
                return e is StatusCodeException s ? StatusCode((int)s.Status) : StatusCode(500);
            }
        }
    }

    [HttpPost]
    public IActionResult Index(
        string urn,
        bool displayAttainment8,
        int[]? selectedSubCategories,
        int viewAs,
        int resultAs,
        int[]? bandingsAs)
    {
        var routeValues = new RouteValueDictionary
        {
            { nameof(urn), urn }
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

        return RedirectToAction(nameof(Index), routeValues);
    }

    [HttpGet]
    [Route("custom-data")]
    [SchoolAuthorization]
    [SchoolRequestTelemetry(TrackedRequestFeature.CustomisedData)]
    public async Task<IActionResult> CustomData(string urn)
    {
        using (logger.BeginScope(new { urn }))
        {
            try
            {
                var userCustomData = await userDataService.GetCustomDataActiveAsync(User, urn);
                if (userCustomData?.Status != Pipeline.JobStatus.Complete)
                {
                    return RedirectToAction("Index", "School", new { urn });
                }

                ViewData[ViewDataKeys.BreadcrumbNode] = BreadcrumbNodes.SchoolCustomisedDataComparison(urn);

                var school = await establishmentApi.GetSchool(urn).GetResultOrThrow<School>();
                var costCodes = await costCodesService.GetCostCodes(school.IsPartOfTrust);
                return View(new SchoolComparisonViewModel(school, costCodes, customDataId: userCustomData.Id));
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
        using (logger.BeginScope(new { urn }))
        {
            try
            {
                var expenditures = await comparisonExpenditureService.GetExpendituresAsync(
                    User, urn, SchoolSpendingDimensions.ResultAsOptions.Actuals, customDataId);

                var perf = await performanceDataService.GetPerformanceDataAsync(
                    urn,
                    expenditures.BuildingExpenditure?.Where(x => !string.IsNullOrWhiteSpace(x.URN)).Select(x => x.URN!).ToArray(),
                    expenditures.PupilExpenditure?.Where(x => !string.IsNullOrWhiteSpace(x.URN)).Select(x => x.URN!).ToArray());

                List<string> exclude = [nameof(SchoolExpenditure.TotalInternalFloorArea)];
                if (!perf.ShowKs4Progress) exclude.Add(nameof(SchoolExpenditureWithPerformance.ProgressBanding));
                if (!perf.ShowAttainment8) exclude.Add(nameof(SchoolExpenditureWithPerformance.TopRankedAttainment8));

                var csvResults = new CsvResult[]
                {
                    new(performanceDataService.Merge(expenditures.BuildingExpenditure, perf.Bandings, perf.TopRankedBuilding),
                        $"comparison-{urn}-building.csv", [.. exclude]),
                    new(performanceDataService.Merge(expenditures.PupilExpenditure, perf.Bandings, perf.TopRankedPupil),
                        $"comparison-{urn}-pupil.csv", [.. exclude])
                };

                return new CsvResults(csvResults, $"comparison-{urn}.zip");
            }
            catch (Exception e)
            {
                logger.LogError(e, "An error downloading expenditure data: {DisplayUrl}", Request.GetDisplayUrl());
                return StatusCode(500);
            }
        }
    }

    private async Task PopulateChartsAsync(
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
                format => Uri.UnescapeDataString(Url.Action("Index", "School", new { urn = format }) ?? string.Empty),
                resultAs,
                x.group.ComparatorSetType,
                bandingsAs,
                displayAttainment8));

        try
        {
            var charts = await chartRenderingApi
                .PostHorizontalBarCharts(new PostHorizontalBarChartsRequest<SchoolComparisonDatum>(requests))
                .GetResultOrDefault<ChartResponse[]>() ?? [];

            var chartMap = charts
                .Where(c => !string.IsNullOrEmpty(c.Id))
                .GroupBy(c => c.Id!)
                .ToDictionary(g => g.Key, g => g.First().Html);

            foreach (var item in subCategories.Groups.SelectMany(g => g.Items))
            {
                if (item.Uuid != null && chartMap.TryGetValue(item.Uuid, out var html))
                {
                    item.ChartSvg = html;
                }
            }
        }
        catch (Exception e)
        {
            logger.LogWarning(e, "Unable to load charts from API");
        }
    }
}
