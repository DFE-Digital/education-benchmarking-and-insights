# Web Module: Agent Mandates

This file defines specialized mandates and procedural constraints for AI agents working within the `web` module.

## Development Standards

- **Surgical Updates**: Changes should be localized to the specific sub-project (`Web.App`, `Web.Identity`, etc.) and follow the established folder structure.
- **Frontend Build Execution**: C# compilation is not enough for UI changes. Frontend assets (`AssetSrc`) must be built via `npm run build` (which triggers `vite` and `gulp`) to populate the `wwwroot` directory.
- **Caching Strategy**: API responses that are static or slow to change MUST be cached using `IMemoryCache` (or Redis) within the Service layer to optimize performance and reduce backend load.
- **GOV.UK Consistency**: All UI elements MUST adhere to the GOV.UK Design System. Use GOV.UK Frontend classes exclusively for layout and typography.
- **Type Safety**: New C# code should have `Nullable` enabled. TypeScript should avoid `any` and use interfaces/types defined in `types/`.
- **Validation**: Form validation must be implemented using `FluentValidation` on the server and mirrored with GOV.UK-styled error summaries.
- **Accessibility**: All pages must pass WCAG 2.2 AA standards, verified by `Web.A11yTests`.

## Anti-Patterns

- **Fat Controllers**: Avoid placing complex business logic or data transformation directly in Controller actions. Use Services.
- **State Duplication**: Do not duplicate state management. Use server-side state (Session/TempData) for page-to-page navigation and Pinia purely for complex, isolated client-side interactions on a single page.
- **Hard-coded URLs**: Never hard-code API or internal URLs. Use `IUriBuilder` or configuration-based resolution.
- **Manual DOM Manipulation**: Avoid using jQuery or raw JS for UI updates. Use Vue 3 for complex client-side state.
- **Direct API Secrets**: Never store API keys in code or `appsettings.json`. Use Azure Key Vault or Environment Variables.
