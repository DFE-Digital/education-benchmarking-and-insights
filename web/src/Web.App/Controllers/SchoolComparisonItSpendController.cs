using System.Net;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.FeatureManagement.Mvc;
using Web.App.Attributes;
using Web.App.Attributes.RequestTelemetry;
using Web.App.Domain;
using Web.App.Domain.Charts;
using Web.App.Infrastructure.Apis;
using Web.App.Infrastructure.Apis.ChartRendering;
using Web.App.Infrastructure.Apis.Establishment;
using Web.App.Infrastructure.Extensions;
using Web.App.ViewModels;
using Web.App.ViewModels.Components;

namespace Web.App.Controllers;

[Controller]
[Route("school/{urn}/comparison/it")]
[ValidateUrn]
[FeatureGate(FeatureFlags.CfrItSpendBreakdown)]
public class SchoolComparisonItSpendController(
    IEstablishmentApi establishmentApi,
    IChartRenderingApi chartRenderingApi,
    ILogger<SchoolComparisonController> logger) : Controller
{
    [HttpGet]
    [SchoolRequestTelemetry(TrackedRequestFeature.BenchmarkItSpend)]
    public async Task<IActionResult> Index(string urn)
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

                var expenditures = new[]
                {
                    new SchoolItExpenditure
                    {
                        URN = "990000",
                        SchoolName = "Stub School (990000)",
                        SchoolType = "Academy",
                        LAName = "Test LA",
                        PeriodCoveredByReturn = 12,
                        TotalPupils = 550,
                        Connectivity = 148500,
                        OnsiteServers = 247500,
                        ItLearningResources = 178200,
                        AdministrationSoftwareAndSystems = 89100,
                        LaptopsDesktopsAndTablets = 297000,
                        OtherHardware = 99000,
                        ItSupport = 178200
                    },
                    new SchoolItExpenditure
                    {
                        URN = "990001",
                        SchoolName = "Stub School (990001)",
                        SchoolType = "Academy",
                        LAName = "Test LA",
                        PeriodCoveredByReturn = 12,
                        TotalPupils = 550,
                        Connectivity = 1485.15m,
                        OnsiteServers = 2475.25m,
                        ItLearningResources = 1782.18m,
                        AdministrationSoftwareAndSystems = 891.09m,
                        LaptopsDesktopsAndTablets = 2970.3m,
                        OtherHardware = 990.1m,
                        ItSupport = 1782.18m
                    },
                    new SchoolItExpenditure
                    {
                        URN = "990002",
                        SchoolName = "Stub School (990002)",
                        SchoolType = "Academy",
                        LAName = "Test LA",
                        PeriodCoveredByReturn = 12,
                        TotalPupils = 550,
                        Connectivity = 14850.3m,
                        OnsiteServers = 24750.5m,
                        ItLearningResources = 17820.36m,
                        AdministrationSoftwareAndSystems = 8910.18m,
                        LaptopsDesktopsAndTablets = 29700.6m,
                        OtherHardware = 9900.2m,
                        ItSupport = 17820.36m
                    }
                };

                var subCategories = new SchoolComparisonViewModelCostSubCategories(urn, expenditures);
                var requests = subCategories.Select(c => new SchoolComparisonItSpendHorizontalBarChartRequest(
                    c.Uuid!,
                    urn,
                    c.Data!,
                    format => Uri.UnescapeDataString(Url.Action("Index", "School", new { urn = format }) ?? string.Empty)));

                ChartResponse[] charts = [];
                try
                {
                    charts = await chartRenderingApi.PostHorizontalBarCharts(new PostHorizontalBarChartsRequest<SchoolComparisonDatum>(requests)).GetResultOrDefault<ChartResponse[]>() ?? [];
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

                var viewModel = new SchoolComparisonItSpendViewModel(school, subCategories);
                return View(viewModel);
            }
            catch (Exception e)
            {
                logger.LogError(e, "An error displaying school IT spending comparison: {DisplayUrl}", Request.GetDisplayUrl());
                return e is StatusCodeException s ? StatusCode((int)s.Status) : StatusCode(500);
            }
        }
    }
}