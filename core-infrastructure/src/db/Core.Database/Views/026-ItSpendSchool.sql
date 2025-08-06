DROP VIEW IF EXISTS VW_ItSpendSchoolActual
    GO

CREATE VIEW VW_ItSpendSchoolActual AS
SELECT  RunId,
        RunType,
        URN,
        TotalPupils,
        PeriodCoveredByReturn,
        ConnectivityCosts                       AS Connectivity, 
        OnsiteServersCosts                      AS OnsiteServers, 
        ItLearningResourcesCosts                AS ItLearningResources,
        AdministrationSoftwareAndSystemsCosts   AS AdministrationSoftwareAndSystems, 
        LaptopsDesktopsAndTabletsCosts          AS LaptopsDesktopsAndTablets, 
        OtherHardwareCosts                      AS OtherHardware, 
        ItSupportCosts                          AS ItSupport
FROM Financial
    GO

DROP VIEW IF EXISTS VW_ItSpendSchoolPercentExpenditure
    GO

CREATE VIEW VW_ItSpendSchoolPercentExpenditure AS
SELECT  RunId,
        RunType,
        URN,
        TotalPupils,
        PeriodCoveredByReturn,
        IIF(TotalExpenditure > 0.0, (ConnectivityCosts / TotalExpenditure) * 100, NULL)                     AS Connectivity,
        IIF(TotalExpenditure > 0.0, (OnsiteServersCosts / TotalExpenditure) * 100, NULL)                    AS OnsiteServers,
        IIF(TotalExpenditure > 0.0, (ItLearningResourcesCosts / TotalExpenditure) * 100, NULL)              AS ItLearningResources,
        IIF(TotalExpenditure > 0.0, (AdministrationSoftwareAndSystemsCosts / TotalExpenditure) * 100, NULL) AS AdministrationSoftwareAndSystems,
        IIF(TotalExpenditure > 0.0, (LaptopsDesktopsAndTabletsCosts / TotalExpenditure) * 100, NULL)        AS LaptopsDesktopsAndTablets,
        IIF(TotalExpenditure > 0.0, (OtherHardwareCosts / TotalExpenditure) * 100, NULL)                    AS OtherHardware,
        IIF(TotalExpenditure > 0.0, (ItSupportCosts / TotalExpenditure) * 100, NULL)                        AS ItSupport
FROM Financial
    GO

DROP VIEW IF EXISTS VW_ItSpendSchoolPercentIncome
    GO

CREATE VIEW VW_ItSpendSchoolPercentIncome AS
SELECT  RunId,
        RunType,
        URN,
        TotalPupils,
        PeriodCoveredByReturn,
        IIF(TotalIncome > 0.0, (ConnectivityCosts / TotalIncome) * 100, NULL)                       AS Connectivity,
        IIF(TotalIncome > 0.0, (OnsiteServersCosts / TotalIncome) * 100, NULL)                      AS OnsiteServers,
        IIF(TotalIncome > 0.0, (ItLearningResourcesCosts / TotalIncome) * 100, NULL)                AS ItLearningResources,
        IIF(TotalIncome > 0.0, (AdministrationSoftwareAndSystemsCosts / TotalIncome) * 100, NULL)   AS AdministrationSoftwareAndSystems,
        IIF(TotalIncome > 0.0, (LaptopsDesktopsAndTabletsCosts / TotalIncome) * 100, NULL)          AS LaptopsDesktopsAndTablets,
        IIF(TotalIncome > 0.0, (OtherHardwareCosts / TotalIncome) * 100, NULL)                      AS OtherHardware,
        IIF(TotalIncome > 0.0, (ItSupportCosts / TotalIncome) * 100, NULL)                          AS ItSupport
FROM Financial
    GO

DROP VIEW IF EXISTS VW_ItSpendSchoolPerUnit
    GO

