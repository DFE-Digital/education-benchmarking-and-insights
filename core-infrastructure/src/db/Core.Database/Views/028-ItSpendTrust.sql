DROP VIEW IF EXISTS VW_ItSpendTrustDefaultActual;
GO

CREATE VIEW VW_ItSpendTrustDefaultActual AS

-- gather all unique combinations of RunId and Year from BudgetForecastReturn

WITH RunIdYearList AS (
    SELECT DISTINCT
        RunId,
        Year
    FROM BudgetForecastReturn
),

-- Build a complete list of every Trust combined with every RunId-Year
-- ensuring all Trusts appear for each possible run.

TrustRunIdYearMatrix AS (
    SELECT
        t.CompanyNumber,
        t.TrustName,
        r.RunId,
        r.Year
    FROM Trust t
    CROSS JOIN RunIdYearList r
)

-- Join the expanded Trust-RunId-Year list to the BudgetForecastReturn data
-- preserving all Trusts even when they have no BudgetForecastReturn.
-- Pivot each IT spend category into its own column 
-- NULL values in IT spend columns indicate missing data for that Trust-RunId-Year

SELECT
    tr.RunId,
    tr.CompanyNumber,
    tr.TrustName,
    tr.Year,
    MAX(CASE WHEN b.Category = 'Administration software and systems' THEN b.Value END) AS AdministrationSoftwareAndSystems,
    MAX(CASE WHEN b.Category = 'Connectivity' THEN b.Value END) AS Connectivity,
    MAX(CASE WHEN b.Category = 'IT Learning resources' THEN b.Value END) AS ItLearningResources,
    MAX(CASE WHEN b.Category = 'IT support and training' THEN b.Value END) AS ItSupport,
    MAX(CASE WHEN b.Category = 'Laptops, desktops and tablets' THEN b.Value END) AS LaptopsDesktopsAndTablets,
    MAX(CASE WHEN b.Category = 'Onsite servers' THEN b.Value END) AS OnsiteServers,
    MAX(CASE WHEN b.Category = 'Other hardware' THEN b.Value END) AS OtherHardware
FROM TrustRunIdYearMatrix tr
LEFT JOIN BudgetForecastReturn b
    ON b.CompanyNumber = tr.CompanyNumber
    AND b.RunId = tr.RunId
    AND b.Year = tr.Year
GROUP BY
    tr.RunId,
    tr.CompanyNumber,
    tr.TrustName,
    tr.Year
GO

DROP VIEW IF EXISTS VW_ItSpendTrustCurrentAllYearsActual;
GO

CREATE VIEW VW_ItSpendTrustCurrentAllYearsActual AS
SELECT
    v.CompanyNumber,
    v.TrustName,
    v.Year,
    v.AdministrationSoftwareAndSystems,
    v.Connectivity,
    v.ItLearningResources,
    v.ItSupport,
    v.LaptopsDesktopsAndTablets,
    v.OnsiteServers,
    v.OtherHardware
FROM VW_ItSpendTrustDefaultActual v
WHERE v.RunId = (SELECT Value FROM Parameters WHERE Name = 'CurrentYear')
AND v.Year BETWEEN 
    (SELECT CAST(Value AS INT) FROM Parameters WHERE Name = 'LatestBFRYear') - 1
    AND (SELECT CAST(Value AS INT) FROM Parameters WHERE Name = 'LatestBFRYear') + 1;
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
    v.ItSupport,
    v.LaptopsDesktopsAndTablets,
    v.OnsiteServers,
    v.OtherHardware
FROM VW_ItSpendTrustCurrentAllYearsActual v
WHERE v.Year = (SELECT CAST(Value AS INT) FROM Parameters WHERE Name = 'LatestBFRYear') - 1
GO