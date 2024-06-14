using Web.App.Domain;
using Web.App.Extensions;
using Web.App.Infrastructure.Apis;
using Web.App.Infrastructure.Extensions;
using Web.App.ViewModels;

namespace Web.App.Services;

public interface ICustomDataService
{
    Task<CustomData> GetCurrentData(string urn);
    CustomData GetCustomDataFromSession(string urn);
    void MergeCustomDataIntoSession(string urn, ICustomDataViewModel data);
    void ClearCustomDataFromSession(string urn);
    Task CreateCustomData(string urn, string userId);
}

public class CustomDataService(
    IHttpContextAccessor httpContextAccessor,
    IFinanceService financeService,
    IIncomeApi incomeApi,
    ICustomDataApi customDataApi,
    IExpenditureApi expenditureApi,
    ILogger<CustomDataService> logger)
    : ICustomDataService
{
    public async Task<CustomData> GetCurrentData(string urn)
    {
        // todo: lookup and return current, potentially customised figures

        var finances = await financeService.GetFinances(urn);
        var income = await incomeApi.School(urn).GetResultOrThrow<Income>();
        var expenditure = await expenditureApi.School(urn).GetResultOrThrow<SchoolExpenditure>();
        var census = await financeService.GetSchoolCensus(urn);
        var floorArea = await financeService.GetSchoolFloorArea(urn);

        return new CustomData(finances, income, expenditure, census, floorArea);
    }

    public CustomData GetCustomDataFromSession(string urn)
    {
        var key = SessionKeys.CustomData(urn);
        var context = httpContextAccessor.HttpContext;
        var data = context?.Session.Get<CustomData>(key) ?? new CustomData();

        logger.LogDebug("Got {CustomData} for {Key} from session", data.ToJson(), urn);
        return data;
    }

    public async Task CreateCustomData(string urn, string userId)
    {
        var key = SessionKeys.CustomData(urn);
        var context = httpContextAccessor.HttpContext;
        var data = context?.Session.Get<CustomData>(key) ?? new CustomData();

        var request = CreateRequest(data);
        request.UserId = userId;

        await customDataApi.UpsertSchoolAsync(urn, request).EnsureSuccess();
    }

    public void ClearCustomDataFromSession(string urn)
    {
        var key = SessionKeys.CustomData(urn);
        var context = httpContextAccessor.HttpContext;
        context?.Session.Remove(key);

        logger.LogDebug("Cleared data for {Key} from session", urn);
    }

    public void MergeCustomDataIntoSession(string urn, ICustomDataViewModel viewModel)
    {
        var key = SessionKeys.CustomData(urn);
        var context = httpContextAccessor.HttpContext;
        var data = context?.Session.Get<CustomData>(key) ?? new CustomData();
        logger.LogDebug("Got {CustomData} for {Key} from session", data.ToJson(), urn);

        data.Merge(viewModel);
        context?.Session.Set(key, data);
        logger.LogDebug("Merged {ViewModel} and set {CustomData} for {Key} in session", viewModel.ToJson(),
            data.ToJson(), urn);
    }

    private static PutCustomDataRequest CreateRequest(CustomData data)
    {
        return new PutCustomDataRequest
        {
            AdministrativeSuppliesNonEducationalCosts = data.AdministrativeSuppliesCosts,
            CateringStaffCosts = data.CateringStaffCosts,
            CateringSuppliesCosts = data.CateringSuppliesCosts,
            IncomeCateringServices = data.CateringIncome,
            ExaminationFeesCosts = data.ExaminationFeesCosts,
            LearningResourcesNonIctCosts = data.LearningResourcesNonIctCosts,
            LearningResourcesIctCosts = data.LearningResourcesIctCosts,
            AdministrativeClericalStaffCosts = data.AdministrativeClericalStaffCosts,
            AuditorsCosts = data.AuditorsCosts,
            OtherStaffCosts = data.OtherStaffCosts,
            ProfessionalServicesNonCurriculumCosts = data.ProfessionalServicesNonCurriculumCosts,
            CleaningCaretakingCosts = data.CleaningCaretakingCosts,
            MaintenancePremisesCosts = data.MaintenancePremisesCosts,
            OtherOccupationCosts = data.OtherOccupationCosts,
            PremisesStaffCosts = data.PremisesStaffCosts,
            AgencySupplyTeachingStaffCosts = data.AgencySupplyTeachingStaffCosts,
            EducationSupportStaffCosts = data.EducationSupportStaffCosts,
            EducationalConsultancyCosts = data.EducationalConsultancyCosts,
            SupplyTeachingStaffCosts = data.SupplyTeachingStaffCosts,
            TeachingStaffCosts = data.TeachingStaffCosts,
            EnergyCosts = data.EnergyCosts,
            WaterSewerageCosts = data.WaterSewerageCosts,
            DirectRevenueFinancingCosts = data.DirectRevenueFinancingCosts,
            GroundsMaintenanceCosts = data.GroundsMaintenanceCosts,
            IndirectEmployeeExpenses = data.IndirectEmployeeExpenses,
            InterestChargesLoanBank = data.InterestChargesLoanBank,
            OtherInsurancePremiumsCosts = data.OtherInsurancePremiumsCosts,
            PrivateFinanceInitiativeCharges = data.PrivateFinanceInitiativeCharges,
            RentRatesCosts = data.RentRatesCosts,
            SpecialFacilitiesCosts = data.SpecialFacilitiesCosts,
            StaffDevelopmentTrainingCosts = data.StaffDevelopmentTrainingCosts,
            StaffRelatedInsuranceCosts = data.StaffRelatedInsuranceCosts,
            SupplyTeacherInsurableCosts = data.SupplyTeacherInsurableCosts,
            TotalIncome = data.TotalIncome,
            TotalExpenditure = data.TotalExpenditure,
            RevenueReserve = data.RevenueReserve,
            TotalPupils = data.NumberOfPupilsFte,
            PercentFreeSchoolMeals = data.FreeSchoolMealPercent,
            PercentSpecialEducationNeeds = data.SpecialEducationalNeedsPercent,
            TotalInternalFloorArea = data.FloorArea,
            WorkforceFTE = data.WorkforceFte,
            TeachersFTE = data.TeachersFte,
            SeniorLeadershipFTE = data.SeniorLeadershipFte
        };
    }
}