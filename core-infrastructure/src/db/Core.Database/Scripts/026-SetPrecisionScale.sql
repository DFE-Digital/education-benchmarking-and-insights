IF EXISTS(SELECT *
          FROM INFORMATION_SCHEMA.TABLES
          WHERE table_name = 'FinancialPlan')
    BEGIN
        ALTER TABLE FinancialPlan
            ALTER COLUMN TeacherContactRatio decimal(16, 2);
        ALTER TABLE FinancialPlan
            ALTER COLUMN InYearBalance decimal(16, 2);
        ALTER TABLE FinancialPlan
            ALTER COLUMN AverageClassSize decimal(16, 2);
    END

IF EXISTS(SELECT *
          FROM INFORMATION_SCHEMA.TABLES
          WHERE table_name = 'NonFinancial')
    BEGIN
        ALTER TABLE NonFinancial
            ALTER COLUMN TotalInternalFloorArea                        decimal(16, 2);
        ALTER TABLE NonFinancial
            ALTER COLUMN BuildingAverageAge                            decimal(16, 2);
        ALTER TABLE NonFinancial
            ALTER COLUMN TotalPupils                                   decimal(16, 2);
        ALTER TABLE NonFinancial
            ALTER COLUMN TotalPupilsSixthForm                          decimal(16, 2);
        ALTER TABLE NonFinancial
            ALTER COLUMN TotalPupilsNursery                            decimal(16, 2);
        ALTER TABLE NonFinancial
            ALTER COLUMN WorkforceHeadcount                            decimal(16, 2);
        ALTER TABLE NonFinancial
            ALTER COLUMN WorkforceFTE                                  decimal(16, 2);
        ALTER TABLE NonFinancial
            ALTER COLUMN TeachersHeadcount                             decimal(16, 2);
        ALTER TABLE NonFinancial
            ALTER COLUMN TeachersFTE                                   decimal(16, 2);
        ALTER TABLE NonFinancial
            ALTER COLUMN SeniorLeadershipHeadcount                     decimal(16, 2);
        ALTER TABLE NonFinancial
            ALTER COLUMN SeniorLeadershipFTE                           decimal(16, 2);
        ALTER TABLE NonFinancial
            ALTER COLUMN TeachingAssistantHeadcount                    decimal(16, 2);
        ALTER TABLE NonFinancial
            ALTER COLUMN TeachingAssistantFTE                          decimal(16, 2);
        ALTER TABLE NonFinancial
            ALTER COLUMN NonClassroomSupportStaffHeadcount             decimal(16, 2);
        ALTER TABLE NonFinancial
            ALTER COLUMN NonClassroomSupportStaffFTE                   decimal(16, 2);
        ALTER TABLE NonFinancial
            ALTER COLUMN AuxiliaryStaffHeadcount                       decimal(16, 2);
        ALTER TABLE NonFinancial
            ALTER COLUMN AuxiliaryStaffFTE                             decimal(16, 2);
        ALTER TABLE NonFinancial
            ALTER COLUMN PercentTeacherWithQualifiedStatus             decimal(16, 2);
        ALTER TABLE NonFinancial
            ALTER COLUMN PercentFreeSchoolMeals                        decimal(16, 2);
        ALTER TABLE NonFinancial
            ALTER COLUMN PercentSpecialEducationNeeds                  decimal(16, 2);
        ALTER TABLE NonFinancial
            ALTER COLUMN PercentWithEducationalHealthCarePlan          decimal(16, 2);
        ALTER TABLE NonFinancial
            ALTER COLUMN PercentWithoutEducationalHealthCarePlan       decimal(16, 2);
        ALTER TABLE NonFinancial
            ALTER COLUMN KS2Progress                                   decimal(16, 2);
        ALTER TABLE NonFinancial
            ALTER COLUMN KS4Progress                                   decimal(16, 2);
        ALTER TABLE NonFinancial
            ALTER COLUMN PredictedPercentChangePupils3To5Years         decimal(16, 2);
        ALTER TABLE NonFinancial
            ALTER COLUMN PercentWithVI                                 decimal(16, 2);
        ALTER TABLE NonFinancial
            ALTER COLUMN PercentWithSPLD                               decimal(16, 2);
        ALTER TABLE NonFinancial
            ALTER COLUMN PercentWithSLD                                decimal(16, 2);
        ALTER TABLE NonFinancial
            ALTER COLUMN PercentWithSLCN                               decimal(16, 2);
        ALTER TABLE NonFinancial
            ALTER COLUMN PercentWithSEMH                               decimal(16, 2);
        ALTER TABLE NonFinancial
            ALTER COLUMN PercentWithPMLD                               decimal(16, 2);
        ALTER TABLE NonFinancial
            ALTER COLUMN PercentWithPD                                 decimal(16, 2);
        ALTER TABLE NonFinancial
            ALTER COLUMN PercentWithOTH                                decimal(16, 2);
        ALTER TABLE NonFinancial
            ALTER COLUMN PercentWithMSI                                decimal(16, 2);
        ALTER TABLE NonFinancial
            ALTER COLUMN PercentWithMLD                                decimal(16, 2);
        ALTER TABLE NonFinancial
            ALTER COLUMN PercentWithHI                                 decimal(16, 2);
        ALTER TABLE NonFinancial
            ALTER COLUMN PercentWithASD                                decimal(16, 2);
    END    
    
    
