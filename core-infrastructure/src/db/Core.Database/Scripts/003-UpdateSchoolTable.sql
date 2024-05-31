IF EXISTS(SELECT *
          FROM INFORMATION_SCHEMA.TABLES
          WHERE table_name = 'School')
    BEGIN
        ALTER TABLE dbo.School
            DROP COLUMN HeadteacherEmail;
        ALTER TABLE dbo.School
            DROP COLUMN HeadteacherName;
        ALTER TABLE dbo.School
            DROP COLUMN ContactEmail;

        ALTER TABLE dbo.School
            ADD AddressStreet nvarchar(100) NULL;
        ALTER TABLE dbo.School
            ADD AddressLocality nvarchar(100) NULL;
        ALTER TABLE dbo.School
            ADD AddressLine3 nvarchar(50) NULL;
        ALTER TABLE dbo.School
            ADD AddressTown nvarchar(50) NULL;
        ALTER TABLE dbo.School
            ADD AddressCounty nvarchar(50) NULL;
        ALTER TABLE dbo.School
            ADD AddressPostcode nvarchar(10) NULL;
    END;       