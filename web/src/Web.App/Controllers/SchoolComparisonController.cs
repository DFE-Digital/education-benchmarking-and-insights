using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.FeatureManagement;
using Web.App.ActionResults;
using Web.App.Attributes;
using Web.App.Attributes.RequestTelemetry;
using Web.App.Domain;
using Web.App.Infrastructure.Apis;
using Web.App.Infrastructure.Apis.Benchmark;
using Web.App.Infrastructure.Apis.Establishment;
using Web.App.Infrastructure.Apis.Insight;
using Web.App.Infrastructure.Extensions;
using Web.App.Services;
using Web.App.ViewModels;
// ReSharper disable UnusedAutoPropertyAccessor.Local

namespace Web.App.Controllers;

[Controller]
[Route("school/{urn}/comparison")]
[ValidateUrn]
public class SchoolComparisonController(
    IEstablishmentApi establishmentApi,
    IExpenditureApi expenditureApi,
    IComparatorSetApi comparatorSetApi,
    ILogger<SchoolComparisonController> logger,
    IUserDataService userDataService,
    ISchoolComparatorSetService schoolComparatorSetService,
    ICostCodesService costCodesService,
    IProgressBandingsService progressBandingsService,
    IFeatureManager featureManager)
    : Controller
{
    [HttpGet]
    [SchoolRequestTelemetry(TrackedRequestFeature.BenchmarkCosts)]
    public async Task<IActionResult> Index(string urn)
    {
        using (logger.BeginScope(new
        {
            urn
        }))
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

                var bandings = await featureManager.IsEnabledAsync(FeatureFlags.KS4ProgressBanding)
                    ? await progressBandingsService.GetKS4ProgressBandings(customComparatorSet ?? defaultComparatorSet?.All ?? [])
                    : null;
                var viewModel = new SchoolComparisonViewModel(school, costCodes, userData.ComparatorSet, userData.CustomData, expenditure, defaultComparatorSet, bandings);

                return View(viewModel);
            }
            catch (Exception e)
            {
                logger.LogError(e, "An error displaying school comparison: {DisplayUrl}", Request.GetDisplayUrl());
                return e is StatusCodeException s ? StatusCode((int)s.Status) : StatusCode(500);
            }
        }
    }

    [HttpGet]
    [Route("custom-data")]
    [SchoolAuthorization]
    [SchoolRequestTelemetry(TrackedRequestFeature.CustomisedData)]
    public async Task<IActionResult> CustomData(string urn)
    {
        using (logger.BeginScope(new
        {
            urn
        }))
        {
            try
            {
                var userCustomData = await userDataService.GetCustomDataActiveAsync(User, urn);
                if (userCustomData?.Status != Pipeline.JobStatus.Complete)
                {
                    return RedirectToAction("Index", "School", new
                    {
                        urn
                    });
                }

                ViewData[ViewDataKeys.BreadcrumbNode] = BreadcrumbNodes.SchoolCustomisedDataComparison(urn);

                var school = await establishmentApi.GetSchool(urn).GetResultOrThrow<School>();
                var costCodes = await costCodesService.GetCostCodes(school.IsPartOfTrust);
                var viewModel = new SchoolComparisonViewModel(school, costCodes, customDataId: userCustomData.Id);

                return View(viewModel);
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
        using (logger.BeginScope(new
        {
            urn
        }))
        {
            try
            {
                SchoolExpenditure[]? buildingResult;
                SchoolExpenditure[]? pupilResult;
                if (customDataId != null)
                {
                    buildingResult = await GetCustomSchoolExpenditure(urn, true, customDataId);
                    pupilResult = await GetCustomSchoolExpenditure(urn, false, customDataId);
                }
                else
                {
                    buildingResult = await GetDefaultSchoolExpenditure(urn, true);
                    pupilResult = await GetDefaultSchoolExpenditure(urn, false);
                }

                KS4ProgressBandings? bandings = null;
                if (await featureManager.IsEnabledAsync(FeatureFlags.KS4ProgressBanding))
                {
                    var urns = buildingResult?
                        .Where(r => !string.IsNullOrWhiteSpace(r.URN))
                        .Select(r => r.URN!)
                        .Union(pupilResult?
                            .Where(r => !string.IsNullOrWhiteSpace(r.URN))
                            .Select(r => r.URN!) ?? []);
                    bandings = await progressBandingsService.GetKS4ProgressBandings(urns?.ToArray() ?? []);
                }

                string[] exclude = [nameof(SchoolExpenditure.TotalInternalFloorArea)];
                IEnumerable<CsvResult> csvResults =
                [
                    new(MergeProgressBandings(buildingResult, bandings), $"comparison-{urn}-building.csv", exclude),
                    new(MergeProgressBandings(pupilResult, bandings), $"comparison-{urn}-pupil.csv", exclude)
                ];
                return new CsvResults(csvResults, $"comparison-{urn}.zip");
            }
            catch (Exception e)
            {
                logger.LogError(e, "An error downloading expenditure data: {DisplayUrl}", Request.GetDisplayUrl());
                return StatusCode(500);
            }
        }
    }

    private async Task<SchoolExpenditure[]?> GetCustomSchoolExpenditure(string urn, bool useBuildingSet, string customDataId)
    {
        var customSet = await schoolComparatorSetService.ReadComparatorSet(urn, customDataId);
        var set = useBuildingSet
            ? customSet?.Building
            : customSet?.Pupil;

        if (set == null || set.Length == 0)
        {
            return [];
        }

        var schools = set.Where(x => x != urn).ToArray();
        var customResult = await expenditureApi
            .SchoolCustom(urn, customDataId, BuildApiQuery())
            .GetResultOrDefault<SchoolExpenditure>();

        var defaultResult = await expenditureApi
            .QuerySchools(BuildApiQuery(schools))
            .GetResultOrDefault<SchoolExpenditure[]>();

        return customResult != null
            ? defaultResult?.Append(customResult).ToArray()
            : defaultResult;
    }

    private async Task<SchoolExpenditure[]> GetDefaultSchoolExpenditure(string urn, bool useBuildingSet)
    {
        var userData = await userDataService.GetSchoolDataAsync(User, urn);
        if (string.IsNullOrEmpty(userData.ComparatorSet))
        {
            var defaultSet = await schoolComparatorSetService.ReadComparatorSet(urn);
            var set = useBuildingSet
                ? defaultSet?.Building
                : defaultSet?.Pupil;

            if (set == null || set.Length == 0)
            {
                return [];
            }

            var defaultResult = await expenditureApi
                .QuerySchools(BuildApiQuery(set))
                .GetResultOrThrow<SchoolExpenditure[]>();

            return defaultResult;
        }

        var userDefinedSet = await schoolComparatorSetService.ReadUserDefinedComparatorSet(urn, userData.ComparatorSet);
        if (userDefinedSet == null || userDefinedSet.Set.Length == 0)
        {
            return [];
        }

        var userDefinedResult = await expenditureApi
            .QuerySchools(BuildApiQuery(userDefinedSet.Set))
            .GetResultOrThrow<SchoolExpenditure[]>();

        return userDefinedResult;
    }

    private static ApiQuery BuildApiQuery(IEnumerable<string>? urns = null, string? dimension = "Actuals")
    {
        var query = new ApiQuery()
            .AddIfNotNull("dimension", dimension);

        foreach (var urn in urns ?? [])
        {
            query.AddIfNotNull("urns", urn);
        }

        return query;
    }

    private static IEnumerable<object>? MergeProgressBandings(SchoolExpenditure[]? expenditures, KS4ProgressBandings? bandings)
    {
        if (expenditures == null || bandings == null)
        {
            return expenditures;
        }

        return expenditures.Select(e => new SchoolExpenditureWithProgress(e, bandings[e.URN]));
    }

    private record SchoolExpenditureWithProgress : SchoolExpenditure
    {
        public SchoolExpenditureWithProgress(SchoolExpenditure expenditure, KS4ProgressBanding? banding) : base(expenditure)
        {
            TotalExpenditure = expenditure.TotalExpenditure;
            TotalTeachingSupportStaffCosts = expenditure.TotalTeachingSupportStaffCosts;
            TeachingStaffCosts = expenditure.TeachingStaffCosts;
            SupplyTeachingStaffCosts = expenditure.SupplyTeachingStaffCosts;
            EducationalConsultancyCosts = expenditure.EducationalConsultancyCosts;
            EducationSupportStaffCosts = expenditure.EducationSupportStaffCosts;
            AgencySupplyTeachingStaffCosts = expenditure.AgencySupplyTeachingStaffCosts;
            TotalNonEducationalSupportStaffCosts = expenditure.TotalNonEducationalSupportStaffCosts;
            AdministrativeClericalStaffCosts = expenditure.AdministrativeClericalStaffCosts;
            AuditorsCosts = expenditure.AuditorsCosts;
            OtherStaffCosts = expenditure.OtherStaffCosts;
            ProfessionalServicesNonCurriculumCosts = expenditure.ProfessionalServicesNonCurriculumCosts;
            TotalEducationalSuppliesCosts = expenditure.TotalEducationalSuppliesCosts;
            ExaminationFeesCosts = expenditure.ExaminationFeesCosts;
            LearningResourcesNonIctCosts = expenditure.LearningResourcesNonIctCosts;
            LearningResourcesIctCosts = expenditure.LearningResourcesIctCosts;
            TotalPremisesStaffServiceCosts = expenditure.TotalPremisesStaffServiceCosts;
            CleaningCaretakingCosts = expenditure.CleaningCaretakingCosts;
            MaintenancePremisesCosts = expenditure.MaintenancePremisesCosts;
            OtherOccupationCosts = expenditure.OtherOccupationCosts;
            PremisesStaffCosts = expenditure.PremisesStaffCosts;
            TotalUtilitiesCosts = expenditure.TotalUtilitiesCosts;
            EnergyCosts = expenditure.EnergyCosts;
            WaterSewerageCosts = expenditure.WaterSewerageCosts;
            AdministrativeSuppliesNonEducationalCosts = expenditure.AdministrativeSuppliesNonEducationalCosts;
            TotalGrossCateringCosts = expenditure.TotalGrossCateringCosts;
            TotalNetCateringCosts = expenditure.TotalNetCateringCosts;
            CateringStaffCosts = expenditure.CateringStaffCosts;
            CateringSuppliesCosts = expenditure.CateringSuppliesCosts;
            TotalOtherCosts = expenditure.TotalOtherCosts;
            GroundsMaintenanceCosts = expenditure.GroundsMaintenanceCosts;
            IndirectEmployeeExpenses = expenditure.IndirectEmployeeExpenses;
            InterestChargesLoanBank = expenditure.InterestChargesLoanBank;
            OtherInsurancePremiumsCosts = expenditure.OtherInsurancePremiumsCosts;
            PrivateFinanceInitiativeCharges = expenditure.PrivateFinanceInitiativeCharges;
            RentRatesCosts = expenditure.RentRatesCosts;
            SpecialFacilitiesCosts = expenditure.SpecialFacilitiesCosts;
            StaffDevelopmentTrainingCosts = expenditure.StaffDevelopmentTrainingCosts;
            StaffRelatedInsuranceCosts = expenditure.StaffRelatedInsuranceCosts;
            SupplyTeacherInsurableCosts = expenditure.SupplyTeacherInsurableCosts;
            CommunityFocusedSchoolStaff = expenditure.CommunityFocusedSchoolStaff;
            CommunityFocusedSchoolCosts = expenditure.CommunityFocusedSchoolCosts;

            URN = expenditure.URN;
            SchoolName = expenditure.SchoolName;
            SchoolType = expenditure.SchoolType;
            LAName = expenditure.LAName;
            PeriodCoveredByReturn = expenditure.PeriodCoveredByReturn;
            TotalPupils = expenditure.TotalPupils;
            TotalInternalFloorArea = expenditure.TotalInternalFloorArea;

            if (banding?.Banding is KS4ProgressBandings.Banding.AboveAverage or KS4ProgressBandings.Banding.WellAboveAverage)
            {
                ProgressBanding = banding.Banding.ToStringValue();
            }
        }

        public string? ProgressBanding { get; set; }
    }
}