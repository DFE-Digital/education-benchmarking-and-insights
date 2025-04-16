DROP VIEW IF EXISTS VW_TrustSummary
GO

CREATE VIEW VW_TrustSummary
AS
    WITH trustSchoolCount AS (
        SELECT s.TrustCompanyNumber,
               COUNT(d.URN) AS SchoolsInTrust
        FROM School s
            LEFT JOIN CurrentDefaultFinancial d ON s.URN = d.URN
        WHERE s.TrustCompanyNumber IS NOT NULL
        GROUP BY s.TrustCompanyNumber
    )
    SELECT t.CompanyNumber,
           t.TrustName,
           CONVERT(float, f.TotalPupils) AS TotalPupils,
           CONVERT(float, s.SchoolsInTrust) AS SchoolsInTrust,
           -- Adds a lowercased TrustName column to support case-insensitive sorting in Azure Search.
           -- This field is included in the TrustIndex for optional use in the 'orderBy' field of the POST body 
           -- for consumers such as the POST /api/trusts/search endpoint.
           -- This avoids relying on Azure Search normalizers, which are still in public preview as of April 2025.
           -- See: https://learn.microsoft.com/en-us/azure/search/search-normalizers
           LOWER(t.TrustName) AS TrustNameSortable
    FROM Trust t
        LEFT JOIN VW_TrustFinancialDefaultCurrent f ON f.CompanyNumber = t.CompanyNumber
        LEFT JOIN trustSchoolCount s ON s.TrustCompanyNumber = t.CompanyNumber
GO