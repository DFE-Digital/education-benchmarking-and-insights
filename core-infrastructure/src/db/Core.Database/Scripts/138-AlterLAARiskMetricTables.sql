IF EXISTS (
    SELECT 1
    FROM INFORMATION_SCHEMA.TABLES
    WHERE table_name = 'LASchoolRiskIndicators'
)
BEGIN
ALTER TABLE dbo.LASchoolRiskIndicators
ALTER COLUMN RiskIndicatorValue            DECIMAL(18,2) NULL;

ALTER TABLE dbo.LASchoolRiskIndicators
ALTER COLUMN RiskIndicatorContribution     DECIMAL(18,2) NULL;

ALTER TABLE dbo.LASchoolRiskIndicators
ALTER COLUMN RiskIndicatorContributionMax  DECIMAL(18,2) NULL;
END;


IF EXISTS (
    SELECT 1
    FROM INFORMATION_SCHEMA.TABLES
    WHERE table_name = 'LASchoolRiskIndicatorsHeaders'
)
BEGIN
ALTER TABLE dbo.LASchoolRiskIndicatorsHeaders
ALTER COLUMN EducationalPerformance        DECIMAL(18,2) NULL;

ALTER TABLE dbo.LASchoolRiskIndicatorsHeaders
ALTER COLUMN EducationalPerformanceMax     DECIMAL(18,2) NULL;

ALTER TABLE dbo.LASchoolRiskIndicatorsHeaders
ALTER COLUMN Financial                     DECIMAL(18,2) NULL;

ALTER TABLE dbo.LASchoolRiskIndicatorsHeaders
ALTER COLUMN FinancialMax                  DECIMAL(18,2) NULL;

ALTER TABLE dbo.LASchoolRiskIndicatorsHeaders
ALTER COLUMN SchoolAndPupil                DECIMAL(18,2) NULL;

ALTER TABLE dbo.LASchoolRiskIndicatorsHeaders
ALTER COLUMN SchoolAndPupilMax             DECIMAL(18,2) NULL;

ALTER TABLE dbo.LASchoolRiskIndicatorsHeaders
ALTER COLUMN Overall                       DECIMAL(18,2) NULL;

ALTER TABLE dbo.LASchoolRiskIndicatorsHeaders
ALTER COLUMN OverallMax                    DECIMAL(18,2) NULL;
END;
