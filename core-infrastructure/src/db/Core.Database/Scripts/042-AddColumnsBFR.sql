IF EXISTS(SELECT *
          FROM INFORMATION_SCHEMA.TABLES
          WHERE table_name = 'Trust')
    BEGIN
        ALTER TABLE dbo.Trust
            ADD TrustUPIN nvarchar(6) NULL;
    END;  