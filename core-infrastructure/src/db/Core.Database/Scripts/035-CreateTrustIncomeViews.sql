IF EXISTS(SELECT 1
          FROM sys.views
          WHERE name = 'TrustIncomeHistoric')
    BEGIN
        DROP VIEW TrustIncomeHistoric
    END
GO

CREATE VIEW TrustIncomeHistoric
AS
SELECT t.CompanyNumber,
       t.TrustName,
       f.Year,
       f.TotalPupils,
       f.TotalIncome,
       f.TotalExpenditure,
       f.TotalGrantFunding,
       f.TotalSelfGeneratedFunding,
       f.DirectRevenueFinancing,
       f.DirectGrants,
       f.PrePost16Funding,
       f.OtherDfeGrants,
       f.OtherIncomeGrants,
       f.GovernmentSource,
       f.CommunityGrants,
       f.Academies,
       f.IncomeFacilitiesServices,
       f.IncomeCateringServices,
       f.DonationsVoluntaryFunds,
       f.ReceiptsSupplyTeacherInsuranceClaims,
       f.InvestmentIncome,
       f.OtherSelfGeneratedIncome,
       f.TotalIncomeCS,
       f.TotalExpenditureCS,
       f.TotalGrantFundingCS,
       f.TotalSelfGeneratedFundingCS,
       f.DirectRevenueFinancingCS,
       f.DirectGrantsCS,
       f.PrePost16FundingCS,
       f.OtherDfeGrantsCS,
       f.OtherIncomeGrantsCS,
       f.GovernmentSourceCS,
       f.CommunityGrantsCS,
       f.AcademiesCS,
       f.IncomeFacilitiesServicesCS,
       f.IncomeCateringServicesCS,
       f.DonationsVoluntaryFundsCS,
       f.ReceiptsSupplyTeacherInsuranceClaimsCS,
       f.InvestmentIncomeCS,
       f.OtherSelfGeneratedIncomeCS
FROM Trust t
         LEFT JOIN (SELECT TrustCompanyNumber                          'CompanyNumber',
                           Year,
                           SUM(TotalPupils)                            'TotalPupils',
                           SUM(TotalIncome)                            'TotalIncome',
                           SUM(TotalExpenditure)                       'TotalExpenditure',
                           SUM(TotalGrantFunding)                      'TotalGrantFunding',
                           SUM(TotalSelfGeneratedFunding)              'TotalSelfGeneratedFunding',
                           SUM(DirectRevenueFinancing)                 'DirectRevenueFinancing',
                           SUM(DirectGrants)                           'DirectGrants',
                           SUM(PrePost16Funding)                       'PrePost16Funding',
                           SUM(OtherDfeGrants)                         'OtherDfeGrants',
                           SUM(OtherIncomeGrants)                      'OtherIncomeGrants',
                           SUM(GovernmentSource)                       'GovernmentSource',
                           SUM(CommunityGrants)                        'CommunityGrants',
                           SUM(Academies)                              'Academies',
                           SUM(IncomeFacilitiesServices)               'IncomeFacilitiesServices',
                           SUM(IncomeCateringServices)                 'IncomeCateringServices',
                           SUM(DonationsVoluntaryFunds)                'DonationsVoluntaryFunds',
                           SUM(ReceiptsSupplyTeacherInsuranceClaims)   'ReceiptsSupplyTeacherInsuranceClaims',
                           SUM(InvestmentIncome)                       'InvestmentIncome',
                           SUM(OtherSelfGeneratedIncome)               'OtherSelfGeneratedIncome',
                           SUM(TotalIncomeCS)                          'TotalIncomeCS',
                           SUM(TotalExpenditureCS)                     'TotalExpenditureCS',
                           SUM(TotalGrantFundingCS)                    'TotalGrantFundingCS',
                           SUM(TotalSelfGeneratedFundingCS)            'TotalSelfGeneratedFundingCS',
                           SUM(DirectRevenueFinancingCS)               'DirectRevenueFinancingCS',
                           SUM(DirectGrantsCS)                         'DirectGrantsCS',
                           SUM(PrePost16FundingCS)                     'PrePost16FundingCS',
                           SUM(OtherDfeGrantsCS)                       'OtherDfeGrantsCS',
                           SUM(OtherIncomeGrantsCS)                    'OtherIncomeGrantsCS',
                           SUM(GovernmentSourceCS)                     'GovernmentSourceCS',
                           SUM(CommunityGrantsCS)                      'CommunityGrantsCS',
                           SUM(AcademiesCS)                            'AcademiesCS',
                           SUM(IncomeFacilitiesServicesCS)             'IncomeFacilitiesServicesCS',
                           SUM(IncomeCateringServicesCS)               'IncomeCateringServicesCS',
                           SUM(DonationsVoluntaryFundsCS)              'DonationsVoluntaryFundsCS',
                           SUM(ReceiptsSupplyTeacherInsuranceClaimsCS) 'ReceiptsSupplyTeacherInsuranceClaimsCS',
                           SUM(InvestmentIncomeCS)                     'InvestmentIncomeCS',
                           SUM(OtherSelfGeneratedIncomeCS)             'OtherSelfGeneratedIncomeCS'
                    FROM SchoolIncomeHistoric
                    WHERE TrustCompanyNumber IS NOT NULL
                    GROUP BY TrustCompanyNumber, Year) f ON f.CompanyNumber = t.CompanyNumber

