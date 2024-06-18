IF NOT EXISTS(SELECT *
              FROM INFORMATION_SCHEMA.TABLES
              WHERE table_name = 'BudgetForecastReturns')
    BEGIN
        CREATE TABLE dbo.BudgetForecastReturns
        (
            RunType             nvarchar(50)  NOT NULL,
            RunId               nvarchar(50)  NOT NULL,
            TrustUPIN           nvarchar(6)   NOT NULL,
            Title               nvarchar(50)  NULL
            Y1P1                decimal       NULL  
            Y1P2                decimal       NULL  
            Y2P1                decimal       NULL  
            Y2P2                decimal       NULL  
            Y1                  decimal       NULL  
            Y2                  decimal       NULL  
            Y3                  decimal       NULL  
            Y4                  decimal       NULL  
            TrustBalance        decimal       NULL  
            Volatility          decimal       NULL  
            VolatilityStatus    varchar(50)   NULL
            CONSTRAINT PK_BudgetForecastReturns PRIMARY KEY (RunType, RunId, TrustUPIN)
        );
    END;


IF NOT EXISTS(SELECT *
              FROM INFORMATION_SCHEMA.TABLES
              WHERE table_name = 'BudgetForecastReturnsMetrics')
    BEGIN
        CREATE TABLE dbo.BudgetForecastReturnsMetrics
        (
            RunType                              nvarchar(50)  NOT NULL,
            RunId                                nvarchar(50)  NOT NULL,
            TrustUPIN                            nvarchar(6)   NOT NULL,
            RevenueReserveAsPercentageOfIncome   decimal       NULL
            StaffCostsAsPercentageOfIncome       decimal       NULL  
            ExpenditureAsPercentageOfIncome      decimal       NULL  
            PercentSelfGeneratedIncome           decimal       NULL  
            PercentGrantFunding                  decimal       NULL  
            RevenueReservesYearMinus2            decimal       NULL  
            RevenueReservesYearMinus1            decimal       NULL  
            RevenueReservesYear0                 decimal       NULL  
            RevenueReservesYear1                 decimal       NULL  
            RevenueReservesYear2                 decimal       NULL  
            RevenueReservesSlope                 decimal       NULL  
            RevenueReservesSlopeFlag             smallint      NULL  
            RevenueReservesYearMinus2PerPupil    decimal       NULL  
            RevenueReservesYearMinus1PerPupil    decimal       NULL  
            RevenueReservesYear0PerPupil         decimal       NULL  
            RevenueReservesYear1PerPupil         decimal       NULL  
            RevenueReservesYear2PerPupil         decimal       NULL  
            RevenueReservesSlopePerPupil         decimal       NULL  
            RevenueReservesSlopeFlagPerPupil     smallint      NULL  



            CONSTRAINT PK_BudgetForecastReturnsMetrics PRIMARY KEY (RunType, RunId, TrustUPIN)
        );
    END;