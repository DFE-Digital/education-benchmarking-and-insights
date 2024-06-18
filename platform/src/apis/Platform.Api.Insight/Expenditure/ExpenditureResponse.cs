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
        var response = new T
        {
            SchoolTotalExpenditure = CalcPupilSchool(model.TotalExpenditure - model.TotalExpenditureCS.GetValueOrDefault(), model, parameters),
            CentralTotalExpenditure = CalcPupilCentral(model.TotalExpenditureCS, model, parameters),
            TotalExpenditure = CalcPupilTotal(model.TotalExpenditure, model, parameters)
        };

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

    private static void SetTeachingTeachingSupportStaff<T>(ExpenditureBaseModel model, ExpenditureParameters parameters, T response) where T : ExpenditureBaseResponse, new()
    {
        response.SchoolTotalTeachingSupportStaffCosts = CalcPupilSchool(model.TotalTeachingSupportStaffCosts - model.TotalTeachingSupportStaffCostsCS.GetValueOrDefault(), model, parameters);
        response.SchoolTeachingStaffCosts = CalcPupilSchool(model.TeachingStaffCosts - model.TeachingStaffCostsCS.GetValueOrDefault(), model, parameters);
        response.SchoolSupplyTeachingStaffCosts = CalcPupilSchool(model.SupplyTeachingStaffCosts - model.SupplyTeachingStaffCostsCS.GetValueOrDefault(), model, parameters);
        response.SchoolEducationalConsultancyCosts = CalcPupilSchool(model.EducationalConsultancyCosts - model.EducationalConsultancyCostsCS.GetValueOrDefault(), model, parameters);
        response.SchoolEducationSupportStaffCosts = CalcPupilSchool(model.EducationSupportStaffCosts - model.EducationSupportStaffCostsCS.GetValueOrDefault(), model, parameters);
        response.SchoolAgencySupplyTeachingStaffCosts = CalcPupilSchool(model.AgencySupplyTeachingStaffCosts - model.AgencySupplyTeachingStaffCostsCS.GetValueOrDefault(), model, parameters);

        response.CentralTotalTeachingSupportStaffCosts = CalcPupilCentral(model.TotalTeachingSupportStaffCostsCS, model, parameters);
        response.CentralTeachingStaffCosts = CalcPupilCentral(model.TeachingStaffCostsCS, model, parameters);
        response.CentralSupplyTeachingStaffCosts = CalcPupilCentral(model.SupplyTeachingStaffCostsCS, model, parameters);
        response.CentralEducationalConsultancyCosts = CalcPupilCentral(model.EducationalConsultancyCostsCS, model, parameters);
        response.CentralEducationSupportStaffCosts = CalcPupilCentral(model.EducationSupportStaffCostsCS, model, parameters);
        response.CentralAgencySupplyTeachingStaffCosts = CalcPupilCentral(model.AgencySupplyTeachingStaffCostsCS, model, parameters);

        response.TotalTeachingSupportStaffCosts = CalcPupilTotal(model.TotalTeachingSupportStaffCosts, model, parameters);
        response.TeachingStaffCosts = CalcPupilTotal(model.TeachingStaffCosts, model, parameters);
        response.SupplyTeachingStaffCosts = CalcPupilTotal(model.SupplyTeachingStaffCosts, model, parameters);
        response.EducationalConsultancyCosts = CalcPupilTotal(model.EducationalConsultancyCosts, model, parameters);
        response.EducationSupportStaffCosts = CalcPupilTotal(model.EducationSupportStaffCosts, model, parameters);
        response.AgencySupplyTeachingStaffCosts = CalcPupilTotal(model.AgencySupplyTeachingStaffCosts, model, parameters);
    }

    private static void SetNonEducationalSupportStaff<T>(ExpenditureBaseModel model, ExpenditureParameters parameters, T response) where T : ExpenditureBaseResponse, new()
    {
        response.SchoolTotalNonEducationalSupportStaffCosts = CalcPupilSchool(model.TotalNonEducationalSupportStaffCosts - model.TotalNonEducationalSupportStaffCostsCS.GetValueOrDefault(), model, parameters);
        response.SchoolAdministrativeClericalStaffCosts = CalcPupilSchool(model.AdministrativeClericalStaffCosts - model.AdministrativeClericalStaffCostsCS.GetValueOrDefault(), model, parameters);
        response.SchoolAuditorsCosts = CalcPupilSchool(model.AuditorsCosts - model.AuditorsCostsCS.GetValueOrDefault(), model, parameters);
        response.SchoolOtherStaffCosts = CalcPupilSchool(model.OtherStaffCosts - model.OtherStaffCostsCS.GetValueOrDefault(), model, parameters);
        response.SchoolProfessionalServicesNonCurriculumCosts = CalcPupilSchool(model.ProfessionalServicesNonCurriculumCosts - model.ProfessionalServicesNonCurriculumCostsCS.GetValueOrDefault(), model, parameters);

        response.CentralTotalNonEducationalSupportStaffCosts = CalcPupilCentral(model.TotalNonEducationalSupportStaffCostsCS, model, parameters);
        response.CentralAdministrativeClericalStaffCosts = CalcPupilCentral(model.AdministrativeClericalStaffCostsCS, model, parameters);
        response.CentralAuditorsCosts = CalcPupilCentral(model.AuditorsCostsCS, model, parameters);
        response.CentralOtherStaffCosts = CalcPupilCentral(model.OtherStaffCostsCS, model, parameters);
        response.CentralProfessionalServicesNonCurriculumCosts = CalcPupilCentral(model.ProfessionalServicesNonCurriculumCostsCS, model, parameters);

        response.TotalNonEducationalSupportStaffCosts = CalcPupilTotal(model.TotalNonEducationalSupportStaffCosts, model, parameters);
        response.AdministrativeClericalStaffCosts = CalcPupilTotal(model.AdministrativeClericalStaffCosts, model, parameters);
        response.AuditorsCosts = CalcPupilTotal(model.AuditorsCosts, model, parameters);
        response.OtherStaffCosts = CalcPupilTotal(model.OtherStaffCosts, model, parameters);
        response.ProfessionalServicesNonCurriculumCosts = CalcPupilTotal(model.ProfessionalServicesNonCurriculumCosts, model, parameters);
    }

    private static void SetEducationalSupplies<T>(ExpenditureBaseModel model, ExpenditureParameters parameters, T response)
        where T : ExpenditureBaseResponse, new()
    {
        response.SchoolTotalEducationalSuppliesCosts = CalcPupilSchool(model.TotalEducationalSuppliesCosts - model.TotalEducationalSuppliesCostsCS.GetValueOrDefault(), model, parameters);
        response.SchoolExaminationFeesCosts = CalcPupilSchool(model.ExaminationFeesCosts - model.ExaminationFeesCostsCS.GetValueOrDefault(), model, parameters);
        response.SchoolLearningResourcesNonIctCosts = CalcPupilSchool(model.LearningResourcesNonIctCosts - model.LearningResourcesNonIctCostsCS.GetValueOrDefault(), model, parameters);

        response.CentralTotalEducationalSuppliesCosts = CalcPupilCentral(model.TotalEducationalSuppliesCostsCS, model, parameters);
        response.CentralExaminationFeesCosts = CalcPupilCentral(model.ExaminationFeesCostsCS, model, parameters);
        response.CentralLearningResourcesNonIctCosts = CalcPupilCentral(model.LearningResourcesNonIctCostsCS, model, parameters);

        response.TotalEducationalSuppliesCosts = CalcPupilTotal(model.TotalEducationalSuppliesCosts, model, parameters);
        response.ExaminationFeesCosts = CalcPupilTotal(model.ExaminationFeesCosts, model, parameters);
        response.LearningResourcesNonIctCosts = CalcPupilTotal(model.LearningResourcesNonIctCosts, model, parameters);
    }

    private static void SetEducationalIct<T>(ExpenditureBaseModel model, ExpenditureParameters parameters, T response) where T : ExpenditureBaseResponse, new()
    {
        response.SchoolLearningResourcesIctCosts = CalcPupilSchool(model.LearningResourcesIctCosts - model.LearningResourcesIctCostsCS.GetValueOrDefault(), model, parameters);
        response.CentralLearningResourcesIctCosts = CalcPupilCentral(model.LearningResourcesIctCostsCS, model, parameters);
        response.LearningResourcesIctCosts = CalcPupilTotal(model.LearningResourcesIctCosts, model, parameters);
    }

    private static void SetPremisesStaffServices<T>(ExpenditureBaseModel model, ExpenditureParameters parameters, T response) where T : ExpenditureBaseResponse, new()
    {
        response.SchoolTotalPremisesStaffServiceCosts = CalcBuildingSchool(model.TotalPremisesStaffServiceCosts - model.TotalPremisesStaffServiceCostsCS.GetValueOrDefault(), model, parameters);
        response.SchoolCleaningCaretakingCosts = CalcBuildingSchool(model.CleaningCaretakingCosts - model.CleaningCaretakingCostsCS.GetValueOrDefault(), model, parameters);
        response.SchoolMaintenancePremisesCosts = CalcBuildingSchool(model.MaintenancePremisesCosts - model.MaintenancePremisesCostsCS.GetValueOrDefault(), model, parameters);
        response.SchoolOtherOccupationCosts = CalcBuildingSchool(model.OtherOccupationCosts - model.OtherOccupationCostsCS.GetValueOrDefault(), model, parameters);
        response.SchoolPremisesStaffCosts = CalcBuildingSchool(model.PremisesStaffCosts - model.PremisesStaffCostsCS.GetValueOrDefault(), model, parameters);

        response.CentralTotalPremisesStaffServiceCosts = CalcBuildingCentral(model.TotalPremisesStaffServiceCostsCS, model, parameters);
        response.CentralCleaningCaretakingCosts = CalcBuildingCentral(model.CleaningCaretakingCostsCS, model, parameters);
        response.CentralMaintenancePremisesCosts = CalcBuildingCentral(model.MaintenancePremisesCostsCS, model, parameters);
        response.CentralOtherOccupationCosts = CalcBuildingCentral(model.OtherOccupationCostsCS, model, parameters);
        response.CentralPremisesStaffCosts = CalcBuildingCentral(model.PremisesStaffCostsCS, model, parameters);

        response.TotalPremisesStaffServiceCosts = CalcBuildingTotal(model.TotalPremisesStaffServiceCosts, model, parameters);
        response.CleaningCaretakingCosts = CalcBuildingTotal(model.CleaningCaretakingCosts, model, parameters);
        response.MaintenancePremisesCosts = CalcBuildingTotal(model.MaintenancePremisesCosts, model, parameters);
        response.OtherOccupationCosts = CalcBuildingTotal(model.OtherOccupationCosts, model, parameters);
        response.PremisesStaffCosts = CalcBuildingTotal(model.PremisesStaffCosts, model, parameters);
    }

    private static void SetUtilities<T>(ExpenditureBaseModel model, ExpenditureParameters parameters, T response) where T : ExpenditureBaseResponse, new()
    {
        response.SchoolTotalUtilitiesCosts = CalcBuildingSchool(model.TotalUtilitiesCosts - model.TotalUtilitiesCostsCS.GetValueOrDefault(), model, parameters);
        response.SchoolEnergyCosts = CalcBuildingSchool(model.EnergyCosts - model.EnergyCostsCS.GetValueOrDefault(), model, parameters);
        response.SchoolWaterSewerageCosts = CalcBuildingSchool(model.WaterSewerageCosts - model.WaterSewerageCostsCS.GetValueOrDefault(), model, parameters);

        response.CentralTotalUtilitiesCosts = CalcBuildingCentral(model.TotalUtilitiesCostsCS, model, parameters);
        response.CentralEnergyCosts = CalcBuildingCentral(model.EnergyCostsCS, model, parameters);
        response.CentralWaterSewerageCosts = CalcBuildingCentral(model.WaterSewerageCostsCS, model, parameters);

        response.TotalUtilitiesCosts = CalcBuildingTotal(model.TotalUtilitiesCosts, model, parameters);
        response.EnergyCosts = CalcBuildingTotal(model.EnergyCosts, model, parameters);
        response.WaterSewerageCosts = CalcBuildingTotal(model.WaterSewerageCosts, model, parameters);
    }

    private static void SetAdministrationSupplies<T>(ExpenditureBaseModel model, ExpenditureParameters parameters, T response) where T : ExpenditureBaseResponse, new()
    {
        response.SchoolAdministrativeSuppliesCosts = CalcPupilSchool(model.AdministrativeSuppliesNonEducationalCosts - model.AdministrativeSuppliesNonEducationalCostsCS.GetValueOrDefault(), model, parameters);
        response.CentralAdministrativeSuppliesCosts = CalcPupilCentral(model.AdministrativeSuppliesNonEducationalCostsCS, model, parameters);
        response.AdministrativeSuppliesCosts = CalcPupilTotal(model.AdministrativeSuppliesNonEducationalCosts, model, parameters);
    }

    private static void SetCateringStaffServices<T>(ExpenditureBaseModel model, ExpenditureParameters parameters, T response) where T : ExpenditureBaseResponse, new()
    {
        response.SchoolTotalGrossCateringCosts = CalcPupilSchool(model.TotalGrossCateringCosts - model.TotalGrossCateringCostsCS.GetValueOrDefault(), model, parameters);
        response.SchoolCateringStaffCosts = CalcPupilSchool(model.CateringStaffCosts - model.CateringStaffCostsCS.GetValueOrDefault(), model, parameters);
        response.SchoolCateringSuppliesCosts = CalcPupilSchool(model.CateringSuppliesCosts - model.CateringSuppliesCostsCS.GetValueOrDefault(), model, parameters);

        response.CentralTotalGrossCateringCosts = CalcPupilCentral(model.TotalGrossCateringCostsCS, model, parameters);
        response.CentralCateringStaffCosts = CalcPupilCentral(model.CateringStaffCostsCS, model, parameters);
        response.CentralCateringSuppliesCosts = CalcPupilCentral(model.CateringSuppliesCostsCS, model, parameters);

        response.TotalGrossCateringCosts = CalcPupilTotal(model.TotalGrossCateringCosts, model, parameters);
        response.CateringStaffCosts = CalcPupilTotal(model.CateringStaffCosts, model, parameters);
        response.CateringSuppliesCosts = CalcPupilTotal(model.CateringSuppliesCosts, model, parameters);
    }

    private static void SetOther<T>(ExpenditureBaseModel model, ExpenditureParameters parameters, T response) where T : ExpenditureBaseResponse, new()
    {
        response.SchoolTotalOtherCosts = CalcPupilSchool(model.TotalOtherCosts - model.TotalOtherCostsCS.GetValueOrDefault(), model, parameters);
        response.SchoolDirectRevenueFinancingCosts = CalcPupilSchool(model.DirectRevenueFinancingCosts - model.DirectRevenueFinancingCostsCS.GetValueOrDefault(), model, parameters);
        response.SchoolGroundsMaintenanceCosts = CalcPupilSchool(model.GroundsMaintenanceCosts - model.GroundsMaintenanceCostsCS.GetValueOrDefault(), model, parameters);
        response.SchoolIndirectEmployeeExpenses = CalcPupilSchool(model.IndirectEmployeeExpenses - model.IndirectEmployeeExpensesCS.GetValueOrDefault(), model, parameters);
        response.SchoolInterestChargesLoanBank = CalcPupilSchool(model.InterestChargesLoanBank - model.InterestChargesLoanBankCS.GetValueOrDefault(), model, parameters);
        response.SchoolOtherInsurancePremiumsCosts = CalcPupilSchool(model.OtherInsurancePremiumsCosts - model.OtherInsurancePremiumsCostsCS.GetValueOrDefault(), model, parameters);
        response.SchoolPrivateFinanceInitiativeCharges = CalcPupilSchool(model.PrivateFinanceInitiativeCharges - model.PrivateFinanceInitiativeChargesCS.GetValueOrDefault(), model, parameters);
        response.SchoolRentRatesCosts = CalcPupilSchool(model.RentRatesCosts - model.RentRatesCostsCS.GetValueOrDefault(), model, parameters);
        response.SchoolSpecialFacilitiesCosts = CalcPupilSchool(model.SpecialFacilitiesCosts - model.SpecialFacilitiesCostsCS.GetValueOrDefault(), model, parameters);
        response.SchoolStaffDevelopmentTrainingCosts = CalcPupilSchool(model.StaffDevelopmentTrainingCosts - model.StaffDevelopmentTrainingCostsCS.GetValueOrDefault(), model, parameters);
        response.SchoolStaffRelatedInsuranceCosts = CalcPupilSchool(model.StaffRelatedInsuranceCosts - model.StaffRelatedInsuranceCostsCS.GetValueOrDefault(), model, parameters);
        response.SchoolSupplyTeacherInsurableCosts = CalcPupilSchool(model.SupplyTeacherInsurableCosts - model.SupplyTeacherInsurableCostsCS.GetValueOrDefault(), model, parameters);
        response.SchoolCommunityFocusedSchoolStaff = CalcPupilSchool(model.CommunityFocusedSchoolStaff - model.CommunityFocusedSchoolStaffCS.GetValueOrDefault(), model, parameters);
        response.SchoolCommunityFocusedSchoolCosts = CalcPupilSchool(model.CommunityFocusedSchoolCosts - model.CommunityFocusedSchoolCostsCS.GetValueOrDefault(), model, parameters);

        response.CentralTotalOtherCosts = CalcPupilCentral(model.TotalOtherCostsCS, model, parameters);
        response.CentralDirectRevenueFinancingCosts = CalcPupilCentral(model.DirectRevenueFinancingCostsCS, model, parameters);
        response.CentralGroundsMaintenanceCosts = CalcPupilCentral(model.GroundsMaintenanceCostsCS, model, parameters);
        response.CentralIndirectEmployeeExpenses = CalcPupilCentral(model.IndirectEmployeeExpensesCS, model, parameters);
        response.CentralInterestChargesLoanBank = CalcPupilCentral(model.InterestChargesLoanBankCS, model, parameters);
        response.CentralOtherInsurancePremiumsCosts = CalcPupilCentral(model.OtherInsurancePremiumsCostsCS, model, parameters);
        response.CentralPrivateFinanceInitiativeCharges = CalcPupilCentral(model.PrivateFinanceInitiativeChargesCS, model, parameters);
        response.CentralRentRatesCosts = CalcPupilCentral(model.RentRatesCostsCS, model, parameters);
        response.CentralSpecialFacilitiesCosts = CalcPupilCentral(model.SpecialFacilitiesCostsCS, model, parameters);
        response.CentralStaffDevelopmentTrainingCosts = CalcPupilCentral(model.StaffDevelopmentTrainingCostsCS, model, parameters);
        response.CentralStaffRelatedInsuranceCosts = CalcPupilCentral(model.StaffRelatedInsuranceCostsCS, model, parameters);
        response.CentralSupplyTeacherInsurableCosts = CalcPupilCentral(model.SupplyTeacherInsurableCostsCS, model, parameters);
        response.CentralCommunityFocusedSchoolStaff = CalcPupilCentral(model.CommunityFocusedSchoolStaffCS, model, parameters);
        response.CentralCommunityFocusedSchoolCosts = CalcPupilCentral(model.CommunityFocusedSchoolCostsCS, model, parameters);

        response.TotalOtherCosts = CalcPupilTotal(model.TotalOtherCosts, model, parameters);
        response.DirectRevenueFinancingCosts = CalcPupilTotal(model.DirectRevenueFinancingCosts, model, parameters);
        response.GroundsMaintenanceCosts = CalcPupilTotal(model.GroundsMaintenanceCosts, model, parameters);
        response.IndirectEmployeeExpenses = CalcPupilTotal(model.IndirectEmployeeExpenses, model, parameters);
        response.InterestChargesLoanBank = CalcPupilTotal(model.InterestChargesLoanBank, model, parameters);
        response.OtherInsurancePremiumsCosts = CalcPupilTotal(model.OtherInsurancePremiumsCosts, model, parameters);
        response.PrivateFinanceInitiativeCharges = CalcPupilTotal(model.PrivateFinanceInitiativeCharges, model, parameters);
        response.RentRatesCosts = CalcPupilTotal(model.RentRatesCosts, model, parameters);
        response.SpecialFacilitiesCosts = CalcPupilTotal(model.SpecialFacilitiesCosts, model, parameters);
        response.StaffDevelopmentTrainingCosts = CalcPupilTotal(model.StaffDevelopmentTrainingCosts, model, parameters);
        response.StaffRelatedInsuranceCosts = CalcPupilTotal(model.StaffRelatedInsuranceCosts, model, parameters);
        response.SupplyTeacherInsurableCosts = CalcPupilTotal(model.SupplyTeacherInsurableCosts, model, parameters);
        response.CommunityFocusedSchoolStaff = CalcPupilTotal(model.CommunityFocusedSchoolStaff, model, parameters);
        response.CommunityFocusedSchoolCosts = CalcPupilTotal(model.CommunityFocusedSchoolCosts, model, parameters);
    }

    private static decimal? CalcPupilTotal(decimal? value, ExpenditureBaseModel model, ExpenditureParameters parameters)
    {
        return CalculateValue(value, model.TotalPupils, model.TotalIncome, model.TotalExpenditure, parameters.Dimension);
    }

    private static decimal? CalcBuildingTotal(decimal? value, ExpenditureBaseModel model, ExpenditureParameters parameters)
    {
        return CalculateValue(value, model.TotalInternalFloorArea, model.TotalIncome, model.TotalExpenditure, parameters.Dimension);
    }

    private static decimal? CalcPupilSchool(decimal? value, ExpenditureBaseModel model, ExpenditureParameters parameters)
    {
        var totalIncome = model.TotalIncome.GetValueOrDefault() - model.TotalIncomeCS.GetValueOrDefault();
        var totalExpenditure = model.TotalExpenditure.GetValueOrDefault() - model.TotalExpenditureCS.GetValueOrDefault();

        return parameters.IncludeBreakdown
            ? CalculateValue(value, model.TotalPupils, totalIncome, totalExpenditure, parameters.Dimension)
            : null;
    }

    private static decimal? CalcBuildingSchool(decimal? value, ExpenditureBaseModel model, ExpenditureParameters parameters)
    {
        var totalIncome = model.TotalIncome.GetValueOrDefault() - model.TotalIncomeCS.GetValueOrDefault();
        var totalExpenditure = model.TotalExpenditure.GetValueOrDefault() - model.TotalExpenditureCS.GetValueOrDefault();

        return parameters.IncludeBreakdown
            ? CalculateValue(value, model.TotalInternalFloorArea, totalIncome, totalExpenditure, parameters.Dimension)
            : null;
    }

    private static decimal? CalcPupilCentral(decimal? value, ExpenditureBaseModel model, ExpenditureParameters parameters)
    {
        return parameters.IncludeBreakdown
            ? CalculateValue(value, model.TotalPupils, model.TotalIncomeCS, model.TotalExpenditureCS, parameters.Dimension)
            : null;
    }

    private static decimal? CalcBuildingCentral(decimal? value, ExpenditureBaseModel model, ExpenditureParameters parameters)
    {
        return parameters.IncludeBreakdown
            ? CalculateValue(value, model.TotalInternalFloorArea, model.TotalIncomeCS, model.TotalExpenditureCS, parameters.Dimension)
            : null;
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