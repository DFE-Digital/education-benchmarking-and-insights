IF NOT EXISTS(SELECT *
              FROM INFORMATION_SCHEMA.TABLES
              WHERE table_name = 'MetricRAG')
    BEGIN
        CREATE TABLE dbo.MetricRAG
        (
            RunType     nvarchar(50) NOT NULL,
            RunId       nvarchar(50) NOT NULL,
            UKPRN       nvarchar(50) NOT NULL,
            Category    nvarchar(50) NOT NULL,
            SubCategory nvarchar(50) NOT NULL,
            Value       decimal      NULL,
            Mean        decimal      NULL,
            DiffMean    decimal      NULL,
            PercentDiff decimal      NULL,
            Percentile  decimal      NULL,
            Decile      decimal      NULL,
            RAG         nvarchar(10) NOT NULL,

            CONSTRAINT PK_MetricRAG PRIMARY KEY (RunType, RunId, UKPRN, Category, SubCategory)
        );
    END