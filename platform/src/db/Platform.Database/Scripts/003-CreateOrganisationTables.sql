IF NOT EXISTS(SELECT *
              FROM INFORMATION_SCHEMA.TABLES
              WHERE table_name = 'Trust')
    BEGIN
        CREATE TABLE dbo.Trust
        (
            CompanyNumber nvarchar(8)    NOT NULL,
            Name          nvarchar(255)  NOT NULL,
            Created       datetimeoffset NOT NULL,
            CreatedBy     nvarchar(255)  NOT NULL,
            UpdatedAt     datetimeoffset NOT NULL,
            UpdatedBy     nvarchar(255)  NOT NULL,
            Deleted       bit            NOT NULL,

            CONSTRAINT PK_Trust PRIMARY KEY (CompanyNumber)
        );
    END

IF NOT EXISTS(SELECT *
              FROM INFORMATION_SCHEMA.TABLES
              WHERE table_name = 'School')
    BEGIN
        CREATE TABLE dbo.School
        (
            URN           nvarchar(6)    NOT NULL,
            Name          nvarchar(255)  NOT NULL,
            FinanceType   nvarchar(100)  NOT NULL,
            Kind          nvarchar(100)  NOT NULL,
            Street        nvarchar(255),
            Locality      nvarchar(255),
            Address3      nvarchar(255),
            Town          nvarchar(100),
            County        nvarchar(100),
            Postcode      nvarchar(10),
            CompanyNumber nvarchar(8),
            Created       datetimeoffset NOT NULL,
            CreatedBy     nvarchar(255)  NOT NULL,
            UpdatedAt     datetimeoffset NOT NULL,
            UpdatedBy     nvarchar(255)  NOT NULL,
            Version       int            NOT NULL,
            Deleted       bit            NOT NULL,

            CONSTRAINT PK_School PRIMARY KEY (URN),
            CONSTRAINT FK_School_Trust FOREIGN KEY (CompanyNumber) REFERENCES Trust
        );
    END



IF NOT EXISTS(SELECT *
              FROM INFORMATION_SCHEMA.TABLES
              WHERE table_name = 'TrustHistory')
    BEGIN
        CREATE TABLE dbo.TrustHistory
        (
            Id            integer identity (1,1) NOT NULL,
            CompanyNumber nvarchar(8)            NOT NULL,
            SchoolYear    nvarchar(10)           NOT NULL,
            Date          date                   NOT NULL,
            Event         nvarchar(100)          NOT NULL,
            URN           nvarchar(6)            NOT NULL,
            Name          nvarchar(255)          NOT NULL,
            Created       datetimeoffset         NOT NULL,
            CreatedBy     nvarchar(255)          NOT NULL,
            UpdatedAt     datetimeoffset         NOT NULL,
            UpdatedBy     nvarchar(255)          NOT NULL,
            Version       int                    NOT NULL,
            Deleted       bit                    NOT NULL,

            CONSTRAINT PK_TrustHistory PRIMARY KEY (Id),
            CONSTRAINT FK_TrustHistory_School FOREIGN KEY (URN) REFERENCES School,
            CONSTRAINT FK_TrustHistory_Trust FOREIGN KEY (CompanyNumber) REFERENCES Trust
        );
    END

IF NOT EXISTS(SELECT *
              FROM INFORMATION_SCHEMA.TABLES
              WHERE table_name = 'LocalAuthority')
    BEGIN
        CREATE TABLE dbo.LocalAuthority
        (
            Code      nvarchar(3)    NOT NULL,
            Name      nvarchar(100)  NOT NULL,
            Created   datetimeoffset NOT NULL,
            CreatedBy nvarchar(255)  NOT NULL,
            UpdatedAt datetimeoffset NOT NULL,
            UpdatedBy nvarchar(255)  NOT NULL,
            Version   int            NOT NULL,
            Deleted   bit            NOT NULL,

            CONSTRAINT PK_LocalAuthority PRIMARY KEY (Code)
        );
    END   