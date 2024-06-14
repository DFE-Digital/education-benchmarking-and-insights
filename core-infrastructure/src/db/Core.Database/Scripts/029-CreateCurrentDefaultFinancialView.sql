IF EXISTS(SELECT 1
          FROM sys.views
          WHERE name = 'CurrentDefaultFinancial')
    BEGIN
        DROP VIEW CurrentDefaultFinancial
    END
GO

CREATE VIEW CurrentDefaultFinancial
AS
SELECT *
FROM Financial
WHERE RunType = 'default'
  AND RunId = (SELECT Value FROM Parameters WHERE Name = 'CurrentYear')
GO