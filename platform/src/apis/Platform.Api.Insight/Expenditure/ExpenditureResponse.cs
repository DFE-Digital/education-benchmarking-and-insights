namespace Platform.Api.Insight.Expenditure;

public static class ExpenditureResponseFactory
{
    public static SchoolExpenditureResponse Create(SchoolExpenditureModel model, ExpenditureParameters parameters)
    {
        var response = CreateResponse<SchoolExpenditureResponse>(model, parameters);

        response.URN = model.URN;
        response.SchoolName = model.SchoolName;
        response.SchoolType = model.SchoolType;
        response.LAName = model.LAName;
        response.TotalPupils = model.TotalPupils;
        response.TotalInternalFloorArea = model.TotalInternalFloorArea;

        return response;
    }

    public static TrustExpenditureResponse Create(TrustExpenditureModel model, ExpenditureParameters parameters)
    {
        var response = CreateResponse<TrustExpenditureResponse>(model, parameters);

        response.CompanyNumber = model.CompanyNumber;
        response.TrustName = model.TrustName;

        return response;
    }

    public static SchoolExpenditureHistoryResponse Create(SchoolExpenditureHistoryModel model, ExpenditureParameters parameters)
    {
        var response = CreateResponse<SchoolExpenditureHistoryResponse>(model, parameters);

        response.URN = model.URN;
        response.Year = model.Year;

        return response;
    }

    public static TrustExpenditureHistoryResponse Create(TrustExpenditureHistoryModel model, ExpenditureParameters parameters)
    {
        var response = CreateResponse<TrustExpenditureHistoryResponse>(model, parameters);

        response.CompanyNumber = model.CompanyNumber;
        response.Year = model.Year;

        return response;
    }

    private static T CreateResponse<T>(ExpenditureBaseModel model, ExpenditureParameters parameters)
        where T : ExpenditureBaseResponse, new()
    {
        var response = new T();

        var schoolTotalExpenditure = CalcPupilSchool(model.TotalExpenditure, model, parameters.Dimension);
        var centralTotalExpenditure = CalcPupilCentral(model.TotalExpenditureCS, model, parameters.Dimension);

        response.SchoolTotalExpenditure = parameters.IncludeBreakdown ? schoolTotalExpenditure : null;
        response.CentralTotalExpenditure = parameters.IncludeBreakdown ? centralTotalExpenditure : null;
        response.TotalExpenditure = CalculateTotal(schoolTotalExpenditure, centralTotalExpenditure, parameters.Dimension);

        if (parameters.Category is null or ExpenditureCategories.TeachingTeachingSupportStaff)
        {
            SetTeachingTeachingSupportStaff(model, parameters, response);
        }

        if (parameters.Category is null or ExpenditureCategories.NonEducationalSupportStaff)
        {
            SetNonEducationalSupportStaff(model, parameters, response);
        }

        if (parameters.Category is null or ExpenditureCategories.EducationalSupplies)
        {
            SetEducationalSupplies(model, parameters, response);
        }

        if (parameters.Category is null or ExpenditureCategories.EducationalIct)
        {
            SetEducationalIct(model, parameters, response);
        }

        if (parameters.Category is null or ExpenditureCategories.PremisesStaffServices)
        {
            SetPremisesStaffServices(model, parameters, response);
        }

        if (parameters.Category is null or ExpenditureCategories.Utilities)
        {
            SetUtilities(model, parameters, response);
        }

        if (parameters.Category is null or ExpenditureCategories.AdministrationSupplies)
        {
            SetAdministrationSupplies(model, parameters, response);
        }

        if (parameters.Category is null or ExpenditureCategories.CateringStaffServices)
        {
            SetCateringStaffServices(model, parameters, response);
        }

        if (parameters.Category is null or ExpenditureCategories.Other)
        {
            SetOther(model, parameters, response);
        }

        return response;
    }

