```sql
TABLE FinancialPlan
(
    URN                            nvarchar(6)    NOT NULL,
    Year                           smallint       NOT NULL,
    Input                          nvarchar(max)  NULL,
    DeploymentPlan                 nvarchar(max)  NULL,
    Created                        datetimeoffset NOT NULL,
    CreatedBy                      nvarchar(255)  NOT NULL,
    UpdatedAt                      datetimeoffset NOT NULL,
    UpdatedBy                      nvarchar(255)  NOT NULL,
    IsComplete                     bit            NOT NULL,
    Version                        int            NOT NULL,
    TeacherContactRatio            decimal(16, 2) NULL,
    ContactRatioRating             nvarchar(5)    NULL,
    InYearBalance                  decimal(16, 2) NULL,
    InYearBalancePercentIncomeRating nvarchar(5)    NULL,
    AverageClassSize               decimal(16, 2) NULL,
    AverageClassSizeRating         nvarchar(5)    NULL,
    CONSTRAINT PK_FinancialPlan PRIMARY KEY (URN, Year)
);

TABLE School
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
    OfstedDescription  nvarchar(20)  NULL,
    Telephone          nvarchar(20)  NULL,
    Website            nvarchar(255) NULL,
    AddressStreet      nvarchar(100) NULL,
    AddressLocality    nvarchar(100) NULL,
    AddressLine3       nvarchar(255) NULL,
    AddressTown        nvarchar(50)  NULL,
    AddressCounty      nvarchar(50)  NULL,
    AddressPostcode    nvarchar(10)  NULL,
    NurseryProvision   nvarchar(50)  NULL,
    SpecialClassProvision nvarchar(50) NULL,
    SixthFormProvision nvarchar(50)  NULL,
    CONSTRAINT PK_School PRIMARY KEY (URN)
);
```
