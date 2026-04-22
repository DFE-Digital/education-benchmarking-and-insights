# Development Scripts

This directory contains utility scripts to assist with local development, environment management, and documentation. All scripts are standalone C# applications that follow a standard execution pattern:

```bash
dotnet run scripts/[tool-name]/app.cs [arguments]
```

## Environment Management

### Environment Switcher Tool

Updates local configuration files for Platform APIs and API Tests.

**Usage:**

1. Navigate to the root of the repository.
2. Ensure `scripts/env-tool/settings.json` is populated (copy from `settings.example.json`).
3. Run the script:

   ```bash
   dotnet run scripts/env-tool/app.cs <api|tests|all> <environment>
   ```

**Example:**

```bash
dotnet run scripts/env-tool/app.cs all local
```

**Features:**

- **Unified Configuration:** Manage settings for both APIs and Tests in a single `settings.json` file.
- **Dynamic Updates:** Automatically identifies and updates the `Values` section in `local.settings.json` and nested properties in `appsettings.local.json`.
- **Target Selection:** Switch settings for APIs, Tests, or both simultaneously.

### Terraform Helper Tool

Automates formatting, validation, and documentation for Terraform modules across the monorepo.

**Prerequisites:**

- [Terraform CLI](https://developer.hashicorp.com/terraform/install) installed.
- [terraform-docs](https://terraform-docs.io/user-guide/installation/) installed.

**Usage:**

1. Ensure `scripts/terraform-tool/settings.json` is populated (copy from `settings.example.json`).
2. Run the script:

   ```bash
   dotnet run scripts/terraform-tool/app.cs
   ```

**Features:**

- **Parallel Execution:** Runs operations concurrently across all modules for maximum speed.
- **Dynamic Discovery:** Automatically scans for `terraform/` directories if no modules are specified in `settings.json`.
- **Comprehensive Summary:** Outputs a Pass/Fail table covering formatting, initialization, validation, and documentation.
- **Extensible:** Support for static analysis tools like TFLint and Checkov (configurable in `settings.json`).

## Database Utilities

### Database Copy Script

Copies data between two SQL databases using `SqlBulkCopy`.

**Usage:**

1. Ensure `scripts/db-copy-tool/settings.json` is populated (copy from `settings.example.json`).
2. Run the script:

   ```bash
   dotnet run scripts/db-copy-tool/app.cs
   ```

**Features:**

- **Surgical Copy:** Only copies data for tables that exist in both the source and target databases.
- **Safety First:** Skips any target table that already contains data to prevent accidental overwrites.
- **Efficient Transfer:** Uses `SqlBulkCopy` for high-performance data movement.
- **Reporting:** Provides a detailed summary of copied, skipped, and failed tables at the end of the run.

## Documentation Utilities

### Markdown Collection Tool

Aggregates repository documentation for ingestion into tools like **NotebookLM**. It discovers markdown files, merges specific directory contents into unified documents, and flattens others into a timestamped destination.

**Usage:**

1. Ensure `scripts/markdown-tool/settings.json` is populated (copy from `settings.example.json`).
2. Run the script:

   ```bash
   dotnet run scripts/markdown-tool/app.cs
   ```

**Features:**

- **NotebookLM Optimized:** Inserts clear source headers (e.g., `# Source: path/to/file.md`) into merged documents to help AI models with grounding and citations.
- **Smart Merging:** Concatenates files from specified directories (e.g., `architecture/`, `features/`) into single documents to stay within source limits.
- **Flattening:** Automatically flattens and copies non-merged markdown files by encoding their path into the filename (e.g., `web-src-README.md`).
- **Ignore Logic:** Supports excluding specific files and entire directory trees (e.g., `node_modules`, `.git`).
- **Summary Report:** Provides a count of scanned, copied, and merged items at the end.
