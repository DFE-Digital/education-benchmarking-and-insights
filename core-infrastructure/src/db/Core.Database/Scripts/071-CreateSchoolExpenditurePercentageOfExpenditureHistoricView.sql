DROP VIEW IF EXISTS SchoolExpenditurePercentageOfExpenditureHistoric
GO

CREATE VIEW SchoolExpenditurePercentageOfExpenditureHistoric AS
  SELECT Year
       , URN
       , FinanceType
       , OverallPhase
       , (TotalExpenditure / TotalExpenditure) * 100               AS TotalExpenditure
       , (TotalPremisesStaffServiceCosts / TotalExpenditure) * 100 AS TotalPremisesStaffServiceCosts
    FROM SchoolExpenditureHistoricWithNulls
GO
