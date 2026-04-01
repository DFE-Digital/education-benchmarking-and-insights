# Core Infrastructure Module

## Module Purpose
The foundational module responsible for provisioning and managing shared Azure cloud resources (Networking, SQL Servers, Key Vault, ACR) that other modules depend upon, and executing SQL schema migrations. It ensures consistent, repeatable infrastructure and database state across all environments.

## Tech Stack
- **Infrastructure as Code:** Terraform (AzureRM, AzureAD providers)
- **Database Migrations:** .NET 8, C#, DbUp (SQL Migration Framework)
- **Cloud Platform:** Microsoft Azure
- **Local Emulation:** Docker (SQL Server, Azurite for local dev)

## Entry Points
- **IaC Provisioning:** `terraform/main.tf` acts as the root module for Azure resource provisioning.
- **Database Migrations:** `src/db/Core.Database/Program.cs` is the console application entry point that executes the DbUp migration lifecycle.
- **Orchestration:** Executed primarily via CI/CD (see `pipelines/core-infrastructure`) and wrapper scripts (e.g., `scripts/terraform.ps1`).

## Core Logic & Data Flow
1. **Infrastructure Provisioning (Control Plane):** Terraform evaluates `.tfvars` to determine environment capacity (SKUs/Sizes) and provisions Azure resources.
2. **Database Schema Deployment (Data Plane):** Post-provisioning, the `Core.Database` .NET utility connects to the Azure SQL instance to apply schema states.
3. **Migration Rules:** 
   - `Scripts/*.sql` (DDL/DML): Executed exactly once. Tracked via `dbo.SchemaVersions`.
   - `Views/*.sql` (Views/SPs): Executed on every run. Must be idempotent.

## Key Definitions
- **Core.Database:** The C# DbUp migration project.
- **SchemaVersions:** Database table tracking applied DbUp scripts.
- **Idempotent Views:** SQL scripts using `CREATE OR ALTER` to allow safe, repeated execution.
- **environment-prefix:** A critical Terraform variable guaranteeing globally unique resource naming across environments.

## Integration Points
- **Platform / Web / Data-Pipeline:** Supplies Azure SQL databases, Storage Accounts, and Application Insights instances to upstream modules.
- **Support-Analytics:** Configures Log Analytics workspaces and diagnostic settings.
- **Pipelines:** Tightly coupled with `pipelines/core-infrastructure` for deployment automation.

## Development Standards
- **IaC Only:** Zero manual resource creation in the Azure Portal; all infrastructure drift is a defect.
- **Atomic Migrations:** Each SQL script in `Scripts/` must perform a single logical change and be named sequentially.
- **Centralized Packages:** Utilize `Directory.Packages.props` for .NET version management.
- **Resource Tagging:** All Azure resources must inherit the `common-tags` defined in `main.tf`.
- **Local Dev:** Use local Docker containers (SQL Server, Azurite) for testing rather than connecting to cloud resources.

## Anti-Patterns
- **Direct Schema Edits:** Modifying the database via SSMS/DataGrip instead of through `Core.Database` migration scripts.
- **Modifying Applied Scripts:** Altering a script in `Scripts/*.sql` after deployment (always create a new, sequential script).
- **Cleartext Secrets:** Hardcoding secrets in `.tf` or `.tfvars`; always use Azure Key Vault or Terraform sensitive variables.
- **Environment Drift:** Manually configuring environments instead of leveraging Terraform variable files.
