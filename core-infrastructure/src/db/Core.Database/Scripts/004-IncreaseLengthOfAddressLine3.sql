IF EXISTS(SELECT *
          FROM INFORMATION_SCHEMA.TABLES
          WHERE table_name = 'School')
    BEGIN
        ALTER TABLE dbo.School ALTER COLUMN AddressLine3 nvarchar(255) NULL; 
    END