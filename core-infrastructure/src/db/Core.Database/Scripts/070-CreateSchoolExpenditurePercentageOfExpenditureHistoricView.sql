DROP VIEW IF EXISTS SchoolExpenditurePercentageOfExpenditureHistoric
GO

CREATE VIEW SchoolExpenditurePercentageOfExpenditureHistoric AS
  SELECT RunId                                                     AS Year
       , SchoolExpenditureHistoricWithNulls.URN
       , FinanceType
       , OverallPhase
       , (TotalExpenditure / TotalExpenditure) * 100               AS TotalExpenditure
       , (TotalPremisesStaffServiceCosts / TotalExpenditure) * 100 AS TotalPremisesStaffServiceCosts
    FROM SchoolExpenditureHistoricWithNulls
   INNER
    JOIN School
      ON (School.URN = SchoolExpenditureHistoricWithNulls.URN)
GO
