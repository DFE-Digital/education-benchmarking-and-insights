DROP VIEW IF EXISTS VW_ItSpendTrustCurrentAllYearsActual;
GO

CREATE VIEW VW_ItSpendTrustCurrentAllYearsActual AS
SELECT
    Pivoted.CompanyNumber,
    t.TrustName,
    Pivoted.Year,
    Pivoted.[Administration software and systems] AS AdministrationSoftwareAndSystems,
    Pivoted.[Connectivity],
    Pivoted.[IT Learning resources] AS ItLearningResources,
    Pivoted.[IT support and training] AS ItSupportAndTraining,
    Pivoted.[Laptops, desktops and tablets] AS LaptopsDesktopsAndTablets,
    Pivoted.[Onsite servers] AS OnsiteServers,
    Pivoted.[Other hardware] AS OtherHardware
FROM (
    SELECT
        b.RunId,
        b.CompanyNumber,
        b.Year,
        b.Category,
        b.Value
    FROM BudgetForecastReturn b
    INNER JOIN Parameters p
        ON p.Name = 'LatestBFRYear'
        AND b.RunId = p.Value
    WHERE b.Year IN (
        CAST(p.Value AS INT) - 1,
        CAST(p.Value AS INT),
        CAST(p.Value AS INT) + 1
    )
) AS Filtered
PIVOT (
    MAX(Value)
    FOR Category IN (
        [Administration software and systems],
        [Connectivity],
        [IT Learning resources],
        [IT support and training],
        [Laptops, desktops and tablets],
        [Onsite servers],
        [Other hardware]
    )
) AS Pivoted
INNER JOIN Trust t
    ON Pivoted.CompanyNumber = t.CompanyNumber;
GO

DROP VIEW IF EXISTS VW_ItSpendTrustCurrentPreviousYearActual;
GO

CREATE VIEW VW_ItSpendTrustCurrentPreviousYearActual AS
SELECT
    v.CompanyNumber,
    v.TrustName,
    v.AdministrationSoftwareAndSystems,
    v.Connectivity,
    v.ItLearningResources,
    v.ItSupportAndTraining,
    v.LaptopsDesktopsAndTablets,
    v.OnsiteServers,
    v.OtherHardware
FROM VW_ItSpendTrustCurrentAllYearsActual v
         INNER JOIN Parameters p
                    ON p.Name = 'LatestBFRYear'
WHERE v.Year = CAST(p.Value AS INT) - 1;