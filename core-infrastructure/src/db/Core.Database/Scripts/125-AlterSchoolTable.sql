IF EXISTS(SELECT 1
          FROM INFORMATION_SCHEMA.TABLES
          WHERE table_name = 'School')
    BEGIN
    ALTER TABLE dbo.School
        ADD NurseryProvision nvarchar(50) NULL;
    ALTER TABLE dbo.School
        ADD SpecialClassProvision nvarchar(50) NULL;
    ALTER TABLE dbo.School
        ADD SixthFormProvision nvarchar(50) NULL;
    END;