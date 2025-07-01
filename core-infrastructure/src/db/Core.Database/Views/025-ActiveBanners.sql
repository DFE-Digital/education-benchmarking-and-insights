DROP VIEW IF EXISTS VW_ActiveBanners
GO

CREATE VIEW VW_ActiveBanners
AS
SELECT [Title], [Heading], [Body], t.[TargetArrayItem] AS [Target], [ValidFrom]
FROM [dbo].[Banner] b
  CROSS APPLY (SELECT *
    FROM OPENJSON(b.[Target]) WITH ([TargetArrayItem] NVARCHAR(100) '$')) t
WHERE [ValidFrom] < GETUTCDATE()
  AND ([ValidTo] IS NULL OR [ValidTo] > GETUTCDATE())
GO