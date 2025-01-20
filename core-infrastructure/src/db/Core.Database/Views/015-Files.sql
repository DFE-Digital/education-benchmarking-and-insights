DROP VIEW IF EXISTS VW_TransparencyFilesAar
GO

CREATE VIEW VW_TransparencyFilesAar AS
SELECT [Label], [FileName]
FROM [dbo].[File]
WHERE [Type] = 'transparency-aar'
  AND [ValidFrom] < GETUTCDATE()
  AND ([ValidTo] IS NULL OR [ValidTo] > GETUTCDATE())
GO

DROP VIEW IF EXISTS VW_TransparencyFilesCfr
GO

CREATE VIEW VW_TransparencyFilesCfr AS
SELECT [Label], [FileName]
FROM [dbo].[File]
WHERE [Type] = 'transparency-cfr'
  AND [ValidFrom] < GETUTCDATE()
  AND ([ValidTo] IS NULL OR [ValidTo] > GETUTCDATE())
GO