DROP VIEW IF EXISTS VW_PublishedNews
GO

CREATE VIEW VW_PublishedNews
AS
SELECT [Title], [Slug], [Body], [Published]
FROM [News]
WHERE [Published] IS NOT NULL
  AND [Published] < GETUTCDATE()
  AND ([Archived] IS NULL OR [Archived] > GETUTCDATE())
GO