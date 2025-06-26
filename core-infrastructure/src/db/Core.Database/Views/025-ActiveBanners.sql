DROP VIEW IF EXISTS VW_ActiveBanners
GO

CREATE VIEW VW_ActiveBanners
AS
  SELECT [Title], [Body], [Target], [ValidFrom]
  FROM [dbo].[Banner]
  WHERE [ValidFrom] < GETUTCDATE()
    AND ([ValidTo] IS NULL OR [ValidTo] > GETUTCDATE())
GO