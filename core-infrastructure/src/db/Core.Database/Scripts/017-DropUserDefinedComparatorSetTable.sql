IF EXISTS(SELECT *
              FROM INFORMATION_SCHEMA.TABLES
              WHERE table_name = 'UserDefinedComparatorSet')
    BEGIN
        DROP TABLE dbo.UserDefinedComparatorSet;
    END;