namespace Web.App.Domain;

public static class CustomDataFactory
{
    public static CustomData Create(
        SchoolIncome income,
        SchoolExpenditure expenditure,
        Census census,
        SchoolCharacteristic characteristic,
        SchoolBalance balance) => new()
        {
            // Administrative supplies
            AdministrativeSuppliesCosts = expenditure.AdministrativeSuppliesCosts,
            // Catering
            CateringStaffCosts = expenditure.CateringStaffCosts,
            CateringSuppliesCosts = expenditure.CateringSuppliesCosts,
            // Educational supplies
            ExaminationFeesCosts = expenditure.ExaminationFeesCosts,
            LearningResourcesNonIctCosts = expenditure.LearningResourcesNonIctCosts,
            // IT
            LearningResourcesIctCosts = expenditure.LearningResourcesIctCosts,
            // Non-educational support staff
            AdministrativeClericalStaffCosts = expenditure.AdministrativeClericalStaffCosts,
            AuditorsCosts = expenditure.AuditorsCosts,
            OtherStaffCosts = expenditure.OtherStaffCosts,
            ProfessionalServicesNonCurriculumCosts = expenditure.ProfessionalServicesNonCurriculumCosts,
            // Premises and services
            CleaningCaretakingCosts = expenditure.CleaningCaretakingCosts,
            MaintenancePremisesCosts = expenditure.MaintenancePremisesCosts,
            OtherOccupationCosts = expenditure.OtherOccupationCosts,
            PremisesStaffCosts = expenditure.PremisesStaffCosts,
            // Teaching and teaching support
            AgencySupplyTeachingStaffCosts = expenditure.AgencySupplyTeachingStaffCosts,
            EducationSupportStaffCosts = expenditure.EducationSupportStaffCosts,
            EducationalConsultancyCosts = expenditure.EducationalConsultancyCosts,
            SupplyTeachingStaffCosts = expenditure.SupplyTeachingStaffCosts,
            TeachingStaffCosts = expenditure.TeachingStaffCosts,
            // Utilities
            EnergyCosts = expenditure.EnergyCosts,
            WaterSewerageCosts = expenditure.WaterSewerageCosts,
            // Other costs
            DirectRevenueFinancingCosts = expenditure.DirectRevenueFinancingCosts,
            GroundsMaintenanceCosts = expenditure.GroundsMaintenanceCosts,
            IndirectEmployeeExpenses = expenditure.IndirectEmployeeExpenses,
            InterestChargesLoanBank = expenditure.InterestChargesLoanBank,
            OtherInsurancePremiumsCosts = expenditure.OtherInsurancePremiumsCosts,
            PrivateFinanceInitiativeCharges = expenditure.PrivateFinanceInitiativeCharges,
            RentRatesCosts = expenditure.RentRatesCosts,
            SpecialFacilitiesCosts = expenditure.SpecialFacilitiesCosts,
            StaffDevelopmentTrainingCosts = expenditure.StaffDevelopmentTrainingCosts,
            StaffRelatedInsuranceCosts = expenditure.StaffRelatedInsuranceCosts,
            SupplyTeacherInsurableCosts = expenditure.SupplyTeacherInsurableCosts,
            // Totals
            TotalIncome = income.TotalIncome,
            TotalExpenditure = expenditure.TotalExpenditure,
            RevenueReserve = balance.RevenueReserve,
            // Non-financial data
            NumberOfPupilsFte = census.TotalPupils,
            FreeSchoolMealPercent = characteristic.PercentFreeSchoolMeals,
            SpecialEducationalNeedsPercent = characteristic.PercentSpecialEducationNeeds,
            FloorArea = characteristic.TotalInternalFloorArea,
            // Workforce data
            WorkforceFte = census.Workforce,
            TeachersFte = census.Teachers,
            QualifiedTeacherPercent = census.PercentTeacherWithQualifiedStatus,
            SeniorLeadershipFte = census.SeniorLeadership,
            TeachingAssistantsFte = census.TeachingAssistant,
            NonClassroomSupportStaffFte = census.NonClassroomSupportStaff,
            AuxiliaryStaffFte = census.AuxiliaryStaff,
            WorkforceHeadcount = census.WorkforceHeadcount
        };
}