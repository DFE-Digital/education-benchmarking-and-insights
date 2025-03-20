IF EXISTS(SELECT 1
          FROM sys.views
          WHERE name = 'FileIntegrity')
    BEGIN
        DROP VIEW FileIntegrity
    END
GO

CREATE VIEW FileIntegrity AS
  SELECT DatasetName,
         DatasetYear,
         FileName,
         FileSizeBytes,
         HashValue,
         HashAlgorithm,
         Description,
         CreatedAt
    FROM (
           SELECT DatasetName,
                  DatasetYear,
                  FileName,
                  FileSizeBytes,
                  HashValue,
                  HashAlgorithm,
                  Description,
                  CreatedAt,
                  Row_Number() OVER (PARTITION BY DatasetName ORDER BY DatasetYear DESC) AS rn
             FROM FileIntegrityHistory
         ) AS RankedHistory
     WHERE rn = 1
GO
