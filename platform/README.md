# Platform

This project contains the platform APIs as consumed by other components within the monorepo.
Each .NET project is deployed as an independent Azure Function App.

## Module Context

The backend API layer providing core benchmarking, school/trust insights, and search capabilities.

### Tech Stack

- **Framework:** .NET 8/9 (C#)
- **Hosting:** Azure Functions (Isolated Worker Process)
- **Data Access:** Dapper (Micro-ORM)
- **Search:** Azure AI Search
- **Caching:** Redis
- **Validation:** FluentValidation

### Core Architecture

1. **API Topology:** Segmented into domain-specific Azure Function applications (e.g., `Platform.Api.Benchmark`). Shared kernel in `platform/src/abstractions`.
2. **Feature Routing:** Requests are routed to specific "Features" directories.
3. **Service Layer:** Functions delegate domain logic to specialized services.
4. **Data Access:** Services use `IDatabaseFactory` and Dapper or `ISearchService`.

## Development Standards

- **Vertical Slices**: Keep all components of a feature (Functions, Services, Models, Validators) within the feature's directory.
- **Shared over Duplication**: Before defining a new cross-cutting interface, check the `abstractions` project (e.g., `Platform.Search`, `Platform.Messaging`) to reuse existing infrastructure.
- **Testing Strategy**: Use `xUnit` and `Moq` for unit testing logic within feature services. Use `Reqnroll` for behavioral/acceptance testing of API endpoints. Always place tests in the `platform/tests/` directory mirroring the corresponding API namespace.
- **SQL-First**: Prefer writing clean, optimized SQL via Dapper over complex ORM abstractions like Entity Framework.
- **Centralized Dependencies**: All NuGet package versions must be managed in `Directory.Packages.props`.
- **Async/Await**: Use asynchronous programming throughout the entire call stack.
- **Validation**: Every input request must be validated using `FluentValidation` before processing.
- **OpenAPI**: All public Function endpoints must be decorated with `OpenApi` attributes for documentation.

## Anti-Patterns

- **Fat Functions**: Avoid putting domain logic directly in Azure Function triggers; always delegate to a service.
- **Complex ORMs**: Do not introduce Entity Framework or other heavy ORMs; stick to Dapper for predictability and performance.
- **Hardcoded Connection Strings**: Never hardcode configuration; use `IOptions` or environment variables managed by Azure Key Vault.
- **Bypassing Service Layer**: Avoid direct database access from Function triggers; always go through the service layer.
- **Ignoring CancellationToken**: Always propagate `CancellationToken` through all async calls to support request cancellation.

## Prerequisites

1. Install [.NET 8.0 SDK](https://dotnet.microsoft.com/en-us/download/dotnet/8.0)
2. Install [Node 22](https://nodejs.org/en/download) and/or switch to that version
   using [nvm](https://github.com/nvm-sh/nvm)
3. Install Visual Studio 2022 Professional (with C# and Azure Workflows) or Rider 2025
4. Clone the project `git clone https://github.com/DFE-Digital/education-benchmarking-and-insights.git`

> **Note:** Ensure that if cloning to a user area the root folder is outside any of the 'OneDrive'
> folders to prevent 'too long path name' errors at build time.

## Getting started

> Local dependencies (SQL Server, Azurite, and Redis) are managed via Docker Compose. See the [Local Environment with Docker guide](../documentation/developers/06_Local-Environment-with-Docker.md) for setup instructions.

### Environment Profiles

Both the Platform APIs and the API Functional Tests are designed to support multiple environments/profiles.

Supported profiles for both include:

- `local`
- `development`
- `test`

When targeting a different environment, ensure you populate the corresponding User Secrets ID (e.g., `--id "platform-development"` or `--id "platform-api-tests-development"`).

### Running Platform APIs

The Platform APIs use Azure Functions (Isolated Worker Process), which intentionally separates the Azure Functions Host/Binding configuration from the .NET Worker application configuration.

#### Required Local Secrets

The following secrets must be configured for the `platform-local` environment using the `dotnet user-secrets` tool:

```bash
dotnet user-secrets set "Sql__ConnectionString" "[value]" --id "platform-local"
dotnet user-secrets set "Cache__Host" "[value]" --id "platform-local"
dotnet user-secrets set "Cache__Port" "[value]" --id "platform-local"
dotnet user-secrets set "Cache__Password" "[value]" --id "platform-local"
dotnet user-secrets set "Search__Name" "[value]" --id "platform-local"
dotnet user-secrets set "Search__Key" "[value]" --id "platform-local"
```

For full details on local environment profiles and managing `local.settings.json`, please read the **Configuration Management** section in the [Platform APIs Developer Guide](../documentation/developers/11_Platform-APIs.md#configuration-management).

### Running tests

#### Unit Tests

From the root of the `platform` run:

```bash
dotnet test --filter "FullyQualifiedName~.Tests"
```

#### Functional Tests

Add the required configuration for `Platform.ApiTests` using the `dotnet user-secrets` tool:

```bash
dotnet user-secrets set "School__Host" "http://localhost:7302" --id "platform-api-tests-local"
dotnet user-secrets set "School__Key" "x" --id "platform-api-tests-local"

dotnet user-secrets set "Trust__Host" "http://localhost:7303" --id "platform-api-tests-local"
dotnet user-secrets set "Trust__Key" "x" --id "platform-api-tests-local"

dotnet user-secrets set "LocalAuthority__Host" "http://localhost:7301" --id "platform-api-tests-local"
dotnet user-secrets set "LocalAuthority__Key" "x" --id "platform-api-tests-local"

dotnet user-secrets set "Insight__Host" "http://localhost:7071" --id "platform-api-tests-local"
dotnet user-secrets set "Insight__Key" "x" --id "platform-api-tests-local"

dotnet user-secrets set "Benchmark__Host" "http://localhost:7072" --id "platform-api-tests-local"
dotnet user-secrets set "Benchmark__Key" "x" --id "platform-api-tests-local"

dotnet user-secrets set "Establishment__Host" "http://localhost:7073" --id "platform-api-tests-local"
dotnet user-secrets set "Establishment__Key" "x" --id "platform-api-tests-local"

dotnet user-secrets set "ChartRendering__Host" "http://localhost:7076" --id "platform-api-tests-local"
dotnet user-secrets set "ChartRendering__Key" "x" --id "platform-api-tests-local"

dotnet user-secrets set "Content__Host" "http://localhost:7077" --id "platform-api-tests-local"
dotnet user-secrets set "Content__Key" "x" --id "platform-api-tests-local"
```

From the root of the `platform` run:

```bash
dotnet test tests/Platform.ApiTests
```

## Deploying Platform APIs

As per the other projects in this monorepo, the Platform APIs are deployed using Terraform via use of the `functions`
module under `./terraform/modules/functions`. The root project at `./terraform` contains the Function App configuration
at each environment level, as well as other supporting resources such as Azure Search and Blob Storage.
