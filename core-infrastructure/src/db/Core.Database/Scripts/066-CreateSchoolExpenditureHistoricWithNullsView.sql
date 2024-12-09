DROP VIEW IF EXISTS SchoolExpenditureHistoricWithNulls
GO

CREATE VIEW SchoolExpenditureHistoricWithNulls AS
  SELECT URN
       , Year
       , FinanceType
       , OverallPhase
       , CASE
             WHEN TotalPupils IS NULL OR TotalPupils <= 0.0 THEN NULL
             ELSE TotalPupils
         END AS TotalPupils
       , CASE
             WHEN TotalInternalFloorArea IS NULL OR TotalInternalFloorArea <= 0.0 THEN NULL
             ELSE TotalInternalFloorArea
         END AS TotalInternalFloorArea
       , CASE
             WHEN TotalExpenditure IS NULL OR TotalExpenditure <= 0.0 THEN NULL
             ELSE TotalExpenditure
         END AS TotalExpenditure
       , CASE
             WHEN TotalIncome IS NULL OR TotalIncome <= 0.0 THEN NULL
             ELSE TotalIncome
         END AS TotalIncome
       , CASE
             WHEN TotalPremisesStaffServiceCosts IS NULL OR TotalPremisesStaffServiceCosts <= 0.0 THEN NULL
             ELSE TotalPremisesStaffServiceCosts
         END AS TotalPremisesStaffServiceCosts
    FROM SchoolExpenditureHistoric
GO
