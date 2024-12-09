DROP VIEW IF EXISTS SchoolExpenditureAvgPercentageOfIncomeComparatorSet
GO

CREATE VIEW SchoolExpenditureAvgPercentageOfIncomeComparatorSet AS
  WITH pupilComparator AS (
    SELECT RunId
         , URN
         , Comparator.value AS PupilComparatorURN
      FROM ComparatorSet
     CROSS APPLY Openjson(Pupil) Comparator
     WHERE RunType = 'default'
  ), buildingComparator AS (
    SELECT RunId
         , URN
         , Comparator.value AS BuildingComparatorURN
      FROM ComparatorSet
     CROSS APPLY Openjson(Building) Comparator
     WHERE RunType = 'default'
  ), pupilExpenditureAvgOfPercentageOfIncomeComparatorSet AS (
    SELECT pupilComparator.URN
         , pupilComparator.RunId
         , Avg(TotalExpenditure) AS AvgTotalExpenditure
      FROM pupilComparator
     INNER
      JOIN SchoolExpenditurePercentageOfIncomeHistoric
        ON (
                 pupilComparator.PupilComparatorURN = SchoolExpenditurePercentageOfIncomeHistoric.URN
             AND pupilComparator.RunId = SchoolExpenditurePercentageOfIncomeHistoric.Year
           )
     GROUP
        BY pupilComparator.URN
         , pupilComparator.RunId
   ), buildingExpenditureAvgOfPercentageOfIncomeComparatorSet AS (
    SELECT buildingComparator.URN
         , buildingComparator.RunId
         , Avg(TotalPremisesStaffServiceCosts) AS AvgTotalPremisesStaffServiceCosts
      FROM buildingComparator
     INNER
      JOIN SchoolExpenditurePercentageOfIncomeHistoric
        ON (
                 buildingComparator.BuildingComparatorURN = SchoolExpenditurePercentageOfIncomeHistoric.URN
             AND buildingComparator.RunId = SchoolExpenditurePercentageOfIncomeHistoric.Year
           )
     GROUP
        BY buildingComparator.URN
         , buildingComparator.RunId
    )
  SELECT pupilExpenditureAvgOfPercentageOfIncomeComparatorSet.URN
       , pupilExpenditureAvgOfPercentageOfIncomeComparatorSet.RunId AS Year
       , AvgTotalExpenditure                    AS TotalExpenditure
       , AvgTotalPremisesStaffServiceCosts      AS TotalPremisesStaffServiceCosts
    FROM pupilExpenditureAvgOfPercentageOfIncomeComparatorSet
    FULL
    JOIN buildingExpenditureAvgOfPercentageOfIncomeComparatorSet
      ON (
               pupilExpenditureAvgOfPercentageOfIncomeComparatorSet.URN = buildingExpenditureAvgOfPercentageOfIncomeComparatorSet.URN
           AND pupilExpenditureAvgOfPercentageOfIncomeComparatorSet.RunId = buildingExpenditureAvgOfPercentageOfIncomeComparatorSet.RunId
         )
GO
