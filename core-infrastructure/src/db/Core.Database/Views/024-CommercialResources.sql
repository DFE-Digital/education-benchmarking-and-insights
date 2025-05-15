DROP VIEW IF EXISTS VW_CommercialResources
    GO

CREATE VIEW VW_CommercialResources AS
    SELECT [Category], [SubCategory], [Title], [Url]
    FROM [dbo].[CommercialResources]
    WHERE [ValidFrom] < GETUTCDATE()
    AND ([ValidTo] IS NULL OR [ValidTo] > GETUTCDATE())
GO