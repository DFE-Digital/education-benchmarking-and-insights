namespace Platform.Api.Insight.Expenditure;


public abstract record ExpenditureBaseModel
{
    public decimal? TotalPupils { get; set; }
    public decimal? TotalInternalFloorArea { get; set; }
    public decimal? TotalIncome { get; set; }
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
    public decimal? AdministrativeSuppliesNonEducationalCosts { get; set; }
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

    public decimal? TotalIncomeCS { get; set; }
    public decimal? TotalExpenditureCS { get; set; }

    public decimal? TotalTeachingSupportStaffCostsCS { get; set; }
    public decimal? TeachingStaffCostsCS { get; set; }
    public decimal? SupplyTeachingStaffCostsCS { get; set; }
    public decimal? EducationalConsultancyCostsCS { get; set; }
    public decimal? EducationSupportStaffCostsCS { get; set; }
    public decimal? AgencySupplyTeachingStaffCostsCS { get; set; }
    public decimal? TotalNonEducationalSupportStaffCostsCS { get; set; }
    public decimal? AdministrativeClericalStaffCostsCS { get; set; }
    public decimal? AuditorsCostsCS { get; set; }
    public decimal? OtherStaffCostsCS { get; set; }
    public decimal? ProfessionalServicesNonCurriculumCostsCS { get; set; }
    public decimal? TotalEducationalSuppliesCostsCS { get; set; }
    public decimal? ExaminationFeesCostsCS { get; set; }
    public decimal? LearningResourcesNonIctCostsCS { get; set; }
    public decimal? LearningResourcesIctCostsCS { get; set; }
    public decimal? TotalPremisesStaffServiceCostsCS { get; set; }
    public decimal? CleaningCaretakingCostsCS { get; set; }
    public decimal? MaintenancePremisesCostsCS { get; set; }
    public decimal? OtherOccupationCostsCS { get; set; }
    public decimal? PremisesStaffCostsCS { get; set; }
    public decimal? TotalUtilitiesCostsCS { get; set; }
    public decimal? EnergyCostsCS { get; set; }
    public decimal? WaterSewerageCostsCS { get; set; }
    public decimal? AdministrativeSuppliesNonEducationalCostsCS { get; set; }
    public decimal? TotalGrossCateringCostsCS { get; set; }
    public decimal? CateringStaffCostsCS { get; set; }
    public decimal? CateringSuppliesCostsCS { get; set; }
    public decimal? TotalOtherCostsCS { get; set; }
    public decimal? DirectRevenueFinancingCostsCS { get; set; }
    public decimal? GroundsMaintenanceCostsCS { get; set; }
    public decimal? IndirectEmployeeExpensesCS { get; set; }
    public decimal? InterestChargesLoanBankCS { get; set; }
    public decimal? OtherInsurancePremiumsCostsCS { get; set; }
    public decimal? PrivateFinanceInitiativeChargesCS { get; set; }
    public decimal? RentRatesCostsCS { get; set; }
    public decimal? SpecialFacilitiesCostsCS { get; set; }
    public decimal? StaffDevelopmentTrainingCostsCS { get; set; }
    public decimal? StaffRelatedInsuranceCostsCS { get; set; }
    public decimal? SupplyTeacherInsurableCostsCS { get; set; }
    public decimal? CommunityFocusedSchoolStaffCS { get; set; }
    public decimal? CommunityFocusedSchoolCostsCS { get; set; }
}

public record SchoolExpenditureModel : ExpenditureBaseModel
{
    public string? URN { get; set; }
    public string? SchoolName { get; set; }
    public string? SchoolType { get; set; }
    public string? LAName { get; set; }
}

public record SchoolExpenditureHistoryModel : ExpenditureBaseModel
{
    public string? URN { get; set; }
    public int? Year { get; set; }
}

public record TrustExpenditureModel : ExpenditureBaseModel
{
    public string? CompanyNumber { get; set; }
    public string? TrustName { get; set; }
}

public record TrustExpenditureHistoryModel : ExpenditureBaseModel
{
    public string? CompanyNumber { get; set; }
    public int? Year { get; set; }
}