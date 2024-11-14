using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.FeatureManagement.Mvc;
using Web.App.Attributes.RequestTelemetry;
using Web.App.Domain;
using Web.App.Infrastructure.Apis;
using Web.App.Infrastructure.Apis.Establishment;
using Web.App.Infrastructure.Apis.Insight;
using Web.App.Infrastructure.Extensions;
using Web.App.Services;
using Web.App.ViewModels;
namespace Web.App.Controllers;

[Controller]
[FeatureGate(FeatureFlags.BenchmarkingReportCards)]
[Route("school/{urn}/benchmarking-report-cards")]
[SchoolBenchmarkingReportCardsTelemetry(TrackedRequestQueryParameters.Referrer)]
public class SchoolBenchmarkingReportCardsController(
    IEstablishmentApi establishmentApi,
    IFinanceService financeService,
    IBalanceApi balanceApi,
    IExpenditureApi expenditureApi,
    ISchoolComparatorSetService schoolComparatorSetService,
    IMetricRagRatingApi metricRagRatingApi,
    ICensusApi censusApi,
    ILogger<SchoolBenchmarkingReportCardsController> logger)
    : Controller
{
    [HttpGet]
    public async Task<IActionResult> Index(string urn)
    {
        using (logger.BeginScope(new
        {
            urn
        }))
        {
            try
            {
                var school = await establishmentApi.GetSchool(urn).GetResultOrThrow<School>();
                var years = await financeService.GetYears();
                var isNonLeadFederation = !string.IsNullOrEmpty(school.FederationLeadURN) && school.FederationLeadURN != urn;
                if (isNonLeadFederation)
                {
                    return Unavailable(school, years, SchoolBenchmarkingReportCardsUnavailableViewModel.UnavailableReason.NonLeadFederatedSchool);
                }

                var balance = await balanceApi
                    .School(urn)
                    .GetResultOrDefault<SchoolBalance>();

                if (balance?.PeriodCoveredByReturn is not 12)
                {
                    return Unavailable(
                        school,
                        years,
                        balance?.PeriodCoveredByReturn == null ? SchoolBenchmarkingReportCardsUnavailableViewModel.UnavailableReason.MissingExpenditure : SchoolBenchmarkingReportCardsUnavailableViewModel.UnavailableReason.PartYear);
                }

                var ratings = await metricRagRatingApi
                    .GetDefaultAsync(new ApiQuery().AddIfNotNull("urns", urn))
                    .GetResultOrDefault<RagRating[]>();

                var set = await schoolComparatorSetService.ReadComparatorSet(urn);
                var pupilExpenditure = set is { Pupil.Length: > 0 }
                    ? await expenditureApi.QuerySchools(BuildQuery(set.Pupil)).GetResultOrDefault<SchoolExpenditure[]>()
                    : [];
                var areaExpenditure = set is { Building.Length: > 0 }
                    ? await expenditureApi.QuerySchools(BuildQuery(set.Building)).GetResultOrDefault<SchoolExpenditure[]>()
                    : [];

                var census = set is { Pupil.Length: > 0 }
                    ? await censusApi.Query(BuildQuery(set.Pupil, "PupilsPerStaffRole")).GetResultOrDefault<Census[]>()
                    : [];

                var viewModel = new SchoolBenchmarkingReportCardsViewModel(school, years, balance, ratings, pupilExpenditure, areaExpenditure, census);
                return View(viewModel);
            }
            catch (Exception e)
            {
                logger.LogError(e, "An error displaying school benchmarking report cards: {DisplayUrl}",
                    Request.GetDisplayUrl());
                return e is StatusCodeException s ? StatusCode((int)s.Status) : StatusCode(500);
            }
        }
    }

    private ViewResult Unavailable(School school, FinanceYears years, SchoolBenchmarkingReportCardsUnavailableViewModel.UnavailableReason reason)
    {
        logger.LogInformation(new EventId((int)reason, reason.ToString()), "Unable to display benchmarking report cards for {urn} ({reason})", school.URN, reason);
        return new ViewResult
        {
            ViewData = new ViewDataDictionary(new EmptyModelMetadataProvider(), new ModelStateDictionary())
            {
                Model = new SchoolBenchmarkingReportCardsUnavailableViewModel(school, years, reason)
            },
            ViewName = nameof(Unavailable),
            StatusCode = StatusCodes.Status404NotFound
        };
    }

    private static ApiQuery BuildQuery(IEnumerable<string> urns, string dimension = "PerUnit")
    {
        var query = new ApiQuery()
            .AddIfNotNull("dimension", dimension);

        foreach (var urn in urns)
        {
            query.AddIfNotNull("urns", urn);
        }

        return query;
    }
}