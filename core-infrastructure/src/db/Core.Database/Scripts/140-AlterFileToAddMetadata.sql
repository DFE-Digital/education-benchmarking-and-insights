IF EXISTS (
    SELECT 1
    FROM INFORMATION_SCHEMA.TABLES
    WHERE table_name = 'File'
)
BEGIN
    IF NOT EXISTS (
        SELECT 1
        FROM INFORMATION_SCHEMA.COLUMNS
        WHERE table_name = 'File'
          AND column_name = 'RunId'
    )
    BEGIN
    ALTER TABLE dbo.[File]
        ADD RunId nvarchar(50) NULL;
    END;

    IF NOT EXISTS (
        SELECT 1
        FROM INFORMATION_SCHEMA.COLUMNS
        WHERE table_name = 'File'
          AND column_name = 'FileSize'
    )
    BEGIN
    ALTER TABLE dbo.[File]
        ADD FileSize DECIMAL(18,2) NULL
    END;
END;
