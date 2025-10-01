using System.Net;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.FeatureManagement.Mvc;
using Web.App.ActionResults;
using Web.App.Attributes;
using Web.App.Attributes.RequestTelemetry;
using Web.App.Domain;
using Web.App.Domain.Charts;
using Web.App.Extensions;
using Web.App.Infrastructure.Apis;
using Web.App.Infrastructure.Apis.Benchmark;
using Web.App.Infrastructure.Apis.ChartRendering;
using Web.App.Infrastructure.Apis.Establishment;
using Web.App.Infrastructure.Apis.Insight;
using Web.App.Infrastructure.Extensions;
using Web.App.Services;
using Web.App.ViewModels;

namespace Web.App.Controllers;

[Controller]
[Authorize]
[Route("trust/{companyNumber}/benchmark-it-spending")]
[ValidateCompanyNumber]
[FeatureGate(FeatureFlags.TrustItSpendBreakdown)]
public class TrustComparisonItSpendController(
    IEstablishmentApi establishmentApi,
    IChartRenderingApi chartRenderingApi,
    IComparatorSetApi comparatorSetApi,
    IItSpendApi itSpendApi,
    IUserDataService userDataService,
    IBudgetForecastApi budgetForecastApi,
    IConfiguration configuration,
    ILogger<TrustComparisonItSpendController> logger) : Controller
{
    [HttpGet]
    [TrustRequestTelemetry(TrackedRequestFeature.BenchmarkItSpend)]
    public async Task<IActionResult> Index(string companyNumber,
        [FromQuery(Name = "comparator-generated")] bool? comparatorGenerated,
        ItSpendingCategories.SubCategoryFilter[] selectedSubCategories,
        Dimensions.ResultAsOptions resultAs = Dimensions.ResultAsOptions.Actuals,
        Views.ViewAsOptions viewAs = Views.ViewAsOptions.Chart)
    {
        using (logger.BeginScope(new
        {
            companyNumber
        }))
        {
            try
            {
                var trust = await establishmentApi.GetTrust(companyNumber).GetResultOrThrow<Trust>();
                var redirectUri = Url.Action("Index", new
                {
                    companyNumber
                });
                var userData = await userDataService.GetTrustDataAsync(User, companyNumber);
                if (userData.ComparatorSet == null)
                {
                    return RedirectToAction("Index", "TrustComparatorsCreateBy", new
                    {
                        companyNumber,
                        redirectUri
                    });
                }

                var userDefinedSet = await comparatorSetApi.GetUserDefinedTrustAsync(trust.CompanyNumber!, userData.ComparatorSet)
                    .GetResultOrDefault<UserDefinedSchoolComparatorSet>();
                if (userDefinedSet == null || userDefinedSet.Set.Length == 0)
                {
                    return RedirectToAction("UserDefined", "TrustComparators", new
                    {
                        companyNumber,
                        redirectUri
                    });
                }

                var hasTrustAuthorisation = Request.HttpContext.User.HasTrustAuthorisation(trust.CompanyNumber, configuration);
                return await TrustComparisonItSpend(trust, userDefinedSet.Set, comparatorGenerated, redirectUri, hasTrustAuthorisation, selectedSubCategories, resultAs, viewAs);
            }
            catch (Exception e)
            {
                logger.LogError(e, "An error displaying trust IT spending comparison: {DisplayUrl}", Request.GetDisplayUrl());
                return e is StatusCodeException s ? StatusCode((int)s.Status) : StatusCode(500);
            }
        }
    }

    [HttpPost]
    public IActionResult Index(string companyNumber, int viewAs, int resultAs, int[]? selectedSubCategories) => RedirectToAction("Index", new
    {
        companyNumber,
        viewAs,
        resultAs,
        selectedSubCategories
    });

    [HttpGet]
    [Produces("application/zip")]
    [ProducesResponseType<byte[]>(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [Route("download")]
    public async Task<IActionResult> Download(string companyNumber)
    {
        using (logger.BeginScope(new
        {
            companyNumber
        }))
        {
            try
            {
                var userData = await userDataService.GetTrustDataAsync(User, companyNumber);
                if (userData.ComparatorSet == null)
                {
                    return StatusCode((int)HttpStatusCode.NotFound);
                }

                var userDefinedSet = await comparatorSetApi.GetUserDefinedTrustAsync(companyNumber, userData.ComparatorSet)
                    .GetResultOrDefault<UserDefinedSchoolComparatorSet>();
                if (userDefinedSet == null || userDefinedSet.Set.Length == 0)
                {
                    return StatusCode((int)HttpStatusCode.NotFound);
                }

                var expenditures = await itSpendApi
                    .QueryTrusts(BuildApiQuery(Dimensions.ResultAsOptions.Actuals, userDefinedSet.Set))
                    .GetResultOrDefault<TrustItSpend[]>() ?? [];

                // TODO: get forecast data conditional on auth claims and add to csvList

                var csvList = new List<CsvResult>
                {
                    new (expenditures, $"benchmark-it-spending-previous-year-{companyNumber}.csv")
                };

                return new CsvResults(csvList, $"benchmark-it-spending-{companyNumber}.zip");
            }
            catch (Exception e)
            {
                logger.LogError(e, "An error downloading IT expenditure data: {DisplayUrl}", Request.GetDisplayUrl());
                return StatusCode(500);
            }
        }
    }

    private async Task<IActionResult> TrustComparisonItSpend(
        Trust trust,
        string[] comparatorSet,
        bool? comparatorGenerated,
        string? redirectUri,
        bool hasTrustAuthorisation,
        ItSpendingCategories.SubCategoryFilter[] selectedSubCategories,
        Dimensions.ResultAsOptions resultAs,
        Views.ViewAsOptions viewAs)
    {
        var bfrYear = await budgetForecastApi
            .GetCurrentBudgetForecastYear(trust.CompanyNumber)
            .GetResultOrDefault(Constants.CurrentYear - 1);

        var expenditures = await itSpendApi
            .QueryTrusts(BuildApiQuery(resultAs, comparatorSet))
            .GetResultOrDefault<TrustItSpend[]>() ?? [];

        var forecasts = hasTrustAuthorisation
            ? await itSpendApi.TrustForecast(trust.CompanyNumber).GetResultOrDefault<TrustItSpendForecastYear[]>()
            : null;

        var subCategories = new TrustComparisonSubCategoriesViewModel(trust.CompanyNumber!, expenditures, forecasts, selectedSubCategories);
        if (viewAs == Views.ViewAsOptions.Chart)
        {
            var charts = await BuildCharts(trust.CompanyNumber!, resultAs, subCategories);

            foreach (var chart in charts)
            {
                var category = subCategories.Items.FirstOrDefault(r => r.Uuid == chart.Id);
                if (category != null)
                {
                    category.ChartSvg = chart.Html;
                }
            }

            if (forecasts != null)
            {
                charts = await BuildForecastCharts(resultAs, subCategories);

                foreach (var chart in charts)
                {
                    var category = subCategories.Items.FirstOrDefault(r => r.Uuid == chart.Id);
                    if (category != null)
                    {
                        category.ForecastChartSvg = chart.Html;
                    }
                }
            }
        }

        var viewModel = new TrustComparisonItSpendViewModel(trust, comparatorGenerated, redirectUri, comparatorSet, subCategories, bfrYear)
        {
            SelectedSubCategories = selectedSubCategories,
            ViewAs = viewAs,
            ResultAs = resultAs
        };

        return View(viewModel);
    }

    private static ApiQuery BuildApiQuery(Dimensions.ResultAsOptions resultAs, IEnumerable<string> companyNumbers)
    {
        var query = new ApiQuery();
        foreach (var companyNumber in companyNumbers)
        {
            query.AddIfNotNull("companyNumbers", companyNumber);
        }

        query.AddIfNotNull("dimension", resultAs.GetQueryParam());
        return query;
    }

    // todo: move both chart request building and API call methods to new service so that it may be unit tested outside the controller
    private async Task<ChartResponse[]> BuildCharts(string companyNumber, Dimensions.ResultAsOptions resultAs, TrustComparisonSubCategoriesViewModel subCategories)
    {
        var requests = subCategories.Items.Select(c => new TrustComparisonItSpendHorizontalBarChartRequest(
            c.Uuid!,
            companyNumber,
            c.Data!,
            format => Uri.UnescapeDataString(
                Url.Action("Index", "Trust", new
                {
                    companyNumber = format
                }) ?? string.Empty),
            resultAs,
            c.ForecastData == null
                ? null
                : Math.Min(c.Data?.Min(d => d.Expenditure) ?? 0, c.ForecastData?.Min(d => d.Expenditure) ?? 0),
            c.ForecastData == null
                ? null
                : Math.Max(c.Data?.Max(d => d.Expenditure) ?? 0, c.ForecastData?.Max(d => d.Expenditure) ?? 0)
        ));

        ChartResponse[] charts = [];
        try
        {
            charts = await chartRenderingApi
                .PostHorizontalBarCharts(new PostHorizontalBarChartsRequest<TrustComparisonDatum>(requests))
                .GetResultOrDefault<ChartResponse[]>() ?? [];
        }
        catch (Exception e)
        {
            logger.LogWarning(e, "Unable to load charts from API");
        }

        return charts;
    }

    private async Task<ChartResponse[]> BuildForecastCharts(Dimensions.ResultAsOptions resultAs, TrustComparisonSubCategoriesViewModel subCategories)
    {
        var requests = subCategories.Items
            .Where(c => c.ForecastData != null)
            .Select(c => new TrustForecastItSpendHorizontalBarChartRequest(
                c.Uuid!,
                c.ForecastData!,
                resultAs,
                Math.Min(c.Data?.Min(d => d.Expenditure) ?? 0, c.ForecastData?.Min(d => d.Expenditure) ?? 0),
                Math.Max(c.Data?.Max(d => d.Expenditure) ?? 0, c.ForecastData?.Max(d => d.Expenditure) ?? 0)
            ))
            .Where(r => r.Data != null && r.Data.Length != 0);

        ChartResponse[] charts = [];
        try
        {
            charts = await chartRenderingApi
                .PostHorizontalBarCharts(new PostHorizontalBarChartsRequest<TrustForecastDatum>(requests))
                .GetResultOrDefault<ChartResponse[]>() ?? [];
        }
        catch (Exception e)
        {
            logger.LogWarning(e, "Unable to load forecast charts from API");
        }

        return charts;
    }
}