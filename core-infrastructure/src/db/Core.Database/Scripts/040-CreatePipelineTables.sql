IF NOT EXISTS(SELECT *
              FROM INFORMATION_SCHEMA.TABLES
              WHERE table_name = 'CompletedPipelineRun')
    BEGIN
        CREATE TABLE dbo.CompletedPipelineRun
        (
            Id              integer identity (1,1) NOT NULL,
            OrchestrationId nvarchar(50)           NULL,
            CompletedAt     datetimeoffset         NOT NULL,
            Message         nvarchar(max)          NULL,

            CONSTRAINT PK_CompletedPipelineRun PRIMARY KEY (Id),
        );

        CREATE INDEX CompletedPipelineRun_OrchestrationId ON CompletedPipelineRun (OrchestrationId)
    END;
