﻿using Web.App.Domain;
namespace Web.App.ViewModels;

public record SchoolCustomDataViewModel : IFinancialDataCustomDataViewModel, INonFinancialDataCustomDataViewModel,
    IWorkforceDataCustomDataViewModel
{
    // Administrative supplies
    public decimal? AdministrativeSuppliesNonEducationalCosts { get; init; }

    // Catering
    public decimal? CateringStaffCosts { get; init; }
    public decimal? CateringSuppliesCosts { get; init; }

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
    public decimal? TotalIncome { get; init; }
    public decimal? TotalExpenditure { get; init; }
    public decimal? RevenueReserve { get; init; }

    // Non-financial data
    public decimal? NumberOfPupilsFte { get; init; }
    public decimal? FreeSchoolMealPercent { get; init; }
    public decimal? SpecialEducationalNeedsPercent { get; init; }
    public decimal? FloorArea { get; init; }

    // Workforce data
    public decimal? WorkforceFte { get; init; }
    public decimal? TeachersFte { get; init; }
    public decimal? QualifiedTeacherPercent { get; init; }
    public decimal? SeniorLeadershipFte { get; init; }
    public decimal? TeachingAssistantsFte { get; init; }
    public decimal? NonClassroomSupportStaffFte { get; init; }
    public decimal? AuxiliaryStaffFte { get; init; }
    public decimal? WorkforceHeadcount { get; init; }

    public static SchoolCustomDataViewModel FromCustomData(CustomData customData) => new()
    {
        // Administrative supplies
        AdministrativeSuppliesNonEducationalCosts = customData.AdministrativeSuppliesNonEducationalCosts,

        // Catering
        CateringStaffCosts = customData.CateringStaffCosts,
        CateringSuppliesCosts = customData.CateringSuppliesCosts,

        // Educational supplies
        ExaminationFeesCosts = customData.ExaminationFeesCosts,
        LearningResourcesNonIctCosts = customData.LearningResourcesNonIctCosts,

        // IT
        LearningResourcesIctCosts = customData.LearningResourcesIctCosts,

        // Non-educational support staff
        AdministrativeClericalStaffCosts = customData.AdministrativeClericalStaffCosts,
        AuditorsCosts = customData.AuditorsCosts,
        OtherStaffCosts = customData.OtherStaffCosts,
        ProfessionalServicesNonCurriculumCosts = customData.ProfessionalServicesNonCurriculumCosts,

        // Premises and services
        CleaningCaretakingCosts = customData.CleaningCaretakingCosts,
        MaintenancePremisesCosts = customData.MaintenancePremisesCosts,
        OtherOccupationCosts = customData.OtherOccupationCosts,
        PremisesStaffCosts = customData.PremisesStaffCosts,

        // Teaching and teaching support
        AgencySupplyTeachingStaffCosts = customData.AgencySupplyTeachingStaffCosts,
        EducationSupportStaffCosts = customData.EducationSupportStaffCosts,
        EducationalConsultancyCosts = customData.EducationalConsultancyCosts,
        SupplyTeachingStaffCosts = customData.SupplyTeachingStaffCosts,
        TeachingStaffCosts = customData.TeachingStaffCosts,

        // Utilities
        EnergyCosts = customData.EnergyCosts,
        WaterSewerageCosts = customData.WaterSewerageCosts,

        // Other costs
        DirectRevenueFinancingCosts = customData.DirectRevenueFinancingCosts,
        GroundsMaintenanceCosts = customData.GroundsMaintenanceCosts,
        IndirectEmployeeExpenses = customData.IndirectEmployeeExpenses,
        InterestChargesLoanBank = customData.InterestChargesLoanBank,
        OtherInsurancePremiumsCosts = customData.OtherInsurancePremiumsCosts,
        PrivateFinanceInitiativeCharges = customData.PrivateFinanceInitiativeCharges,
        RentRatesCosts = customData.RentRatesCosts,
        SpecialFacilitiesCosts = customData.SpecialFacilitiesCosts,
        StaffDevelopmentTrainingCosts = customData.StaffDevelopmentTrainingCosts,
        StaffRelatedInsuranceCosts = customData.StaffRelatedInsuranceCosts,
        SupplyTeacherInsurableCosts = customData.SupplyTeacherInsurableCosts,

        // Totals
        TotalIncome = customData.TotalIncome,
        TotalExpenditure = customData.TotalExpenditure,
        RevenueReserve = customData.RevenueReserve,

        // Non-financial data
        NumberOfPupilsFte = customData.NumberOfPupilsFte,
        FreeSchoolMealPercent = customData.FreeSchoolMealPercent,
        SpecialEducationalNeedsPercent = customData.SpecialEducationalNeedsPercent,
        FloorArea = customData.FloorArea,

        // Workforce data
        WorkforceFte = customData.WorkforceFte,
        TeachersFte = customData.TeachersFte,
        QualifiedTeacherPercent = customData.QualifiedTeacherPercent,
        SeniorLeadershipFte = customData.SeniorLeadershipFte,
        TeachingAssistantsFte = customData.TeachingAssistantsFte,
        NonClassroomSupportStaffFte = customData.NonClassroomSupportStaffFte,
        AuxiliaryStaffFte = customData.AuxiliaryStaffFte,
        WorkforceHeadcount = customData.WorkforceHeadcount
    };
}

public static class SchoolCustomDataViewModelTitles
{
    public const string TotalIncome = "Total income";
    public const string TotalExpenditure = "Total spending";
    public const string RevenueReserve = "Revenue reserve";
    public const string NumberOfPupilsFte = "Number of pupils (full time equivalent)";
    public const string FreeSchoolMealPercent = "Pupils eligible for free school meals (FSM)";
    public const string SpecialEducationalNeedsPercent = "Pupils with special educational needs (SEN)";
    public const string FloorArea = "Gross internal floor area";
    public const string WorkforceFte = "School workforce (full time equivalent)";
    public const string TeachersFte = "Number of teachers (full time equivalent)";
    public const string QualifiedTeacherPercent = "Teachers with qualified teacher status";
    public const string SeniorLeadershipFte = "Senior leadership (full time equivalent)";
    public const string TeachingAssistantsFte = "Teaching Assistants (full time equivalent)";
    public const string NonClassroomSupportStaffFte = "Non-classroom support staff - excluding auxiliary staff (full time equivalent)";
    public const string AuxiliaryStaffFte = "Auxiliary staff (full time equivalent)";
    public const string WorkforceHeadcount = "School workforce (headcount)";
}