DROP VIEW IF EXISTS VW_ItSpendTrustDefaultActual;
GO

CREATE VIEW VW_ItSpendTrustDefaultActual AS
SELECT
    b.RunId,
    b.CompanyNumber,
    t.TrustName,
    b.Year,
    MAX(CASE WHEN b.Category = 'Administration software and systems' THEN b.Value END) AS AdministrationSoftwareAndSystems,
    MAX(CASE WHEN b.Category = 'Connectivity' THEN b.Value END) AS Connectivity,
    MAX(CASE WHEN b.Category = 'IT Learning resources' THEN b.Value END) AS ItLearningResources,
    MAX(CASE WHEN b.Category = 'IT support and training' THEN b.Value END) AS ItSupport,
    MAX(CASE WHEN b.Category = 'Laptops, desktops and tablets' THEN b.Value END) AS LaptopsDesktopsAndTablets,
    MAX(CASE WHEN b.Category = 'Onsite servers' THEN b.Value END) AS OnsiteServers,
    MAX(CASE WHEN b.Category = 'Other hardware' THEN b.Value END) AS OtherHardware
FROM BudgetForecastReturn b
INNER JOIN Trust t
    ON b.CompanyNumber = t.CompanyNumber
GROUP BY
    b.RunId,
    b.CompanyNumber,
    t.TrustName,
    b.Year;
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
INNER JOIN Parameters p
    ON p.Name = 'LatestBFRYear'
WHERE v.RunId = p.Value
AND v.Year IN (
    CAST(p.Value AS INT) - 1,
    CAST(p.Value AS INT),
    CAST(p.Value AS INT) + 1
);
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
INNER JOIN Parameters p
    ON p.Name = 'LatestBFRYear'
WHERE v.Year = CAST(p.Value AS INT) - 1;
GO