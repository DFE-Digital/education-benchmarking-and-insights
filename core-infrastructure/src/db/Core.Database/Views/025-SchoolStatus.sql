DROP VIEW IF EXISTS VW_SchoolStatus
GO

CREATE VIEW VW_SchoolStatus
AS
    WITH
        trustSchoolCount
            AS
            (
                SELECT s.TrustCompanyNumber,
                       COUNT(d.URN) AS SchoolsInTrust
                FROM School s
                         LEFT JOIN CurrentDefaultFinancial d ON s.URN = d.URN
                WHERE s.TrustCompanyNumber IS NOT NULL
                GROUP BY s.TrustCompanyNumber
            )
    SELECT s.URN,
           s.SchoolName,
           IIF(c.SchoolsInTrust IS NULL, 0, 1) AS IsPartOfTrust,
           CASE
               WHEN c.SchoolsInTrust IS NULL THEN 0
               WHEN c.SchoolsInTrust = 1 THEN 0
               ELSE 1
               END                             AS IsMat
    FROM School s
             LEFT JOIN VW_TrustFinancialDefaultCurrent f ON s.TrustCompanyNumber = f.CompanyNumber
             LEFT JOIN trustSchoolCount c ON c.TrustCompanyNumber = f.CompanyNumber
GO