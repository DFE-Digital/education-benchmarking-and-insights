DROP VIEW IF EXISTS VW_SchoolsFinancialSummaryActual
    GO

CREATE VIEW VW_SchoolsFinancialSummaryActual
AS
SELECT f.RunType,
       f.RunId,
       f.URN,
       f.PeriodCoveredByReturn,
       f.TotalExpenditure,
       f.TotalIncome,
       f.TotalPupils,
       f.TotalTeachingSupportStaffCosts,
       f.RevenueReserve,
       f.FinanceType,
       f.OverallPhase,
       s.SchoolName,
       s.LACode,
       s.TrustCompanyNumber,
       s.NurseryProvision,
       s.SpecialClassProvision,
       s.SixthFormProvision
FROM Financial f
         LEFT JOIN School s on f.URN = s.URN
-- exclude none lead federation schools
WHERE s.FederationLeadURN = s.URN -- federation lead
OR s.FederationLeadURN IS NULL -- not federated
    GO

DROP VIEW IF EXISTS VW_SchoolsFinancialSummaryPerUnit
    GO

CREATE VIEW VW_SchoolsFinancialSummaryPerUnit
AS
SELECT RunType,
       RunId,
       URN,
       TotalPupils,
       IIF(TotalPupils > 0.0, TotalExpenditure / TotalPupils, NULL)                          AS TotalExpenditure,
       IIF(TotalPupils > 0.0, TotalTeachingSupportStaffCosts / TotalPupils, NULL)            AS TotalTeachingSupportStaffCosts,
       IIF(TotalPupils > 0.0, RevenueReserve / TotalPupils, NULL)                          AS RevenueReserve,
       PeriodCoveredByReturn,
       FinanceType,
       OverallPhase,
       SchoolName,
       LACode,
       TrustCompanyNumber,
       NurseryProvision,
       SpecialClassProvision,
       SixthFormProvision
FROM VW_SchoolsFinancialSummaryActual
    GO

DROP VIEW IF EXISTS VW_SchoolsFinancialSummaryPercentExpenditure
    GO

CREATE VIEW VW_SchoolsFinancialSummaryPercentExpenditure
AS
SELECT RunType,
       RunId,
       URN,
       TotalPupils,
       IIF(TotalExpenditure > 0.0, (TotalExpenditure / TotalExpenditure) * 100, NULL)                          AS TotalExpenditure,
       IIF(TotalExpenditure > 0.0, (TotalTeachingSupportStaffCosts / TotalExpenditure) * 100, NULL)            AS TotalTeachingSupportStaffCosts,
       IIF(TotalExpenditure > 0.0, (RevenueReserve / TotalExpenditure)  * 100, NULL)                          AS RevenueReserve,
       PeriodCoveredByReturn,
       FinanceType,
       OverallPhase,
       SchoolName,
       LACode,
       TrustCompanyNumber,
       NurseryProvision,
       SpecialClassProvision,
       SixthFormProvision
FROM VW_SchoolsFinancialSummaryActual
    GO

DROP VIEW IF EXISTS VW_SchoolsFinancialSummaryPercentIncome
    GO

CREATE VIEW VW_SchoolsFinancialSummaryPercentIncome
AS
SELECT RunType,
       RunId,
       URN,
       TotalPupils,
       IIF(TotalIncome > 0.0, (TotalExpenditure / TotalIncome) * 100, NULL)                          AS TotalExpenditure,
       IIF(TotalIncome > 0.0, (TotalTeachingSupportStaffCosts / TotalIncome) * 100, NULL)            AS TotalTeachingSupportStaffCosts,
       IIF(TotalIncome > 0.0, (RevenueReserve / TotalIncome)  * 100, NULL)                          AS RevenueReserve,
       PeriodCoveredByReturn,
       FinanceType,
       OverallPhase,
       SchoolName,
       LACode,
       TrustCompanyNumber,
       NurseryProvision,
       SpecialClassProvision,
       SixthFormProvision
FROM VW_SchoolsFinancialSummaryActual
    GO

DROP VIEW IF EXISTS VW_SchoolsFinancialSummaryDefaultActual
    GO

CREATE VIEW VW_SchoolsFinancialSummaryDefaultActual AS
SELECT RunId,
       URN,
       TotalPupils,
       TotalExpenditure,
       TotalTeachingSupportStaffCosts,
       RevenueReserve,
       PeriodCoveredByReturn,
       FinanceType,
       OverallPhase,
       SchoolName,
       LACode,
       TrustCompanyNumber,
       NurseryProvision,
       SpecialClassProvision,
       SixthFormProvision
FROM VW_SchoolsFinancialSummaryActual
WHERE RunType = 'default'
    GO

DROP VIEW IF EXISTS VW_SchoolsFinancialSummaryDefaultPerUnit
    GO

