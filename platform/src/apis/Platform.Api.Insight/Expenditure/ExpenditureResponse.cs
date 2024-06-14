namespace Platform.Api.Insight.Expenditure;

public static class ExpenditureResponseFactory
{
    public static SchoolExpenditureResponse Create(SchoolExpenditureModel model, string dimension,
        string? category = null)
    {
        var response = CreateResponse<SchoolExpenditureResponse>(model, dimension, category);

        response.URN = model.URN;
        response.SchoolName = model.SchoolName;
        response.SchoolType = model.SchoolType;
        response.LAName = model.LAName;
        response.TotalPupils = model.TotalPupils;
        response.TotalInternalFloorArea = model.TotalInternalFloorArea;

        return response;
    }

    public static TrustExpenditureResponse Create(TrustExpenditureModel model, string dimension,
        string? category = null)
    {
        var response = CreateResponse<TrustExpenditureResponse>(model, dimension, category);

        response.CompanyNumber = model.CompanyNumber;
        response.TrustName = model.TrustName;

        return response;
    }

    public static SchoolExpenditureHistoryResponse Create(SchoolExpenditureHistoryModel model, string dimension,
        string? category = null)
    {
        var response = CreateResponse<SchoolExpenditureHistoryResponse>(model, dimension, category);

        response.URN = model.URN;
        response.Year = model.Year;

        return response;
    }

    public static TrustExpenditureHistoryResponse Create(TrustExpenditureHistoryModel model, string dimension,
        string? category = null)
    {
        var response = CreateResponse<TrustExpenditureHistoryResponse>(model, dimension, category);

        response.CompanyNumber = model.CompanyNumber;
        response.Year = model.Year;

        return response;
    }

    private static T CreateResponse<T>(ExpenditureBaseModel model, string dimension, string? category)
        where T : ExpenditureBaseResponse, new()
    {
        var response = new T();

        response.SchoolTotalExpenditure = CalculateValue(model.TotalExpenditure,
            model.TotalPupils, model.TotalIncome, model.TotalExpenditure, dimension);

        response.CentralTotalExpenditure = CalculateValue(model.TotalExpenditureCS,
            model.TotalPupils, model.TotalIncomeCS, model.TotalExpenditureCS, dimension);

        response.TotalExpenditure =
            CalculateTotal(response.SchoolTotalExpenditure, response.CentralTotalExpenditure);

        if (category is null or ExpenditureCategories.TeachingTeachingSupportStaff)
        {
            SetTeachingTeachingSupportStaff(model, dimension, response);
        }

        if (category is null or ExpenditureCategories.NonEducationalSupportStaff)
        {
            SetNonEducationalSupportStaff(model, dimension, response);
        }

        if (category is null or ExpenditureCategories.EducationalSupplies)
        {
            SetEducationalSupplies(model, dimension, response);
        }

        if (category is null or ExpenditureCategories.EducationalIct)
        {
            SetEducationalIct(model, dimension, response);
        }

        if (category is null or ExpenditureCategories.PremisesStaffServices)
        {
            SetPremisesStaffServices(model, dimension, response);
        }

        if (category is null or ExpenditureCategories.Utilities)
        {
            SetUtilities(model, dimension, response);
        }

        if (category is null or ExpenditureCategories.AdministrationSupplies)
        {
            SetAdministrationSupplies(model, dimension, response);
        }

        if (category is null or ExpenditureCategories.CateringStaffServices)
        {
            SetCateringStaffServices(model, dimension, response);
        }

        if (category is null or ExpenditureCategories.Other)
        {
            //SetOther(model, dimension, response);
        }

        return response;
    }

