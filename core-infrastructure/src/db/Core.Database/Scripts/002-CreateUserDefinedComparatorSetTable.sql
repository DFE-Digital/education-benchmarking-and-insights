IF NOT EXISTS(SELECT *
              FROM INFORMATION_SCHEMA.TABLES
              WHERE table_name = 'UserDefinedComparatorSet')
    BEGIN
        CREATE TABLE dbo.UserDefinedComparatorSet
        (
            RunType nvarchar(50)  NOT NULL,
            RunId   nvarchar(50)  NOT NULL,
            UKPRN   nvarchar(50)  NOT NULL,
            [Set]   nvarchar(max) NULL,

            CONSTRAINT PK_UserDefinedComparatorSet PRIMARY KEY (RunType, RunId, UKPRN)
        );
    END