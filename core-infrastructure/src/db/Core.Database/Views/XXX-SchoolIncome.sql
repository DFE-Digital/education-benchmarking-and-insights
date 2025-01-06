DROP VIEW IF EXISTS VW_SchoolIncomeDefaultCurrentActual
GO

CREATE VIEW VW_SchoolIncomeDefaultCurrentActual AS
SELECT s.URN,
       s.SchoolName,
       s.SchoolType,
       s.OverallPhase,
       s.LACode,
       s.LAName,
       s.TrustCompanyNumber,
       f.PeriodCoveredByReturn,
       f.TotalPupils,
       f.TotalIncome,
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
       f.OtherSelfGeneratedIncome
FROM School s
         LEFT JOIN VW_FinancialDefaultCurrent f ON f.URN = s.URN
GO

DROP VIEW IF EXISTS VW_SchoolIncomeDefaultActual
GO

CREATE VIEW VW_SchoolIncomeDefaultActual AS
SELECT RunId,
       URN,
       TotalPupils,
       TotalIncome,
       TotalGrantFunding,
       TotalSelfGeneratedFunding,
       DirectRevenueFinancing,
       DirectGrants,
       PrePost16Funding,
       OtherDfeGrants,
       OtherIncomeGrants,
       GovernmentSource,
       CommunityGrants,
       Academies,
       IncomeFacilitiesServices,
       IncomeCateringServices,
       DonationsVoluntaryFunds,
       ReceiptsSupplyTeacherInsuranceClaims,
       InvestmentIncome,
       OtherSelfGeneratedIncome
FROM Financial
WHERE RunType = 'default'
GO