    private static void SetTeachingTeachingSupportStaff<T>(ExpenditureBaseModel model, string dimension, T response)
        where T : ExpenditureBaseResponse, new()
    {
        response.SchoolTotalTeachingSupportStaffCosts = CalculateValue(model.TotalTeachingSupportStaffCosts,
            model.TotalPupils, model.TotalIncome, model.TotalExpenditure, dimension);

        response.SchoolTeachingStaffCosts = CalculateValue(model.TeachingStaffCosts, model.TotalPupils,
            model.TotalIncome, model.TotalExpenditure, dimension);

        response.SchoolSupplyTeachingStaffCosts = CalculateValue(model.SupplyTeachingStaffCosts, model.TotalPupils,
            model.TotalIncome, model.TotalExpenditure, dimension);

        response.SchoolEducationalConsultancyCosts = CalculateValue(model.EducationalConsultancyCosts,
            model.TotalPupils, model.TotalIncome, model.TotalExpenditure, dimension);

        response.SchoolEducationSupportStaffCosts = CalculateValue(model.EducationSupportStaffCosts,
            model.TotalPupils, model.TotalIncome, model.TotalExpenditure, dimension);

        response.SchoolAgencySupplyTeachingStaffCosts = CalculateValue(model.AgencySupplyTeachingStaffCosts,
            model.TotalPupils, model.TotalIncome, model.TotalExpenditure, dimension);


        response.CentralTotalTeachingSupportStaffCosts = CalculateValue(model.TotalTeachingSupportStaffCostsCS,
            model.TotalPupils, model.TotalIncomeCS, model.TotalExpenditureCS, dimension);

        response.CentralTeachingStaffCosts = CalculateValue(model.TeachingStaffCostsCS, model.TotalPupils,
            model.TotalIncomeCS, model.TotalExpenditureCS, dimension);

        response.CentralSupplyTeachingStaffCosts = CalculateValue(model.SupplyTeachingStaffCostsCS, model.TotalPupils,
            model.TotalIncomeCS, model.TotalExpenditureCS, dimension);

        response.CentralEducationalConsultancyCosts = CalculateValue(model.EducationalConsultancyCostsCS,
            model.TotalPupils, model.TotalIncomeCS, model.TotalExpenditureCS, dimension);

        response.CentralEducationSupportStaffCosts = CalculateValue(model.EducationSupportStaffCostsCS,
            model.TotalPupils, model.TotalIncomeCS, model.TotalExpenditureCS, dimension);

        response.CentralAgencySupplyTeachingStaffCosts = CalculateValue(model.AgencySupplyTeachingStaffCostsCS,
            model.TotalPupils, model.TotalIncomeCS, model.TotalExpenditureCS, dimension);


        response.TotalTeachingSupportStaffCosts = CalculateTotal(response.SchoolTotalTeachingSupportStaffCosts,
            response.CentralTotalTeachingSupportStaffCosts);
        response.TeachingStaffCosts =
            CalculateTotal(response.SchoolTeachingStaffCosts, response.CentralTeachingStaffCosts);
        response.SupplyTeachingStaffCosts = CalculateTotal(response.SchoolSupplyTeachingStaffCosts,
            response.CentralSupplyTeachingStaffCosts);
        response.EducationalConsultancyCosts = CalculateTotal(response.SchoolEducationalConsultancyCosts,
            response.CentralEducationalConsultancyCosts);
        response.EducationSupportStaffCosts = CalculateTotal(response.SchoolEducationSupportStaffCosts,
            response.CentralEducationSupportStaffCosts);
        response.AgencySupplyTeachingStaffCosts = CalculateTotal(response.SchoolAgencySupplyTeachingStaffCosts,
            response.CentralAgencySupplyTeachingStaffCosts);
    }

