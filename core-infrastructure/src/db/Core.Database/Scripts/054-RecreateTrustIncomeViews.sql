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
       f.RunId 'Year',
       f.TotalPupils,
       f.TotalIncome,
       f.TotalExpenditure,
       f.TotalGrantFunding,
       f.TotalSelfGeneratedFunding,
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
  LEFT JOIN TrustFinancial f
    ON t.CompanyNumber = f.CompanyNumber
 CROSS JOIN (SELECT Value FROM Parameters p WHERE p.Name = 'LatestAARYear') y
 WHERE f.RunType = 'default'
   AND f.RunId BETWEEN y.Value - 5 AND y.Value

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
 LEFT JOIN TrustFinancial f
   ON t.CompanyNumber = f.CompanyNumber
WHERE f.RunType = 'default'
  AND f.RunId = (SELECT Value FROM Parameters WHERE Name = 'CurrentYear')

GO
