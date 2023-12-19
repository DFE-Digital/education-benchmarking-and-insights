namespace EducationBenchmarking.Platform.Shared;

public class SchoolExpenditure : School
{
    public string? LocalAuthority { get; set; }
    public decimal? TotalExpenditurePerPupil { get; set; }
    
    public decimal? TotalTeachingSupportStaffCostsPerPupil { get; set; }
    public decimal? TeachingStaffCostsPerPupil { get; set; }
    public decimal? SupplyTeachingStaffCostsPerPupil { get; set; }
    public decimal? EducationalConsultancyCostsPerPupil { get; set; }
    public decimal? EducationSupportStaffCostsPerPupil { get; set; }
    public decimal? AgencySupplyTeachingStaffCostsPerPupil { get; set; }

    public decimal? TotalOtherCostsPerPupil { get; set; }
    public decimal? OtherInsurancePremiumsCostsPerPupil { get; set; }
    public decimal? DirectRevenueFinancingCostsPerPupil { get; set; }
    public decimal? GroundsMaintenanceCostsPerPupil { get; set; }
    public decimal? IndirectEmployeeExpensesPerPupil { get; set; }
    public decimal? InterestChargesLoanBankPerPupil { get; set; }
    public decimal? PrivateFinanceInitiativeChargesPerPupil { get; set; }
    public decimal? RentRatesCostsPerPupil { get; set; }
    public decimal? SpecialFacilitiesCostsPerPupil { get; set; }
    public decimal? StaffDevelopmentTrainingCostsPerPupil { get; set; }
    public decimal? StaffRelatedInsuranceCostsPerPupil { get; set; }
    public decimal? SupplyTeacherInsurableCostsPerPupil { get; set; }
    public decimal? CommunityFocusedSchoolStaffPerPupil { get; set; }
    public decimal? CommunityFocusedSchoolCostsPerPupil { get; set; }
    
    public decimal? AdministrativeClericalStaffCostsPerPupil { get; set; }
    public decimal? AuditorsCostsPerPupil { get; set; }
    public decimal? OtherStaffCostsPerPupil { get; set; }
    public decimal? ProfessionalServicesNonCurriculumCostsPerPupil { get; set; }
    
    public decimal? ExaminationFeesCostsPerPupil { get; set; }
    public decimal? BreakdownEducationalSuppliesCostsPerPupil { get; set; }
    public decimal? LearningResourcesNonIctCostsPerPupil { get; set; }
    
    public decimal? LearningResourcesIctCostsPerPupil { get; set; }
    
    public decimal? CleaningCaretakingCostsPerPupil { get; set; }
    public decimal? MaintenancePremisesCostsPerPupil { get; set; }
    public decimal? OtherOccupationCostsPerPupil { get; set; }
    public decimal? PremisesStaffCostsPerPupil { get; set; }
    
    public decimal? AdministrativeSuppliesCostsPerPupil { get; set; }
    
    public decimal? NetCateringCostsPerPupil { get; set; }
    public decimal? CateringStaffCostsPerPupil { get; set; }
    public decimal? CateringSuppliesCostsPerPupil { get; set; }
    public decimal? IncomeCateringPerPupil { get; set; }
    
    public decimal? TotalUtilitiesCostPerMetreSq { get; set; }
    public decimal? EnergyCostPerMetreSq { get; set; }
    public decimal? WaterSewerageCostsPerMetreSq { get; set; }
}