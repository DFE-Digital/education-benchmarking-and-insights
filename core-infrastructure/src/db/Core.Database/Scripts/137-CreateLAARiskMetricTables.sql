IF NOT EXISTS(SELECT *
              FROM INFORMATION_SCHEMA.TABLES
              WHERE table_name = 'LASchoolRiskIndicators')
    BEGIN
        CREATE TABLE dbo.LASchoolRiskIndicators
        (
            URN                        nvarchar(6)  NOT NULL,
            RunId                      nvarchar(50) NOT NULL,
            RiskGroup                  nvarchar(50) NULL,
            RiskIndicator              nvarchar(50) NOT NULL,
            RiskIndicatorValue         decimal      NULL,
            RiskIndicatorFlag          nvarchar(50) NULL,
            RiskIndicatorContribution    decimal      NULL,
            RiskIndicatorContributionMax decimal      NULL,

            CONSTRAINT PK_LASchoolRiskIndicators PRIMARY KEY (URN, RunId, RiskIndicator)
        );
    END;

IF NOT EXISTS(SELECT *
              FROM INFORMATION_SCHEMA.TABLES
              WHERE table_name = 'LASchoolRiskIndicatorsHeaders')
    BEGIN
        CREATE TABLE dbo.LASchoolRiskIndicatorsHeaders
        (
            URN                        nvarchar(6)  NOT NULL,
            RunId                      nvarchar(50) NOT NULL,
            EducationalPerformance     decimal      NULL,
            EducationalPerformanceMax  decimal      NULL,
            Financial                  decimal      NULL,
            FinancialMax               decimal      NULL,
            SchoolAndPupil             decimal      NULL,
            SchoolAndPupilMax          decimal      NULL,
            Overall                    decimal      NULL,
            OverallMax                 decimal      NULL,
            OverallGrade               nvarchar(50) NULL,

            CONSTRAINT PK_LASchoolRiskIndicatorsHeaders PRIMARY KEY (URN, RunId)
        );
    END;
