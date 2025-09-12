IF EXISTS(SELECT 1
          FROM sys.views
          WHERE name = 'SchoolExpenditureCustom')
    BEGIN
        DROP VIEW SchoolExpenditureCustom
    END
GO

CREATE VIEW SchoolExpenditureCustom
AS
SELECT s.URN,
       s.SchoolName,
       s.SchoolType,
       s.LAName,
       s.TrustCompanyNumber,
       f.RunId,
       f.TotalPupils,
       f.TotalInternalFloorArea,
       f.TotalIncome,
       f.TotalExpenditure,
       f.TotalTeachingSupportStaffCosts,
       f.TeachingStaffCosts,
       f.SupplyTeachingStaffCosts,
       f.EducationalConsultancyCosts,
       f.EducationSupportStaffCosts,
       f.AgencySupplyTeachingStaffCosts,
       f.TotalNonEducationalSupportStaffCosts,
       f.AdministrativeClericalStaffCosts,
       f.AuditorsCosts,
       f.OtherStaffCosts,
       f.ProfessionalServicesNonCurriculumCosts,
       f.TotalEducationalSuppliesCosts,
       f.ExaminationFeesCosts,
       f.LearningResourcesNonIctCosts,
       f.LearningResourcesIctCosts,
       f.TotalPremisesStaffServiceCosts,
       f.CleaningCaretakingCosts,
       f.MaintenancePremisesCosts,
       f.OtherOccupationCosts,
       f.PremisesStaffCosts,
       f.TotalUtilitiesCosts,
       f.EnergyCosts,
       f.WaterSewerageCosts,
       f.AdministrativeSuppliesNonEducationalCosts,
       f.TotalGrossCateringCosts,
       f.CateringStaffCosts,
       f.CateringSuppliesCosts,
       f.TotalOtherCosts,
       f.DirectRevenueFinancingCosts,
       f.GroundsMaintenanceCosts,
       f.IndirectEmployeeExpenses,
       f.InterestChargesLoanBank,
       f.OtherInsurancePremiumsCosts,
       f.PrivateFinanceInitiativeCharges,
       f.RentRatesCosts,
       f.SpecialFacilitiesCosts,
       f.StaffDevelopmentTrainingCosts,
       f.StaffRelatedInsuranceCosts,
       f.SupplyTeacherInsurableCosts,
       f.CommunityFocusedSchoolStaff,
       f.CommunityFocusedSchoolCosts,
       f.TotalIncomeCS,
       f.TotalExpenditureCS,
       f.TotalTeachingSupportStaffCostsCS,
       f.TeachingStaffCostsCS,
       f.SupplyTeachingStaffCostsCS,
       f.EducationalConsultancyCostsCS,
       f.EducationSupportStaffCostsCS,
       f.AgencySupplyTeachingStaffCostsCS,
       f.TotalNonEducationalSupportStaffCostsCS,
       f.AdministrativeClericalStaffCostsCS,
       f.AuditorsCostsCS,
       f.OtherStaffCostsCS,
       f.ProfessionalServicesNonCurriculumCostsCS,
       f.TotalEducationalSuppliesCostsCS,
       f.ExaminationFeesCostsCS,
       f.LearningResourcesNonIctCostsCS,
       f.LearningResourcesIctCostsCS,
       f.TotalPremisesStaffServiceCostsCS,
       f.CleaningCaretakingCostsCS,
       f.MaintenancePremisesCostsCS,
       f.OtherOccupationCostsCS,
       f.PremisesStaffCostsCS,
       f.TotalUtilitiesCostsCS,
       f.EnergyCostsCS,
       f.WaterSewerageCostsCS,
       f.AdministrativeSuppliesNonEducationalCostsCS,
       f.TotalGrossCateringCostsCS,
       f.CateringStaffCostsCS,
       f.CateringSuppliesCostsCS,
       f.TotalOtherCostsCS,
       f.DirectRevenueFinancingCostsCS,
       f.GroundsMaintenanceCostsCS,
       f.IndirectEmployeeExpensesCS,
       f.InterestChargesLoanBankCS,
       f.OtherInsurancePremiumsCostsCS,
       f.PrivateFinanceInitiativeChargesCS,
       f.RentRatesCostsCS,
       f.SpecialFacilitiesCostsCS,
       f.StaffDevelopmentTrainingCostsCS,
       f.StaffRelatedInsuranceCostsCS,
       f.SupplyTeacherInsurableCostsCS
FROM School s
         LEFT JOIN Financial f on f.URN = s.URN
WHERE f.RunType = 'custom'

GO



IF EXISTS(SELECT 1
          FROM sys.views
          WHERE name = 'SchoolCensusCustom')
    BEGIN
        DROP VIEW SchoolCensusCustom
    END
GO

CREATE VIEW SchoolCensusCustom
AS
SELECT s.URN,
       s.SchoolName,
       s.SchoolType,
       s.LAName,
       nf.RunId,
       nf.TotalPupils,
       nf.WorkforceFTE,
       nf.WorkforceHeadcount,
       nf.TeachersFTE,
       nf.SeniorLeadershipFTE,
       nf.TeachingAssistantFTE,
       nf.NonClassroomSupportStaffFTE,
       nf.AuxiliaryStaffFTE,
       nf.PercentTeacherWithQualifiedStatus
FROM School s
         LEFT JOIN NonFinancial nf on nf.URN = s.URN
WHERE nf.RunType = 'custom'

GO