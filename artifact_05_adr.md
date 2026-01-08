# Architecture Decision Record

## ADR-001: Use Dapper for Financial Plan Persistence

### Status
Accepted

### Context
The **FinancialPlan** feature requires the creation and management of school financial scenarios. The technical scope mandates:
1.  Persisting complex user inputs (income, expenditure, workforce) as serialized JSON.
2.  Storing specific derived efficiency metrics (e.g., `TeacherContactRatio`, `AverageClassSize`) and their RAG ratings in dedicated columns for querying.
3.  Ensuring referential integrity against the `School` table.
4.  Managing record lifecycles, including versioning (`Version`) and audit trails (`Created`, `UpdatedAt`).

The existing infrastructure leverages Azure Functions and an SQL database. The schema is pre-defined, requiring a flexible data access layer that can interact with specific columns without imposing the overhead or rigid modeling of a full-featured ORM like Entity Framework Core, especially given the hybrid nature of the data (relational metrics + JSON document).

### Decision
We will use **Dapper** (a lightweight Micro-ORM) for the data access layer of the `FinancialPlan` feature.

The implementation will utilize raw SQL queries within the Azure Function to handle `SELECT`, `INSERT`, and `UPDATE` operations.

### Rationale

1.  **Hybrid Data Handling (Relational + JSON)**:
    The schema requires storing a large JSON blob (`Input` column) alongside structured, queryable columns (`TeacherContactRatio`, `InYearBalance`). Dapper allows us to easily map the C# object to a JSON string parameter while simultaneously mapping derived properties to specific SQL columns. This avoids complex entity configurations required by heavier ORMs to handle mixed types.

2.  **Explicit Control over SQL Logic**:
    The feature requires specific logic during updates, such as incrementing the `Version` column (`Version = Version + 1`) and updating audit timestamps. Dapper allows us to write this logic directly in the SQL statement, ensuring atomic updates and reducing the need to fetch, modify, and save the entire entity for simple counter increments.

3.  **Performance and Cold Starts**:
    As an Azure Function, startup time and execution speed are critical. Dapper adds negligible overhead on top of ADO.NET, ensuring the function remains performant even under load. It avoids the "cold start" penalties often associated with initializing complex `DbContext` instances in serverless environments.

4.  **Referential Integrity & Validation**:
    The decision allows for efficient, lightweight pre-checks. The code explicitly queries the `School` table (`SELECT URN FROM School...`) to validate existence before attempting modification. This provides a fast fail-safe mechanism that prevents constraint violations at the application level.

### Consequences

**Positive:**
*   **Simplicity:** The code remains procedural and easy to follow (Check School -> Check Existing Plan -> Insert/Update).
*   **Efficiency:** Only the necessary columns are selected or updated.
*   **Flexibility:** Adapting to future schema changes (e.g., adding a new metric column) only requires updating the SQL string and the Dapper parameter object.

**Negative:**
*   **Type Safety:** SQL queries are string-based, meaning schema mismatches (e.g., renaming a column in the DB but not the string) will only be caught at runtime, not compile time.
*   **Maintenance:** Developers must be comfortable writing and maintaining raw SQL.
