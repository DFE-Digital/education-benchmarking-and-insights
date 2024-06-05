IF EXISTS(SELECT 1
          FROM sys.views
          WHERE name = 'SchoolPhasesCoveredByTrust')
    BEGIN
        DROP VIEW SchoolPhasesCoveredByTrust
    END
GO

CREATE VIEW SchoolPhasesCoveredByTrust
AS
SELECT TrustCompanyNumber 'CompanyNumber', OverallPhase
FROM School
WHERE TrustCompanyNumber IS NOT NULL
GO


IF EXISTS(SELECT 1
          FROM sys.views
          WHERE name = 'SchoolsInTrust')
    BEGIN
        DROP VIEW SchoolsInTrust
    END
GO

CREATE VIEW SchoolsInTrust
AS
SELECT TrustCompanyNumber 'CompanyNumber', COUNT(URN) 'SchoolsInTrust' FROM School
WHERE TrustCompanyNumber IS NOT NULL
GROUP BY TrustCompanyNumber;
GO