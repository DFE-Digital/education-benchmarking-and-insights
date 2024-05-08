using Web.App.Domain;

namespace Web.App.ViewModels;

public class SchoolCustomDataChangeViewModel(
    School school,
    Finances finances,
    Income income,
    SchoolExpenditure expenditure,
    Census census,
    FloorAreaMetric floorArea)
{
    public string? Name => school.Name;
    public string? Urn => school.Urn;

    // Administrative supplies
    public decimal AdministrativeSuppliesCosts => expenditure.AdministrativeSuppliesCosts;

    // Catering
    public decimal CateringStaffCosts => expenditure.CateringStaffCosts;
    public decimal CateringSupplies => expenditure.CateringSuppliesCosts;
    public decimal CateringIncome => income.IncomeCatering.GetValueOrDefault();

    // Educational supplies
    public decimal ExaminationFeesCosts => expenditure.ExaminationFeesCosts;
    public decimal LearningResourcesNonIctCosts => expenditure.LearningResourcesNonIctCosts;

    // IT
    public decimal LearningResourcesIctCosts => expenditure.LearningResourcesIctCosts;

    // Non-educational support staff
    public decimal AdministrativeClericalStaffCosts => expenditure.AdministrativeClericalStaffCosts;
    public decimal AuditorsCosts => expenditure.AuditorsCosts;
    public decimal OtherStaffCosts => expenditure.OtherStaffCosts;
    public decimal ProfessionalServicesNonCurriculumCosts => expenditure.ProfessionalServicesNonCurriculumCosts;
    
    // Premises and services
    public decimal CleaningCaretakingCosts => expenditure.CleaningCaretakingCosts;
    public decimal MaintenancePremisesCosts => expenditure.MaintenancePremisesCosts;
    public decimal OtherOccupationCosts => expenditure.OtherOccupationCosts;
    public decimal PremisesStaffCosts => expenditure.PremisesStaffCosts;

    // Teaching and teaching support
    public decimal AgencySupplyTeachingStaffCosts => expenditure.AgencySupplyTeachingStaffCosts;
    public decimal EducationSupportStaffCosts => expenditure.EducationSupportStaffCosts;
    public decimal EducationalConsultancyCosts => expenditure.EducationalConsultancyCosts;
    public decimal SupplyTeachingStaffCosts => expenditure.SupplyTeachingStaffCosts;
    public decimal TeachingStaffCosts => expenditure.TeachingStaffCosts;
    
    // Utilities
    public decimal EnergyCosts => expenditure.EnergyCosts;
    public decimal WaterSewerageCosts => expenditure.WaterSewerageCosts;
    
    // Other costs
    public decimal DirectRevenueFinancingCosts => expenditure.DirectRevenueFinancingCosts;
    public decimal GroundsMaintenanceCosts => expenditure.GroundsMaintenanceCosts;
    public decimal IndirectEmployeeExpenses => expenditure.IndirectEmployeeExpenses;
    public decimal InterestChargesLoanBank => expenditure.InterestChargesLoanBank;
    public decimal OtherInsurancePremiumsCosts => expenditure.OtherInsurancePremiumsCosts;
    public decimal PrivateFinanceInitiativeCharges => expenditure.PrivateFinanceInitiativeCharges;
    public decimal RentRatesCosts => expenditure.RentRatesCosts;
    public decimal SpecialFacilitiesCosts => expenditure.SpecialFacilitiesCosts;
    public decimal StaffDevelopmentTrainingCosts => expenditure.StaffDevelopmentTrainingCosts;
    public decimal StaffRelatedInsuranceCosts => expenditure.StaffRelatedInsuranceCosts;
    public decimal SupplyTeacherInsurableCosts => expenditure.SupplyTeacherInsurableCosts;
    
    // Totals
    public decimal TotalIncome => finances.TotalIncome;
    public decimal TotalExpenditure => finances.TotalExpenditure;
    public decimal RevenueReserve => finances.RevenueReserve;
    
    // Non-financial data
    public decimal TotalNumberOfTeachersFte => finances.TotalNumberOfTeachersFte;
    public decimal FreeSchoolMealPercent => finances.FreeSchoolMealPercent;
    public decimal SpecialEducationalNeedsPercent => finances.SpecialEducationalNeedsPercent;
    public int FloorArea => floorArea.FloorArea.GetValueOrDefault();
    // TODO: Predicted percentage pupil change 3 to 5 years

    // Workforce data
    public decimal WorkforceFte => census.WorkforceFte.GetValueOrDefault();
    public decimal TeachersFte => census.TeachersFte.GetValueOrDefault();
    public decimal SeniorLeadershipFte => census.SeniorLeadershipFte.GetValueOrDefault();
    // TODO: Teacher contact ratio (less than 1)
    // TODO: Average class size
}