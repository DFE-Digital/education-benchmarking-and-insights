# Core Infrastructure: Agent Mandates

This file defines specialized mandates and procedural constraints for AI agents working within the `core-infrastructure` module.

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
