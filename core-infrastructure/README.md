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
dotnet run --project src/db/Core.Database -- -c "[CONNECTION_STRING]"
```

#### Creating a New Migration

1. **Schema Changes (DDL):** Add a new SQL file to `src/db/Core.Database/Scripts/` following the `NNN-Description.sql` naming convention.
2. **View Changes:** Update the existing view in `src/db/Core.Database/Views/` or create a new one using `CREATE OR ALTER`.

## Infrastructure Management (Terraform)

Refer to the [Terraform README](./terraform/README.md) for detailed information on provisioning resources and managing state.
