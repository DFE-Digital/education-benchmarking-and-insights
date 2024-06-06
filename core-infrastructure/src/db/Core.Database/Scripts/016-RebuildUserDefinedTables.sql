IF EXISTS(SELECT *
          FROM INFORMATION_SCHEMA.TABLES
          WHERE table_name = 'UserData')
    BEGIN
        DROP TABLE dbo.UserData;
    END;


IF NOT EXISTS(SELECT *
              FROM INFORMATION_SCHEMA.TABLES
              WHERE table_name = 'UserData')
    BEGIN
        CREATE TABLE dbo.UserData
        (
            Id               nvarchar(50)   NOT NULL,
            Type             nvarchar(50)   NOT NULL,
            UserId           nvarchar(255)  NOT NULL,
            OrganisationType nvarchar(10)   NOT NULL,
            OrganisationId   nvarchar(8)    NOT NULL,
            Status           nvarchar(10)   NOT NULL,
            Expiry           datetimeoffset NOT NULL,

            CONSTRAINT PK_UserData PRIMARY KEY (Id)
        );
    END;

IF NOT EXISTS(SELECT *
              FROM INFORMATION_SCHEMA.TABLES
              WHERE table_name = 'UserDefinedComparatorSet')
    BEGIN
        DROP TABLE dbo.UserDefinedComparatorSet;
    END;

IF NOT EXISTS(SELECT *
              FROM INFORMATION_SCHEMA.TABLES
              WHERE table_name = 'UserDefinedSchoolComparatorSet')
    BEGIN
        CREATE TABLE dbo.UserDefinedSchoolComparatorSet
        (
            RunType nvarchar(50)  NOT NULL,
            RunId   nvarchar(50)  NOT NULL,
            URN     nvarchar(6)   NOT NULL,
            [Set]   nvarchar(max) NULL,

            CONSTRAINT PK_UserDefinedSchoolComparatorSet PRIMARY KEY (RunType, RunId, URN)
        );
    END;

IF NOT EXISTS(SELECT *
              FROM INFORMATION_SCHEMA.TABLES
              WHERE table_name = 'UserDefinedTrustComparatorSet')
    BEGIN
        CREATE TABLE dbo.UserDefinedTrustComparatorSet
        (
            RunType       nvarchar(50)  NOT NULL,
            RunId         nvarchar(50)  NOT NULL,
            CompanyNumber nvarchar(8)   NOT NULL,
            [Set]         nvarchar(max) NULL,

            CONSTRAINT PK_UserDefinedTrustComparatorSet PRIMARY KEY (RunType, RunId, CompanyNumber)
        );
    END;       