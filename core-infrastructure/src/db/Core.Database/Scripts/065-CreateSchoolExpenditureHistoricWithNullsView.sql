DROP VIEW IF EXISTS SchoolExpenditureHistoricWithNulls
GO

CREATE VIEW SchoolExpenditureHistoricWithNulls AS
  SELECT Financial.URN
       , Financial.RunId
       , CASE
             WHEN TotalPupils IS NULL OR TotalPupils <= 0.0 THEN NULL
             ELSE TotalPupils
         END AS TotalPupils
       , CASE
             WHEN TotalInternalFloorArea IS NULL OR TotalInternalFloorArea <= 0.0 THEN NULL
             ELSE TotalInternalFloorArea
         END AS TotalInternalFloorArea
       , CASE
             WHEN TotalPupils IS NULL OR TotalPupils <= 0.0 THEN NULL
             ELSE TotalExpenditure
         END AS TotalExpenditure
       , CASE
             WHEN TotalPupils IS NULL OR TotalPupils <= 0.0 THEN NULL
             ELSE TotalIncome
         END AS TotalIncome
       , CASE
             WHEN TotalInternalFloorArea IS NULL OR TotalInternalFloorArea <= 0.0 THEN NULL
             ELSE TotalPremisesStaffServiceCosts
         END AS TotalPremisesStaffServiceCosts
    FROM Financial
   WHERE RunType = 'default'
GO
