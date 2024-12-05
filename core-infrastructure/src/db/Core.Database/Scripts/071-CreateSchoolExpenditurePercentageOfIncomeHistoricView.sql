DROP VIEW IF EXISTS SchoolExpenditurePercentageOfIncomeHistoric
GO

CREATE VIEW SchoolExpenditurePercentageOfIncomeHistoric AS
  SELECT RunId                                                AS Year
       , SchoolExpenditureHistoricWithNulls.URN
       , FinanceType
       , OverallPhase
       , (TotalExpenditure / TotalIncome) * 100               AS TotalExpenditure
       , (TotalPremisesStaffServiceCosts / TotalIncome) * 100 AS TotalPremisesStaffServiceCosts
    FROM SchoolExpenditureHistoricWithNulls
   INNER
    JOIN School
      ON (School.URN = SchoolExpenditureHistoricWithNulls.URN)
GO