CREATE VIEW VW_ItSpendSchoolPerUnit AS
SELECT  RunId,
        RunType,
        URN,
        TotalPupils,
        PeriodCoveredByReturn,
        IIF(TotalPupils > 0.0, ConnectivityCosts / TotalPupils, NULL)                       AS Connectivity,
        IIF(TotalPupils > 0.0, OnsiteServersCosts / TotalPupils, NULL)                      AS OnsiteServers,
        IIF(TotalPupils > 0.0, ItLearningResourcesCosts / TotalPupils, NULL)                AS ItLearningResources,
        IIF(TotalPupils > 0.0, AdministrationSoftwareAndSystemsCosts / TotalPupils, NULL)   AS AdministrationSoftwareAndSystems,
        IIF(TotalPupils > 0.0, LaptopsDesktopsAndTabletsCosts / TotalPupils, NULL)          AS LaptopsDesktopsAndTablets,
        IIF(TotalPupils > 0.0, OtherHardwareCosts / TotalPupils, NULL)                      AS OtherHardware,
        IIF(TotalPupils > 0.0, ItSupportCosts / TotalPupils, NULL)                          AS ItSupport
FROM Financial
    GO

DROP VIEW IF EXISTS VW_ItSpendSchoolDefaultActual
    GO

CREATE VIEW VW_ItSpendSchoolDefaultActual AS
SELECT *
FROM VW_ItSpendSchoolActual
WHERE RunType = 'default'
    GO

DROP VIEW IF EXISTS VW_ItSpendSchoolDefaultPercentExpenditure
    GO

CREATE VIEW VW_ItSpendSchoolDefaultPercentExpenditure AS
SELECT *
FROM VW_ItSpendSchoolPercentExpenditure
WHERE RunType = 'default'
    GO

DROP VIEW IF EXISTS VW_ItSpendSchoolDefaultPercentIncome
    GO

CREATE VIEW VW_ItSpendSchoolDefaultPercentIncome AS
SELECT *
FROM VW_ItSpendSchoolPercentIncome
WHERE RunType = 'default'
    GO

DROP VIEW IF EXISTS VW_ItSpendSchoolDefaultPerUnit
    GO

CREATE VIEW VW_ItSpendSchoolDefaultPerUnit AS
SELECT *
FROM VW_ItSpendSchoolPerUnit
WHERE RunType = 'default'
    GO

DROP VIEW IF EXISTS VW_ItSpendSchoolDefaultCurrentActual
    GO

CREATE VIEW VW_ItSpendSchoolDefaultCurrentActual AS
SELECT c.*,
       s.SchoolName,
       s.SchoolType,
       s.LAName
FROM School s
         LEFT JOIN VW_ItSpendSchoolDefaultActual c ON c.URN = s.URN
WHERE c.RunId = (SELECT Value FROM Parameters WHERE Name = 'CurrentYear')
    GO

DROP VIEW IF EXISTS VW_ItSpendSchoolDefaultCurrentPercentExpenditure
    GO

CREATE VIEW VW_ItSpendSchoolDefaultCurrentPercentExpenditure AS
SELECT c.*,
       s.SchoolName,
       s.SchoolType,
       s.LAName
FROM School s
         LEFT JOIN VW_ItSpendSchoolDefaultPercentExpenditure c ON c.URN = s.URN
WHERE c.RunId = (SELECT Value FROM Parameters WHERE Name = 'CurrentYear')
    GO

DROP VIEW IF EXISTS VW_ItSpendSchoolDefaultCurrentPercentIncome
    GO

CREATE VIEW VW_ItSpendSchoolDefaultCurrentPercentIncome AS
SELECT c.*,
       s.SchoolName,
       s.SchoolType,
       s.LAName
FROM School s
         LEFT JOIN VW_ItSpendSchoolDefaultPercentIncome c ON c.URN = s.URN
WHERE c.RunId = (SELECT Value FROM Parameters WHERE Name = 'CurrentYear')
    GO

DROP VIEW IF EXISTS VW_ItSpendSchoolDefaultCurrentPerUnit
    GO

CREATE VIEW VW_ItSpendSchoolDefaultCurrentPerUnit AS
SELECT c.*,
       s.SchoolName,
       s.SchoolType,
       s.LAName
FROM School s
         LEFT JOIN VW_ItSpendSchoolDefaultPerUnit c ON c.URN = s.URN
WHERE c.RunId = (SELECT Value FROM Parameters WHERE Name = 'CurrentYear')
    GO