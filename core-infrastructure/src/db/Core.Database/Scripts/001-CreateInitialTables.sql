IF NOT EXISTS(SELECT *
              FROM INFORMATION_SCHEMA.TABLES
              WHERE table_name = 'ComparatorSet')
    BEGIN
        CREATE TABLE dbo.ComparatorSet
        (
            RunType  nvarchar(50)  NOT NULL,
            RunId    nvarchar(50)  NOT NULL,
            URN      nvarchar(6)   NOT NULL,
            SetType  nvarchar(50)  NOT NULL,
            Pupil    nvarchar(max) NULL,
            Building nvarchar(max) NULL,

            CONSTRAINT PK_ComparatorSet PRIMARY KEY (RunType, RunId, URN, SetType)
        );
    END;

IF NOT EXISTS(SELECT *
              FROM INFORMATION_SCHEMA.TABLES
              WHERE table_name = 'UserDefinedComparatorSet')
    BEGIN
        CREATE TABLE dbo.UserDefinedComparatorSet
        (
            RunType nvarchar(50)  NOT NULL,
            RunId   nvarchar(50)  NOT NULL,
            URN     nvarchar(6)   NOT NULL,
            [Set]   nvarchar(max) NULL,

            CONSTRAINT PK_UserDefinedComparatorSet PRIMARY KEY (RunType, RunId, URN)
        );
    END;

IF NOT EXISTS(SELECT *
              FROM INFORMATION_SCHEMA.TABLES
              WHERE table_name = 'MetricRAG')
    BEGIN
        CREATE TABLE dbo.MetricRAG
        (
            RunType     nvarchar(50) NOT NULL,
            RunId       nvarchar(50) NOT NULL,
            URN         nvarchar(6)  NOT NULL,
            Category    nvarchar(50) NOT NULL,
            SubCategory nvarchar(50) NOT NULL,
            Value       decimal      NULL,
            Mean        decimal      NULL,
            DiffMean    decimal      NULL,
            PercentDiff decimal      NULL,
            Percentile  decimal      NULL,
            Decile      decimal      NULL,
            RAG         nvarchar(10) NOT NULL,

            CONSTRAINT PK_MetricRAG PRIMARY KEY (RunType, RunId, URN, Category, SubCategory)
        );
    END;

IF NOT EXISTS(SELECT *
              FROM INFORMATION_SCHEMA.TABLES
              WHERE table_name = 'FinancialPlan')
    BEGIN
        CREATE TABLE dbo.FinancialPlan
        (
            URN            nvarchar(6)    NOT NULL,
            Year           smallint       NOT NULL,
            Input          nvarchar(max)  NULL,
            DeploymentPlan nvarchar(max)  NULL,
            Created        datetimeoffset NOT NULL,
            CreatedBy      nvarchar(255)  NOT NULL,
            UpdatedAt      datetimeoffset NOT NULL,
            UpdatedBy      nvarchar(255)  NOT NULL,
            IsComplete     bit            NOT NULL,
            Version        int            NOT NULL,

            CONSTRAINT PK_FinancialPlan PRIMARY KEY (URN, Year),
        );
    END;

IF NOT EXISTS(SELECT *
              FROM INFORMATION_SCHEMA.TABLES
              WHERE table_name = 'LocalAuthority')
    BEGIN
        CREATE TABLE dbo.LocalAuthority
        (
            Code nvarchar(3)   NOT NULL,
            Name nvarchar(100) NOT NULL,

            CONSTRAINT PK_LocalAuthority PRIMARY KEY (Code)
        );
    END;

IF NOT EXISTS(SELECT *
              FROM INFORMATION_SCHEMA.TABLES
              WHERE table_name = 'TrustHistory')
    BEGIN
        CREATE TABLE dbo.TrustHistory
        (
            Id            integer identity (1,1) NOT NULL,
            CompanyNumber nvarchar(8)            NOT NULL,
            EventDate     date                   NOT NULL,
            EventName     nvarchar(100)          NOT NULL,
            AcademicYear  smallint               NOT NULL,
            SchoolURN     nvarchar(6)            NOT NULL,
            SchoolName    nvarchar(255)          NOT NULL,

            CONSTRAINT PK_TrustHistory PRIMARY KEY (Id),
        );

        CREATE INDEX TrustHistory_CompanyNumber ON TrustHistory (CompanyNumber)
    END;

IF NOT EXISTS(SELECT *
              FROM INFORMATION_SCHEMA.TABLES
              WHERE table_name = 'Trust')
    BEGIN
        CREATE TABLE dbo.Trust
        (
            CompanyNumber nvarchar(8)   NOT NULL,
            TrustName     nvarchar(255) NOT NULL,
            CFOName       nvarchar(255) NULL,
            CFOEmail      nvarchar(255) NULL,
            OpenDate      date          NULL,
            UID           nvarchar(50)  NULL,

            CONSTRAINT PK_Trust PRIMARY KEY (CompanyNumber),
        );
    END;

IF NOT EXISTS(SELECT *
              FROM INFORMATION_SCHEMA.TABLES
              WHERE table_name = 'School')
    BEGIN
        CREATE TABLE dbo.School
        (
            URN                nvarchar(6)   NOT NULL,
            SchoolName         nvarchar(255) NOT NULL,
            TrustCompanyNumber nvarchar(8)   NULL,
            TrustName          nvarchar(255) NULL,
            FederationLeadURN  nvarchar(6)   NULL,
            FederationLeadName nvarchar(255) NULL,
            LACode             nvarchar(3)   NULL,
            LAName             nvarchar(100) NULL,
            LondonWeighting    nvarchar(10)  NOT NULL,
            FinanceType        nvarchar(10)  NOT NULL,
            OverallPhase       nvarchar(50)  NOT NULL,
            SchoolType         nvarchar(50)  NOT NULL,
            HasSixthForm       bit           NOT NULL,
            HasNursery         bit           NOT NULL,
            IsPFISchool        bit           NOT NULL,
            OfstedDate         date          NULL,
            OfstedRating       tinyint       NULL,
            OfstedDescription  nvarchar(20)  NULL,
            Telephone          nvarchar(20)  NULL,
            Website            nvarchar(255) NULL,
            ContactEmail       nvarchar(255) NULL,
            HeadteacherName    nvarchar(255) NULL,
            HeadteacherEmail   nvarchar(255) NULL,

            CONSTRAINT PK_School PRIMARY KEY (URN),
        );

        CREATE INDEX School_TrustCompanyNumber ON School (TrustCompanyNumber)
        CREATE INDEX School_FederationLeadURN ON School (FederationLeadURN)
        CREATE INDEX School_LACode ON School (LACode)
    END;       