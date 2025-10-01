using Microsoft.AspNetCore.Authorization;
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

                return await TrustComparisonItSpend(trust, userDefinedSet.Set, comparatorGenerated, redirectUri, selectedSubCategories, resultAs, viewAs);
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

    private async Task<IActionResult> TrustComparisonItSpend(
        Trust trust,
        string[] comparatorSet,
        bool? comparatorGenerated,
        string? redirectUri,
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

        var subCategories = new TrustComparisonSubCategoriesViewModel(trust.CompanyNumber!, expenditures, selectedSubCategories);
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
            resultAs
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
}