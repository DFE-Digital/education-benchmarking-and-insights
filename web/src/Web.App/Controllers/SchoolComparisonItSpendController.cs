using System.Net;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.FeatureManagement.Mvc;
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
    ILogger<SchoolComparisonController> logger) : Controller
{
    [HttpGet]
    [SchoolRequestTelemetry(TrackedRequestFeature.BenchmarkItSpend)]
    public async Task<IActionResult> Index(string urn,
        ItSpendingCategories.SubCategoryFilter[] selectedSubCategories,
        SchoolComparisonItSpendViewModel.ResultAsOptions resultAs = SchoolComparisonItSpendViewModel.ResultAsOptions.SpendPerPupil,
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
                var school = await establishmentApi.GetSchool(urn).GetResultOrThrow<School>();
                if (school.FinanceType != EstablishmentTypes.Maintained)
                {
                    return StatusCode((int)HttpStatusCode.NotFound);
                }

                var set = await comparatorSetApi.GetDefaultSchoolAsync(urn).GetResultOrThrow<SchoolComparatorSet>();
                var expenditures = await itSpendApi
                    .QuerySchools(BuildApiQuery(set.Pupil))
                    .GetResultOrDefault<SchoolItSpend[]>() ?? [];

                var subCategories = new SchoolComparisonSubCategoriesViewModel(urn, expenditures, selectedSubCategories);
                var requests = subCategories.Select(c => new SchoolComparisonItSpendHorizontalBarChartRequest(
                    c.Uuid!,
                    urn,
                    c.Data!,
                    format => Uri.UnescapeDataString(
                        Url.Action("Index", "School", new { urn = format }) ?? string.Empty)));

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

                foreach (var chart in charts)
                {
                    var category = subCategories.FirstOrDefault(r => r.Uuid == chart.Id);
                    if (category != null)
                    {
                        category.ChartSvg = chart.Html;
                    }
                }

                var viewModel = new SchoolComparisonItSpendViewModel(school, subCategories)
                {
                    SelectedSubCategories = selectedSubCategories,
                    ViewAs = viewAs,
                    ResultAs = resultAs
                };
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
    public IActionResult Index(string urn, int viewAs, int resultAs, int[]? selectedSubCategories)
    {

        return RedirectToAction("Index", new
        {
            urn,
            viewAs,
            resultAs,
            selectedSubCategories
        });
    }

    private static ApiQuery BuildApiQuery(IEnumerable<string>? urns = null)
    {
        var query = new ApiQuery();
        foreach (var urn in urns ?? [])
        {
            query.AddIfNotNull("urns", urn);
        }

        return query;
    }
}