    private static void SetTeachingTeachingSupportStaff<T>(ExpenditureBaseModel model, ExpenditureParameters parameters, T response)
        where T : ExpenditureBaseResponse, new()
    {

        var schoolTotalTeachingSupportStaffCosts = CalcPupilSchool(model.TotalTeachingSupportStaffCosts, model, parameters.Dimension);
        var schoolTeachingStaffCosts = CalcPupilSchool(model.TeachingStaffCosts, model, parameters.Dimension);
        var schoolSupplyTeachingStaffCosts = CalcPupilSchool(model.SupplyTeachingStaffCosts, model, parameters.Dimension);
        var schoolEducationalConsultancyCosts = CalcPupilSchool(model.EducationalConsultancyCosts, model, parameters.Dimension);
        var schoolEducationSupportStaffCosts = CalcPupilSchool(model.EducationSupportStaffCosts, model, parameters.Dimension);
        var schoolAgencySupplyTeachingStaffCosts = CalcPupilSchool(model.AgencySupplyTeachingStaffCosts, model, parameters.Dimension);

        var centralTotalTeachingSupportStaffCosts = CalcPupilCentral(model.TotalTeachingSupportStaffCostsCS, model, parameters.Dimension);
        var centralTeachingStaffCosts = CalcPupilCentral(model.TeachingStaffCostsCS, model, parameters.Dimension);
        var centralSupplyTeachingStaffCosts = CalcPupilCentral(model.SupplyTeachingStaffCostsCS, model, parameters.Dimension);
        var centralEducationalConsultancyCosts = CalcPupilCentral(model.EducationalConsultancyCostsCS, model, parameters.Dimension);
        var centralEducationSupportStaffCosts = CalcPupilCentral(model.EducationSupportStaffCostsCS, model, parameters.Dimension);
        var centralAgencySupplyTeachingStaffCosts = CalcPupilCentral(model.AgencySupplyTeachingStaffCostsCS, model, parameters.Dimension);

        response.SchoolTotalTeachingSupportStaffCosts = parameters.IncludeBreakdown ? schoolTotalTeachingSupportStaffCosts : null;
        response.SchoolTeachingStaffCosts = parameters.IncludeBreakdown ? schoolTeachingStaffCosts : null;
        response.SchoolSupplyTeachingStaffCosts = parameters.IncludeBreakdown ? schoolSupplyTeachingStaffCosts : null;
        response.SchoolEducationalConsultancyCosts = parameters.IncludeBreakdown ? schoolEducationalConsultancyCosts : null;
        response.SchoolEducationSupportStaffCosts = parameters.IncludeBreakdown ? schoolEducationSupportStaffCosts : null;
        response.SchoolAgencySupplyTeachingStaffCosts = parameters.IncludeBreakdown ? schoolAgencySupplyTeachingStaffCosts : null;

        response.CentralTotalTeachingSupportStaffCosts = parameters.IncludeBreakdown ? centralTotalTeachingSupportStaffCosts : null;
        response.CentralTeachingStaffCosts = parameters.IncludeBreakdown ? centralTeachingStaffCosts : null;
        response.CentralSupplyTeachingStaffCosts = parameters.IncludeBreakdown ? centralSupplyTeachingStaffCosts : null;
        response.CentralEducationalConsultancyCosts = parameters.IncludeBreakdown ? centralEducationalConsultancyCosts : null;
        response.CentralEducationSupportStaffCosts = parameters.IncludeBreakdown ? centralEducationSupportStaffCosts : null;
        response.CentralAgencySupplyTeachingStaffCosts = parameters.IncludeBreakdown ? centralEducationSupportStaffCosts : null;

        response.TotalTeachingSupportStaffCosts = CalculateTotal(schoolTotalTeachingSupportStaffCosts, centralTotalTeachingSupportStaffCosts, parameters.Dimension);
        response.TeachingStaffCosts = CalculateTotal(schoolTeachingStaffCosts, centralTeachingStaffCosts, parameters.Dimension);
        response.SupplyTeachingStaffCosts = CalculateTotal(schoolSupplyTeachingStaffCosts, centralSupplyTeachingStaffCosts, parameters.Dimension);
        response.EducationalConsultancyCosts = CalculateTotal(schoolEducationalConsultancyCosts, centralEducationalConsultancyCosts, parameters.Dimension);
        response.EducationSupportStaffCosts = CalculateTotal(schoolEducationSupportStaffCosts, centralEducationSupportStaffCosts, parameters.Dimension);
        response.AgencySupplyTeachingStaffCosts = CalculateTotal(schoolAgencySupplyTeachingStaffCosts, centralAgencySupplyTeachingStaffCosts, parameters.Dimension);
    }

    private static void SetNonEducationalSupportStaff<T>(ExpenditureBaseModel model, ExpenditureParameters parameters, T response)
        where T : ExpenditureBaseResponse, new()
    {
        var schoolTotalNonEducationalSupportStaffCosts = CalcPupilSchool(model.TotalNonEducationalSupportStaffCosts, model, parameters.Dimension);
        var schoolAdministrativeClericalStaffCosts = CalcPupilSchool(model.AdministrativeClericalStaffCosts, model, parameters.Dimension);
        var schoolAuditorsCosts = CalcPupilSchool(model.AuditorsCosts, model, parameters.Dimension);
        var schoolOtherStaffCosts = CalcPupilSchool(model.OtherStaffCosts, model, parameters.Dimension);
        var schoolProfessionalServicesNonCurriculumCosts = CalcPupilSchool(model.ProfessionalServicesNonCurriculumCosts, model, parameters.Dimension);

        var centralTotalNonEducationalSupportStaffCosts = CalcPupilCentral(model.TotalNonEducationalSupportStaffCostsCS, model, parameters.Dimension);
        var centralAdministrativeClericalStaffCosts = CalcPupilCentral(model.AdministrativeClericalStaffCostsCS, model, parameters.Dimension);
        var centralAuditorsCosts = CalcPupilCentral(model.AuditorsCostsCS, model, parameters.Dimension);
        var centralOtherStaffCosts = CalcPupilCentral(model.OtherStaffCostsCS, model, parameters.Dimension);
        var centralProfessionalServicesNonCurriculumCosts = CalcPupilCentral(model.ProfessionalServicesNonCurriculumCostsCS, model, parameters.Dimension);

        response.SchoolTotalNonEducationalSupportStaffCosts = parameters.IncludeBreakdown ? schoolTotalNonEducationalSupportStaffCosts : null;
        response.SchoolAdministrativeClericalStaffCosts = parameters.IncludeBreakdown ? schoolAdministrativeClericalStaffCosts : null;
        response.SchoolAuditorsCosts = parameters.IncludeBreakdown ? schoolAuditorsCosts : null;
        response.SchoolOtherStaffCosts = parameters.IncludeBreakdown ? schoolOtherStaffCosts : null;
        response.SchoolProfessionalServicesNonCurriculumCosts = parameters.IncludeBreakdown ? schoolProfessionalServicesNonCurriculumCosts : null;

        response.CentralTotalNonEducationalSupportStaffCosts = parameters.IncludeBreakdown ? centralTotalNonEducationalSupportStaffCosts : null;
        response.CentralAdministrativeClericalStaffCosts = parameters.IncludeBreakdown ? centralAdministrativeClericalStaffCosts : null;
        response.CentralAuditorsCosts = parameters.IncludeBreakdown ? centralAuditorsCosts : null;
        response.CentralOtherStaffCosts = parameters.IncludeBreakdown ? centralOtherStaffCosts : null;
        response.CentralProfessionalServicesNonCurriculumCosts = parameters.IncludeBreakdown ? centralProfessionalServicesNonCurriculumCosts : null;

        response.TotalNonEducationalSupportStaffCosts = CalculateTotal(schoolTotalNonEducationalSupportStaffCosts, centralTotalNonEducationalSupportStaffCosts, parameters.Dimension);
        response.AdministrativeClericalStaffCosts = CalculateTotal(schoolAdministrativeClericalStaffCosts, centralAdministrativeClericalStaffCosts, parameters.Dimension);
        response.AuditorsCosts = CalculateTotal(schoolAuditorsCosts, centralAuditorsCosts, parameters.Dimension);
        response.OtherStaffCosts = CalculateTotal(schoolOtherStaffCosts, centralOtherStaffCosts, parameters.Dimension);
        response.ProfessionalServicesNonCurriculumCosts = CalculateTotal(schoolProfessionalServicesNonCurriculumCosts, centralProfessionalServicesNonCurriculumCosts, parameters.Dimension);
    }

