DROP VIEW IF EXISTS SchoolExpenditurePerUnitHistoric
GO

CREATE VIEW SchoolExpenditurePerUnitHistoric AS
  SELECT SchoolExpenditureHistoricWithNulls.URN
       , Year
       , FinanceType
       , OverallPhase
       , TotalExpenditure / TotalPupils                          AS TotalExpenditure
       , TotalPremisesStaffServiceCosts / TotalInternalFloorArea AS TotalPremisesStaffServiceCosts
    FROM SchoolExpenditureHistoricWithNulls
GO

DROP VIEW IF EXISTS SchoolExpenditureAvgPerUnitHistoric
GO

CREATE VIEW SchoolExpenditureAvgPerUnitHistoric AS
  SELECT Year
       , FinanceType
       , OverallPhase
       , Avg(TotalExpenditure)               AS TotalExpenditure
       , Avg(TotalPremisesStaffServiceCosts) AS TotalPremisesStaffServiceCosts
    FROM SchoolExpenditurePerUnitHistoric
   GROUP
      BY Year
       , FinanceType
       , OverallPhase
GO