IF EXISTS(SELECT *
          FROM INFORMATION_SCHEMA.TABLES
          WHERE table_name = 'Financial')
    BEGIN
        ALTER TABLE Financial
            ALTER COLUMN TotalPupils decimal(16, 2);
        ALTER TABLE Financial
            ALTER COLUMN TotalInternalFloorArea decimal(16, 2);
        ALTER TABLE Financial
            ALTER COLUMN TotalIncome decimal(16, 2);
        ALTER TABLE Financial
            ALTER COLUMN TotalExpenditure decimal(16, 2);
        ALTER TABLE Financial
            ALTER COLUMN InYearBalance decimal(16, 2);
        ALTER TABLE Financial
            ALTER COLUMN RevenueReserve decimal(16, 2);
        ALTER TABLE Financial
            ALTER COLUMN TotalGrantFunding decimal(16, 2);
        ALTER TABLE Financial
            ALTER COLUMN TotalSelfGeneratedFunding decimal(16, 2);
        ALTER TABLE Financial
            ALTER COLUMN DirectRevenueFinancing decimal(16, 2);
        ALTER TABLE Financial
            ALTER COLUMN DirectGrants decimal(16, 2);
        ALTER TABLE Financial
            ALTER COLUMN PrePost16Funding decimal(16, 2);
        ALTER TABLE Financial
            ALTER COLUMN OtherDfeGrants decimal(16, 2);
        ALTER TABLE Financial
            ALTER COLUMN OtherIncomeGrants decimal(16, 2);
        ALTER TABLE Financial
            ALTER COLUMN GovernmentSource decimal(16, 2);
        ALTER TABLE Financial
            ALTER COLUMN CommunityGrants decimal(16, 2);
        ALTER TABLE Financial
            ALTER COLUMN Academies decimal(16, 2);
        ALTER TABLE Financial
            ALTER COLUMN IncomeFacilitiesServices decimal(16, 2);
        ALTER TABLE Financial
            ALTER COLUMN IncomeCateringServices decimal(16, 2);
        ALTER TABLE Financial
            ALTER COLUMN DonationsVoluntaryFunds decimal(16, 2);
        ALTER TABLE Financial
            ALTER COLUMN ReceiptsSupplyTeacherInsuranceClaims decimal(16, 2);
        ALTER TABLE Financial
            ALTER COLUMN InvestmentIncome decimal(16, 2);
        ALTER TABLE Financial
            ALTER COLUMN OtherSelfGeneratedIncome decimal(16, 2);
        ALTER TABLE Financial
            ALTER COLUMN TotalTeachingSupportStaffCosts decimal(16, 2);
        ALTER TABLE Financial
            ALTER COLUMN TeachingStaffCosts decimal(16, 2);
        ALTER TABLE Financial
            ALTER COLUMN SupplyTeachingStaffCosts decimal(16, 2);
        ALTER TABLE Financial
            ALTER COLUMN EducationalConsultancyCosts decimal(16, 2);
        ALTER TABLE Financial
            ALTER COLUMN EducationSupportStaffCosts decimal(16, 2);
        ALTER TABLE Financial
            ALTER COLUMN AgencySupplyTeachingStaffCosts decimal(16, 2);
        ALTER TABLE Financial
            ALTER COLUMN TotalNonEducationalSupportStaffCosts decimal(16, 2);
        ALTER TABLE Financial
            ALTER COLUMN AdministrativeClericalStaffCosts decimal(16, 2);
        ALTER TABLE Financial
            ALTER COLUMN AuditorsCosts decimal(16, 2);
        ALTER TABLE Financial
            ALTER COLUMN OtherStaffCosts decimal(16, 2);
        ALTER TABLE Financial
            ALTER COLUMN ProfessionalServicesNonCurriculumCosts decimal(16, 2);
        ALTER TABLE Financial
            ALTER COLUMN TotalEducationalSuppliesCosts decimal(16, 2);
        ALTER TABLE Financial
            ALTER COLUMN ExaminationFeesCosts decimal(16, 2);
        ALTER TABLE Financial
            ALTER COLUMN BreakdownEducationalSuppliesCosts decimal(16, 2);
        ALTER TABLE Financial
            ALTER COLUMN LearningResourcesNonIctCosts decimal(16, 2);
        ALTER TABLE Financial
            ALTER COLUMN LearningResourcesIctCosts decimal(16, 2);
        ALTER TABLE Financial
            ALTER COLUMN TotalPremisesStaffServiceCosts decimal(16, 2);
        ALTER TABLE Financial
            ALTER COLUMN CleaningCaretakingCosts decimal(16, 2);
        ALTER TABLE Financial
            ALTER COLUMN MaintenancePremisesCosts decimal(16, 2);
        ALTER TABLE Financial
            ALTER COLUMN OtherOccupationCosts decimal(16, 2);
        ALTER TABLE Financial
            ALTER COLUMN PremisesStaffCosts decimal(16, 2);
        ALTER TABLE Financial
            ALTER COLUMN TotalUtilitiesCosts decimal(16, 2);
        ALTER TABLE Financial
            ALTER COLUMN EnergyCosts decimal(16, 2);
        ALTER TABLE Financial
            ALTER COLUMN WaterSewerageCosts decimal(16, 2);
        ALTER TABLE Financial
            ALTER COLUMN AdministrativeSuppliesNonEducationalCosts decimal(16, 2);
        ALTER TABLE Financial
            ALTER COLUMN TotalGrossCateringCosts decimal(16, 2);
        ALTER TABLE Financial
            ALTER COLUMN TotalNetCateringCostsCosts decimal(16, 2);
        ALTER TABLE Financial
            ALTER COLUMN CateringStaffCosts decimal(16, 2);
        ALTER TABLE Financial
            ALTER COLUMN CateringSuppliesCosts decimal(16, 2);
        ALTER TABLE Financial
            ALTER COLUMN TotalOtherCosts decimal(16, 2);
        ALTER TABLE Financial
            ALTER COLUMN DirectRevenueFinancingCosts decimal(16, 2);
        ALTER TABLE Financial
            ALTER COLUMN GroundsMaintenanceCosts decimal(16, 2);
        ALTER TABLE Financial
            ALTER COLUMN IndirectEmployeeExpenses decimal(16, 2);
        ALTER TABLE Financial
            ALTER COLUMN InterestChargesLoanBank decimal(16, 2);
        ALTER TABLE Financial
            ALTER COLUMN OtherInsurancePremiumsCosts decimal(16, 2);
        ALTER TABLE Financial
            ALTER COLUMN PrivateFinanceInitiativeCharges decimal(16, 2);
        ALTER TABLE Financial
            ALTER COLUMN RentRatesCosts decimal(16, 2);
        ALTER TABLE Financial
            ALTER COLUMN SpecialFacilitiesCosts decimal(16, 2);
        ALTER TABLE Financial
            ALTER COLUMN StaffDevelopmentTrainingCosts decimal(16, 2);
        ALTER TABLE Financial
            ALTER COLUMN StaffRelatedInsuranceCosts decimal(16, 2);
        ALTER TABLE Financial
            ALTER COLUMN SupplyTeacherInsurableCosts decimal(16, 2);
        ALTER TABLE Financial
            ALTER COLUMN CommunityFocusedSchoolStaff decimal(16, 2);
        ALTER TABLE Financial
            ALTER COLUMN CommunityFocusedSchoolCosts decimal(16, 2);
        ALTER TABLE Financial
            ALTER COLUMN TotalIncomeCS decimal(16, 2);
        ALTER TABLE Financial
            ALTER COLUMN TotalExpenditureCS decimal(16, 2);
        ALTER TABLE Financial
            ALTER COLUMN InYearBalanceCS decimal(16, 2);
        ALTER TABLE Financial
            ALTER COLUMN RevenueReserveCS decimal(16, 2);
        ALTER TABLE Financial
            ALTER COLUMN TotalGrantFundingCS decimal(16, 2);
        ALTER TABLE Financial
            ALTER COLUMN TotalSelfGeneratedFundingCS decimal(16, 2);
        ALTER TABLE Financial
            ALTER COLUMN DirectRevenueFinancingCS decimal(16, 2);
        ALTER TABLE Financial
            ALTER COLUMN DirectGrantsCS decimal(16, 2);
        ALTER TABLE Financial
            ALTER COLUMN PrePost16FundingCS decimal(16, 2);
        ALTER TABLE Financial
            ALTER COLUMN OtherDfeGrantsCS decimal(16, 2);
        ALTER TABLE Financial
            ALTER COLUMN OtherIncomeGrantsCS decimal(16, 2);
        ALTER TABLE Financial
            ALTER COLUMN GovernmentSourceCS decimal(16, 2);
        ALTER TABLE Financial
            ALTER COLUMN CommunityGrantsCS decimal(16, 2);
        ALTER TABLE Financial
            ALTER COLUMN AcademiesCS decimal(16, 2);
        ALTER TABLE Financial
            ALTER COLUMN IncomeFacilitiesServicesCS decimal(16, 2);
        ALTER TABLE Financial
            ALTER COLUMN IncomeCateringServicesCS decimal(16, 2);
        ALTER TABLE Financial
            ALTER COLUMN DonationsVoluntaryFundsCS decimal(16, 2);
        ALTER TABLE Financial
            ALTER COLUMN ReceiptsSupplyTeacherInsuranceClaimsCS decimal(16, 2);
        ALTER TABLE Financial
            ALTER COLUMN InvestmentIncomeCS decimal(16, 2);
        ALTER TABLE Financial
            ALTER COLUMN OtherSelfGeneratedIncomeCS decimal(16, 2);
        ALTER TABLE Financial
            ALTER COLUMN TotalTeachingSupportStaffCostsCS decimal(16, 2);
        ALTER TABLE Financial
            ALTER COLUMN TeachingStaffCostsCS decimal(16, 2);
        ALTER TABLE Financial
            ALTER COLUMN SupplyTeachingStaffCostsCS decimal(16, 2);
        ALTER TABLE Financial
            ALTER COLUMN EducationalConsultancyCostsCS decimal(16, 2);
        ALTER TABLE Financial
            ALTER COLUMN EducationSupportStaffCostsCS decimal(16, 2);
        ALTER TABLE Financial
            ALTER COLUMN AgencySupplyTeachingStaffCostsCS decimal(16, 2);
        ALTER TABLE Financial
            ALTER COLUMN TotalNonEducationalSupportStaffCostsCS decimal(16, 2);
        ALTER TABLE Financial
            ALTER COLUMN AdministrativeClericalStaffCostsCS decimal(16, 2);
        ALTER TABLE Financial
            ALTER COLUMN AuditorsCostsCS decimal(16, 2);
        ALTER TABLE Financial
            ALTER COLUMN OtherStaffCostsCS decimal(16, 2);
        ALTER TABLE Financial
            ALTER COLUMN ProfessionalServicesNonCurriculumCostsCS decimal(16, 2);
        ALTER TABLE Financial
            ALTER COLUMN TotalEducationalSuppliesCostsCS decimal(16, 2);
        ALTER TABLE Financial
            ALTER COLUMN ExaminationFeesCostsCS decimal(16, 2);
        ALTER TABLE Financial
            ALTER COLUMN BreakdownEducationalSuppliesCostsCS decimal(16, 2);
        ALTER TABLE Financial
            ALTER COLUMN LearningResourcesNonIctCostsCS decimal(16, 2);
        ALTER TABLE Financial
            ALTER COLUMN LearningResourcesIctCostsCS decimal(16, 2);
        ALTER TABLE Financial
            ALTER COLUMN TotalPremisesStaffServiceCostsCS decimal(16, 2);
        ALTER TABLE Financial
            ALTER COLUMN CleaningCaretakingCostsCS decimal(16, 2);
        ALTER TABLE Financial
            ALTER COLUMN MaintenancePremisesCostsCS decimal(16, 2);
        ALTER TABLE Financial
            ALTER COLUMN OtherOccupationCostsCS decimal(16, 2);
        ALTER TABLE Financial
            ALTER COLUMN PremisesStaffCostsCS decimal(16, 2);
        ALTER TABLE Financial
            ALTER COLUMN TotalUtilitiesCostsCS decimal(16, 2);
        ALTER TABLE Financial
            ALTER COLUMN EnergyCostsCS decimal(16, 2);
        ALTER TABLE Financial
            ALTER COLUMN WaterSewerageCostsCS decimal(16, 2);
        ALTER TABLE Financial
            ALTER COLUMN AdministrativeSuppliesNonEducationalCostsCS decimal(16, 2);
        ALTER TABLE Financial
            ALTER COLUMN TotalGrossCateringCostsCS decimal(16, 2);
        ALTER TABLE Financial
            ALTER COLUMN TotalNetCateringCostsCostsCS decimal(16, 2);
        ALTER TABLE Financial
            ALTER COLUMN CateringStaffCostsCS decimal(16, 2);
        ALTER TABLE Financial
            ALTER COLUMN CateringSuppliesCostsCS decimal(16, 2);
        ALTER TABLE Financial
            ALTER COLUMN TotalOtherCostsCS decimal(16, 2);
        ALTER TABLE Financial
            ALTER COLUMN DirectRevenueFinancingCostsCS decimal(16, 2);
        ALTER TABLE Financial
            ALTER COLUMN GroundsMaintenanceCostsCS decimal(16, 2);
        ALTER TABLE Financial
            ALTER COLUMN IndirectEmployeeExpensesCS decimal(16, 2);
        ALTER TABLE Financial
            ALTER COLUMN InterestChargesLoanBankCS decimal(16, 2);
        ALTER TABLE Financial
            ALTER COLUMN OtherInsurancePremiumsCostsCS decimal(16, 2);
        ALTER TABLE Financial
            ALTER COLUMN PrivateFinanceInitiativeChargesCS decimal(16, 2);
        ALTER TABLE Financial
            ALTER COLUMN RentRatesCostsCS decimal(16, 2);
        ALTER TABLE Financial
            ALTER COLUMN SpecialFacilitiesCostsCS decimal(16, 2);
        ALTER TABLE Financial
            ALTER COLUMN StaffDevelopmentTrainingCostsCS decimal(16, 2);
        ALTER TABLE Financial
            ALTER COLUMN StaffRelatedInsuranceCostsCS decimal(16, 2);
        ALTER TABLE Financial
            ALTER COLUMN SupplyTeacherInsurableCostsCS decimal(16, 2);
        ALTER TABLE Financial
            ALTER COLUMN CommunityFocusedSchoolStaffCS decimal(16, 2);
        ALTER TABLE Financial
            ALTER COLUMN CommunityFocusedSchoolCostsCS decimal(16, 2);
    END
    
    