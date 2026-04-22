# Core Infrastructure

Foundation for the Financial Benchmarking and Insights Tool (FBIT), managing Azure cloud resources and SQL database schema migrations.

## Tech Stack

- **IaC:** Terraform (AzureRM, AzureAD, AzAPI, MSSQL)
- **Database Migrations:** .NET 10, DbUp (SQL Migration Framework)
- **Cloud Platform:** Microsoft Azure
- **Local Emulation:** Docker (SQL Server, Azurite)

## Module Structure

- `terraform/`: Infrastructure as Code definitions for the shared environment.
- `src/db/Core.Database/`: .NET tool for managing SQL schema and views.
  - `Scripts/`: One-time DDL/DML scripts (tracked in `SchemaVersions`).
  - `Views/`: Idempotent view/SP definitions applied on every run.

## Development Standards

- **IaC Only**: Zero manual resource creation in the Azure Portal; all infrastructure drift is a defect.
- **Documentation Quality**: All Markdown files must adhere to the repository-wide linting standards enforced via pre-commit hooks and CI checks.
- **Atomic Migrations**: Each SQL script in `Scripts/` must perform a single logical change and be named sequentially.
- **Centralized Configuration**: Utilize `Directory.Packages.props` for .NET version management and `Directory.Build.props` for global build properties. Avoid duplicating these settings in individual `.csproj` files.
- **Resource Tagging**: All Azure resources must inherit the `common-tags` defined in `main.tf`.
- **Local Dev**: Use local Docker containers (SQL Server, Azurite) for testing rather than connecting to cloud resources.

## Anti-Patterns

- **Direct Schema Edits**: Modifying the database via SSMS/DataGrip instead of through `Core.Database` migration scripts.
- **Modifying Applied Scripts**: Altering a script in `Scripts/*.sql` after deployment (always create a new, sequential script).
- **Cleartext Secrets**: Hardcoding secrets in `.tf` or `.tfvars`; always use Azure Key Vault or Terraform sensitive variables.
- **Environment Drift**: Manually configuring environments instead of leveraging Terraform variable files.

## Getting Started

### Local Development

Local dependencies are managed via Docker Compose.

1. Ensure Docker is running.
2. Run `docker-compose up` from the repository root.
3. Use the [Local Environment with Docker guide](../documentation/developers/06_Local-Environment-with-Docker.md) for full setup.

### Database Migrations

The `Core.Database` project uses DbUp to manage the schema.

#### Running Migrations

To run migrations against a target database:

```powershell
dotnet run --project core-infrastructure/src/db/Core.Database -- -c "[CONNECTION_STRING]"
```

#### Creating a New Migration

1. **Schema Changes (DDL):** Add a new SQL file to `src/db/Core.Database/Scripts/` following the `NNN-Description.sql` naming convention.
2. **View Changes:** Update the existing view in `src/db/Core.Database/Views/` or create a new one using `CREATE OR ALTER`.

## Infrastructure Management (Terraform)

Refer to the [Terraform README](./terraform/README.md) for detailed information on provisioning resources and managing state.
