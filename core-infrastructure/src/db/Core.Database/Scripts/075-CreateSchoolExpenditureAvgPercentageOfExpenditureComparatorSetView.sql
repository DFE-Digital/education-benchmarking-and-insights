DROP VIEW IF EXISTS SchoolExpenditureAvgPercentageOfExpenditureComparatorSet
GO

CREATE VIEW SchoolExpenditureAvgPercentageOfExpenditureComparatorSet AS
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
  ), pupilExpenditureAvgOfPercentageOfExpenditureComparatorSet AS (
    SELECT pupilComparator.URN
         , pupilComparator.RunId
         , Avg(TotalExpenditure) AS AvgTotalExpenditure
      FROM pupilComparator
     INNER
      JOIN SchoolExpenditurePercentageOfExpenditureHistoric
        ON (
                 pupilComparator.PupilComparatorURN = SchoolExpenditurePercentageOfExpenditureHistoric.URN
             AND pupilComparator.RunId = SchoolExpenditurePercentageOfExpenditureHistoric.Year
           )
     GROUP
        BY pupilComparator.URN
         , pupilComparator.RunId
   ), buildingExpenditureAvgOfPercentageOfExpenditureComparatorSet AS (
    SELECT buildingComparator.URN
         , buildingComparator.RunId
         , Avg(TotalPremisesStaffServiceCosts) AS AvgTotalPremisesStaffServiceCosts
      FROM buildingComparator
     INNER
      JOIN SchoolExpenditurePercentageOfExpenditureHistoric
        ON (
                 buildingComparator.BuildingComparatorURN = SchoolExpenditurePercentageOfExpenditureHistoric.URN
             AND buildingComparator.RunId = SchoolExpenditurePercentageOfExpenditureHistoric.Year
           )
     GROUP
        BY buildingComparator.URN
         , buildingComparator.RunId
    )
  SELECT pupilExpenditureAvgOfPercentageOfExpenditureComparatorSet.URN
       , pupilExpenditureAvgOfPercentageOfExpenditureComparatorSet.RunId AS Year
       , AvgTotalExpenditure                    AS TotalExpenditure
       , AvgTotalPremisesStaffServiceCosts      AS TotalPremisesStaffServiceCosts
    FROM pupilExpenditureAvgOfPercentageOfExpenditureComparatorSet
    FULL
    JOIN buildingExpenditureAvgOfPercentageOfExpenditureComparatorSet
      ON (
               pupilExpenditureAvgOfPercentageOfExpenditureComparatorSet.URN = buildingExpenditureAvgOfPercentageOfExpenditureComparatorSet.URN
           AND pupilExpenditureAvgOfPercentageOfExpenditureComparatorSet.RunId = buildingExpenditureAvgOfPercentageOfExpenditureComparatorSet.RunId
         )
GO
