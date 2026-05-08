using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using Web.App.ActionResults;
using Web.App.Attributes;
using Web.App.Domain.Charts;
using Web.App.Domain.LocalAuthorities;
using Web.App.Extensions;
using Web.App.Infrastructure.Apis;
using Web.App.Infrastructure.Extensions;
using Web.App.Services;
using Web.App.ViewModels;
using LocalAuthority = Web.App.Domain.LocalAuthority;

namespace Web.App.Controllers;

[Controller]
[Route("local-authority/{code}/high-needs-spending")]
[ValidateLaCode]
public class LocalAuthorityHighNeedsSpendingController(
    ILogger<LocalAuthorityHighNeedsSpendingController> logger,
    ILocalAuthorityApi api,
    IChartRenderingApi chartRenderingApi,
    ILocalAuthorityComparatorSetService comparatorSetService,
    IFinanceService financeService)
    : Controller
{
    [HttpGet]
    public async Task<IActionResult> Index(string code,
        HighNeedsSpendingCategories.SubCategoryFilter[] selectedSubCategories,
        Views.ViewAsOptions viewAs = Views.ViewAsOptions.Chart,
        HighNeedsDimensions.ResultAsOptions resultAs = HighNeedsDimensions.ResultAsOptions.PerPupil,
        HighNeedsDimensions.SubmissionTypeOptions type = HighNeedsDimensions.SubmissionTypeOptions.Outturn)
    {
        using (logger.BeginScope(new { code }))
        {
            try
            {

                var la = await api.SingleAsync(code).GetResultOrThrow<LocalAuthority>();

                var set = comparatorSetService.ReadUserDefinedComparatorSetFromSession(code).Set;
                if (set.Length == 0)
                {
                    return RedirectToAction("Index", "LocalAuthorityComparators", new { code, type = LocalAuthorityBenchmarkType.HighNeedsSpending });
                }

                var query = BuildQuery(new[]
                {
                    code
                }.Concat(set).ToArray(),
                resultAs,
                type);

                var expenditures = await api
                    .QueryHighNeedsV2Async(query)
                    .GetResultOrThrow<HighNeedsSpending[]>();

                var subCategories = new HighNeedsSpendingComparisonSubCategoriesViewModel(expenditures, selectedSubCategories, code);

                var charts = await BuildCharts(code, subCategories, resultAs, type);

                var years = await financeService.GetYears();

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

                var viewModel = new LocalAuthorityHighNeedsSpendingViewModel(la, set, subCategories)
                {
                    SelectedSubCategories = selectedSubCategories,
                    ViewAs = viewAs,
                    ResultAs = resultAs,
                    Type = type,
                    Year = years.S251
                };

                return View(viewModel);
            }
            catch (Exception e)
            {
                logger.LogError(e, "An error displaying local authority high needs spending: {DisplayUrl}", Request.GetDisplayUrl());
                return e is StatusCodeException s ? StatusCode((int)s.Status) : StatusCode(500);
            }
        }
    }

    [HttpPost]
    public IActionResult Index(string code, int[]? selectedSubCategories, int viewAs, int resultAs, int type) => RedirectToAction("Index", new
    {
        code,
        selectedSubCategories,
        viewAs,
        resultAs,
        type
    });

    [HttpGet]
    [Produces("application/zip")]
    [ProducesResponseType<byte[]>(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [Route("download")]
    public async Task<IActionResult> Download(
        string code,
        HighNeedsDimensions.ResultAsOptions resultAs = HighNeedsDimensions.ResultAsOptions.PerPupil,
        HighNeedsDimensions.SubmissionTypeOptions type = HighNeedsDimensions.SubmissionTypeOptions.Outturn)
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
                    }.Concat(set).ToArray(),
                    resultAs,
                    type);

                var expenditures = await api
                    .QueryHighNeedsV2Async(query)
                    .GetResultOrThrow<HighNeedsSpending[]>();

                var typeLabel = type.GetSubmissionTypeDescription().ToSlug();
                var resultAsLabel = resultAs.GetResultAsDescription().TrimStart('£').ToSlug();

                return new CsvResults([new CsvResult(expenditures, $"benchmark-high-needs-spending-{typeLabel}-{resultAsLabel}-{code}.csv")], $"benchmark-high-needs-spending-{typeLabel}-{resultAsLabel}-{code}.zip");
            }
            catch (Exception e)
            {
                logger.LogError(e, "An error downloading high needs spending data: {DisplayUrl}", Request.GetDisplayUrl());
                return StatusCode(500);
            }
        }
    }

    private static ApiQuery BuildQuery(
        string[] codes,
        HighNeedsDimensions.ResultAsOptions dimension,
        HighNeedsDimensions.SubmissionTypeOptions submissionType)
    {
        var query = new ApiQuery();
        foreach (var c in codes)
        {
            query.AddIfNotNull("code", c);
        }

        query.AddIfNotNull("dimension", dimension.GetResultAsQueryParam());

        query.AddIfNotNull("type", submissionType.GetSubmissionTypeQueryParam());

        return query;
    }

    private async Task<ChartResponse[]> BuildCharts(string code,
        HighNeedsSpendingComparisonSubCategoriesViewModel subCategories,
        HighNeedsDimensions.ResultAsOptions resultAs,
        HighNeedsDimensions.SubmissionTypeOptions type)
    {
        var requests = subCategories
            .Groups
            .SelectMany(x => x.Items)
            .Select(x => new HighNeedsSpendingHorizontalBarChartRequest(
                x.Uuid!,
                code,
                x.Data!,
                resultAs,
                type));

        ChartResponse[] charts = [];
        try
        {
            charts = await chartRenderingApi
                .PostHorizontalBarCharts(new PostHorizontalBarChartsRequest<HighNeedsSpendingComparisonDatum>(requests))
                .GetResultOrDefault<ChartResponse[]>() ?? [];
        }
        catch (Exception e)
        {
            logger.LogWarning(e, "Unable to load charts from API");
        }

        return charts;
    }

}