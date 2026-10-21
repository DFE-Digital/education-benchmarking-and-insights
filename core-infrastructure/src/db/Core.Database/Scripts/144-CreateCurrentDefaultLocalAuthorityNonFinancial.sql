IF EXISTS(SELECT 1
          FROM sys.views
          WHERE name = 'CurrentDefaultLocalAuthorityNonFinancial')
    BEGIN
        DROP VIEW CurrentDefaultLocalAuthorityNonFinancial
    END
GO

CREATE VIEW CurrentDefaultLocalAuthorityNonFinancial AS
SELECT *
FROM LocalAuthorityNonFinancial
WHERE RunType = 'default'
    AND RunId = (SELECT Value FROM Parameters WHERE Name = 'CurrentYear')
GO
