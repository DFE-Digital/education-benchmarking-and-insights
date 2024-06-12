IF EXISTS(SELECT *
          FROM INFORMATION_SCHEMA.TABLES
          WHERE table_name = 'FinancialPlan')
    BEGIN
        ALTER TABLE dbo.FinancialPlan
            ADD TeacherContactRatio decimal NULL;
        
        ALTER TABLE dbo.FinancialPlan
            ADD ContactRatioRating nvarchar(5) NULL;
        
        ALTER TABLE dbo.FinancialPlan
            ADD InYearBalance decimal NULL;
        
        ALTER TABLE dbo.FinancialPlan
            ADD InYearBalancePercentIncomeRating nvarchar(5) NULL;
        
        ALTER TABLE dbo.FinancialPlan
            ADD AverageClassSize decimal NULL;
        
        ALTER TABLE dbo.FinancialPlan
            ADD AverageClassSizeRating nvarchar(5) NULL;
    END;      