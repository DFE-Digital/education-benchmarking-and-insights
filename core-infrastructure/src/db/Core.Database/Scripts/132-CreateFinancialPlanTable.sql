I will read `artifact_01_schema.md` to verify the existing schema and confirm if any additional indexes might be beneficial despite the optimization report's conclusion.
-- Migration: Create FinancialPlan table and ensure optimal indexing

-- 1. Create FinancialPlan table (Idempotent)
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[FinancialPlan]') AND type in (N'U'))
BEGIN
    CREATE TABLE [dbo].[FinancialPlan]
    (
        [URN]                              NVARCHAR(6)    NOT NULL,
        [Year]                             SMALLINT       NOT NULL,
        [Input]                            NVARCHAR(MAX)  NULL,
        [DeploymentPlan]                   NVARCHAR(MAX)  NULL,
        [Created]                          DATETIMEOFFSET NOT NULL,
        [CreatedBy]                        NVARCHAR(255)  NOT NULL,
        [UpdatedAt]                        DATETIMEOFFSET NOT NULL,
        [UpdatedBy]                        NVARCHAR(255)  NOT NULL,
        [IsComplete]                       BIT            NOT NULL,
        [Version]                          INT            NOT NULL,
        [TeacherContactRatio]              DECIMAL(16, 2) NULL,
        [ContactRatioRating]               NVARCHAR(5)    NULL,
        [InYearBalance]                    DECIMAL(16, 2) NULL,
        [InYearBalancePercentIncomeRating] NVARCHAR(5)    NULL,
        [AverageClassSize]                 DECIMAL(16, 2) NULL,
        [AverageClassSizeRating]           NVARCHAR(5)    NULL,
        CONSTRAINT [PK_FinancialPlan] PRIMARY KEY CLUSTERED ([URN] ASC, [Year] ASC)
    );
END
GO

-- 2. Recommended Indexes
-- The optimization review (artifact_03_optimization.md) confirms that the 
-- Primary Keys (PK_School and PK_FinancialPlan) provide optimal performance 
-- for the current search predicates. No additional non-clustered indexes are 
-- recommended at this time.

PRINT 'Migration complete: FinancialPlan table ensured. No additional indexes required.';
GO

/*
-- ROLLBACK SECTION --

IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[FinancialPlan]') AND type in (N'U'))
BEGIN
    DROP TABLE [dbo].[FinancialPlan];
END
GO

PRINT 'Rollback complete: FinancialPlan table removed.';
GO
*/
```