    private static void SetNonEducationalSupportStaff<T>(ExpenditureBaseModel model, string dimension, T response)
        where T : ExpenditureBaseResponse, new()
    {
        response.SchoolTotalNonEducationalSupportStaffCosts = CalculateValue(model.TotalNonEducationalSupportStaffCosts,
            model.TotalPupils, model.TotalIncome, model.TotalExpenditure, dimension);

        response.SchoolAdministrativeClericalStaffCosts = CalculateValue(model.AdministrativeClericalStaffCosts,
            model.TotalPupils,
            model.TotalIncome, model.TotalExpenditure, dimension);

        response.SchoolAuditorsCosts = CalculateValue(model.AuditorsCosts, model.TotalPupils,
            model.TotalIncome, model.TotalExpenditure, dimension);

        response.SchoolOtherStaffCosts = CalculateValue(model.OtherStaffCosts,
            model.TotalPupils, model.TotalIncome, model.TotalExpenditure, dimension);

        response.SchoolProfessionalServicesNonCurriculumCosts = CalculateValue(
            model.ProfessionalServicesNonCurriculumCosts,
            model.TotalPupils, model.TotalIncome, model.TotalExpenditure, dimension);


        response.CentralTotalNonEducationalSupportStaffCosts = CalculateValue(
            model.TotalNonEducationalSupportStaffCostsCS,
            model.TotalPupils, model.TotalIncomeCS, model.TotalExpenditureCS, dimension);

        response.CentralAdministrativeClericalStaffCosts = CalculateValue(model.AdministrativeClericalStaffCostsCS,
            model.TotalPupils,
            model.TotalIncomeCS, model.TotalExpenditureCS, dimension);

        response.CentralAuditorsCosts = CalculateValue(model.AuditorsCostsCS, model.TotalPupils,
            model.TotalIncomeCS, model.TotalExpenditureCS, dimension);

        response.CentralOtherStaffCosts = CalculateValue(model.OtherStaffCostsCS,
            model.TotalPupils, model.TotalIncomeCS, model.TotalExpenditureCS, dimension);

        response.CentralProfessionalServicesNonCurriculumCosts = CalculateValue(
            model.ProfessionalServicesNonCurriculumCostsCS,
            model.TotalPupils, model.TotalIncomeCS, model.TotalExpenditureCS, dimension);


        response.TotalNonEducationalSupportStaffCosts = CalculateTotal(
            response.SchoolTotalNonEducationalSupportStaffCosts, response.CentralTotalNonEducationalSupportStaffCosts);
        response.AdministrativeClericalStaffCosts = CalculateTotal(response.SchoolAdministrativeClericalStaffCosts,
            response.CentralAdministrativeClericalStaffCosts);
        response.AuditorsCosts = CalculateTotal(response.SchoolAuditorsCosts, response.CentralAuditorsCosts);
        response.OtherStaffCosts = CalculateTotal(response.SchoolOtherStaffCosts, response.CentralOtherStaffCosts);
        response.ProfessionalServicesNonCurriculumCosts = CalculateTotal(
            response.SchoolProfessionalServicesNonCurriculumCosts,
            response.CentralProfessionalServicesNonCurriculumCosts);
    }

    private static void SetEducationalSupplies<T>(ExpenditureBaseModel model, string dimension, T response)
        where T : ExpenditureBaseResponse, new()
    {
        response.SchoolTotalEducationalSuppliesCosts = CalculateValue(model.TotalEducationalSuppliesCosts,
            model.TotalPupils, model.TotalIncome, model.TotalExpenditure, dimension);

        response.SchoolExaminationFeesCosts = CalculateValue(model.ExaminationFeesCosts, model.TotalPupils,
            model.TotalIncome, model.TotalExpenditure, dimension);

        response.SchoolLearningResourcesNonIctCosts = CalculateValue(model.LearningResourcesNonIctCosts,
            model.TotalPupils,
            model.TotalIncome, model.TotalExpenditure, dimension);


        response.CentralTotalEducationalSuppliesCosts = CalculateValue(model.TotalEducationalSuppliesCostsCS,
            model.TotalPupils, model.TotalIncomeCS, model.TotalExpenditureCS, dimension);

        response.CentralExaminationFeesCosts = CalculateValue(model.ExaminationFeesCostsCS, model.TotalPupils,
            model.TotalIncomeCS, model.TotalExpenditureCS, dimension);

        response.CentralLearningResourcesNonIctCosts = CalculateValue(model.LearningResourcesNonIctCostsCS,
            model.TotalPupils,
            model.TotalIncomeCS, model.TotalExpenditureCS, dimension);


        response.TotalEducationalSuppliesCosts = CalculateTotal(response.SchoolTotalEducationalSuppliesCosts,
            response.CentralTotalEducationalSuppliesCosts);
        response.ExaminationFeesCosts =
            CalculateTotal(response.SchoolExaminationFeesCosts, response.CentralExaminationFeesCosts);
        response.LearningResourcesNonIctCosts = CalculateTotal(response.SchoolLearningResourcesNonIctCosts,
            response.CentralLearningResourcesNonIctCosts);
    }