GO


IF EXISTS(SELECT 1
          FROM sys.views
          WHERE name = 'TrustIncome')
    BEGIN
        DROP VIEW TrustIncome
    END
GO

CREATE VIEW TrustIncome
AS
SELECT t.CompanyNumber,
       t.TrustName,
       f.TotalPupils,
       f.TotalIncome,
       f.TotalExpenditure,
       f.TotalGrantFunding,
       f.TotalSelfGeneratedFunding,
       f.DirectRevenueFinancing,
       f.DirectGrants,
       f.PrePost16Funding,
       f.OtherDfeGrants,
       f.OtherIncomeGrants,
       f.GovernmentSource,
       f.CommunityGrants,
       f.Academies,
       f.IncomeFacilitiesServices,
       f.IncomeCateringServices,
       f.DonationsVoluntaryFunds,
       f.ReceiptsSupplyTeacherInsuranceClaims,
       f.InvestmentIncome,
       f.OtherSelfGeneratedIncome,
       f.TotalIncomeCS,
       f.TotalExpenditureCS,
       f.TotalGrantFundingCS,
       f.TotalSelfGeneratedFundingCS,
       f.DirectRevenueFinancingCS,
       f.DirectGrantsCS,
       f.PrePost16FundingCS,
       f.OtherDfeGrantsCS,
       f.OtherIncomeGrantsCS,
       f.GovernmentSourceCS,
       f.CommunityGrantsCS,
       f.AcademiesCS,
       f.IncomeFacilitiesServicesCS,
       f.IncomeCateringServicesCS,
       f.DonationsVoluntaryFundsCS,
       f.ReceiptsSupplyTeacherInsuranceClaimsCS,
       f.InvestmentIncomeCS,
       f.OtherSelfGeneratedIncomeCS
FROM Trust t
         LEFT JOIN (SELECT TrustCompanyNumber                          'CompanyNumber',
                           SUM(TotalPupils)                            'TotalPupils',
                           SUM(TotalIncome)                            'TotalIncome',
                           SUM(TotalExpenditure)                       'TotalExpenditure',
                           SUM(TotalGrantFunding)                      'TotalGrantFunding',
                           SUM(TotalSelfGeneratedFunding)              'TotalSelfGeneratedFunding',
                           SUM(DirectRevenueFinancing)                 'DirectRevenueFinancing',
                           SUM(DirectGrants)                           'DirectGrants',
                           SUM(PrePost16Funding)                       'PrePost16Funding',
                           SUM(OtherDfeGrants)                         'OtherDfeGrants',
                           SUM(OtherIncomeGrants)                      'OtherIncomeGrants',
                           SUM(GovernmentSource)                       'GovernmentSource',
                           SUM(CommunityGrants)                        'CommunityGrants',
                           SUM(Academies)                              'Academies',
                           SUM(IncomeFacilitiesServices)               'IncomeFacilitiesServices',
                           SUM(IncomeCateringServices)                 'IncomeCateringServices',
                           SUM(DonationsVoluntaryFunds)                'DonationsVoluntaryFunds',
                           SUM(ReceiptsSupplyTeacherInsuranceClaims)   'ReceiptsSupplyTeacherInsuranceClaims',
                           SUM(InvestmentIncome)                       'InvestmentIncome',
                           SUM(OtherSelfGeneratedIncome)               'OtherSelfGeneratedIncome',
                           SUM(TotalIncomeCS)                          'TotalIncomeCS',
                           SUM(TotalExpenditureCS)                     'TotalExpenditureCS',
                           SUM(TotalGrantFundingCS)                    'TotalGrantFundingCS',
                           SUM(TotalSelfGeneratedFundingCS)            'TotalSelfGeneratedFundingCS',
                           SUM(DirectRevenueFinancingCS)               'DirectRevenueFinancingCS',
                           SUM(DirectGrantsCS)                         'DirectGrantsCS',
                           SUM(PrePost16FundingCS)                     'PrePost16FundingCS',
                           SUM(OtherDfeGrantsCS)                       'OtherDfeGrantsCS',
                           SUM(OtherIncomeGrantsCS)                    'OtherIncomeGrantsCS',
                           SUM(GovernmentSourceCS)                     'GovernmentSourceCS',
                           SUM(CommunityGrantsCS)                      'CommunityGrantsCS',
                           SUM(AcademiesCS)                            'AcademiesCS',
                           SUM(IncomeFacilitiesServicesCS)             'IncomeFacilitiesServicesCS',
                           SUM(IncomeCateringServicesCS)               'IncomeCateringServicesCS',
                           SUM(DonationsVoluntaryFundsCS)              'DonationsVoluntaryFundsCS',
                           SUM(ReceiptsSupplyTeacherInsuranceClaimsCS) 'ReceiptsSupplyTeacherInsuranceClaimsCS',
                           SUM(InvestmentIncomeCS)                     'InvestmentIncomeCS',
                           SUM(OtherSelfGeneratedIncomeCS)             'OtherSelfGeneratedIncomeCS'
                    FROM SchoolIncome
                    WHERE TrustCompanyNumber IS NOT NULL
                    GROUP BY TrustCompanyNumber) f ON f.CompanyNumber = t.CompanyNumber

GO