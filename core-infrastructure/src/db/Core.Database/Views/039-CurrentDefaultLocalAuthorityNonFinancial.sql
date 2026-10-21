DROP VIEW IF EXISTS VW_CurrentDefaultLocalAuthorityNonFinancial
    GO

CREATE VIEW VW_CurrentDefaultLocalAuthorityNonFinancial AS
SELECT *
    FROM LocalAuthorityNonFinancial
    WHERE RunType = 'default'
    AND RunId = (SELECT Value FROM Parameters WHERE Name = 'CurrentYear')
GO
