IF EXISTS(SELECT 1
          FROM sys.views
          WHERE name = 'AARHistoryYears')
    BEGIN
        DROP VIEW AARHistoryYears
    END
GO

CREATE VIEW AARHistoryYears
AS
SELECT Value 'Year' FROM Parameters WHERE Name = 'LatestAARYear'
UNION
SELECT Value-1 'Year' FROM Parameters WHERE Name = 'LatestAARYear'
UNION
SELECT Value-2 'Year' FROM Parameters WHERE Name = 'LatestAARYear'
UNION
SELECT Value-3 'Year' FROM Parameters WHERE Name = 'LatestAARYear'
UNION
SELECT Value-4 'Year' FROM Parameters WHERE Name = 'LatestAARYear'
GO


IF EXISTS(SELECT 1
          FROM sys.views
          WHERE name = 'CFRHistoryYears')
    BEGIN
        DROP VIEW CFRHistoryYears
    END
GO

CREATE VIEW CFRHistoryYears
AS
SELECT Value 'Year' FROM Parameters WHERE Name = 'LatestCFRYear'
UNION
SELECT Value-1 'Year' FROM Parameters WHERE Name = 'LatestCFRYear'
UNION
SELECT Value-2 'Year' FROM Parameters WHERE Name = 'LatestCFRYear'
UNION
SELECT Value-3 'Year' FROM Parameters WHERE Name = 'LatestCFRYear'
UNION
SELECT Value-4 'Year' FROM Parameters WHERE Name = 'LatestCFRYear'
GO