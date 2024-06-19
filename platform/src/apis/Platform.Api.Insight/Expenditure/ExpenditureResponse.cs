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
            SchoolTotalExpenditure = CalcPupilAmount(model.TotalExpenditure - model.TotalExpenditureCS.GetValueOrDefault(), model, parameters),
            CentralTotalExpenditure = CalcPupilAmount(model.TotalExpenditureCS, model, parameters),
            TotalExpenditure = CalcPupilTotal(model.TotalExpenditure, model.TotalExpenditureCS.GetValueOrDefault(), model, parameters)
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
        response.SchoolTotalTeachingSupportStaffCosts = CalcPupilAmount(model.TotalTeachingSupportStaffCosts - model.TotalTeachingSupportStaffCostsCS.GetValueOrDefault(), model, parameters);
        response.SchoolTeachingStaffCosts = CalcPupilAmount(model.TeachingStaffCosts - model.TeachingStaffCostsCS.GetValueOrDefault(), model, parameters);
        response.SchoolSupplyTeachingStaffCosts = CalcPupilAmount(model.SupplyTeachingStaffCosts - model.SupplyTeachingStaffCostsCS.GetValueOrDefault(), model, parameters);
        response.SchoolEducationalConsultancyCosts = CalcPupilAmount(model.EducationalConsultancyCosts - model.EducationalConsultancyCostsCS.GetValueOrDefault(), model, parameters);
        response.SchoolEducationSupportStaffCosts = CalcPupilAmount(model.EducationSupportStaffCosts - model.EducationSupportStaffCostsCS.GetValueOrDefault(), model, parameters);
        response.SchoolAgencySupplyTeachingStaffCosts = CalcPupilAmount(model.AgencySupplyTeachingStaffCosts - model.AgencySupplyTeachingStaffCostsCS.GetValueOrDefault(), model, parameters);

        response.CentralTotalTeachingSupportStaffCosts = CalcPupilAmount(model.TotalTeachingSupportStaffCostsCS, model, parameters);
        response.CentralTeachingStaffCosts = CalcPupilAmount(model.TeachingStaffCostsCS, model, parameters);
        response.CentralSupplyTeachingStaffCosts = CalcPupilAmount(model.SupplyTeachingStaffCostsCS, model, parameters);
        response.CentralEducationalConsultancyCosts = CalcPupilAmount(model.EducationalConsultancyCostsCS, model, parameters);
        response.CentralEducationSupportStaffCosts = CalcPupilAmount(model.EducationSupportStaffCostsCS, model, parameters);
        response.CentralAgencySupplyTeachingStaffCosts = CalcPupilAmount(model.AgencySupplyTeachingStaffCostsCS, model, parameters);

        response.TotalTeachingSupportStaffCosts = CalcPupilTotal(model.TotalTeachingSupportStaffCosts, model.TotalTeachingSupportStaffCostsCS.GetValueOrDefault(), model, parameters);
        response.TeachingStaffCosts = CalcPupilTotal(model.TeachingStaffCosts, model.TeachingStaffCostsCS.GetValueOrDefault(), model, parameters);
        response.SupplyTeachingStaffCosts = CalcPupilTotal(model.SupplyTeachingStaffCosts, model.EducationalConsultancyCostsCS.GetValueOrDefault(), model, parameters);
        response.EducationalConsultancyCosts = CalcPupilTotal(model.EducationalConsultancyCosts, model.EducationalConsultancyCostsCS.GetValueOrDefault(), model, parameters);
        response.EducationSupportStaffCosts = CalcPupilTotal(model.EducationSupportStaffCosts, model.EducationSupportStaffCostsCS.GetValueOrDefault(), model, parameters);
        response.AgencySupplyTeachingStaffCosts = CalcPupilTotal(model.AgencySupplyTeachingStaffCosts, model.AgencySupplyTeachingStaffCostsCS.GetValueOrDefault(), model, parameters);
    }

    private static void SetNonEducationalSupportStaff<T>(ExpenditureBaseModel model, ExpenditureParameters parameters, T response) where T : ExpenditureBaseResponse, new()
    {
        response.SchoolTotalNonEducationalSupportStaffCosts = CalcPupilAmount(model.TotalNonEducationalSupportStaffCosts - model.TotalNonEducationalSupportStaffCostsCS.GetValueOrDefault(), model, parameters);
        response.SchoolAdministrativeClericalStaffCosts = CalcPupilAmount(model.AdministrativeClericalStaffCosts - model.AdministrativeClericalStaffCostsCS.GetValueOrDefault(), model, parameters);
        response.SchoolAuditorsCosts = CalcPupilAmount(model.AuditorsCosts - model.AuditorsCostsCS.GetValueOrDefault(), model, parameters);
        response.SchoolOtherStaffCosts = CalcPupilAmount(model.OtherStaffCosts - model.OtherStaffCostsCS.GetValueOrDefault(), model, parameters);
        response.SchoolProfessionalServicesNonCurriculumCosts = CalcPupilAmount(model.ProfessionalServicesNonCurriculumCosts - model.ProfessionalServicesNonCurriculumCostsCS.GetValueOrDefault(), model, parameters);

        response.CentralTotalNonEducationalSupportStaffCosts = CalcPupilAmount(model.TotalNonEducationalSupportStaffCostsCS, model, parameters);
        response.CentralAdministrativeClericalStaffCosts = CalcPupilAmount(model.AdministrativeClericalStaffCostsCS, model, parameters);
        response.CentralAuditorsCosts = CalcPupilAmount(model.AuditorsCostsCS, model, parameters);
        response.CentralOtherStaffCosts = CalcPupilAmount(model.OtherStaffCostsCS, model, parameters);
        response.CentralProfessionalServicesNonCurriculumCosts = CalcPupilAmount(model.ProfessionalServicesNonCurriculumCostsCS, model, parameters);

        response.TotalNonEducationalSupportStaffCosts = CalcPupilTotal(model.TotalNonEducationalSupportStaffCosts, model.TotalNonEducationalSupportStaffCostsCS.GetValueOrDefault(), model, parameters);
        response.AdministrativeClericalStaffCosts = CalcPupilTotal(model.AdministrativeClericalStaffCosts, model.AdministrativeClericalStaffCostsCS.GetValueOrDefault(), model, parameters);
        response.AuditorsCosts = CalcPupilTotal(model.AuditorsCosts, model.AuditorsCostsCS.GetValueOrDefault(), model, parameters);
        response.OtherStaffCosts = CalcPupilTotal(model.OtherStaffCosts, model.OtherStaffCostsCS.GetValueOrDefault(), model, parameters);
        response.ProfessionalServicesNonCurriculumCosts = CalcPupilTotal(model.ProfessionalServicesNonCurriculumCosts, model.ProfessionalServicesNonCurriculumCostsCS.GetValueOrDefault(), model, parameters);
    }

    private static void SetEducationalSupplies<T>(ExpenditureBaseModel model, ExpenditureParameters parameters, T response)
        where T : ExpenditureBaseResponse, new()
    {
        response.SchoolTotalEducationalSuppliesCosts = CalcPupilAmount(model.TotalEducationalSuppliesCosts - model.TotalEducationalSuppliesCostsCS.GetValueOrDefault(), model, parameters);
        response.SchoolExaminationFeesCosts = CalcPupilAmount(model.ExaminationFeesCosts - model.ExaminationFeesCostsCS.GetValueOrDefault(), model, parameters);
        response.SchoolLearningResourcesNonIctCosts = CalcPupilAmount(model.LearningResourcesNonIctCosts - model.LearningResourcesNonIctCostsCS.GetValueOrDefault(), model, parameters);

        response.CentralTotalEducationalSuppliesCosts = CalcPupilAmount(model.TotalEducationalSuppliesCostsCS, model, parameters);
        response.CentralExaminationFeesCosts = CalcPupilAmount(model.ExaminationFeesCostsCS, model, parameters);
        response.CentralLearningResourcesNonIctCosts = CalcPupilAmount(model.LearningResourcesNonIctCostsCS, model, parameters);

        response.TotalEducationalSuppliesCosts = CalcPupilTotal(model.TotalEducationalSuppliesCosts, model.TotalEducationalSuppliesCostsCS.GetValueOrDefault(), model, parameters);
        response.ExaminationFeesCosts = CalcPupilTotal(model.ExaminationFeesCosts, model.ExaminationFeesCostsCS.GetValueOrDefault(), model, parameters);
        response.LearningResourcesNonIctCosts = CalcPupilTotal(model.LearningResourcesNonIctCosts, model.LearningResourcesNonIctCostsCS.GetValueOrDefault(), model, parameters);
    }

    private static void SetEducationalIct<T>(ExpenditureBaseModel model, ExpenditureParameters parameters, T response) where T : ExpenditureBaseResponse, new()
    {
        response.SchoolLearningResourcesIctCosts = CalcPupilAmount(model.LearningResourcesIctCosts - model.LearningResourcesIctCostsCS.GetValueOrDefault(), model, parameters);
        response.CentralLearningResourcesIctCosts = CalcPupilAmount(model.LearningResourcesIctCostsCS, model, parameters);
        response.LearningResourcesIctCosts = CalcPupilTotal(model.LearningResourcesIctCosts, model.LearningResourcesIctCostsCS.GetValueOrDefault(), model, parameters);
    }

    private static void SetPremisesStaffServices<T>(ExpenditureBaseModel model, ExpenditureParameters parameters, T response) where T : ExpenditureBaseResponse, new()
    {
        response.SchoolTotalPremisesStaffServiceCosts = CalcBuildingAmount(model.TotalPremisesStaffServiceCosts - model.TotalPremisesStaffServiceCostsCS.GetValueOrDefault(), model, parameters);
        response.SchoolCleaningCaretakingCosts = CalcBuildingAmount(model.CleaningCaretakingCosts - model.CleaningCaretakingCostsCS.GetValueOrDefault(), model, parameters);
        response.SchoolMaintenancePremisesCosts = CalcBuildingAmount(model.MaintenancePremisesCosts - model.MaintenancePremisesCostsCS.GetValueOrDefault(), model, parameters);
        response.SchoolOtherOccupationCosts = CalcBuildingAmount(model.OtherOccupationCosts - model.OtherOccupationCostsCS.GetValueOrDefault(), model, parameters);
        response.SchoolPremisesStaffCosts = CalcBuildingAmount(model.PremisesStaffCosts - model.PremisesStaffCostsCS.GetValueOrDefault(), model, parameters);

        response.CentralTotalPremisesStaffServiceCosts = CalcBuildingAmount(model.TotalPremisesStaffServiceCostsCS, model, parameters);
        response.CentralCleaningCaretakingCosts = CalcBuildingAmount(model.CleaningCaretakingCostsCS, model, parameters);
        response.CentralMaintenancePremisesCosts = CalcBuildingAmount(model.MaintenancePremisesCostsCS, model, parameters);
        response.CentralOtherOccupationCosts = CalcBuildingAmount(model.OtherOccupationCostsCS, model, parameters);
        response.CentralPremisesStaffCosts = CalcBuildingAmount(model.PremisesStaffCostsCS, model, parameters);

        response.TotalPremisesStaffServiceCosts = CalcBuildingTotal(model.TotalPremisesStaffServiceCosts, model.TotalPremisesStaffServiceCostsCS.GetValueOrDefault(), model, parameters);
        response.CleaningCaretakingCosts = CalcBuildingTotal(model.CleaningCaretakingCosts, model.CleaningCaretakingCostsCS.GetValueOrDefault(), model, parameters);
        response.MaintenancePremisesCosts = CalcBuildingTotal(model.MaintenancePremisesCosts, model.MaintenancePremisesCostsCS.GetValueOrDefault(), model, parameters);
        response.OtherOccupationCosts = CalcBuildingTotal(model.OtherOccupationCosts, model.OtherOccupationCostsCS.GetValueOrDefault(), model, parameters);
        response.PremisesStaffCosts = CalcBuildingTotal(model.PremisesStaffCosts, model.PremisesStaffCostsCS.GetValueOrDefault(), model, parameters);
    }

    private static void SetUtilities<T>(ExpenditureBaseModel model, ExpenditureParameters parameters, T response) where T : ExpenditureBaseResponse, new()
    {
        response.SchoolTotalUtilitiesCosts = CalcBuildingAmount(model.TotalUtilitiesCosts - model.TotalUtilitiesCostsCS.GetValueOrDefault(), model, parameters);
        response.SchoolEnergyCosts = CalcBuildingAmount(model.EnergyCosts - model.EnergyCostsCS.GetValueOrDefault(), model, parameters);
        response.SchoolWaterSewerageCosts = CalcBuildingAmount(model.WaterSewerageCosts - model.WaterSewerageCostsCS.GetValueOrDefault(), model, parameters);

        response.CentralTotalUtilitiesCosts = CalcBuildingAmount(model.TotalUtilitiesCostsCS, model, parameters);
        response.CentralEnergyCosts = CalcBuildingAmount(model.EnergyCostsCS, model, parameters);
        response.CentralWaterSewerageCosts = CalcBuildingAmount(model.WaterSewerageCostsCS, model, parameters);

        response.TotalUtilitiesCosts = CalcBuildingTotal(model.TotalUtilitiesCosts, model.TotalUtilitiesCostsCS.GetValueOrDefault(), model, parameters);
        response.EnergyCosts = CalcBuildingTotal(model.EnergyCosts, model.EnergyCostsCS.GetValueOrDefault(), model, parameters);
        response.WaterSewerageCosts = CalcBuildingTotal(model.WaterSewerageCosts, model.WaterSewerageCostsCS.GetValueOrDefault(), model, parameters);
    }

    private static void SetAdministrationSupplies<T>(ExpenditureBaseModel model, ExpenditureParameters parameters, T response) where T : ExpenditureBaseResponse, new()
    {
        response.SchoolAdministrativeSuppliesCosts = CalcPupilAmount(model.AdministrativeSuppliesNonEducationalCosts - model.AdministrativeSuppliesNonEducationalCostsCS.GetValueOrDefault(), model, parameters);
        response.CentralAdministrativeSuppliesCosts = CalcPupilAmount(model.AdministrativeSuppliesNonEducationalCostsCS, model, parameters);
        response.AdministrativeSuppliesCosts = CalcPupilTotal(model.AdministrativeSuppliesNonEducationalCosts, model.AdministrativeSuppliesNonEducationalCostsCS.GetValueOrDefault(), model, parameters);
    }

    private static void SetCateringStaffServices<T>(ExpenditureBaseModel model, ExpenditureParameters parameters, T response) where T : ExpenditureBaseResponse, new()
    {
        response.SchoolTotalGrossCateringCosts = CalcPupilAmount(model.TotalGrossCateringCosts - model.TotalGrossCateringCostsCS.GetValueOrDefault(), model, parameters);
        response.SchoolCateringStaffCosts = CalcPupilAmount(model.CateringStaffCosts - model.CateringStaffCostsCS.GetValueOrDefault(), model, parameters);
        response.SchoolCateringSuppliesCosts = CalcPupilAmount(model.CateringSuppliesCosts - model.CateringSuppliesCostsCS.GetValueOrDefault(), model, parameters);

        response.CentralTotalGrossCateringCosts = CalcPupilAmount(model.TotalGrossCateringCostsCS, model, parameters);
        response.CentralCateringStaffCosts = CalcPupilAmount(model.CateringStaffCostsCS, model, parameters);
        response.CentralCateringSuppliesCosts = CalcPupilAmount(model.CateringSuppliesCostsCS, model, parameters);

        response.TotalGrossCateringCosts = CalcPupilTotal(model.TotalGrossCateringCosts, model.TotalGrossCateringCostsCS.GetValueOrDefault(), model, parameters);
        response.CateringStaffCosts = CalcPupilTotal(model.CateringStaffCosts, model.CateringStaffCostsCS.GetValueOrDefault(), model, parameters);
        response.CateringSuppliesCosts = CalcPupilTotal(model.CateringSuppliesCosts, model.CateringSuppliesCostsCS.GetValueOrDefault(), model, parameters);
    }

    private static void SetOther<T>(ExpenditureBaseModel model, ExpenditureParameters parameters, T response) where T : ExpenditureBaseResponse, new()
    {
        response.SchoolTotalOtherCosts = CalcPupilAmount(model.TotalOtherCosts - model.TotalOtherCostsCS.GetValueOrDefault(), model, parameters);
        response.SchoolDirectRevenueFinancingCosts = CalcPupilAmount(model.DirectRevenueFinancingCosts - model.DirectRevenueFinancingCostsCS.GetValueOrDefault(), model, parameters);
        response.SchoolGroundsMaintenanceCosts = CalcPupilAmount(model.GroundsMaintenanceCosts - model.GroundsMaintenanceCostsCS.GetValueOrDefault(), model, parameters);
        response.SchoolIndirectEmployeeExpenses = CalcPupilAmount(model.IndirectEmployeeExpenses - model.IndirectEmployeeExpensesCS.GetValueOrDefault(), model, parameters);
        response.SchoolInterestChargesLoanBank = CalcPupilAmount(model.InterestChargesLoanBank - model.InterestChargesLoanBankCS.GetValueOrDefault(), model, parameters);
        response.SchoolOtherInsurancePremiumsCosts = CalcPupilAmount(model.OtherInsurancePremiumsCosts - model.OtherInsurancePremiumsCostsCS.GetValueOrDefault(), model, parameters);
        response.SchoolPrivateFinanceInitiativeCharges = CalcPupilAmount(model.PrivateFinanceInitiativeCharges - model.PrivateFinanceInitiativeChargesCS.GetValueOrDefault(), model, parameters);
        response.SchoolRentRatesCosts = CalcPupilAmount(model.RentRatesCosts - model.RentRatesCostsCS.GetValueOrDefault(), model, parameters);
        response.SchoolSpecialFacilitiesCosts = CalcPupilAmount(model.SpecialFacilitiesCosts - model.SpecialFacilitiesCostsCS.GetValueOrDefault(), model, parameters);
        response.SchoolStaffDevelopmentTrainingCosts = CalcPupilAmount(model.StaffDevelopmentTrainingCosts - model.StaffDevelopmentTrainingCostsCS.GetValueOrDefault(), model, parameters);
        response.SchoolStaffRelatedInsuranceCosts = CalcPupilAmount(model.StaffRelatedInsuranceCosts - model.StaffRelatedInsuranceCostsCS.GetValueOrDefault(), model, parameters);
        response.SchoolSupplyTeacherInsurableCosts = CalcPupilAmount(model.SupplyTeacherInsurableCosts - model.SupplyTeacherInsurableCostsCS.GetValueOrDefault(), model, parameters);
        response.SchoolCommunityFocusedSchoolStaff = CalcPupilAmount(model.CommunityFocusedSchoolStaff - model.CommunityFocusedSchoolStaffCS.GetValueOrDefault(), model, parameters);
        response.SchoolCommunityFocusedSchoolCosts = CalcPupilAmount(model.CommunityFocusedSchoolCosts - model.CommunityFocusedSchoolCostsCS.GetValueOrDefault(), model, parameters);

        response.CentralTotalOtherCosts = CalcPupilAmount(model.TotalOtherCostsCS, model, parameters);
        response.CentralDirectRevenueFinancingCosts = CalcPupilAmount(model.DirectRevenueFinancingCostsCS, model, parameters);
        response.CentralGroundsMaintenanceCosts = CalcPupilAmount(model.GroundsMaintenanceCostsCS, model, parameters);
        response.CentralIndirectEmployeeExpenses = CalcPupilAmount(model.IndirectEmployeeExpensesCS, model, parameters);
        response.CentralInterestChargesLoanBank = CalcPupilAmount(model.InterestChargesLoanBankCS, model, parameters);
        response.CentralOtherInsurancePremiumsCosts = CalcPupilAmount(model.OtherInsurancePremiumsCostsCS, model, parameters);
        response.CentralPrivateFinanceInitiativeCharges = CalcPupilAmount(model.PrivateFinanceInitiativeChargesCS, model, parameters);
        response.CentralRentRatesCosts = CalcPupilAmount(model.RentRatesCostsCS, model, parameters);
        response.CentralSpecialFacilitiesCosts = CalcPupilAmount(model.SpecialFacilitiesCostsCS, model, parameters);
        response.CentralStaffDevelopmentTrainingCosts = CalcPupilAmount(model.StaffDevelopmentTrainingCostsCS, model, parameters);
        response.CentralStaffRelatedInsuranceCosts = CalcPupilAmount(model.StaffRelatedInsuranceCostsCS, model, parameters);
        response.CentralSupplyTeacherInsurableCosts = CalcPupilAmount(model.SupplyTeacherInsurableCostsCS, model, parameters);
        response.CentralCommunityFocusedSchoolStaff = CalcPupilAmount(model.CommunityFocusedSchoolStaffCS, model, parameters);
        response.CentralCommunityFocusedSchoolCosts = CalcPupilAmount(model.CommunityFocusedSchoolCostsCS, model, parameters);

        response.TotalOtherCosts = CalcPupilTotal(model.TotalOtherCosts, model.TotalOtherCostsCS.GetValueOrDefault(), model, parameters);
        response.DirectRevenueFinancingCosts = CalcPupilTotal(model.DirectRevenueFinancingCosts, model.DirectRevenueFinancingCostsCS.GetValueOrDefault(), model, parameters);
        response.GroundsMaintenanceCosts = CalcPupilTotal(model.GroundsMaintenanceCosts, model.GroundsMaintenanceCostsCS.GetValueOrDefault(), model, parameters);
        response.IndirectEmployeeExpenses = CalcPupilTotal(model.IndirectEmployeeExpenses, model.IndirectEmployeeExpensesCS.GetValueOrDefault(), model, parameters);
        response.InterestChargesLoanBank = CalcPupilTotal(model.InterestChargesLoanBank, model.InterestChargesLoanBankCS.GetValueOrDefault(), model, parameters);
        response.OtherInsurancePremiumsCosts = CalcPupilTotal(model.OtherInsurancePremiumsCosts, model.OtherInsurancePremiumsCostsCS.GetValueOrDefault(), model, parameters);
        response.PrivateFinanceInitiativeCharges = CalcPupilTotal(model.PrivateFinanceInitiativeCharges, model.PrivateFinanceInitiativeChargesCS.GetValueOrDefault(), model, parameters);
        response.RentRatesCosts = CalcPupilTotal(model.RentRatesCosts, model.RentRatesCostsCS.GetValueOrDefault(), model, parameters);
        response.SpecialFacilitiesCosts = CalcPupilTotal(model.SpecialFacilitiesCosts, model.SpecialFacilitiesCostsCS.GetValueOrDefault(), model, parameters);
        response.StaffDevelopmentTrainingCosts = CalcPupilTotal(model.StaffDevelopmentTrainingCosts, model.StaffDevelopmentTrainingCostsCS.GetValueOrDefault(), model, parameters);
        response.StaffRelatedInsuranceCosts = CalcPupilTotal(model.StaffRelatedInsuranceCosts, model.StaffRelatedInsuranceCostsCS.GetValueOrDefault(), model, parameters);
        response.SupplyTeacherInsurableCosts = CalcPupilTotal(model.SupplyTeacherInsurableCosts, model.SupplyTeacherInsurableCostsCS.GetValueOrDefault(), model, parameters);
        response.CommunityFocusedSchoolStaff = CalcPupilTotal(model.CommunityFocusedSchoolStaff, model.CommunityFocusedSchoolStaffCS.GetValueOrDefault(), model, parameters);
        response.CommunityFocusedSchoolCosts = CalcPupilTotal(model.CommunityFocusedSchoolCosts, model.CommunityFocusedSchoolCostsCS.GetValueOrDefault(), model, parameters);
    }

    private static decimal? CalcPupilTotal(decimal? value, decimal valueCentral, ExpenditureBaseModel model, ExpenditureParameters parameters)
    {
        var totalIncome = model.TotalIncome;
        var totalExpenditure = model.TotalExpenditure;
        if (parameters.ExcludeCentralServices)
        {
            value -= valueCentral;
            totalIncome = model.TotalIncome.GetValueOrDefault() - model.TotalIncomeCS.GetValueOrDefault();
            totalExpenditure = model.TotalExpenditure.GetValueOrDefault() - model.TotalExpenditureCS.GetValueOrDefault();
        }

        return CalculateValue(value, model.TotalPupils, totalIncome, totalExpenditure, parameters.Dimension);
    }

    private static decimal? CalcBuildingTotal(decimal? value, decimal valueCentral, ExpenditureBaseModel model, ExpenditureParameters parameters)
    {
        var totalIncome = model.TotalIncome;
        var totalExpenditure = model.TotalExpenditure;
        if (parameters.ExcludeCentralServices)
        {
            value -= valueCentral;
            totalIncome = model.TotalIncome.GetValueOrDefault() - model.TotalIncomeCS.GetValueOrDefault();
            totalExpenditure = model.TotalExpenditure.GetValueOrDefault() - model.TotalExpenditureCS.GetValueOrDefault();
        }

        return CalculateValue(value, model.TotalInternalFloorArea, totalIncome, totalExpenditure, parameters.Dimension);
    }

    private static decimal? CalcPupilAmount(decimal? value, ExpenditureBaseModel model, ExpenditureParameters parameters)
    {
        return parameters.ExcludeCentralServices
            ? null
            : CalculateValue(value, model.TotalPupils, model.TotalIncome, model.TotalExpenditure, parameters.Dimension);
    }

    private static decimal? CalcBuildingAmount(decimal? value, ExpenditureBaseModel model, ExpenditureParameters parameters)
    {
        return parameters.ExcludeCentralServices
            ? null
            : CalculateValue(value, model.TotalInternalFloorArea, model.TotalIncome, model.TotalExpenditure, parameters.Dimension);
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