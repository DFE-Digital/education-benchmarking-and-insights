# Secret Management

This guide outlines the principles and procedures for handling sensitive information (API keys, connection strings, credentials) within this project.

## Core Principles

1. **Zero Secrets in Source Control**: No secrets must ever be committed to the repository. While pre-commit hooks are in place to help, the ultimate responsibility lies with the developer.
2. **No Plaintext Secrets in the Workspace**: **Secrets should not be stored in plaintext within the workspace directory.**
3. **Encryption at Rest**: If secrets must reside within the workspace for tool compatibility (e.g., `local.settings.json` for Azure Functions), they **MUST** be encrypted at rest using platform-native tools.
4. **Externalised Configuration**: Prefer using out-of-process secret stores like .NET User Secrets or Azure Key Vault for local development.

## Local Development Workflow

### .NET User Secrets (Preferred)

For all .NET-based projects (APIs and Tests), use the `dotnet user-secrets` tool. This stores secrets in a JSON file in the user's profile folder, entirely outside the workspace, preventing accidental commits and keeping the workspace clean of sensitive data.

For the specific `dotnet user-secrets` commands and project-specific IDs used in this repository, see the [Adding User Secrets section in the Platform APIs Guide](./11_Platform-APIs.md#adding-user-secrets).

### Azure Functions: `local.settings.json`

Azure Functions bindings require environment variables, often provided via `local.settings.json`. This file is ignored by source control, so developers must create it by copying the provided `local.settings.example.json`.

To comply with our security principles, `local.settings.json` **MUST** be encrypted at rest on your local machine whenever it contains sensitive information, despite being ignored by git.

For instructions on adding, encrypting, and decrypting host settings, see the [Encrypted local.settings.json section in the Platform APIs Guide](./11_Platform-APIs.md#encrypted-localsettingsjson).

**Note**: Never leave `local.settings.json` in a decrypted state within the workspace.

## Git Configuration & Settings Files

**Important**: Standard settings files (such as `appsettings.json` or `launchSettings.json`) are **NOT** ignored by `.gitignore`. They are tracked by source control to share non-sensitive configuration across the team.

Because these files are tracked, it is strictly prohibited to place plaintext secrets within them. You must move the secret entirely out of the workspace using .NET User Secrets.

*(Note: While `local.settings.json` is ignored by source control, it must still be encrypted at rest within your local workspace as described above).*

### Pre-commit Hooks

This repository uses `pre-commit` to scan for potential secrets before allowing a commit.

- Ensure you have run `make install-hooks` (or the equivalent for your OS) to activate these checks.
- If the hook flags a file, review it immediately. Never use `--no-verify` to bypass security checks.

## Azure Integration

In non-local environments (Development, Test, Production), secrets are never stored in files. They are:

- Managed in **Azure Key Vault**.
- Injected into App Service / Function App settings via **Key Vault References**.
- Accessed by the application using **Managed Identities**, ensuring no credentials exist in the application code or configuration.

*For more information on setting up your local environment, see [Getting Started](./01_Getting-Started.md).*

<!-- Leave the rest of this page blank -->
\newpage
