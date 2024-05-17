IF NOT EXISTS(SELECT *
               FROM INFORMATION_SCHEMA.TABLES
               WHERE table_name = 'ComparatorSet')
    BEGIN
        CREATE TABLE dbo.ComparatorSet
        (
            RunType  nvarchar(50)  NOT NULL,
            RunId    nvarchar(50)  NOT NULL,
            UKPRN    nvarchar(50)  NOT NULL,
            SetType  nvarchar(50)  NOT NULL,
            Pupil    nvarchar(max) NULL,
            Building nvarchar(max) NULL,

            CONSTRAINT PK_ComparatorSet PRIMARY KEY (RunType, RunId, UKPRN, SetType)
        );
    END