IF NOT EXISTS(SELECT *
              FROM INFORMATION_SCHEMA.TABLES
              WHERE table_name = 'TrustFinancial')
    BEGIN
        CREATE TABLE dbo.TrustFinancial
        (
            CompanyNumber                                            nvarchar(8)     NOT NULL,
            RunType                                                  nvarchar(50)    NOT NULL,
            RunId                                                    nvarchar(50)    NOT NULL,
            TrustPosition                                            nvarchar(10)        NULL,
            TotalPupils                                              decimal(16, 2)      NULL,
            TotalInternalFloorArea                                   decimal(16, 2)      NULL,
            TotalIncome                                              decimal(16, 2)      NULL,
            TotalExpenditure                                         decimal(16, 2)      NULL,
            InYearBalance                                            decimal(16, 2)      NULL,
            RevenueReserve                                           decimal(16, 2)      NULL,
            TotalGrantFunding                                        decimal(16, 2)      NULL,
            TotalSelfGeneratedFunding                                decimal(16, 2)      NULL,
            DirectGrants                                             decimal(16, 2)      NULL,
            PrePost16Funding                                         decimal(16, 2)      NULL,
            TargetedGrants                                           decimal(16,2)       NULL,
            OtherDfeGrants                                           decimal(16, 2)      NULL,
            OtherIncomeGrants                                        decimal(16, 2)      NULL,
            GovernmentSource                                         decimal(16, 2)      NULL,
            CommunityGrants                                          decimal(16, 2)      NULL,
            Academies                                                decimal(16, 2)      NULL,
            IncomeFacilitiesServices                                 decimal(16, 2)      NULL,
            IncomeCateringServices                                   decimal(16, 2)      NULL,
            DonationsVoluntaryFunds                                  decimal(16, 2)      NULL,
            ReceiptsSupplyTeacherInsuranceClaims                     decimal(16, 2)      NULL,
            InvestmentIncome                                         decimal(16, 2)      NULL,
            OtherSelfGeneratedIncome                                 decimal(16, 2)      NULL,
            TotalTeachingSupportStaffCosts                           decimal(16, 2)      NULL,
            TeachingStaffCosts                                       decimal(16, 2)      NULL,
            SupplyTeachingStaffCosts                                 decimal(16, 2)      NULL,
            EducationalConsultancyCosts                              decimal(16, 2)      NULL,
            EducationSupportStaffCosts                               decimal(16, 2)      NULL,
            AgencySupplyTeachingStaffCosts                           decimal(16, 2)      NULL,
            TotalNonEducationalSupportStaffCosts                     decimal(16, 2)      NULL,
            AdministrativeClericalStaffCosts                         decimal(16, 2)      NULL,
            AuditorsCosts                                            decimal(16, 2)      NULL,
            OtherStaffCosts                                          decimal(16, 2)      NULL,
            ProfessionalServicesNonCurriculumCosts                   decimal(16, 2)      NULL,
            TotalEducationalSuppliesCosts                            decimal(16, 2)      NULL,
            ExaminationFeesCosts                                     decimal(16, 2)      NULL,
            LearningResourcesNonIctCosts                             decimal(16, 2)      NULL,
            LearningResourcesIctCosts                                decimal(16, 2)      NULL,
            TotalPremisesStaffServiceCosts                           decimal(16, 2)      NULL,
            CleaningCaretakingCosts                                  decimal(16, 2)      NULL,
            MaintenancePremisesCosts                                 decimal(16, 2)      NULL,
            OtherOccupationCosts                                     decimal(16, 2)      NULL,
            PremisesStaffCosts                                       decimal(16, 2)      NULL,
            TotalUtilitiesCosts                                      decimal(16, 2)      NULL,
            EnergyCosts                                              decimal(16, 2)      NULL,
            WaterSewerageCosts                                       decimal(16, 2)      NULL,
            AdministrativeSuppliesNonEducationalCosts                decimal(16, 2)      NULL,
            TotalGrossCateringCosts                                  decimal(16, 2)      NULL,
            TotalNetCateringCostsCosts                               decimal(16, 2)      NULL,
            CateringStaffCosts                                       decimal(16, 2)      NULL,
            CateringSuppliesCosts                                    decimal(16, 2)      NULL,
            TotalOtherCosts                                          decimal(16, 2)      NULL,
            DirectRevenueFinancingCosts                              decimal(16, 2)      NULL,
            GroundsMaintenanceCosts                                  decimal(16, 2)      NULL,
            IndirectEmployeeExpenses                                 decimal(16, 2)      NULL,
            InterestChargesLoanBank                                  decimal(16, 2)      NULL,
            OtherInsurancePremiumsCosts                              decimal(16, 2)      NULL,
            PrivateFinanceInitiativeCharges                          decimal(16, 2)      NULL,
            RentRatesCosts                                           decimal(16, 2)      NULL,
            SpecialFacilitiesCosts                                   decimal(16, 2)      NULL,
            StaffDevelopmentTrainingCosts                            decimal(16, 2)      NULL,
            StaffRelatedInsuranceCosts                               decimal(16, 2)      NULL,
            SupplyTeacherInsurableCosts                              decimal(16, 2)      NULL,
            TotalIncomeCS                                            decimal(16, 2)      NULL,
            TotalExpenditureCS                                       decimal(16, 2)      NULL,
            InYearBalanceCS                                          decimal(16, 2)      NULL,
            RevenueReserveCS                                         decimal(16, 2)      NULL,
            TotalGrantFundingCS                                      decimal(16, 2)      NULL,
            TotalSelfGeneratedFundingCS                              decimal(16, 2)      NULL,
            DirectRevenueFinancingCS                                 decimal(16, 2)      NULL,
            DirectGrantsCS                                           decimal(16, 2)      NULL,
            PrePost16FundingCS                                       decimal(16, 2)      NULL,
            OtherDfeGrantsCS                                         decimal(16, 2)      NULL,
            OtherIncomeGrantsCS                                      decimal(16, 2)      NULL,
            GovernmentSourceCS                                       decimal(16, 2)      NULL,
            CommunityGrantsCS                                        decimal(16, 2)      NULL,
            AcademiesCS                                              decimal(16, 2)      NULL,
            IncomeFacilitiesServicesCS                               decimal(16, 2)      NULL,
            IncomeCateringServicesCS                                 decimal(16, 2)      NULL,
            DonationsVoluntaryFundsCS                                decimal(16, 2)      NULL,
            ReceiptsSupplyTeacherInsuranceClaimsCS                   decimal(16, 2)      NULL,
            InvestmentIncomeCS                                       decimal(16, 2)      NULL,
            OtherSelfGeneratedIncomeCS                               decimal(16, 2)      NULL,
            TotalTeachingSupportStaffCostsCS                         decimal(16, 2)      NULL,
            TeachingStaffCostsCS                                     decimal(16, 2)      NULL,
            SupplyTeachingStaffCostsCS                               decimal(16, 2)      NULL,
            EducationalConsultancyCostsCS                            decimal(16, 2)      NULL,
            EducationSupportStaffCostsCS                             decimal(16, 2)      NULL,
            AgencySupplyTeachingStaffCostsCS                         decimal(16, 2)      NULL,
            TotalNonEducationalSupportStaffCostsCS                   decimal(16, 2)      NULL,
            AdministrativeClericalStaffCostsCS                       decimal(16, 2)      NULL,
            AuditorsCostsCS                                          decimal(16, 2)      NULL,
            OtherStaffCostsCS                                        decimal(16, 2)      NULL,
            ProfessionalServicesNonCurriculumCostsCS                 decimal(16, 2)      NULL,
            TotalEducationalSuppliesCostsCS                          decimal(16, 2)      NULL,
            ExaminationFeesCostsCS                                   decimal(16, 2)      NULL,
            LearningResourcesNonIctCostsCS                           decimal(16, 2)      NULL,
            LearningResourcesIctCostsCS                              decimal(16, 2)      NULL,
            TotalPremisesStaffServiceCostsCS                         decimal(16, 2)      NULL,
            CleaningCaretakingCostsCS                                decimal(16, 2)      NULL,
            MaintenancePremisesCostsCS                               decimal(16, 2)      NULL,
            OtherOccupationCostsCS                                   decimal(16, 2)      NULL,
            PremisesStaffCostsCS                                     decimal(16, 2)      NULL,
            TotalUtilitiesCostsCS                                    decimal(16, 2)      NULL,
            EnergyCostsCS                                            decimal(16, 2)      NULL,
            WaterSewerageCostsCS                                     decimal(16, 2)      NULL,
            AdministrativeSuppliesNonEducationalCostsCS              decimal(16, 2)      NULL,
            TotalGrossCateringCostsCS                                decimal(16, 2)      NULL,
            TotalNetCateringCostsCostsCS                             decimal(16, 2)      NULL,
            CateringStaffCostsCS                                     decimal(16, 2)      NULL,
            CateringSuppliesCostsCS                                  decimal(16, 2)      NULL,
            TotalOtherCostsCS                                        decimal(16, 2)      NULL,
            DirectRevenueFinancingCostsCS                            decimal(16, 2)      NULL,
            GroundsMaintenanceCostsCS                                decimal(16, 2)      NULL,
            IndirectEmployeeExpensesCS                               decimal(16, 2)      NULL,
            InterestChargesLoanBankCS                                decimal(16, 2)      NULL,
            OtherInsurancePremiumsCostsCS                            decimal(16, 2)      NULL,
            PrivateFinanceInitiativeChargesCS                        decimal(16, 2)      NULL,
            RentRatesCostsCS                                         decimal(16, 2)      NULL,
            SpecialFacilitiesCostsCS                                 decimal(16, 2)      NULL,
            StaffDevelopmentTrainingCostsCS                          decimal(16, 2)      NULL,
            StaffRelatedInsuranceCostsCS                             decimal(16, 2)      NULL,
            SupplyTeacherInsurableCostsCS                            decimal(16, 2)      NULL,

            CONSTRAINT PK_TrustFinancial PRIMARY KEY (RunType, RunId, CompanyNumber)
        );
    END;