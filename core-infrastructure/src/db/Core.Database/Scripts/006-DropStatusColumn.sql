IF EXISTS(SELECT *
          FROM INFORMATION_SCHEMA.COLUMNS
          WHERE table_name = 'UserDefinedComparatorSet'
            AND column_name = 'Status')
    BEGIN
        ALTER TABLE dbo.UserDefinedComparatorSet
            DROP COLUMN Status;
    END
GO