    private static void SetEducationalSupplies<T>(ExpenditureBaseModel model, ExpenditureParameters parameters, T response)
        where T : ExpenditureBaseResponse, new()
    {
        var schoolTotalEducationalSuppliesCosts = CalcPupilSchool(model.TotalEducationalSuppliesCosts, model, parameters.Dimension);
        var schoolExaminationFeesCosts = CalcPupilSchool(model.ExaminationFeesCosts, model, parameters.Dimension);
        var schoolLearningResourcesNonIctCosts = CalcPupilSchool(model.LearningResourcesNonIctCosts, model, parameters.Dimension);

        var centralTotalEducationalSuppliesCosts = CalcPupilCentral(model.TotalEducationalSuppliesCostsCS, model, parameters.Dimension);
        var centralExaminationFeesCosts = CalcPupilCentral(model.ExaminationFeesCostsCS, model, parameters.Dimension);
        var centralLearningResourcesNonIctCosts = CalcPupilCentral(model.LearningResourcesNonIctCostsCS, model, parameters.Dimension);

        response.SchoolTotalEducationalSuppliesCosts = parameters.IncludeBreakdown ? schoolTotalEducationalSuppliesCosts : null;
        response.SchoolExaminationFeesCosts = parameters.IncludeBreakdown ? schoolExaminationFeesCosts : null;
        response.SchoolLearningResourcesNonIctCosts = parameters.IncludeBreakdown ? schoolLearningResourcesNonIctCosts : null;

        response.CentralTotalEducationalSuppliesCosts = parameters.IncludeBreakdown ? centralTotalEducationalSuppliesCosts : null;
        response.CentralExaminationFeesCosts = parameters.IncludeBreakdown ? centralExaminationFeesCosts : null;
        response.CentralLearningResourcesNonIctCosts = parameters.IncludeBreakdown ? centralLearningResourcesNonIctCosts : null;

        response.TotalEducationalSuppliesCosts = CalculateTotal(schoolTotalEducationalSuppliesCosts, centralTotalEducationalSuppliesCosts, parameters.Dimension);
        response.ExaminationFeesCosts = CalculateTotal(schoolExaminationFeesCosts, centralExaminationFeesCosts, parameters.Dimension);
        response.LearningResourcesNonIctCosts = CalculateTotal(schoolLearningResourcesNonIctCosts, centralLearningResourcesNonIctCosts, parameters.Dimension);
    }

    private static void SetEducationalIct<T>(ExpenditureBaseModel model, ExpenditureParameters parameters, T response)
        where T : ExpenditureBaseResponse, new()
    {
        var schoolLearningResourcesIctCosts = CalcPupilSchool(model.LearningResourcesIctCosts, model, parameters.Dimension);
        var centralLearningResourcesIctCosts = CalcPupilCentral(model.LearningResourcesIctCostsCS, model, parameters.Dimension);

        response.SchoolLearningResourcesIctCosts = parameters.IncludeBreakdown ? schoolLearningResourcesIctCosts : null;
        response.CentralLearningResourcesIctCosts = parameters.IncludeBreakdown ? centralLearningResourcesIctCosts : null;
        response.LearningResourcesIctCosts = CalculateTotal(schoolLearningResourcesIctCosts, centralLearningResourcesIctCosts, parameters.Dimension);
    }

    private static void SetPremisesStaffServices<T>(ExpenditureBaseModel model, ExpenditureParameters parameters, T response)
        where T : ExpenditureBaseResponse, new()
    {
        var schoolTotalPremisesStaffServiceCosts = CalcBuildingSchool(model.TotalPremisesStaffServiceCosts, model, parameters.Dimension);
        var schoolCleaningCaretakingCosts = CalcBuildingSchool(model.CleaningCaretakingCosts, model, parameters.Dimension);
        var schoolMaintenancePremisesCosts = CalcBuildingSchool(model.MaintenancePremisesCosts, model, parameters.Dimension);
        var schoolOtherOccupationCosts = CalcBuildingSchool(model.OtherOccupationCosts, model, parameters.Dimension);
        var schoolPremisesStaffCosts = CalcBuildingSchool(model.PremisesStaffCosts, model, parameters.Dimension);

        var centralTotalPremisesStaffServiceCosts = CalcBuildingCentral(model.TotalPremisesStaffServiceCostsCS, model, parameters.Dimension);
        var centralCleaningCaretakingCosts = CalcBuildingCentral(model.CleaningCaretakingCostsCS, model, parameters.Dimension);
        var centralMaintenancePremisesCosts = CalcBuildingCentral(model.MaintenancePremisesCostsCS, model, parameters.Dimension);
        var centralOtherOccupationCosts = CalcBuildingCentral(model.OtherOccupationCostsCS, model, parameters.Dimension);
        var centralPremisesStaffCosts = CalcBuildingCentral(model.PremisesStaffCostsCS, model, parameters.Dimension);

        response.SchoolTotalPremisesStaffServiceCosts = parameters.IncludeBreakdown ? schoolTotalPremisesStaffServiceCosts : null;
        response.SchoolCleaningCaretakingCosts = parameters.IncludeBreakdown ? schoolCleaningCaretakingCosts : null;
        response.SchoolMaintenancePremisesCosts = parameters.IncludeBreakdown ? schoolMaintenancePremisesCosts : null;
        response.SchoolOtherOccupationCosts = parameters.IncludeBreakdown ? schoolOtherOccupationCosts : null;
        response.SchoolPremisesStaffCosts = parameters.IncludeBreakdown ? schoolPremisesStaffCosts : null;

        response.CentralTotalPremisesStaffServiceCosts = parameters.IncludeBreakdown ? centralTotalPremisesStaffServiceCosts : null;
        response.CentralCleaningCaretakingCosts = parameters.IncludeBreakdown ? centralCleaningCaretakingCosts : null;
        response.CentralMaintenancePremisesCosts = parameters.IncludeBreakdown ? centralMaintenancePremisesCosts : null;
        response.CentralOtherOccupationCosts = parameters.IncludeBreakdown ? centralOtherOccupationCosts : null;
        response.CentralPremisesStaffCosts = parameters.IncludeBreakdown ? centralPremisesStaffCosts : null;

        response.TotalPremisesStaffServiceCosts = CalculateTotal(schoolTotalPremisesStaffServiceCosts, centralTotalPremisesStaffServiceCosts, parameters.Dimension);
        response.CleaningCaretakingCosts = CalculateTotal(schoolCleaningCaretakingCosts, centralCleaningCaretakingCosts, parameters.Dimension);
        response.MaintenancePremisesCosts = CalculateTotal(schoolMaintenancePremisesCosts, centralMaintenancePremisesCosts, parameters.Dimension);
        response.OtherOccupationCosts = CalculateTotal(schoolOtherOccupationCosts, centralOtherOccupationCosts, parameters.Dimension);
        response.PremisesStaffCosts = CalculateTotal(schoolPremisesStaffCosts, centralPremisesStaffCosts, parameters.Dimension);
    }

