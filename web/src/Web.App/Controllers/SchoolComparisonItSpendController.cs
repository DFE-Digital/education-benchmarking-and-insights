using System.Net;
using Microsoft.ApplicationInsights;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.FeatureManagement.Mvc;
using Web.App.ActionResults;
using Web.App.Attributes;
using Web.App.Attributes.RequestTelemetry;
using Web.App.Domain;
using Web.App.Domain.Charts;
using Web.App.Infrastructure.Apis;
using Web.App.Infrastructure.Apis.Benchmark;
using Web.App.Infrastructure.Apis.ChartRendering;
using Web.App.Infrastructure.Apis.Establishment;
using Web.App.Infrastructure.Apis.Insight;
using Web.App.Infrastructure.Extensions;
using Web.App.ViewModels;

namespace Web.App.Controllers;

[Controller]
[Route("school/{urn}/benchmark-it-spending")]
[ValidateUrn]
[FeatureGate(FeatureFlags.CfrItSpendBreakdown)]
public class SchoolComparisonItSpendController(
    IEstablishmentApi establishmentApi,
    IChartRenderingApi chartRenderingApi,
    IComparatorSetApi comparatorSetApi,
    IItSpendApi itSpendApi,
    ILogger<SchoolComparisonController> logger,
    TelemetryClient telemetryClient) : Controller
{
    [HttpGet]
    [SchoolRequestTelemetry(TrackedRequestFeature.BenchmarkItSpend)]
    public async Task<IActionResult> Index(string urn,
        ItSpendingCategories.SubCategoryFilter[] selectedSubCategories,
        Dimensions.ResultAsOptions resultAs = Dimensions.ResultAsOptions.SpendPerPupil,
        SchoolComparisonItSpendViewModel.ViewAsOptions viewAs = SchoolComparisonItSpendViewModel.ViewAsOptions.Chart)
    {
        using (logger.BeginScope(new
        {
            urn
        }))
        {
            try
            {
                ViewData[ViewDataKeys.BreadcrumbNode] = BreadcrumbNodes.SchoolComparisonItSpend(urn);
                var (school, expenditures) = await GetItSpendForMaintainedSchool(urn, resultAs);
                if (school == null)
                {
                    return StatusCode((int)HttpStatusCode.NotFound);
                }

                var subCategories = new SchoolComparisonSubCategoriesViewModel(urn, expenditures, selectedSubCategories);
                if (viewAs == SchoolComparisonItSpendViewModel.ViewAsOptions.Chart)
                {
                    var charts = await BuildCharts(urn, resultAs, subCategories);

                    foreach (var chart in charts)
                    {
                        var category = subCategories.FirstOrDefault(r => r.Uuid == chart.Id);
                        if (category != null)
                        {
                            category.ChartSvg = chart.Html;
                        }
                    }
                }

                var viewModel = new SchoolComparisonItSpendViewModel(school, subCategories, expenditures)
                {
                    SelectedSubCategories = selectedSubCategories,
                    ViewAs = viewAs,
                    ResultAs = resultAs
                };

                TrackEvent(urn, selectedSubCategories, resultAs, viewAs);

                return View(viewModel);
            }
            catch (Exception e)
            {
                logger.LogError(e, "An error displaying school IT spending comparison: {DisplayUrl}",
                    Request.GetDisplayUrl());
                return e is StatusCodeException s ? StatusCode((int)s.Status) : StatusCode(500);
            }
        }
    }

    [HttpPost]
    public IActionResult Index(string urn, int viewAs, int resultAs, int[]? selectedSubCategories) => RedirectToAction("Index", new
    {
        urn,
        viewAs,
        resultAs,
        selectedSubCategories
    });

    [HttpGet]
    [Produces("application/zip")]
    [ProducesResponseType<byte[]>(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [Route("download")]
    public async Task<IActionResult> Download(string urn)
    {
        using (logger.BeginScope(new
        {
            urn
        }))
        {
            try
            {
                var (school, schoolItSpend) = await GetItSpendForMaintainedSchool(urn, Dimensions.ResultAsOptions.Actuals);
                if (school == null)
                {
                    return StatusCode((int)HttpStatusCode.NotFound);
                }

                return new CsvResults([new CsvResult(schoolItSpend, $"benchmark-it-spending-{urn}.csv")], $"benchmark-it-spending-{urn}.zip");
            }
            catch (Exception e)
            {
                logger.LogError(e, "An error downloading IT expenditure data: {DisplayUrl}", Request.GetDisplayUrl());
                return StatusCode(500);
            }
        }
    }

    private void TrackEvent(string urn, ItSpendingCategories.SubCategoryFilter[] selectedCategories, Dimensions.ResultAsOptions resultAs, SchoolComparisonItSpendViewModel.ViewAsOptions viewAs)
    {
        var categories = selectedCategories.Length == 0 ? ItSpendingCategories.All : selectedCategories;

        var properties = new Dictionary<string, string>
        {
            { "URN", urn },
            { "Categories", string.Join(" | ", categories.Order()) },
            { "ResultAs", resultAs.ToString() },
            { "ViewAs", viewAs.ToString() }
        };

        telemetryClient.TrackEvent(CustomEvents.BenchmarkItSpending, properties);
    }

    private static ApiQuery BuildApiQuery(Dimensions.ResultAsOptions resultAs, IEnumerable<string>? urns = null)
    {
        var query = new ApiQuery();
        foreach (var urn in urns ?? [])
        {
            query.AddIfNotNull("urns", urn);
        }

        query.AddIfNotNull("dimension", resultAs.GetQueryParam());
        return query;
    }

    private async Task<ChartResponse[]> BuildCharts(string urn, Dimensions.ResultAsOptions resultAs,
        SchoolComparisonSubCategoriesViewModel subCategories)
    {
        var requests = subCategories.Select(c => new SchoolComparisonItSpendHorizontalBarChartRequest(
            c.Uuid!,
            urn,
            c.Data!,
            format => Uri.UnescapeDataString(
                Url.Action("Index", "School", new
                {
                    urn = format
                }) ?? string.Empty),
            resultAs
        ));

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

    private async Task<(School? school, SchoolItSpend[] schoolItSpend)> GetItSpendForMaintainedSchool(
        string urn,
        Dimensions.ResultAsOptions resultAs)
    {
        var school = await establishmentApi
            .GetSchool(urn)
            .GetResultOrDefault<School>();
        if (school?.FinanceType != EstablishmentTypes.Maintained)
        {
            return (null, []);
        }

        var set = await comparatorSetApi
            .GetDefaultSchoolAsync(urn)
            .GetResultOrThrow<SchoolComparatorSet>();

        var expenditures = await itSpendApi
            .QuerySchools(BuildApiQuery(resultAs, set.Pupil))
            .GetResultOrDefault<SchoolItSpend[]>();

        return (school, expenditures ?? []);
    }
}