    private static void SetEducationalIct<T>(ExpenditureBaseModel model, string dimension, T response)
        where T : ExpenditureBaseResponse, new()
    {
        response.SchoolLearningResourcesIctCosts = CalculateValue(model.LearningResourcesIctCosts,
            model.TotalPupils, model.TotalIncome, model.TotalExpenditure, dimension);


        response.CentralLearningResourcesIctCosts = CalculateValue(model.LearningResourcesIctCostsCS,
            model.TotalPupils, model.TotalIncomeCS, model.TotalExpenditureCS, dimension);


        response.LearningResourcesIctCosts = CalculateTotal(response.SchoolLearningResourcesIctCosts,
            response.CentralLearningResourcesIctCosts);
    }

    private static void SetPremisesStaffServices<T>(ExpenditureBaseModel model, string dimension, T response)
        where T : ExpenditureBaseResponse, new()
    {
        response.SchoolTotalPremisesStaffServiceCosts = CalculateValue(model.TotalPremisesStaffServiceCosts,
            model.TotalInternalFloorArea, model.TotalIncome, model.TotalExpenditure, dimension);

        response.SchoolCleaningCaretakingCosts = CalculateValue(model.CleaningCaretakingCosts,
            model.TotalInternalFloorArea,
            model.TotalIncome, model.TotalExpenditure, dimension);

        response.SchoolMaintenancePremisesCosts = CalculateValue(model.MaintenancePremisesCosts,
            model.TotalInternalFloorArea,
            model.TotalIncome, model.TotalExpenditure, dimension);

        response.SchoolOtherOccupationCosts = CalculateValue(model.OtherOccupationCosts,
            model.TotalInternalFloorArea, model.TotalIncome, model.TotalExpenditure, dimension);

        response.SchoolPremisesStaffCosts = CalculateValue(model.PremisesStaffCosts,
            model.TotalInternalFloorArea, model.TotalIncome, model.TotalExpenditure, dimension);


        response.CentralTotalPremisesStaffServiceCosts = CalculateValue(model.TotalPremisesStaffServiceCostsCS,
            model.TotalInternalFloorArea, model.TotalIncomeCS, model.TotalExpenditureCS, dimension);

        response.CentralCleaningCaretakingCosts = CalculateValue(model.CleaningCaretakingCostsCS,
            model.TotalInternalFloorArea,
            model.TotalIncomeCS, model.TotalExpenditureCS, dimension);

        response.CentralMaintenancePremisesCosts = CalculateValue(model.MaintenancePremisesCostsCS,
            model.TotalInternalFloorArea,
            model.TotalIncomeCS, model.TotalExpenditureCS, dimension);

        response.CentralOtherOccupationCosts = CalculateValue(model.OtherOccupationCostsCS,
            model.TotalInternalFloorArea, model.TotalIncomeCS, model.TotalExpenditureCS, dimension);

        response.CentralPremisesStaffCosts = CalculateValue(model.PremisesStaffCostsCS,
            model.TotalInternalFloorArea, model.TotalIncomeCS, model.TotalExpenditureCS, dimension);


        response.TotalPremisesStaffServiceCosts = CalculateTotal(response.SchoolTotalPremisesStaffServiceCosts,
            response.CentralTotalPremisesStaffServiceCosts);
        response.CleaningCaretakingCosts = CalculateTotal(response.SchoolCleaningCaretakingCosts,
            response.CentralCleaningCaretakingCosts);
        response.MaintenancePremisesCosts = CalculateTotal(response.SchoolMaintenancePremisesCosts,
            response.CentralMaintenancePremisesCosts);
        response.OtherOccupationCosts =
            CalculateTotal(response.SchoolOtherOccupationCosts, response.CentralOtherOccupationCosts);
        response.PremisesStaffCosts =
            CalculateTotal(response.SchoolPremisesStaffCosts, response.CentralPremisesStaffCosts);
    }