    private static void SetUtilities<T>(ExpenditureBaseModel model, ExpenditureParameters parameters, T response)
        where T : ExpenditureBaseResponse, new()
    {
        var schoolTotalUtilitiesCosts = CalcBuildingSchool(model.TotalUtilitiesCosts, model, parameters.Dimension);
        var schoolEnergyCosts = CalcBuildingSchool(model.EnergyCosts, model, parameters.Dimension);
        var schoolWaterSewerageCosts = CalcBuildingSchool(model.WaterSewerageCosts, model, parameters.Dimension);

        var centralTotalUtilitiesCosts = CalcBuildingCentral(model.TotalUtilitiesCostsCS, model, parameters.Dimension);
        var centralEnergyCosts = CalcBuildingCentral(model.EnergyCostsCS, model, parameters.Dimension);
        var centralWaterSewerageCosts = CalcBuildingCentral(model.WaterSewerageCostsCS, model, parameters.Dimension);

        response.SchoolTotalUtilitiesCosts = parameters.IncludeBreakdown ? schoolTotalUtilitiesCosts : null;
        response.SchoolEnergyCosts = parameters.IncludeBreakdown ? schoolEnergyCosts : null;
        response.SchoolWaterSewerageCosts = parameters.IncludeBreakdown ? schoolWaterSewerageCosts : null;

        response.CentralTotalUtilitiesCosts = parameters.IncludeBreakdown ? centralTotalUtilitiesCosts : null;
        response.CentralEnergyCosts = parameters.IncludeBreakdown ? centralEnergyCosts : null;
        response.CentralWaterSewerageCosts = parameters.IncludeBreakdown ? centralWaterSewerageCosts : null;

        response.TotalUtilitiesCosts = CalculateTotal(schoolTotalUtilitiesCosts, centralTotalUtilitiesCosts, parameters.Dimension);
        response.EnergyCosts = CalculateTotal(schoolEnergyCosts, centralEnergyCosts, parameters.Dimension);
        response.WaterSewerageCosts = CalculateTotal(schoolWaterSewerageCosts, centralWaterSewerageCosts, parameters.Dimension);
    }

    private static void SetAdministrationSupplies<T>(ExpenditureBaseModel model, ExpenditureParameters parameters, T response)
        where T : ExpenditureBaseResponse, new()
    {
        var schoolAdministrativeSuppliesCosts = CalcPupilSchool(model.AdministrativeSuppliesNonEducationalCosts, model, parameters.Dimension);
        var centralAdministrativeSuppliesCosts = CalcPupilCentral(model.AdministrativeSuppliesNonEducationalCostsCS, model, parameters.Dimension);

        response.SchoolAdministrativeSuppliesCosts = parameters.IncludeBreakdown ? schoolAdministrativeSuppliesCosts : null;
        response.CentralAdministrativeSuppliesCosts = parameters.IncludeBreakdown ? centralAdministrativeSuppliesCosts : null;
        response.AdministrativeSuppliesCosts = CalculateTotal(schoolAdministrativeSuppliesCosts, centralAdministrativeSuppliesCosts, parameters.Dimension);
    }

    private static void SetCateringStaffServices<T>(ExpenditureBaseModel model, ExpenditureParameters parameters, T response)
        where T : ExpenditureBaseResponse, new()
    {
        var schoolTotalGrossCateringCosts = CalcPupilSchool(model.TotalGrossCateringCosts, model, parameters.Dimension);
        var schoolCateringStaffCosts = CalcPupilSchool(model.CateringStaffCosts, model, parameters.Dimension);
        var schoolCateringSuppliesCosts = CalcPupilSchool(model.CateringSuppliesCosts, model, parameters.Dimension);

        var centralTotalGrossCateringCosts = CalcPupilCentral(model.TotalGrossCateringCostsCS, model, parameters.Dimension);
        var centralCateringStaffCosts = CalcPupilCentral(model.CateringStaffCostsCS, model, parameters.Dimension);
        var centralCateringSuppliesCosts = CalcPupilCentral(model.CateringSuppliesCostsCS, model, parameters.Dimension);

        response.SchoolTotalGrossCateringCosts = parameters.IncludeBreakdown ? schoolTotalGrossCateringCosts : null;
        response.SchoolCateringStaffCosts = parameters.IncludeBreakdown ? schoolCateringStaffCosts : null;
        response.SchoolCateringSuppliesCosts = parameters.IncludeBreakdown ? schoolCateringSuppliesCosts : null;

        response.CentralTotalGrossCateringCosts = parameters.IncludeBreakdown ? centralTotalGrossCateringCosts : null;
        response.CentralCateringStaffCosts = parameters.IncludeBreakdown ? centralCateringStaffCosts : null;
        response.CentralCateringSuppliesCosts = parameters.IncludeBreakdown ? centralCateringSuppliesCosts : null;

        response.TotalGrossCateringCosts = CalculateTotal(schoolTotalGrossCateringCosts, centralTotalGrossCateringCosts, parameters.Dimension);
        response.CateringStaffCosts = CalculateTotal(schoolCateringStaffCosts, centralCateringStaffCosts, parameters.Dimension);
        response.CateringSuppliesCosts = CalculateTotal(schoolCateringSuppliesCosts, centralCateringSuppliesCosts, parameters.Dimension);
    }

