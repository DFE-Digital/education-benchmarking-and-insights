# Platform Module: Agent Mandates

This file defines specialized mandates and procedural constraints for AI agents working within the `platform` module.

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
