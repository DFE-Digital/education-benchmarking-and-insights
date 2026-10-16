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
        ADD RunId nvarchar(50) NOT NULL CONSTRAINT DF_File_RunId DEFAULT 'DEFAULT';
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

    IF EXISTS (
        SELECT 1
        FROM INFORMATION_SCHEMA.TABLE_CONSTRAINTS
        WHERE CONSTRAINT_NAME = 'PK_File'
          AND CONSTRAINT_TYPE = 'PRIMARY KEY'
    )
    BEGIN
        IF NOT EXISTS (
            SELECT 1
            FROM INFORMATION_SCHEMA.KEY_COLUMN_USAGE
            WHERE CONSTRAINT_NAME = 'PK_File'
              AND COLUMN_NAME = 'RunId'
        )
        BEGIN
            ALTER TABLE dbo.[File]
                DROP CONSTRAINT PK_File;

            ALTER TABLE dbo.[File]
                ADD CONSTRAINT PK_File PRIMARY KEY (Type, Label, RunId);
        END;
    END;
END;
