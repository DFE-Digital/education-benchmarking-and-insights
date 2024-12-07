DROP VIEW IF EXISTS SchoolExpenditureAvgComparatorSet
GO

CREATE VIEW SchoolExpenditureAvgComparatorSet AS
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
  ), pupilExpenditureAvgComparatorSet AS (
    SELECT pupilComparator.URN
         , pupilComparator.RunId
         , Avg(TotalExpenditure) AS AvgTotalExpenditure
      FROM pupilComparator
     INNER
      JOIN SchoolExpenditureHistoricWithNulls
        ON (
                 pupilComparator.PupilComparatorURN = SchoolExpenditureHistoricWithNulls.URN
             AND pupilComparator.RunId = SchoolExpenditureHistoricWithNulls.RunId
           )
     GROUP
        BY pupilComparator.URN
         , pupilComparator.RunId
   ), buildingExpenditureAvgComparatorSet AS (
    SELECT buildingComparator.URN
         , buildingComparator.RunId
         , Avg(TotalPremisesStaffServiceCosts) AS AvgTotalPremisesStaffServiceCosts
      FROM buildingComparator
     INNER
      JOIN SchoolExpenditureHistoricWithNulls
        ON (
                 buildingComparator.BuildingComparatorURN = SchoolExpenditureHistoricWithNulls.URN
             AND buildingComparator.RunId = SchoolExpenditureHistoricWithNulls.RunId
           )
     GROUP
        BY buildingComparator.URN
         , buildingComparator.RunId
    )
  SELECT pupilExpenditureAvgComparatorSet.URN
       , pupilExpenditureAvgComparatorSet.RunId AS Year
       , AvgTotalExpenditure                    AS TotalExpenditure
       , AvgTotalPremisesStaffServiceCosts      AS TotalPremisesStaffServiceCosts
    FROM pupilExpenditureAvgComparatorSet
    FULL
    JOIN buildingExpenditureAvgComparatorSet
      ON (
               pupilExpenditureAvgComparatorSet.URN = buildingExpenditureAvgComparatorSet.URN
           AND pupilExpenditureAvgComparatorSet.RunId = buildingExpenditureAvgComparatorSet.RunId
         )     
GO
