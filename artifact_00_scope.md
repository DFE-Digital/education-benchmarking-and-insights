Based on the analysis of the SQL schema and existing C# models, here is the technical scope for the **FinancialPlan** feature.

### Feature Definition
The **FinancialPlan** feature facilitates the creation, management, and analysis of school financial scenarios for specific academic years. It is responsible for capturing detailed user inputs regarding income, expenditure, and workforce deployment (including curriculum planning and staffing structures) and persisting this complex data, likely as serialized JSON, within the `FinancialPlan` table. Crucially, the feature logic must derive key efficiency metrics—specifically **Teacher Contact Ratio**, **Average Class Size**, and **In-Year Balance**—and their associated RAG (Red/Amber/Green) ratings from these inputs, storing them in dedicated columns to enable efficient querying and benchmarking comparisons. The feature ensures referential integrity by validating against the `School` table and manages the plan's lifecycle through versioning, completion status (`IsComplete`), and audit trails.

### Required Tables
*   `FinancialPlan`
*   `School`
