DROP VIEW IF EXISTS VW_LocalAuthorityEducationHealthCarePlans
GO

CREATE VIEW VW_LocalAuthorityEducationHealthCarePlans
AS
    SELECT RunId,
        RunType,
        LaCode,
        Population2To18,
        EHCPTotal AS Total,
        EHCPMainstream AS Mainstream,
        EHCPResourced AS Resourced,
        EHCPSpecial AS Special,
        EHCPIndependent AS Independent,
        EHCPHospital AS Hospital,
        EHCPPost16 AS Post16,
        EHCPOther AS Other
    FROM LocalAuthorityNonFinancial
GO

DROP VIEW IF EXISTS VW_LocalAuthorityEducationHealthCarePlansPerPopulation
GO

CREATE VIEW VW_LocalAuthorityEducationHealthCarePlansPerPopulation
AS
    SELECT RunId,
        RunType,
        LaCode,
        Population2To18,
        IIF(Population2To18 > 0.0, EHCPTotal / (Population2To18 / 1000), NULL) AS Total,
        IIF(Population2To18 > 0.0, EHCPMainstream / (Population2To18 / 1000), NULL) AS Mainstream,
        IIF(Population2To18 > 0.0, EHCPResourced / (Population2To18 / 1000), NULL) AS Resourced,
        IIF(Population2To18 > 0.0, EHCPSpecial / (Population2To18 / 1000), NULL) AS Special,
        IIF(Population2To18 > 0.0, EHCPIndependent / (Population2To18 / 1000), NULL) AS Independent,
        IIF(Population2To18 > 0.0, EHCPHospital / (Population2To18 / 1000), NULL) AS Hospital,
        IIF(Population2To18 > 0.0, EHCPPost16 / (Population2To18 / 1000), NULL) AS Post16,
        IIF(Population2To18 > 0.0, EHCPOther / (Population2To18 / 1000), NULL) AS Other
    FROM LocalAuthorityNonFinancial
GO

DROP VIEW IF EXISTS VW_LocalAuthorityEducationHealthCarePlansDefaultActual
GO

CREATE VIEW VW_LocalAuthorityEducationHealthCarePlansDefaultActual
AS
    SELECT *
    FROM VW_LocalAuthorityEducationHealthCarePlans
    WHERE RunType = 'default'
GO

DROP VIEW IF EXISTS VW_LocalAuthorityEducationHealthCarePlansDefaultPerPopulation
GO

CREATE VIEW VW_LocalAuthorityEducationHealthCarePlansDefaultPerPopulation
AS
    SELECT *
    FROM VW_LocalAuthorityEducationHealthCarePlansPerPopulation
    WHERE RunType = 'default'
GO

DROP VIEW IF EXISTS VW_LocalAuthorityEducationHealthCarePlansDefaultCurrentActual
GO

CREATE VIEW VW_LocalAuthorityEducationHealthCarePlansDefaultCurrentActual
AS
    SELECT c.*,
        l.Name
    FROM LocalAuthority l
        LEFT JOIN VW_LocalAuthorityEducationHealthCarePlansDefaultActual c ON c.LaCode = l.Code
    WHERE c.RunId = (SELECT Value
    FROM Parameters
    WHERE Name = 'CurrentYear')
GO

DROP VIEW IF EXISTS VW_LocalAuthorityEducationHealthCarePlansDefaultCurrentPerPopulation
GO

CREATE VIEW VW_LocalAuthorityEducationHealthCarePlansDefaultCurrentPerPopulation
AS
    SELECT c.*,
        l.Name
    FROM LocalAuthority l
        LEFT JOIN VW_LocalAuthorityEducationHealthCarePlansDefaultPerPopulation c ON c.LaCode = l.Code
    WHERE c.RunId = (SELECT Value
    FROM Parameters
    WHERE Name = 'CurrentYear')
GO