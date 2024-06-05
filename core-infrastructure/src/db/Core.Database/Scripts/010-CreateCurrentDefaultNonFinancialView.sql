IF EXISTS(SELECT 1
          FROM sys.views
          WHERE name = 'CurrentDefaultNonFinancial')
    BEGIN
        DROP VIEW CurrentDefaultNonFinancial
    END
GO

CREATE VIEW CurrentDefaultNonFinancial
AS
SELECT *
FROM NonFinancial
WHERE RunType = 'default'
  AND RunId = (SELECT Value FROM Parameters WHERE Name = 'CurrentYear')
GO