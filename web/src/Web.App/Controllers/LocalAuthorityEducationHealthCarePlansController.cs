using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using Web.App.ActionResults;
using Web.App.Attributes;
using Web.App.Domain.Charts;
using Web.App.Domain.LocalAuthorities;
using Web.App.Infrastructure.Apis;
using Web.App.Infrastructure.Extensions;
using Web.App.Services;
using Web.App.ViewModels;
using LocalAuthority = Web.App.Domain.LocalAuthority;

namespace Web.App.Controllers;

[Controller]
[Route("local-authority/{code}/education-health-care-plans")]
[ValidateLaCode]
public class LocalAuthorityEducationHealthCarePlansController(
    ILogger<LocalAuthorityEducationHealthCarePlansController> logger,
    ILocalAuthorityApi api,
    IChartRenderingApi chartRenderingApi,
    ILocalAuthorityComparatorSetService comparatorSetService)
    : Controller
{
    [HttpGet]
    public async Task<IActionResult> Index(
        string code,
        EducationHealthCarePlansCategories.SubCategoryFilter[] selectedSubCategories,
        Views.ViewAsOptions viewAs = Views.ViewAsOptions.Chart)
    {
        using (logger.BeginScope(new { code }))
        {
            try
            {

                var la = await api.SingleAsync(code).GetResultOrThrow<LocalAuthority>();

                var set = comparatorSetService.ReadUserDefinedComparatorSetFromSession(code).Set;
                if (set.Length == 0)
                {
                    return RedirectToAction("Index", "LocalAuthorityComparators", new { code, type = LocalAuthorityBenchmarkType.EducationHealthCarePlans });
                }

                var query = BuildQuery(new[]
                {
                    code
                }.Concat(set).ToArray(), EducationHealthCarePlanProperties.Dimension);
                var plans = await api
                    .QueryEhcpAsync(query)
                    .GetResultOrThrow<EducationHealthCarePlans[]>();

                var subCategories = new EducationHealthCarePlansComparisonSubCategoriesViewModel(plans, selectedSubCategories);

                var charts = await BuildCharts(code, subCategories);

                subCategories.Items!.ForEach(i =>
                {
                    var chart = charts.FirstOrDefault(c => c.Id != null && c.Id == i.Uuid);
                    if (chart != null)
                    {
                        i.ChartSvg = chart.Html;
                    }
                });

                var viewModel = new LocalAuthorityEducationHealthCarePlansViewModel(la, set, subCategories)
                {
                    SelectedSubCategories = selectedSubCategories,
                    ViewAs = viewAs,
                };

                return View(viewModel);
            }
            catch (Exception e)
            {
                logger.LogError(e, "An error displaying local authority education, health care plans: {DisplayUrl}", Request.GetDisplayUrl());
                return e is StatusCodeException s ? StatusCode((int)s.Status) : StatusCode(500);
            }
        }
    }

    [HttpPost]
    public IActionResult Index(string code, int viewAs, int[]? selectedSubCategories) => RedirectToAction("Index", new
    {
        code,
        selectedSubCategories,
        viewAs
    });

    [HttpGet]
    [Produces("application/zip")]
    [ProducesResponseType<byte[]>(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [Route("download")]
    public async Task<IActionResult> Download(string code)
    {
        using (logger.BeginScope(new
        {
            code
        }))
        {
            try
            {
                var set = comparatorSetService.ReadUserDefinedComparatorSetFromSession(code).Set;
                if (set.Length == 0)
                {
                    return RedirectToAction("Index", "LocalAuthorityComparators", new { code, type = LocalAuthorityBenchmarkType.EducationHealthCarePlans });
                }

                var query = BuildQuery(new[]
                {
                    code
                }.Concat(set).ToArray(), EducationHealthCarePlanProperties.Dimension);
                var plans = await api
                    .QueryEhcpAsync(query)
                    .GetResultOrThrow<EducationHealthCarePlans[]>();

                return new CsvResults([new CsvResult(plans, $"benchmark-education-health-care-plans-{code}.csv")], $"benchmark-education-health-care-plans-{code}.zip");
            }
            catch (Exception e)
            {
                logger.LogError(e, "An error downloading education health care plans data: {DisplayUrl}", Request.GetDisplayUrl());
                return StatusCode(500);
            }
        }
    }

    private static ApiQuery BuildQuery(string[] codes, string dimension)
    {
        var query = new ApiQuery();
        foreach (var c in codes)
        {
            query.AddIfNotNull("code", c);
        }

        query.AddIfNotNull("dimension", dimension);
        return query;
    }

    private async Task<ChartResponse[]> BuildCharts(string code,
        EducationHealthCarePlansComparisonSubCategoriesViewModel subCategories)
    {
        var requests = subCategories.Items.Select(c => new EducationHealthCarePlanHorizontalBarChartRequest(
            c.Uuid!,
            code,
            c.Data!
        ));

        ChartResponse[] charts = [];
        try
        {
            charts = await chartRenderingApi
                .PostHorizontalBarCharts(new PostHorizontalBarChartsRequest<EducationHealthCarePlansComparisonDatum>(requests))
                .GetResultOrDefault<ChartResponse[]>() ?? [];
        }
        catch (Exception e)
        {
            logger.LogWarning(e, "Unable to load charts from API");
        }

        return charts;
    }
}