    private static void SetOther<T>(ExpenditureBaseModel model, ExpenditureParameters parameters, T response)
    where T : ExpenditureBaseResponse, new()
    {
        var schoolTotalOtherCosts = CalcPupilSchool(model.TotalOtherCosts, model, parameters.Dimension);
        var schoolDirectRevenueFinancingCosts = CalcPupilSchool(model.DirectRevenueFinancingCosts, model, parameters.Dimension);
        var schoolGroundsMaintenanceCosts = CalcPupilSchool(model.GroundsMaintenanceCosts, model, parameters.Dimension);
        var schoolIndirectEmployeeExpenses = CalcPupilSchool(model.IndirectEmployeeExpenses, model, parameters.Dimension);
        var schoolInterestChargesLoanBank = CalcPupilSchool(model.InterestChargesLoanBank, model, parameters.Dimension);
        var schoolOtherInsurancePremiumsCosts = CalcPupilSchool(model.OtherInsurancePremiumsCosts, model, parameters.Dimension);
        var schoolPrivateFinanceInitiativeCharges = CalcPupilSchool(model.PrivateFinanceInitiativeCharges, model, parameters.Dimension);
        var schoolRentRatesCosts = CalcPupilSchool(model.RentRatesCosts, model, parameters.Dimension);
        var schoolSpecialFacilitiesCosts = CalcPupilSchool(model.SpecialFacilitiesCosts, model, parameters.Dimension);
        var schoolStaffDevelopmentTrainingCosts = CalcPupilSchool(model.StaffDevelopmentTrainingCosts, model, parameters.Dimension);
        var schoolStaffRelatedInsuranceCosts = CalcPupilSchool(model.StaffRelatedInsuranceCosts, model, parameters.Dimension);
        var schoolSupplyTeacherInsurableCosts = CalcPupilSchool(model.SupplyTeacherInsurableCosts, model, parameters.Dimension);
        var schoolCommunityFocusedSchoolStaff = CalcPupilSchool(model.CommunityFocusedSchoolStaff, model, parameters.Dimension);
        var schoolCommunityFocusedSchoolCosts = CalcPupilSchool(model.CommunityFocusedSchoolCosts, model, parameters.Dimension);

        var centralTotalOtherCosts = CalcPupilCentral(model.TotalOtherCostsCS, model, parameters.Dimension);
        var centralDirectRevenueFinancingCosts = CalcPupilCentral(model.DirectRevenueFinancingCostsCS, model, parameters.Dimension);
        var centralGroundsMaintenanceCosts = CalcPupilCentral(model.GroundsMaintenanceCostsCS, model, parameters.Dimension);
        var centralIndirectEmployeeExpenses = CalcPupilCentral(model.IndirectEmployeeExpensesCS, model, parameters.Dimension);
        var centralInterestChargesLoanBank = CalcPupilCentral(model.InterestChargesLoanBankCS, model, parameters.Dimension);
        var centralOtherInsurancePremiumsCosts = CalcPupilCentral(model.OtherInsurancePremiumsCostsCS, model, parameters.Dimension);
        var centralPrivateFinanceInitiativeCharges = CalcPupilCentral(model.PrivateFinanceInitiativeChargesCS, model, parameters.Dimension);
        var centralRentRatesCosts = CalcPupilCentral(model.RentRatesCostsCS, model, parameters.Dimension);
        var centralSpecialFacilitiesCosts = CalcPupilCentral(model.SpecialFacilitiesCostsCS, model, parameters.Dimension);
        var centralStaffDevelopmentTrainingCosts = CalcPupilCentral(model.StaffDevelopmentTrainingCostsCS, model, parameters.Dimension);
        var centralStaffRelatedInsuranceCosts = CalcPupilCentral(model.StaffRelatedInsuranceCostsCS, model, parameters.Dimension);
        var centralSupplyTeacherInsurableCosts = CalcPupilCentral(model.SupplyTeacherInsurableCostsCS, model, parameters.Dimension);
        var centralCommunityFocusedSchoolStaff = CalcPupilCentral(model.CommunityFocusedSchoolStaffCS, model, parameters.Dimension);
        var centralCommunityFocusedSchoolCosts = CalcPupilCentral(model.CommunityFocusedSchoolCostsCS, model, parameters.Dimension);

        response.SchoolTotalOtherCosts = parameters.IncludeBreakdown ? schoolTotalOtherCosts : null;
        response.SchoolDirectRevenueFinancingCosts = parameters.IncludeBreakdown ? schoolDirectRevenueFinancingCosts : null;
        response.SchoolGroundsMaintenanceCosts = parameters.IncludeBreakdown ? schoolGroundsMaintenanceCosts : null;
        response.SchoolIndirectEmployeeExpenses = parameters.IncludeBreakdown ? schoolIndirectEmployeeExpenses : null;
        response.SchoolInterestChargesLoanBank = parameters.IncludeBreakdown ? schoolInterestChargesLoanBank : null;
        response.SchoolOtherInsurancePremiumsCosts = parameters.IncludeBreakdown ? schoolOtherInsurancePremiumsCosts : null;
        response.SchoolPrivateFinanceInitiativeCharges = parameters.IncludeBreakdown ? schoolPrivateFinanceInitiativeCharges : null;
        response.SchoolRentRatesCosts = parameters.IncludeBreakdown ? schoolRentRatesCosts : null;
        response.SchoolSpecialFacilitiesCosts = parameters.IncludeBreakdown ? schoolSpecialFacilitiesCosts : null;
        response.SchoolStaffDevelopmentTrainingCosts = parameters.IncludeBreakdown ? schoolStaffDevelopmentTrainingCosts : null;
        response.SchoolStaffRelatedInsuranceCosts = parameters.IncludeBreakdown ? schoolStaffRelatedInsuranceCosts : null;
        response.SchoolSupplyTeacherInsurableCosts = parameters.IncludeBreakdown ? schoolSupplyTeacherInsurableCosts : null;
        response.SchoolCommunityFocusedSchoolStaff = parameters.IncludeBreakdown ? schoolCommunityFocusedSchoolStaff : null;
        response.SchoolCommunityFocusedSchoolCosts = parameters.IncludeBreakdown ? schoolCommunityFocusedSchoolCosts : null;

        response.CentralTotalOtherCosts = parameters.IncludeBreakdown ? centralTotalOtherCosts : null;
        response.CentralDirectRevenueFinancingCosts = parameters.IncludeBreakdown ? centralDirectRevenueFinancingCosts : null;
        response.CentralGroundsMaintenanceCosts = parameters.IncludeBreakdown ? centralGroundsMaintenanceCosts : null;
        response.CentralIndirectEmployeeExpenses = parameters.IncludeBreakdown ? centralIndirectEmployeeExpenses : null;
        response.CentralInterestChargesLoanBank = parameters.IncludeBreakdown ? centralInterestChargesLoanBank : null;
        response.CentralOtherInsurancePremiumsCosts = parameters.IncludeBreakdown ? centralOtherInsurancePremiumsCosts : null;
        response.CentralPrivateFinanceInitiativeCharges = parameters.IncludeBreakdown ? centralPrivateFinanceInitiativeCharges : null;
        response.CentralRentRatesCosts = parameters.IncludeBreakdown ? centralRentRatesCosts : null;
        response.CentralSpecialFacilitiesCosts = parameters.IncludeBreakdown ? centralSpecialFacilitiesCosts : null;
        response.CentralStaffDevelopmentTrainingCosts = parameters.IncludeBreakdown ? centralStaffDevelopmentTrainingCosts : null;
        response.CentralStaffRelatedInsuranceCosts = parameters.IncludeBreakdown ? centralStaffRelatedInsuranceCosts : null;
        response.CentralSupplyTeacherInsurableCosts = parameters.IncludeBreakdown ? centralSupplyTeacherInsurableCosts : null;
        response.CentralCommunityFocusedSchoolStaff = parameters.IncludeBreakdown ? centralCommunityFocusedSchoolStaff : null;
        response.CentralCommunityFocusedSchoolCosts = parameters.IncludeBreakdown ? centralCommunityFocusedSchoolCosts : null;

        response.TotalOtherCosts = CalculateTotal(schoolTotalOtherCosts, centralTotalOtherCosts, parameters.Dimension);
        response.DirectRevenueFinancingCosts = CalculateTotal(schoolDirectRevenueFinancingCosts, centralDirectRevenueFinancingCosts, parameters.Dimension);
        response.GroundsMaintenanceCosts = CalculateTotal(schoolGroundsMaintenanceCosts, centralGroundsMaintenanceCosts, parameters.Dimension);
        response.IndirectEmployeeExpenses = CalculateTotal(schoolIndirectEmployeeExpenses, centralIndirectEmployeeExpenses, parameters.Dimension);
        response.InterestChargesLoanBank = CalculateTotal(schoolInterestChargesLoanBank, centralInterestChargesLoanBank, parameters.Dimension);
        response.OtherInsurancePremiumsCosts = CalculateTotal(schoolOtherInsurancePremiumsCosts, centralOtherInsurancePremiumsCosts, parameters.Dimension);
        response.PrivateFinanceInitiativeCharges = CalculateTotal(schoolPrivateFinanceInitiativeCharges, centralPrivateFinanceInitiativeCharges, parameters.Dimension);
        response.RentRatesCosts = CalculateTotal(schoolRentRatesCosts, centralRentRatesCosts, parameters.Dimension);
        response.SpecialFacilitiesCosts = CalculateTotal(schoolSpecialFacilitiesCosts, centralSpecialFacilitiesCosts, parameters.Dimension);
        response.StaffDevelopmentTrainingCosts = CalculateTotal(schoolStaffDevelopmentTrainingCosts, centralStaffDevelopmentTrainingCosts, parameters.Dimension);
        response.StaffRelatedInsuranceCosts = CalculateTotal(schoolStaffRelatedInsuranceCosts, centralStaffRelatedInsuranceCosts, parameters.Dimension);
        response.SupplyTeacherInsurableCosts = CalculateTotal(schoolSupplyTeacherInsurableCosts, centralSupplyTeacherInsurableCosts, parameters.Dimension);
        response.CommunityFocusedSchoolStaff = CalculateTotal(schoolCommunityFocusedSchoolStaff, centralCommunityFocusedSchoolStaff, parameters.Dimension);
        response.CommunityFocusedSchoolCosts = CalculateTotal(schoolCommunityFocusedSchoolCosts, centralCommunityFocusedSchoolCosts, parameters.Dimension);
    }


