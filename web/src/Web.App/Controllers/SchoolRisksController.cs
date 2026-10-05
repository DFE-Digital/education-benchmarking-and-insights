using System.Net;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.FeatureManagement.Mvc;
using Web.App.ActionResults;
using Web.App.Attributes;
using Web.App.Domain;
using Web.App.Domain.Charts;
using Web.App.Domain.LocalAuthorities;
using Web.App.Infrastructure.Apis;
using Web.App.Infrastructure.Extensions;
using Web.App.ViewModels;

namespace Web.App.Controllers;

[Controller]
[LocalAuthorityAuthorization]
[Route("local-authority/{code}/risks/school/{urn}")]
[ValidateLaCode]
[FeatureGate(FeatureFlags.LocalAuthorityRiskIndicators)]
public class SchoolRisksController(
    ILogger<SchoolRisksController> logger,
    ISchoolApi schoolApi,
    IChartRenderingApi chartRenderingApi)
    : Controller
{
    [HttpGet]
    public async Task<IActionResult> Index(string code, string urn)
    {
        using (logger.BeginScope(new { code, urn }))
        {
            try
            {
                var school = await schoolApi.SingleAsync(urn).GetResultOrThrow<School>();
                if (school.LACode != code)
                {
                    return NotFound();
                }

                var risks = await schoolApi.RisksAsync(urn).GetResultOrThrow<LocalAuthorityRiskIndicators>();
                var metrics = await schoolApi.RisksMetricsAsync(urn).GetResultOrThrow<RisksMetrics[]>();

                var viewModel = new SchoolRisksViewModel(school, risks, metrics);

                return View(viewModel);
            }
            catch (Exception e)
            {
                logger.LogError(e, "An error displaying school risk indicators: {DisplayUrl}", Request.GetDisplayUrl());
                return e is StatusCodeException s ? StatusCode((int)s.Status) : StatusCode(500);
            }
        }
    }

    [HttpGet]
    [Produces("application/zip")]
    [ProducesResponseType<byte[]>(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [Route("download")]
    public async Task<IActionResult> Download(string code, string urn)
    {
        using (logger.BeginScope(new
        {
            code,
            urn
        }))
        {
            try
            {
                var school = await schoolApi.SingleAsync(urn).GetResultOrThrow<School>();
                if (school.LACode != code)
                {
                    return NotFound();
                }

                var risks = await schoolApi.RisksAsync(urn).GetResultOrThrow<LocalAuthorityRiskIndicators>();
                var metrics = await schoolApi.RisksMetricsAsync(urn).GetResultOrThrow<RisksMetrics[]>();

                return new CsvResults(
                    [
                        new CsvResult(
                                [risks],
                                $"{school.SchoolName}-risk-score.csv"
                            ),
                        new CsvResult(
                            metrics,
                            $"{school.SchoolName}-risk-score-metrics.csv"
                        )

                    ], $"{school.SchoolName}-risk-score.zip");
            }
            catch (Exception e)
            {
                logger.LogError(e, "An error downloading school risk indicators data: {DisplayUrl}", Request.GetDisplayUrl());
                return StatusCode(500);
            }
        }
    }

    [HttpGet]
    [Route("history")]
    public async Task<IActionResult> History(
        string code,
        string urn,
        Views.ViewAsOptions viewAs = Views.ViewAsOptions.Chart)
    {
        using (logger.BeginScope(new { code, urn }))
        {
            try
            {
                var school = await schoolApi.SingleAsync(urn).GetResultOrThrow<School>();
                if (school.LACode != code)
                {
                    return NotFound();
                }

                var risksHistoryRows = await schoolApi.RisksHistoryAsync(urn).GetResultOrThrow<LocalAuthorityRiskIndicatorsHistoryRows>();
                var trends = risksHistoryRows.ToTrends();

                if (viewAs == Views.ViewAsOptions.Chart)
                {
                    var seriesList = new[]
                    {
                        trends.Overall,
                        trends.Financial,
                        trends.EducationalPerformance,
                        trends.SchoolAndPupil
                    };

                    var chartResponses = await BuildHistoryCharts(seriesList) ?? [];

                    if (chartResponses.Length != 0)
                    {
                        HydrateSeriesWithHistoryCharts(seriesList, chartResponses);
                    }
                }

                var viewModel = new SchoolRisksHistoryViewModel(school, trends)
                {
                    ViewAs = viewAs
                };

                return View(viewModel);
            }
            catch (Exception e)
            {
                logger.LogError(e, "An error displaying school risk indicators history: {DisplayUrl}", Request.GetDisplayUrl());
                return e is StatusCodeException s ? StatusCode((int)s.Status) : StatusCode(500);
            }
        }
    }

    [HttpPost]
    [Route("history")]
    public IActionResult History(
        string code,
        string urn,
        int? viewAs)
    {
        return RedirectToAction("History", new
        {
            code,
            urn,
            viewAs
        });
    }

    [HttpGet]
    [Produces("application/zip")]
    [ProducesResponseType<byte[]>(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [Route("history/download")]
    public async Task<IActionResult> HistoryDownload(string code, string urn)
    {
        using (logger.BeginScope(new
        {
            code,
            urn
        }))
        {
            try
            {
                var school = await schoolApi.SingleAsync(urn).GetResultOrThrow<School>();
                if (school.LACode != code)
                {
                    return StatusCode((int)HttpStatusCode.NotFound);
                }

                var risksHistoryRows = await schoolApi.RisksHistoryAsync(urn).GetResultOrThrow<LocalAuthorityRiskIndicatorsHistoryRows>();

                return new CsvResults([new CsvResult(risksHistoryRows.Rows, $"{school.SchoolName}-trend-in-risk-scores.csv")], $"{school.SchoolName}-trend-in-risk-scores.zip");
            }
            catch (Exception e)
            {
                logger.LogError(e, "An error downloading school risk indicators history data: {DisplayUrl}", Request.GetDisplayUrl());
                return StatusCode(500);
            }
        }
    }

    private async Task<ChartResponse[]?> BuildHistoryCharts(RiskHistorySeries[] seriesList)
    {
        var chartRequests = seriesList.Select(series => new PostLineChartRequest<RiskHistoryData>
        {
            Id = Guid.NewGuid().ToString(),
            Width = 600,
            XAxisLabel = "Financial year",
            ValueField = "value",
            KeyField = "year",
            ShowValueDots = true,
            ShowValueLabels = true,
            Data = series.Data.ToArray()
        });

        var payload = new PostLineChartsRequest<RiskHistoryData>(chartRequests);

        try
        {
            return await chartRenderingApi.PostLineCharts(payload)
                .GetResultOrDefault<ChartResponse[]>();
        }
        catch (Exception e)
        {
            logger.LogWarning(e, "Unable to load charts from API");
            return [];
        }
    }

    private static void HydrateSeriesWithHistoryCharts(RiskHistorySeries[] seriesList, ChartResponse[] chartResponses)
    {
        for (var i = 0; i < seriesList.Length; i++)
        {
            seriesList[i].Uuid = chartResponses[i].Id;
            seriesList[i].ChartSvg = chartResponses[i].Html;
        }
    }
}
