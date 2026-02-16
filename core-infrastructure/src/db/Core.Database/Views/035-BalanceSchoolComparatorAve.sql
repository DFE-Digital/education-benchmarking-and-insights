DROP VIEW IF EXISTS VW_IncomeSchoolDefaultComparatorAveActual
GO

CREATE VIEW VW_IncomeSchoolDefaultComparatorAveActual AS
WITH comparators AS (
    SELECT RunId
         , URN
         , Comparator.value AS ComparatorURN
    FROM ComparatorSet
        CROSS APPLY Openjson(Pupil) Comparator
        WHERE RunType = 'default'
)
SELECT s.URN
        , s.RunId
        , Avg (InYearBalance)  AS 'InYearBalance'
        , Avg (RevenueReserve) AS 'RevenueReserve'
FROM comparators s
    INNER JOIN VW_BalanceSchoolDefaultNormalisedActual c
        ON (
            s.ComparatorURN = c.URN
                AND s.RunId = c.RunId
            )
GROUP
    BY s.URN
     , s.RunId
GO

DROP VIEW IF EXISTS VW_BalanceSchoolDefaultComparatorAvePercentExpenditure
GO

CREATE VIEW VW_BalanceSchoolDefaultComparatorAvePercentExpenditure AS
WITH comparators AS (
    SELECT RunId
         , URN
         , Comparator.value AS ComparatorURN
    FROM ComparatorSet
             CROSS APPLY Openjson(Pupil) Comparator
    WHERE RunType = 'default'
)
SELECT s.URN
     , s.RunId
     , Avg (InYearBalance)  AS 'InYearBalance'
     , Avg (RevenueReserve) AS 'RevenueReserve'
FROM comparators s
         INNER JOIN VW_BalanceSchoolDefaultNormalisedPercentExpenditure c
                    ON (
                        s.ComparatorURN = c.URN
                            AND s.RunId = c.RunId
                        )
GROUP
    BY s.URN
     , s.RunId
GO

DROP VIEW IF EXISTS VW_BalanceSchoolDefaultComparatorAvePercentIncome
GO

CREATE VIEW VW_BalanceSchoolDefaultComparatorAvePercentIncome AS
WITH comparators AS (
    SELECT RunId
         , URN
         , Comparator.value AS ComparatorURN
    FROM ComparatorSet
             CROSS APPLY Openjson(Pupil) Comparator
    WHERE RunType = 'default'
)
SELECT s.URN
     , s.RunId
     , Avg (InYearBalance)  AS 'InYearBalance'
     , Avg (RevenueReserve) AS 'RevenueReserve'
FROM comparators s
         INNER JOIN VW_BalanceSchoolDefaultNormalisedPercentIncome c
                    ON (
                        s.ComparatorURN = c.URN
                            AND s.RunId = c.RunId
                        )
GROUP
    BY s.URN
     , s.RunId
GO

DROP VIEW IF EXISTS VW_BalanceSchoolDefaultComparatorAvePerUnit
GO

CREATE VIEW VW_BalanceSchoolDefaultComparatorAvePerUnit AS
WITH comparators AS (
    SELECT RunId
         , URN
         , Comparator.value AS ComparatorURN
    FROM ComparatorSet
             CROSS APPLY Openjson(Pupil) Comparator
    WHERE RunType = 'default'
)
SELECT s.URN
     , s.RunId
     , Avg (InYearBalance)  AS 'InYearBalance'
     , Avg (RevenueReserve) AS 'RevenueReserve'
FROM comparators s
         INNER JOIN VW_BalanceSchoolDefaultNormalisedPerUnit c
                    ON (
                        s.ComparatorURN = c.URN
                            AND s.RunId = c.RunId
                        )
GROUP
    BY s.URN
     , s.RunId
GO