    private static decimal? CalculateTotal(decimal? school, decimal? central, string dimension)
    {
        return dimension switch
        {
            ExpenditureDimensions.Actuals => school.GetValueOrDefault() + central.GetValueOrDefault(),
            ExpenditureDimensions.PerUnit => school.GetValueOrDefault() + central.GetValueOrDefault(),
            ExpenditureDimensions.PercentIncome => (school.GetValueOrDefault() + central.GetValueOrDefault()) / 2,
            ExpenditureDimensions.PercentExpenditure => (school.GetValueOrDefault() + central.GetValueOrDefault()) / 2,
            _ => null
        };
    }


    private static decimal? CalcPupilSchool(decimal? value, ExpenditureBaseModel model, string dimension)
    {
        return CalculateValue(value, model.TotalPupils, model.TotalIncome, model.TotalExpenditure, dimension);
    }

    private static decimal? CalcBuildingSchool(decimal? value, ExpenditureBaseModel model, string dimension)
    {
        return CalculateValue(value, model.TotalInternalFloorArea, model.TotalIncome, model.TotalExpenditure, dimension);
    }

    private static decimal? CalcPupilCentral(decimal? value, ExpenditureBaseModel model, string dimension)
    {
        return CalculateValue(value, model.TotalPupils, model.TotalIncomeCS, model.TotalExpenditureCS, dimension);
    }

