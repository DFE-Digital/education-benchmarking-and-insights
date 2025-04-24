DROP VIEW IF EXISTS VW_LocalAuthoritySummary
GO

CREATE VIEW VW_LocalAuthoritySummary
AS
    SELECT Code,
           Name,
           -- Adds a lowercased Name column to support case-insensitive sorting in Azure Search.
           -- This field is included in the NameIndex for optional use in the 'orderBy' field of the POST body 
           -- for consumers such as the POST /api/local-authorities/search endpoint.
           -- This avoids relying on Azure Search normalizers, which are still in public preview as of April 2025.
           -- See: https://learn.microsoft.com/en-us/azure/search/search-normalizers
           LOWER(Name) AS LocalAuthorityNameSortable
    FROM LocalAuthority
GO