DROP VIEW IF EXISTS VW_IncomeSchoolDefaultCurrentActual
GO

CREATE VIEW VW_IncomeSchoolDefaultCurrentActual AS
SELECT s.URN,
       s.SchoolName,
       s.SchoolType,
       s.LAName,
       f.PeriodCoveredByReturn,
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
         LEFT JOIN Financial f ON f.URN = s.URN
WHERE RunType = 'default'
  AND RunId = (SELECT Value FROM Parameters WHERE Name = 'CurrentYear')
GO


DROP VIEW IF EXISTS VW_IncomeSchoolDefaultActual
GO

CREATE VIEW VW_IncomeSchoolDefaultActual AS
SELECT RunId,
       URN,
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

DROP VIEW IF EXISTS VW_IncomeSchoolDefaultPercentExpenditure
GO

CREATE VIEW VW_IncomeSchoolDefaultPercentExpenditure AS
SELECT RunId,
       URN,
       IIF(TotalExpenditure != 0, (TotalIncome / TotalExpenditure) * 100, NULL) AS 'TotalIncome',
       IIF(TotalExpenditure != 0, (TotalGrantFunding / TotalExpenditure) * 100, NULL) AS 'TotalGrantFunding',
       IIF(TotalExpenditure != 0, (TotalSelfGeneratedFunding / TotalExpenditure) * 100, NULL) AS 'TotalSelfGeneratedFunding',
       IIF(TotalExpenditure != 0, (DirectRevenueFinancing / TotalExpenditure) * 100, NULL) AS 'DirectRevenueFinancing',
       IIF(TotalExpenditure != 0, (DirectGrants / TotalExpenditure) * 100, NULL) AS 'DirectGrants',
       IIF(TotalExpenditure != 0, (PrePost16Funding / TotalExpenditure) * 100, NULL) AS 'PrePost16Funding',
       IIF(TotalExpenditure != 0, (OtherDfeGrants / TotalExpenditure) * 100, NULL) AS 'OtherDfeGrants',
       IIF(TotalExpenditure != 0, (OtherIncomeGrants / TotalExpenditure) * 100, NULL) AS 'OtherIncomeGrants',
       IIF(TotalExpenditure != 0, (GovernmentSource / TotalExpenditure) * 100, NULL) AS 'GovernmentSource',
       IIF(TotalExpenditure != 0, (CommunityGrants / TotalExpenditure) * 100, NULL) AS 'CommunityGrants',
       IIF(TotalExpenditure != 0, (Academies / TotalExpenditure) * 100, NULL) AS 'Academies',
       IIF(TotalExpenditure != 0, (IncomeFacilitiesServices / TotalExpenditure) * 100, NULL) AS 'IncomeFacilitiesServices',
       IIF(TotalExpenditure != 0, (IncomeCateringServices / TotalExpenditure) * 100, NULL) AS 'IncomeCateringServices',
       IIF(TotalExpenditure != 0, (DonationsVoluntaryFunds / TotalExpenditure) * 100, NULL) AS 'DonationsVoluntaryFunds',
       IIF(TotalExpenditure != 0, (ReceiptsSupplyTeacherInsuranceClaims / TotalExpenditure) * 100, NULL) AS 'ReceiptsSupplyTeacherInsuranceClaims',
       IIF(TotalExpenditure != 0, (InvestmentIncome / TotalExpenditure) * 100, NULL) AS 'InvestmentIncome',
       IIF(TotalExpenditure != 0, (OtherSelfGeneratedIncome / TotalExpenditure) * 100, NULL) AS 'OtherSelfGeneratedIncome'
FROM Financial
WHERE RunType = 'default'
GO

DROP VIEW IF EXISTS VW_IncomeSchoolDefaultPercentIncome
GO

