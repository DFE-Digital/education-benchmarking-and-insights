DROP VIEW IF EXISTS VW_SchoolSummary
GO

CREATE VIEW VW_SchoolSummary
AS
    SELECT s.URN,
           s.SchoolName,
           s.AddressStreet,
           s.AddressLocality,
           s.AddressLine3,
           s.AddressTown,
           s.AddressCounty,
           s.AddressPostcode,
           s.OverallPhase,
           f.PeriodCoveredByReturn,
           CONVERT(float, f.TotalPupils) AS TotalPupils,
           -- Adds a lowercased SchoolName column to support case-insensitive sorting in Azure Search.
           -- This field is included in the SchoolIndex for optional use in the 'orderBy' field of the POST body 
           -- for consumers such as the POST /api/schools/search endpoint.
           -- This avoids relying on Azure Search normalizers, which are still in public preview as of April 2025.
           -- See: https://learn.microsoft.com/en-us/azure/search/search-normalizers
           LOWER(s.SchoolName) AS SchoolNameSortable
    FROM School s
             LEFT JOIN CurrentDefaultFinancial f on f.URN = s.URN
GO