DROP VIEW IF EXISTS VW_ActiveFiles
GO

CREATE VIEW VW_ActiveFiles AS
SELECT [Type], [Label], [FileName]
FROM [dbo].[File]
WHERE [ValidFrom] < GETUTCDATE()
  AND ([ValidTo] IS NULL OR [ValidTo] > GETUTCDATE())
GO