CREATE VIEW VW_IncomeSchoolDefaultPercentIncome AS
SELECT RunId,
       URN,
       IIF(TotalIncome != 0, (TotalIncome / TotalIncome) * 100, NULL) AS 'TotalIncome',
       IIF(TotalIncome != 0, (TotalGrantFunding / TotalIncome) * 100, NULL) AS 'TotalGrantFunding',
       IIF(TotalIncome != 0, (TotalSelfGeneratedFunding / TotalIncome) * 100, NULL) AS 'TotalSelfGeneratedFunding',
       IIF(TotalIncome != 0, (DirectRevenueFinancing / TotalIncome) * 100, NULL) AS 'DirectRevenueFinancing',
       IIF(TotalIncome != 0, (DirectGrants / TotalIncome) * 100, NULL) AS 'DirectGrants',
       IIF(TotalIncome != 0, (PrePost16Funding / TotalIncome) * 100, NULL) AS 'PrePost16Funding',
       IIF(TotalIncome != 0, (OtherDfeGrants / TotalIncome) * 100, NULL) AS 'OtherDfeGrants',
       IIF(TotalIncome != 0, (OtherIncomeGrants / TotalIncome) * 100, NULL) AS 'OtherIncomeGrants',
       IIF(TotalIncome != 0, (GovernmentSource / TotalIncome) * 100, NULL) AS 'GovernmentSource',
       IIF(TotalIncome != 0, (CommunityGrants / TotalIncome) * 100, NULL) AS 'CommunityGrants',
       IIF(TotalIncome != 0, (Academies / TotalIncome) * 100, NULL) AS 'Academies',
       IIF(TotalIncome != 0, (IncomeFacilitiesServices / TotalIncome) * 100, NULL) AS 'IncomeFacilitiesServices',
       IIF(TotalIncome != 0, (IncomeCateringServices / TotalIncome) * 100, NULL) AS 'IncomeCateringServices',
       IIF(TotalIncome != 0, (DonationsVoluntaryFunds / TotalIncome) * 100, NULL) AS 'DonationsVoluntaryFunds',
       IIF(TotalIncome != 0, (ReceiptsSupplyTeacherInsuranceClaims / TotalIncome) * 100, NULL) AS 'ReceiptsSupplyTeacherInsuranceClaims',
       IIF(TotalIncome != 0, (InvestmentIncome / TotalIncome) * 100, NULL) AS 'InvestmentIncome',
       IIF(TotalIncome != 0, (OtherSelfGeneratedIncome / TotalIncome) * 100, NULL) AS 'OtherSelfGeneratedIncome'
FROM Financial
WHERE RunType = 'default'
GO

DROP VIEW IF EXISTS VW_IncomeSchoolDefaultPerUnit
GO

CREATE VIEW VW_IncomeSchoolDefaultPerUnit AS
SELECT RunId,
       URN,
       IIF(TotalPupils != 0, TotalIncome / TotalPupils, NULL) AS 'TotalIncome',
       IIF(TotalPupils != 0, TotalGrantFunding / TotalPupils, NULL) AS 'TotalGrantFunding',
       IIF(TotalPupils != 0, TotalSelfGeneratedFunding / TotalPupils, NULL) AS 'TotalSelfGeneratedFunding',
       IIF(TotalPupils != 0, DirectRevenueFinancing / TotalPupils, NULL) AS 'DirectRevenueFinancing',
       IIF(TotalPupils != 0, DirectGrants / TotalPupils, NULL) AS 'DirectGrants',
       IIF(TotalPupils != 0, PrePost16Funding / TotalPupils, NULL) AS 'PrePost16Funding',
       IIF(TotalPupils != 0, OtherDfeGrants / TotalPupils, NULL) AS 'OtherDfeGrants',
       IIF(TotalPupils != 0, OtherIncomeGrants / TotalPupils, NULL) AS 'OtherIncomeGrants',
       IIF(TotalPupils != 0, GovernmentSource / TotalPupils, NULL) AS 'GovernmentSource',
       IIF(TotalPupils != 0, CommunityGrants / TotalPupils, NULL) AS 'CommunityGrants',
       IIF(TotalPupils != 0, Academies / TotalPupils, NULL) AS 'Academies',
       IIF(TotalPupils != 0, IncomeFacilitiesServices / TotalPupils, NULL) AS 'IncomeFacilitiesServices',
       IIF(TotalPupils != 0, IncomeCateringServices / TotalPupils, NULL) AS 'IncomeCateringServices',
       IIF(TotalPupils != 0, DonationsVoluntaryFunds / TotalPupils, NULL) AS 'DonationsVoluntaryFunds',
       IIF(TotalPupils != 0, ReceiptsSupplyTeacherInsuranceClaims / TotalPupils, NULL) AS 'ReceiptsSupplyTeacherInsuranceClaims',
       IIF(TotalPupils != 0, InvestmentIncome / TotalPupils, NULL) AS 'InvestmentIncome',
       IIF(TotalPupils != 0, OtherSelfGeneratedIncome / TotalPupils, NULL) AS 'OtherSelfGeneratedIncome'
FROM Financial
WHERE RunType = 'default'
GO