using System.Diagnostics.CodeAnalysis;
using Web.App.ViewModels;
namespace Web.App.Domain;

[ExcludeFromCodeCoverage]
public record CustomData
{
    // Administrative supplies
    public decimal? AdministrativeSuppliesCosts { get; set; }

    // Catering
    public decimal? CateringStaffCosts { get; set; }
    public decimal? CateringSuppliesCosts { get; set; }

    // Educational supplies
    public decimal? ExaminationFeesCosts { get; set; }
    public decimal? LearningResourcesNonIctCosts { get; set; }

    // IT
    public decimal? LearningResourcesIctCosts { get; set; }

    // Non-educational support staff
    public decimal? AdministrativeClericalStaffCosts { get; set; }
    public decimal? AuditorsCosts { get; set; }
    public decimal? OtherStaffCosts { get; set; }
    public decimal? ProfessionalServicesNonCurriculumCosts { get; set; }

    // Premises and services
    public decimal? CleaningCaretakingCosts { get; set; }
    public decimal? MaintenancePremisesCosts { get; set; }
    public decimal? OtherOccupationCosts { get; set; }
    public decimal? PremisesStaffCosts { get; set; }

    // Teaching and teaching support
    public decimal? AgencySupplyTeachingStaffCosts { get; set; }
    public decimal? EducationSupportStaffCosts { get; set; }
    public decimal? EducationalConsultancyCosts { get; set; }
    public decimal? SupplyTeachingStaffCosts { get; set; }
    public decimal? TeachingStaffCosts { get; set; }

    // Utilities
    public decimal? EnergyCosts { get; set; }
    public decimal? WaterSewerageCosts { get; set; }

    // Other costs
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

    // Totals
    public decimal? TotalIncome { get; set; }
    public decimal? TotalExpenditure { get; set; }
    public decimal? RevenueReserve { get; set; }

    // Non-financial data
    public decimal? NumberOfPupilsFte { get; set; }
    public decimal? FreeSchoolMealPercent { get; set; }
    public decimal? SpecialEducationalNeedsPercent { get; set; }
    public decimal? FloorArea { get; set; }

    // Workforce data
    public decimal? WorkforceFte { get; set; }
    public decimal? TeachersFte { get; set; }
    public decimal? QualifiedTeacherPercent { get; set; }
    public decimal? SeniorLeadershipFte { get; set; }
    public decimal? TeachingAssistantsFte { get; set; }
    public decimal? NonClassroomSupportStaffFte { get; set; }
    public decimal? AuxiliaryStaffFte { get; set; }
    public decimal? WorkforceHeadcount { get; set; }

    public void Merge(ICustomDataViewModel viewModel)
    {
        switch (viewModel)
        {
            case IFinancialDataCustomDataViewModel financialViewModel:
                Merge(financialViewModel);
                break;
            case INonFinancialDataCustomDataViewModel nonFinancialViewModel:
                Merge(nonFinancialViewModel);
                break;
            case IWorkforceDataCustomDataViewModel workforceViewModel:
                Merge(workforceViewModel);
                break;
        }
    }

    private void Merge(IFinancialDataCustomDataViewModel viewModel)
    {
        // Administrative supplies
        AdministrativeSuppliesCosts = viewModel.AdministrativeSuppliesCosts;

        // Catering
        CateringStaffCosts = viewModel.CateringStaffCosts;
        CateringSuppliesCosts = viewModel.CateringSuppliesCosts;

        // Educational supplies
        ExaminationFeesCosts = viewModel.ExaminationFeesCosts;
        LearningResourcesNonIctCosts = viewModel.LearningResourcesNonIctCosts;

        // IT
        LearningResourcesIctCosts = viewModel.LearningResourcesIctCosts;

        // Non-educational support staff
        AdministrativeClericalStaffCosts = viewModel.AdministrativeClericalStaffCosts;
        AuditorsCosts = viewModel.AuditorsCosts;
        OtherStaffCosts = viewModel.OtherStaffCosts;
        ProfessionalServicesNonCurriculumCosts = viewModel.ProfessionalServicesNonCurriculumCosts;

        // Premises and services
        CleaningCaretakingCosts = viewModel.CleaningCaretakingCosts;
        MaintenancePremisesCosts = viewModel.MaintenancePremisesCosts;
        OtherOccupationCosts = viewModel.OtherOccupationCosts;
        PremisesStaffCosts = viewModel.PremisesStaffCosts;

        // Teaching and teaching support
        AgencySupplyTeachingStaffCosts = viewModel.AgencySupplyTeachingStaffCosts;
        EducationSupportStaffCosts = viewModel.EducationSupportStaffCosts;
        EducationalConsultancyCosts = viewModel.EducationalConsultancyCosts;
        SupplyTeachingStaffCosts = viewModel.SupplyTeachingStaffCosts;
        TeachingStaffCosts = viewModel.TeachingStaffCosts;

        // Utilities
        EnergyCosts = viewModel.EnergyCosts;
        WaterSewerageCosts = viewModel.WaterSewerageCosts;

        // Other costs
        DirectRevenueFinancingCosts = viewModel.DirectRevenueFinancingCosts;
        GroundsMaintenanceCosts = viewModel.GroundsMaintenanceCosts;
        IndirectEmployeeExpenses = viewModel.IndirectEmployeeExpenses;
        InterestChargesLoanBank = viewModel.InterestChargesLoanBank;
        OtherInsurancePremiumsCosts = viewModel.OtherInsurancePremiumsCosts;
        PrivateFinanceInitiativeCharges = viewModel.PrivateFinanceInitiativeCharges;
        RentRatesCosts = viewModel.RentRatesCosts;
        SpecialFacilitiesCosts = viewModel.SpecialFacilitiesCosts;
        StaffDevelopmentTrainingCosts = viewModel.StaffDevelopmentTrainingCosts;
        StaffRelatedInsuranceCosts = viewModel.StaffRelatedInsuranceCosts;
        SupplyTeacherInsurableCosts = viewModel.SupplyTeacherInsurableCosts;

        // Totals
        TotalIncome = viewModel.TotalIncome;
        TotalExpenditure = viewModel.TotalExpenditure;
        RevenueReserve = viewModel.RevenueReserve;
    }

    private void Merge(INonFinancialDataCustomDataViewModel viewModel)
    {
        NumberOfPupilsFte = viewModel.NumberOfPupilsFte;
        FreeSchoolMealPercent = viewModel.FreeSchoolMealPercent;
        SpecialEducationalNeedsPercent = viewModel.SpecialEducationalNeedsPercent;
        FloorArea = viewModel.FloorArea;
    }

    private void Merge(IWorkforceDataCustomDataViewModel viewModel)
    {
        WorkforceFte = viewModel.WorkforceFte;
        TeachersFte = viewModel.TeachersFte;
        QualifiedTeacherPercent = viewModel.QualifiedTeacherPercent;
        SeniorLeadershipFte = viewModel.SeniorLeadershipFte;
        TeachingAssistantsFte = viewModel.TeachingAssistantsFte;
        NonClassroomSupportStaffFte = viewModel.NonClassroomSupportStaffFte;
        AuxiliaryStaffFte = viewModel.AuxiliaryStaffFte;
        WorkforceHeadcount = viewModel.WorkforceHeadcount;
    }
}