    private static decimal? CalcBuildingCentral(decimal? value, ExpenditureBaseModel model, string dimension)
    {
        return CalculateValue(value, model.TotalInternalFloorArea, model.TotalIncomeCS, model.TotalExpenditureCS, dimension);
    }

    private static decimal? CalculateValue(decimal? value, decimal? totalUnit, decimal? totalIncome,
        decimal? totalExpenditure, string dimension)
    {
        if (value == null)
        {
            return value;
        }

        return dimension switch
        {
            ExpenditureDimensions.Actuals => value,
            ExpenditureDimensions.PerUnit => totalUnit != 0 ? value / totalUnit : 0,
            ExpenditureDimensions.PercentIncome => totalIncome != 0 ? value / totalIncome * 100 : 0,
            ExpenditureDimensions.PercentExpenditure => totalExpenditure != 0 ? value / totalExpenditure * 100 : 0,
            _ => null
        };
    }
}

public abstract record ExpenditureBaseResponse
{
    public decimal? SchoolTotalExpenditure { get; set; }
    public decimal? SchoolTotalTeachingSupportStaffCosts { get; set; }
    public decimal? SchoolTeachingStaffCosts { get; set; }
    public decimal? SchoolSupplyTeachingStaffCosts { get; set; }
    public decimal? SchoolEducationalConsultancyCosts { get; set; }
    public decimal? SchoolEducationSupportStaffCosts { get; set; }
    public decimal? SchoolAgencySupplyTeachingStaffCosts { get; set; }
    public decimal? SchoolTotalNonEducationalSupportStaffCosts { get; set; }
    public decimal? SchoolAdministrativeClericalStaffCosts { get; set; }
    public decimal? SchoolAuditorsCosts { get; set; }
    public decimal? SchoolOtherStaffCosts { get; set; }
    public decimal? SchoolProfessionalServicesNonCurriculumCosts { get; set; }
    public decimal? SchoolTotalEducationalSuppliesCosts { get; set; }
    public decimal? SchoolExaminationFeesCosts { get; set; }
    public decimal? SchoolLearningResourcesNonIctCosts { get; set; }
    public decimal? SchoolLearningResourcesIctCosts { get; set; }
    public decimal? SchoolTotalPremisesStaffServiceCosts { get; set; }
    public decimal? SchoolCleaningCaretakingCosts { get; set; }
    public decimal? SchoolMaintenancePremisesCosts { get; set; }
    public decimal? SchoolOtherOccupationCosts { get; set; }
    public decimal? SchoolPremisesStaffCosts { get; set; }
    public decimal? SchoolTotalUtilitiesCosts { get; set; }
    public decimal? SchoolEnergyCosts { get; set; }
    public decimal? SchoolWaterSewerageCosts { get; set; }
    public decimal? SchoolAdministrativeSuppliesCosts { get; set; }
    public decimal? SchoolTotalGrossCateringCosts { get; set; }
    public decimal? SchoolCateringStaffCosts { get; set; }
    public decimal? SchoolCateringSuppliesCosts { get; set; }
    public decimal? SchoolTotalOtherCosts { get; set; }
    public decimal? SchoolDirectRevenueFinancingCosts { get; set; }
    public decimal? SchoolGroundsMaintenanceCosts { get; set; }
    public decimal? SchoolIndirectEmployeeExpenses { get; set; }
    public decimal? SchoolInterestChargesLoanBank { get; set; }
    public decimal? SchoolOtherInsurancePremiumsCosts { get; set; }
    public decimal? SchoolPrivateFinanceInitiativeCharges { get; set; }
    public decimal? SchoolRentRatesCosts { get; set; }
    public decimal? SchoolSpecialFacilitiesCosts { get; set; }
    public decimal? SchoolStaffDevelopmentTrainingCosts { get; set; }
    public decimal? SchoolStaffRelatedInsuranceCosts { get; set; }
    public decimal? SchoolSupplyTeacherInsurableCosts { get; set; }
    public decimal? SchoolCommunityFocusedSchoolStaff { get; set; }
    public decimal? SchoolCommunityFocusedSchoolCosts { get; set; }

