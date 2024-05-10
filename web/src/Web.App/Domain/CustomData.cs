using System.Diagnostics.CodeAnalysis;

namespace Web.App.Domain;


[ExcludeFromCodeCoverage]
public record CustomData
{
    public CustomData()
    {
    }

    public CustomData(
        Finances finances,
        Income income,
        SchoolExpenditure expenditure,
        Census census,
        FloorAreaMetric floorArea)
    {
        // Administrative supplies
        AdministrativeSuppliesCosts = expenditure.AdministrativeSuppliesCosts;

        // Catering
        CateringStaffCosts = expenditure.CateringStaffCosts;
        CateringSupplies = expenditure.CateringSuppliesCosts;
        CateringIncome = income.IncomeCatering;

        // Educational supplies
        ExaminationFeesCosts = expenditure.ExaminationFeesCosts;
        LearningResourcesNonIctCosts = expenditure.LearningResourcesNonIctCosts;

        // IT
        LearningResourcesIctCosts = expenditure.LearningResourcesIctCosts;

        // Non-educational support staff
        AdministrativeClericalStaffCosts = expenditure.AdministrativeClericalStaffCosts;
        AuditorsCosts = expenditure.AuditorsCosts;
        OtherStaffCosts = expenditure.OtherStaffCosts;
        ProfessionalServicesNonCurriculumCosts = expenditure.ProfessionalServicesNonCurriculumCosts;

        // Premises and services
        CleaningCaretakingCosts = expenditure.CleaningCaretakingCosts;
        MaintenancePremisesCosts = expenditure.MaintenancePremisesCosts;
        OtherOccupationCosts = expenditure.OtherOccupationCosts;
        PremisesStaffCosts = expenditure.PremisesStaffCosts;

        // Teaching and teaching support
        AgencySupplyTeachingStaffCosts = expenditure.AgencySupplyTeachingStaffCosts;
        EducationSupportStaffCosts = expenditure.EducationSupportStaffCosts;
        EducationalConsultancyCosts = expenditure.EducationalConsultancyCosts;
        SupplyTeachingStaffCosts = expenditure.SupplyTeachingStaffCosts;
        TeachingStaffCosts = expenditure.TeachingStaffCosts;

        // Utilities
        EnergyCosts = expenditure.EnergyCosts;
        WaterSewerageCosts = expenditure.WaterSewerageCosts;

        // Other costs
        DirectRevenueFinancingCosts = expenditure.DirectRevenueFinancingCosts;
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

        // Totals
        TotalIncome = finances.TotalIncome;
        TotalExpenditure = finances.TotalExpenditure;
        RevenueReserve = finances.RevenueReserve;

        // Non-financial data
        TotalNumberOfTeachersFte = finances.TotalNumberOfTeachersFte;
        FreeSchoolMealPercent = finances.FreeSchoolMealPercent;
        SpecialEducationalNeedsPercent = finances.SpecialEducationalNeedsPercent;
        FloorArea = floorArea.FloorArea;

        // Workforce data
        WorkforceFte = census.WorkforceFte;
        TeachersFte = census.TeachersFte;
        SeniorLeadershipFte = census.SeniorLeadershipFte;
    }

    // Administrative supplies
    public decimal? AdministrativeSuppliesCosts { get; init; }

    // Catering
    public decimal? CateringStaffCosts { get; init; }
    public decimal? CateringSupplies { get; init; }
    public decimal? CateringIncome { get; init; }

    // Educational supplies
    public decimal? ExaminationFeesCosts { get; init; }
    public decimal? LearningResourcesNonIctCosts { get; init; }

    // IT
    public decimal? LearningResourcesIctCosts { get; init; }

    // Non-educational support staff
    public decimal? AdministrativeClericalStaffCosts { get; init; }
    public decimal? AuditorsCosts { get; init; }
    public decimal? OtherStaffCosts { get; init; }
    public decimal? ProfessionalServicesNonCurriculumCosts { get; init; }

    // Premises and services
    public decimal? CleaningCaretakingCosts { get; init; }
    public decimal? MaintenancePremisesCosts { get; init; }
    public decimal? OtherOccupationCosts { get; init; }
    public decimal? PremisesStaffCosts { get; init; }

    // Teaching and teaching support
    public decimal? AgencySupplyTeachingStaffCosts { get; init; }
    public decimal? EducationSupportStaffCosts { get; init; }
    public decimal? EducationalConsultancyCosts { get; init; }
    public decimal? SupplyTeachingStaffCosts { get; init; }
    public decimal? TeachingStaffCosts { get; init; }

    // Utilities
    public decimal? EnergyCosts { get; init; }
    public decimal? WaterSewerageCosts { get; init; }

    // Other costs
    public decimal? DirectRevenueFinancingCosts { get; init; }
    public decimal? GroundsMaintenanceCosts { get; init; }
    public decimal? IndirectEmployeeExpenses { get; init; }
    public decimal? InterestChargesLoanBank { get; init; }
    public decimal? OtherInsurancePremiumsCosts { get; init; }
    public decimal? PrivateFinanceInitiativeCharges { get; init; }
    public decimal? RentRatesCosts { get; init; }
    public decimal? SpecialFacilitiesCosts { get; init; }
    public decimal? StaffDevelopmentTrainingCosts { get; init; }
    public decimal? StaffRelatedInsuranceCosts { get; init; }
    public decimal? SupplyTeacherInsurableCosts { get; init; }

    // Totals
    public decimal TotalIncome { get; init; }
    public decimal TotalExpenditure { get; init; }
    public decimal RevenueReserve { get; init; }

    // Non-financial data
    public decimal? TotalNumberOfTeachersFte { get; init; }
    public decimal? FreeSchoolMealPercent { get; init; }
    public decimal? SpecialEducationalNeedsPercent { get; init; }
    public int? FloorArea { get; init; }

    // Workforce data
    public decimal? WorkforceFte { get; init; }
    public decimal? TeachersFte { get; init; }
    public decimal? SeniorLeadershipFte { get; init; }
}