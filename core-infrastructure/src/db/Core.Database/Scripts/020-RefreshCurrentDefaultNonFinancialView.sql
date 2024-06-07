ALTER VIEW CurrentDefaultNonFinancial
AS
SELECT *
FROM NonFinancial
WHERE RunType = 'default'
  AND RunId = (SELECT Value FROM Parameters WHERE Name = 'CurrentYear')
GO