    public decimal? CentralTotalExpenditure { get; set; }
    public decimal? CentralTotalTeachingSupportStaffCosts { get; set; }
    public decimal? CentralTeachingStaffCosts { get; set; }
    public decimal? CentralSupplyTeachingStaffCosts { get; set; }
    public decimal? CentralEducationalConsultancyCosts { get; set; }
    public decimal? CentralEducationSupportStaffCosts { get; set; }
    public decimal? CentralAgencySupplyTeachingStaffCosts { get; set; }
    public decimal? CentralTotalNonEducationalSupportStaffCosts { get; set; }
    public decimal? CentralAdministrativeClericalStaffCosts { get; set; }
    public decimal? CentralAuditorsCosts { get; set; }
    public decimal? CentralOtherStaffCosts { get; set; }
    public decimal? CentralProfessionalServicesNonCurriculumCosts { get; set; }
    public decimal? CentralTotalEducationalSuppliesCosts { get; set; }
    public decimal? CentralExaminationFeesCosts { get; set; }
    public decimal? CentralLearningResourcesNonIctCosts { get; set; }
    public decimal? CentralLearningResourcesIctCosts { get; set; }
    public decimal? CentralTotalPremisesStaffServiceCosts { get; set; }
    public decimal? CentralCleaningCaretakingCosts { get; set; }
    public decimal? CentralMaintenancePremisesCosts { get; set; }
    public decimal? CentralOtherOccupationCosts { get; set; }
    public decimal? CentralPremisesStaffCosts { get; set; }
    public decimal? CentralTotalUtilitiesCosts { get; set; }
    public decimal? CentralEnergyCosts { get; set; }
    public decimal? CentralWaterSewerageCosts { get; set; }
    public decimal? CentralAdministrativeSuppliesCosts { get; set; }
    public decimal? CentralTotalGrossCateringCosts { get; set; }
    public decimal? CentralCateringStaffCosts { get; set; }
    public decimal? CentralCateringSuppliesCosts { get; set; }
    public decimal? CentralTotalOtherCosts { get; set; }
    public decimal? CentralDirectRevenueFinancingCosts { get; set; }
    public decimal? CentralGroundsMaintenanceCosts { get; set; }
    public decimal? CentralIndirectEmployeeExpenses { get; set; }
    public decimal? CentralInterestChargesLoanBank { get; set; }
    public decimal? CentralOtherInsurancePremiumsCosts { get; set; }
    public decimal? CentralPrivateFinanceInitiativeCharges { get; set; }
    public decimal? CentralRentRatesCosts { get; set; }
    public decimal? CentralSpecialFacilitiesCosts { get; set; }
    public decimal? CentralStaffDevelopmentTrainingCosts { get; set; }
    public decimal? CentralStaffRelatedInsuranceCosts { get; set; }
    public decimal? CentralSupplyTeacherInsurableCosts { get; set; }
    public decimal? CentralCommunityFocusedSchoolStaff { get; set; }
    public decimal? CentralCommunityFocusedSchoolCosts { get; set; }

    public decimal? TotalExpenditure { get; set; }
    public decimal? TotalTeachingSupportStaffCosts { get; set; }
    public decimal? TeachingStaffCosts { get; set; }
    public decimal? SupplyTeachingStaffCosts { get; set; }
    public decimal? EducationalConsultancyCosts { get; set; }
    public decimal? EducationSupportStaffCosts { get; set; }
    public decimal? AgencySupplyTeachingStaffCosts { get; set; }
    public decimal? TotalNonEducationalSupportStaffCosts { get; set; }
    public decimal? AdministrativeClericalStaffCosts { get; set; }
    public decimal? AuditorsCosts { get; set; }
    public decimal? OtherStaffCosts { get; set; }
    public decimal? ProfessionalServicesNonCurriculumCosts { get; set; }
    public decimal? TotalEducationalSuppliesCosts { get; set; }
    public decimal? ExaminationFeesCosts { get; set; }
    public decimal? LearningResourcesNonIctCosts { get; set; }
    public decimal? LearningResourcesIctCosts { get; set; }
    public decimal? TotalPremisesStaffServiceCosts { get; set; }
    public decimal? CleaningCaretakingCosts { get; set; }
    public decimal? MaintenancePremisesCosts { get; set; }
    public decimal? OtherOccupationCosts { get; set; }
    public decimal? PremisesStaffCosts { get; set; }
    public decimal? TotalUtilitiesCosts { get; set; }
    public decimal? EnergyCosts { get; set; }
    public decimal? WaterSewerageCosts { get; set; }
    public decimal? AdministrativeSuppliesCosts { get; set; }
    public decimal? TotalGrossCateringCosts { get; set; }
    public decimal? CateringStaffCosts { get; set; }
    public decimal? CateringSuppliesCosts { get; set; }
    public decimal? TotalOtherCosts { get; set; }
    public decimal? DirectRevenueFinancingCosts { get; set; }
    public decimal? GroundsMaintenanceCosts { get; set; }
    public decimal? IndirectEmployeeExpenses { get; set; }
    public decimal? InterestChargesLoanBank { get; set; }
    public decimal? OtherInsurancePremiumsCosts { get; set; }
    public decimal? PrivateFinanceInitiativeCharges { get; set; }
    public decimal? RentRatesCosts { get; set; }
    public decimal? SpecialFacilitiesCosts { get; set; }
    public decimal? StaffDevelopmentTrainingCosts { get; set; }
    public decimal? StaffRelatedInsuranceCosts { get; set; }
    public decimal? SupplyTeacherInsurableCosts { get; set; }
    public decimal? CommunityFocusedSchoolStaff { get; set; }
    public decimal? CommunityFocusedSchoolCosts { get; set; }
}

public record SchoolExpenditureResponse : ExpenditureBaseResponse
{
    public string? URN { get; set; }
    public string? SchoolName { get; set; }
    public string? SchoolType { get; set; }
    public string? LAName { get; set; }
    public decimal? TotalPupils { get; set; }
    public decimal? TotalInternalFloorArea { get; set; }
}

public record TrustExpenditureResponse : ExpenditureBaseResponse
{
    public string? CompanyNumber { get; set; }
    public string? TrustName { get; set; }
}

public record SchoolExpenditureHistoryResponse : ExpenditureBaseResponse
{
    public string? URN { get; set; }
    public int? Year { get; set; }
    public string? Term => Year != null ? $"{Year - 1} to {Year}" : null;
}

public record TrustExpenditureHistoryResponse : ExpenditureBaseResponse
{
    public string? CompanyNumber { get; set; }
    public int? Year { get; set; }
    public string? Term => Year != null ? $"{Year - 1} to {Year}" : null;
}