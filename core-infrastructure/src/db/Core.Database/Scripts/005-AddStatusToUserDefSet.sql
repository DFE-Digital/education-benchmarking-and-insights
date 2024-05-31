IF EXISTS(SELECT *
          FROM INFORMATION_SCHEMA.TABLES
          WHERE table_name = 'UserDefinedComparatorSet')
    BEGIN
        ALTER TABLE dbo.UserDefinedComparatorSet
            ADD Status nvarchar(10) NULL;
    END