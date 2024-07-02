using Web.App.Domain;
using Web.App.Extensions;
using Web.App.Infrastructure.Apis;
using Web.App.Infrastructure.Apis.Benchmark;
using Web.App.Infrastructure.Apis.Insight;
using Web.App.Infrastructure.Extensions;
using Web.App.ViewModels;
namespace Web.App.Services;

public interface ICustomDataService
{
    Task<CustomData> GetCurrentData(string urn);
    CustomData? GetCustomDataFromSession(string urn);
    void SetCustomDataInSession(string urn, CustomData data);
    void MergeCustomDataIntoSession(string urn, ICustomDataViewModel data);
    void ClearCustomDataFromSession(string urn);
    Task CreateCustomData(string urn, string userId);
    Task<CustomData?> GetCustomDataById(string urn, string identifier);
    Task RemoveCustomData(string urn, string identifier);
}

public class CustomDataService(
    IHttpContextAccessor httpContextAccessor,
    IIncomeApi incomeApi,
    ICustomDataApi customDataApi,
    IExpenditureApi expenditureApi,
    ISchoolInsightApi schoolInsightApi,
    ICensusApi censusApi,
    IBalanceApi balanceApi,
    ILogger<CustomDataService> logger)
    : ICustomDataService
{
    public async Task<CustomData> GetCurrentData(string urn)
    {
        var income = await incomeApi.School(urn).GetResultOrThrow<SchoolIncome>();
        var expenditure = await expenditureApi.School(urn).GetResultOrThrow<SchoolExpenditure>();
        var census = await censusApi.Get(urn).GetResultOrThrow<Census>();
        var characteristics = await schoolInsightApi.GetCharacteristicsAsync(urn).GetResultOrThrow<SchoolCharacteristic>();
        var balance = await balanceApi.School(urn).GetResultOrThrow<SchoolBalance>();

        return CustomDataFactory.Create(income, expenditure, census, characteristics, balance);
    }

    public CustomData? GetCustomDataFromSession(string urn)
    {
        var key = SessionKeys.CustomData(urn);
        var context = httpContextAccessor.HttpContext;
        var data = context?.Session.Get<CustomData>(key);

        logger.LogDebug("Got {CustomData} for {Key} from session", data.ToJson(), urn);
        return data;
    }

    public void SetCustomDataInSession(string urn, CustomData data)
    {
        var key = SessionKeys.CustomData(urn);
        var context = httpContextAccessor.HttpContext;
        context?.Session.Set(key, data);

        logger.LogDebug("Set {CustomData} for {Key} in session", data.ToJson(), urn);
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
        SetCustomDataInSession(urn, data);
        logger.LogDebug("Merged {ViewModel} and set {CustomData} for {Key} in session", viewModel.ToJson(),
            data.ToJson(), urn);
    }

    public async Task<CustomData?> GetCustomDataById(string urn, string identifier)
    {
        var customDataSchool = await customDataApi.GetSchoolAsync(urn, identifier).GetResultOrDefault<CustomDataSchool>();
        if (customDataSchool?.Data == null)
        {
            return null;
        }

        var parsed = customDataSchool.Data.FromJson<PutCustomDataRequest>();
        return parsed == null ? null : CreateCustomData(parsed);
    }

    public async Task RemoveCustomData(string urn, string identifier) =>
        await customDataApi.RemoveSchoolAsync(urn, identifier).EnsureSuccess();

    private static PutCustomDataRequest CreateRequest(CustomData data) => new()
    {
        AdministrativeSuppliesNonEducationalCosts = data.AdministrativeSuppliesCosts,
        CateringStaffCosts = data.CateringStaffCosts,
        CateringSuppliesCosts = data.CateringSuppliesCosts,
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
        TotalPupils = data.NumberOfPupilsFte,
        PercentFreeSchoolMeals = data.FreeSchoolMealPercent,
        PercentSpecialEducationNeeds = data.SpecialEducationalNeedsPercent,
        TotalInternalFloorArea = data.FloorArea,
        WorkforceFTE = data.WorkforceFte,
        TeachersFTE = data.TeachersFte,
        PercentTeacherWithQualifiedStatus = data.QualifiedTeacherPercent,
        SeniorLeadershipFTE = data.SeniorLeadershipFte,
        TeachingAssistantFTE = data.TeachingAssistantsFte,
        NonClassroomSupportStaffFTE = data.NonClassroomSupportStaffFte,
        AuxiliaryStaffFTE = data.AuxiliaryStaffFte,
        WorkforceHeadcount = data.WorkforceHeadcount
    };

    private static CustomData CreateCustomData(PutCustomDataRequest data) => new()
    {
        AdministrativeSuppliesCosts = data.AdministrativeSuppliesNonEducationalCosts,
        CateringStaffCosts = data.CateringStaffCosts,
        CateringSuppliesCosts = data.CateringSuppliesCosts,
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
        NumberOfPupilsFte = data.TotalPupils,
        FreeSchoolMealPercent = data.PercentFreeSchoolMeals,
        SpecialEducationalNeedsPercent = data.PercentSpecialEducationNeeds,
        FloorArea = data.TotalInternalFloorArea,
        WorkforceFte = data.WorkforceFTE,
        TeachersFte = data.TeachersFTE,
        QualifiedTeacherPercent = data.PercentTeacherWithQualifiedStatus,
        SeniorLeadershipFte = data.SeniorLeadershipFTE,
        TeachingAssistantsFte = data.TeachingAssistantFTE,
        NonClassroomSupportStaffFte = data.NonClassroomSupportStaffFTE,
        AuxiliaryStaffFte = data.AuxiliaryStaffFTE,
        WorkforceHeadcount = data.WorkforceHeadcount
    };
}