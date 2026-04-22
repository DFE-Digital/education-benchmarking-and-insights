IF EXISTS(SELECT 1
          FROM INFORMATION_SCHEMA.TABLES
          WHERE table_name = 'LocalAuthorityNonFinancial')
    BEGIN
    ALTER TABLE dbo.LocalAuthorityFinancial
        ADD BudgetSENTransportDSG             DECIMAL(16,2) NULL,
            BudgetHometoSchoolTransportPre16  DECIMAL(16,2) NULL,
            BudgetHometoSchoolTransport1618   DECIMAL(16,2) NULL,
            BudgetHometoSchoolTransport1925   DECIMAL(16,2) NULL,
            BudgetEdPsychologyService         DECIMAL(16,2) NULL,
            BudgetSENAdmin                    DECIMAL(16,2) NULL,
            OutturnSENTransportDSG            DECIMAL(16,2) NULL,
            OutturnHometoSchoolTransportPre16 DECIMAL(16,2) NULL,
            OutturnHometoSchoolTransport1618  DECIMAL(16,2) NULL,
            OutturnHometoSchoolTransport1925  DECIMAL(16,2) NULL,
            OutturnEdPsychologyService        DECIMAL(16,2) NULL,
            OutturnSENAdmin                   DECIMAL(16,2) NULL
    END;

