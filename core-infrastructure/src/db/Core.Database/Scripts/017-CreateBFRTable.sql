IF NOT EXISTS(SELECT *
              FROM INFORMATION_SCHEMA.TABLES
              WHERE table_name = 'BFR')
    BEGIN
        CREATE TABLE dbo.ComparatorSet
        (
            RunType         nvarchar(50)  NOT NULL,
            RunId           nvarchar(50)  NOT NULL,
            TrustUPIN       nvarchar(6)   NOT NULL,

            CONSTRAINT PK_ComparatorSet PRIMARY KEY (RunType, RunId, TrustUPIN)
        );
    END;