    private static void SetUtilities<T>(ExpenditureBaseModel model, string dimension, T response)
        where T : ExpenditureBaseResponse, new()
    {
        response.SchoolTotalUtilitiesCosts = CalculateValue(model.TotalUtilitiesCosts,
            model.TotalInternalFloorArea, model.TotalIncome, model.TotalExpenditure, dimension);

        response.SchoolEnergyCosts = CalculateValue(model.EnergyCosts,
            model.TotalInternalFloorArea,
            model.TotalIncome, model.TotalExpenditure, dimension);

        response.SchoolWaterSewerageCosts = CalculateValue(model.WaterSewerageCosts,
            model.TotalInternalFloorArea,
            model.TotalIncome, model.TotalExpenditure, dimension);


        response.CentralTotalUtilitiesCosts = CalculateValue(model.TotalUtilitiesCostsCS,
            model.TotalInternalFloorArea, model.TotalIncomeCS, model.TotalExpenditureCS, dimension);

        response.CentralEnergyCosts = CalculateValue(model.EnergyCostsCS,
            model.TotalInternalFloorArea,
            model.TotalIncomeCS, model.TotalExpenditureCS, dimension);

        response.CentralWaterSewerageCosts = CalculateValue(model.WaterSewerageCostsCS,
            model.TotalInternalFloorArea,
            model.TotalIncomeCS, model.TotalExpenditureCS, dimension);


        response.TotalUtilitiesCosts = CalculateTotal(response.SchoolTotalUtilitiesCosts,
            response.CentralTotalUtilitiesCosts);
        response.EnergyCosts = CalculateTotal(response.SchoolEnergyCosts,
            response.CentralEnergyCosts);
        response.WaterSewerageCosts = CalculateTotal(response.SchoolWaterSewerageCosts,
            response.CentralWaterSewerageCosts);
    }

    private static void SetAdministrationSupplies<T>(ExpenditureBaseModel model, string dimension, T response)
        where T : ExpenditureBaseResponse, new()
    {
        response.SchoolAdministrativeSuppliesCosts = CalculateValue(model.AdministrativeSuppliesNonEducationalCosts,
            model.TotalPupils, model.TotalIncome, model.TotalExpenditure, dimension);


        response.CentralAdministrativeSuppliesCosts = CalculateValue(model.AdministrativeSuppliesNonEducationalCostsCS,
            model.TotalPupils, model.TotalIncomeCS, model.TotalExpenditureCS, dimension);


        response.AdministrativeSuppliesCosts = CalculateTotal(response.SchoolAdministrativeSuppliesCosts,
            response.CentralAdministrativeSuppliesCosts);
    }

    private static void SetCateringStaffServices<T>(ExpenditureBaseModel model, string dimension, T response)
        where T : ExpenditureBaseResponse, new()
    {
        response.SchoolTotalGrossCateringCosts = CalculateValue(model.TotalGrossCateringCosts,
            model.TotalPupils, model.TotalIncome, model.TotalExpenditure, dimension);

        response.SchoolCateringStaffCosts = CalculateValue(model.CateringStaffCosts, model.TotalPupils,
            model.TotalIncome, model.TotalExpenditure, dimension);

        response.SchoolCateringSuppliesCosts = CalculateValue(model.CateringSuppliesCosts,
            model.TotalPupils,
            model.TotalIncome, model.TotalExpenditure, dimension);


        response.CentralTotalGrossCateringCosts = CalculateValue(model.TotalGrossCateringCostsCS,
            model.TotalPupils, model.TotalIncomeCS, model.TotalExpenditureCS, dimension);

        response.CentralCateringStaffCosts = CalculateValue(model.CateringStaffCostsCS, model.TotalPupils,
            model.TotalIncomeCS, model.TotalExpenditureCS, dimension);

        response.CentralCateringSuppliesCosts = CalculateValue(model.CateringSuppliesCostsCS,
            model.TotalPupils,
            model.TotalIncomeCS, model.TotalExpenditureCS, dimension);


        response.TotalGrossCateringCosts = CalculateTotal(response.SchoolTotalGrossCateringCosts,
            response.CentralTotalGrossCateringCosts);
        response.CateringStaffCosts =
            CalculateTotal(response.SchoolCateringStaffCosts, response.CentralCateringStaffCosts);
        response.CateringSuppliesCosts = CalculateTotal(response.SchoolCateringSuppliesCosts,
            response.CentralCateringSuppliesCosts);
    }

    private static decimal? CalculateTotal(decimal? school, decimal? central)
    {
        return school.GetValueOrDefault() + central.GetValueOrDefault();
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