CREATE VIEW VW_SchoolsFinancialSummaryDefaultPerUnit AS
SELECT RunId,
       URN,
       TotalPupils,
       TotalExpenditure,
       TotalTeachingSupportStaffCosts,
       RevenueReserve,
       PeriodCoveredByReturn,
       FinanceType,
       OverallPhase,
       SchoolName,
       LACode,
       TrustCompanyNumber,
       NurseryProvision,
       SpecialClassProvision,
       SixthFormProvision
FROM VW_SchoolsFinancialSummaryPerUnit
WHERE RunType = 'default'
    GO

DROP VIEW IF EXISTS VW_SchoolsFinancialSummaryDefaultPercentExpenditure
    GO

CREATE VIEW VW_SchoolsFinancialSummaryDefaultPercentExpenditure AS
SELECT RunId,
       URN,
       TotalPupils,
       TotalExpenditure,
       TotalTeachingSupportStaffCosts,
       RevenueReserve,
       PeriodCoveredByReturn,
       FinanceType,
       OverallPhase,
       SchoolName,
       LACode,
       TrustCompanyNumber,
       NurseryProvision,
       SpecialClassProvision,
       SixthFormProvision
FROM VW_SchoolsFinancialSummaryPercentExpenditure
WHERE RunType = 'default'
    GO

DROP VIEW IF EXISTS VW_SchoolsFinancialSummaryDefaultPercentIncome
    GO

CREATE VIEW VW_SchoolsFinancialSummaryDefaultPercentIncome AS
SELECT RunId,
       URN,
       TotalPupils,
       TotalExpenditure,
       TotalTeachingSupportStaffCosts,
       RevenueReserve,
       PeriodCoveredByReturn,
       FinanceType,
       OverallPhase,
       SchoolName,
       LACode,
       TrustCompanyNumber,
       NurseryProvision,
       SpecialClassProvision,
       SixthFormProvision
FROM VW_SchoolsFinancialSummaryPercentIncome
WHERE RunType = 'default'
    GO

DROP VIEW IF EXISTS VW_SchoolsFinancialSummaryDefaultCurrentActual
    GO

CREATE VIEW VW_SchoolsFinancialSummaryDefaultCurrentActual AS
SELECT URN,
       TotalPupils,
       TotalExpenditure,
       TotalTeachingSupportStaffCosts,
       RevenueReserve,
       PeriodCoveredByReturn,
       FinanceType,
       OverallPhase,
       SchoolName,
       LACode,
       TrustCompanyNumber,
       NurseryProvision,
       SpecialClassProvision,
       SixthFormProvision
FROM VW_SchoolsFinancialSummaryDefaultActual
WHERE RunId = (SELECT Value FROM Parameters WHERE Name = 'CurrentYear')
    GO

DROP VIEW IF EXISTS VW_SchoolsFinancialSummaryDefaultCurrentPerUnit
    GO

CREATE VIEW VW_SchoolsFinancialSummaryDefaultCurrentPerUnit AS
SELECT URN,
       TotalPupils,
       TotalExpenditure,
       TotalTeachingSupportStaffCosts,
       RevenueReserve,
       PeriodCoveredByReturn,
       FinanceType,
       OverallPhase,
       SchoolName,
       LACode,
       TrustCompanyNumber,
       NurseryProvision,
       SpecialClassProvision,
       SixthFormProvision
FROM VW_SchoolsFinancialSummaryDefaultPerUnit
WHERE RunId = (SELECT Value FROM Parameters WHERE Name = 'CurrentYear')
    GO

DROP VIEW IF EXISTS VW_SchoolsFinancialSummaryDefaultCurrentPercentExpenditure
    GO

CREATE VIEW VW_SchoolsFinancialSummaryDefaultCurrentPercentExpenditure AS
SELECT URN,
       TotalPupils,
       TotalExpenditure,
       TotalTeachingSupportStaffCosts,
       RevenueReserve,
       PeriodCoveredByReturn,
       FinanceType,
       OverallPhase,
       SchoolName,
       LACode,
       TrustCompanyNumber,
       NurseryProvision,
       SpecialClassProvision,
       SixthFormProvision
FROM VW_SchoolsFinancialSummaryDefaultPercentExpenditure
WHERE RunId = (SELECT Value FROM Parameters WHERE Name = 'CurrentYear')
    GO

DROP VIEW IF EXISTS VW_SchoolsFinancialSummaryDefaultCurrentPercentIncome
    GO

CREATE VIEW VW_SchoolsFinancialSummaryDefaultCurrentPercentIncome AS
SELECT URN,
       TotalPupils,
       TotalExpenditure,
       TotalTeachingSupportStaffCosts,
       RevenueReserve,
       PeriodCoveredByReturn,
       FinanceType,
       OverallPhase,
       SchoolName,
       LACode,
       TrustCompanyNumber,
       NurseryProvision,
       SpecialClassProvision,
       SixthFormProvision
FROM VW_SchoolsFinancialSummaryDefaultPercentIncome
WHERE RunId = (SELECT Value FROM Parameters WHERE Name = 'CurrentYear')
    GO


