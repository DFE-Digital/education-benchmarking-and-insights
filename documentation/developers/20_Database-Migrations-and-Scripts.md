# Database Migrations and Scripts

This guide sets out the standards and examples for creating safe, defensive, and idempotent database migration scripts within the FBIT repository.

## Core Principles

Database deployments are complex and rarely follow a perfectly linear or clean path. To ensure reliability across all environments, all scripts must adhere to these foundational principles:

1. **Defensive Deployments**: Deployments aren't always clean or linear. Even with tracking tables ensuring scripts only run once, failures mid-deployment can leave things in a partially applied state. A defensive script provides a safer re-run path without needing manual intervention.
2. **Partial Execution Safety**: If a script fails halfway through (e.g., table created but index not, or column added but data not migrated), a non-idempotent script can make recovery harder. Guards like `IF EXISTS` / `IF NOT EXISTS` help us resume safely from the point of failure.
3. **Environment Drift Resilience**: Lower environments (and occasionally production via hotfixes or manual changes) don't always perfectly match expectations. Defensive scripts reduce the chance of unexpected failures due to that drift.
4. **Operational Safety > Purity**: While scripts should ideally run once and fail loudly in development, the cost of a failed deployment in production is high. Production-ready scripts must be resilient and idempotent.
5. **Consistency**: Regardless of the specific implementation details, maintaining a clear and enforced standard across the codebase is critical to reducing risk and confusion.

## Handling Exceptions

While idempotency is the standard, there may be rare cases where a script cannot follow these rules (e.g., a complex data migration that must run as a single atomic transaction). In these cases, follow this protocol:

1. **Document the "Why"**: Use SQL comments at the top of the script to explicitly state why the standard defensive pattern is not being used.
    *Example: `-- EXCEPTION: This script performs a one-way data transformation that cannot be easily guarded.`*
2. **Flag in Pull Request**: Explicitly mention the exception in the PR description so reviewers can give it extra scrutiny.
3. **Define a Recovery Path**: If the script is non-idempotent and fails halfway through, provide instructions (in the PR or as comments in the script) on how to manually recover or "reset" the state.

## Standards by Directory

### `Scripts/` Directory (One-time Migrations)

Scripts in this directory are executed once and tracked in the `SchemaVersions` table. They perform DDL (schema changes) or DML (data changes).

**Standard:** All scripts must be **idempotent**. Every logical change (creating a table, adding a column, adding an index) must be wrapped in a defensive guard.

#### 1. Creating a Table

```sql
IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'MyNewTable' AND schema_id = SCHEMA_ID('dbo'))
BEGIN
    CREATE TABLE dbo.MyNewTable (
        Id INT PRIMARY KEY IDENTITY(1,1),
        Name NVARCHAR(255) NOT NULL,
        CreatedAt DATETIME2 DEFAULT GETUTCDATE()
    );
END
GO
```

#### 2. Adding a Column

```sql
IF NOT EXISTS (SELECT * FROM sys.columns WHERE Name = N'MyNewColumn' AND Object_ID = Object_ID(N'dbo.MyExistingTable'))
BEGIN
    ALTER TABLE dbo.MyExistingTable ADD MyNewColumn NVARCHAR(100) NULL;
END
GO
```

#### 3. Adding/Modifying Multiple Columns

**Crucial:** Each column change must be wrapped in its own individual guard. This allows the script to partially succeed and be safely re-run if it fails mid-execution.

```sql
IF NOT EXISTS (SELECT * FROM sys.columns WHERE Name = N'ColumnA' AND Object_ID = Object_ID(N'dbo.MyTable'))
BEGIN
    ALTER TABLE dbo.MyTable ADD ColumnA INT NULL;
END
GO

IF NOT EXISTS (SELECT * FROM sys.columns WHERE Name = N'ColumnB' AND Object_ID = Object_ID(N'dbo.MyTable'))
BEGIN
    ALTER TABLE dbo.MyTable ADD ColumnB NVARCHAR(50) NULL;
END
GO
```

#### 4. Modifying a Column Type

Check the current properties of the column before altering it to ensure you aren't re-applying a change or failing if the drift has already occurred.

```sql
IF EXISTS (SELECT * FROM sys.columns WHERE Name = N'ExistingColumn' AND Object_ID = Object_ID(N'dbo.MyTable') AND max_length = 50)
BEGIN
    ALTER TABLE dbo.MyTable ALTER COLUMN ExistingColumn NVARCHAR(255) NOT NULL;
END
GO
```

#### 5. Creating an Index

```sql
IF NOT EXISTS (SELECT * FROM sys.indexes WHERE name = 'IX_MyTable_MyColumn' AND object_id = OBJECT_ID('dbo.MyTable'))
BEGIN
    CREATE INDEX IX_MyTable_MyColumn ON dbo.MyTable (MyColumn);
END
GO
```

#### 6. Idempotent Data Seeding (DML)

Use `IF NOT EXISTS` or the `MERGE` statement to ensure that re-running the script doesn't result in duplicate data or unique constraint violations.

```sql
IF NOT EXISTS (SELECT 1 FROM dbo.Parameters WHERE Name = 'MyParameter')
BEGIN
    INSERT INTO dbo.Parameters (Name, Value) VALUES ('MyParameter', 'DefaultValue');
END
GO
```

### `Views/` Directory (Idempotent Scripts)

Scripts in this directory (including views, stored procedures, and functions) are re-run on **every** migration.

**Standard:** All scripts must use the `DROP ... IF EXISTS` followed by `CREATE` pattern to ensure the definition is always up-to-date and re-runnable.

```sql
DROP VIEW IF EXISTS VW_MyView
GO

CREATE VIEW VW_MyView AS
SELECT Id, Name
FROM dbo.MyTable
WHERE IsActive = 1
GO
```

> **Note:** While `CREATE OR ALTER` is available in newer versions of SQL Server, the established pattern in this repository is `DROP ... IF EXISTS` to ensure a clean slate for every deployment.
