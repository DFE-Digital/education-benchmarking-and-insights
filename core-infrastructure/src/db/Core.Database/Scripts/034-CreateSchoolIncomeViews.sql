IF EXISTS(SELECT 1
          FROM sys.views
          WHERE name = 'SchoolIncomeHistoric')
    BEGIN
        DROP VIEW SchoolIncomeHistoric
    END
GO

CREATE VIEW SchoolIncomeHistoric
AS
SELECT s.URN,
       f.RunId 'Year',
       s.TrustCompanyNumber,
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
FROM School s,
     (SELECT Value FROM Parameters p WHERE p.Name = 'LatestAARYear') y,
     Financial f
WHERE s.URN = f.URN
  AND f.RunType = 'default'
  AND s.FinanceType = 'Academy'
  AND f.RunId BETWEEN y.Value - 5 AND y.Value
UNION
SELECT s.URN,
       f.RunId 'Year',
       s.TrustCompanyNumber,
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
FROM School s,
     (SELECT Value FROM Parameters p WHERE p.Name = 'LatestCFRYear') y,
     Financial f
WHERE s.URN = f.URN
  AND f.RunType = 'default'
  AND s.FinanceType = 'Maintained'
  AND f.RunId BETWEEN y.Value - 5 AND y.Value

GO


IF EXISTS(SELECT 1
          FROM sys.views
          WHERE name = 'SchoolIncome')
    BEGIN
        DROP VIEW SchoolIncome
    END
GO

CREATE VIEW SchoolIncome
AS
SELECT s.URN,
       s.SchoolName,
       s.SchoolType,
       s.LAName,
       s.TrustCompanyNumber,
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
FROM School s
         LEFT JOIN CurrentDefaultFinancial f on f.URN = s.URN

GO