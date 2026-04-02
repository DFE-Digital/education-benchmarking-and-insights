# Web Module Context

## Module Purpose

The `web` module is the primary user-facing portal for the Education
Benchmarking and Insights platform. It provides interactive tools for schools,
trusts, and local authorities to analyze financial performance, build
multi-year plans, and compare themselves against similar entities.

## Tech Stack

- **Backend:** .NET 8 (ASP.NET Core MVC)
- **Frontend:** Vue 3 (Composition API), Pinia, TypeScript, Vite, GOV.UK
  Frontend (Sass/Nunjucks-compatible templates)
- **Frontend Build Pipeline:** Orchestrated via `npm scripts` (`vite`,
  `vue-tsc`), processed by `gulp` (`gulpfile.cjs`) to compile assets into
  `wwwroot`.
- **Communication:** Typed `HttpClient` wrappers for backend Azure Function
  APIs.
- **Caching:** `IMemoryCache` (local) and `StackExchange.Redis` (distributed)
  for API response optimization.
- **Testing:**
  - **C#:** xUnit, Moq, FluentAssertions
  - **TypeScript:** Vitest, Vue Test Utils
  - **E2E:** Playwright
  - **A11y:** Axe-core/Playwright
- **Cross-Cutting:** Serilog, FluentValidation, Newtonsoft.Json,
  Microsoft.FeatureManagement

## Core Logic & Data Flow

### Sub-Project Boundaries

- **`Web.App`**: The core application housing MVC controllers, domain logic,
  and Vue frontend assets.
- **`Web.Identity`**: Handles authentication flows and DfE Sign-in (OIDC)
  integration.
- **`Web.Redirect`**: Manages legacy or vanity URL redirections.
- **`Web.Shutter`**: Handles maintenance mode, outages, and "shuttered"
  application states.

### Data Flow Pattern

The module follows a "Service-Oriented MVC" pattern. Controllers orchestrate
requests, but heavy lifting is delegated to Infrastructure and Service layers.

## Key Definitions

- **`School` / `Trust` / `LocalAuthority`** (`src/Web.App/Domain/Establishment/`):
  The primary domain entities representing the educational organizations.
- **`ComparatorSet`** (`src/Web.App/Domain/Benchmark/`): A user-defined or
  system-generated collection of similar organizations used for benchmarking.
- **`FinancialPlan`** (`src/Web.App/Domain/Benchmark/FinancialPlan.cs`): A core
  feature allowing schools to input and track multi-year financial forecasts.
- **`ApiBase`** (`src/Web.App/Infrastructure/Apis/ApiBase.cs`): The abstract
  base class that handles standard HTTP verbs and Azure Function key
  injection.
- **`ViewComponent`** (`src/Web.App/ViewComponents/`): Reusable server-side UI
  components used for complex elements like navigation, header, or specialized
  tables.
- **`CustomData`** (`src/Web.App/Domain/Benchmark/CustomData.cs`): Represents
  user-modified financial or non-financial data used to perform "what-if"
  benchmarking scenarios.

## Integration Points

- **Backend APIs**: Depends on `Establishment`, `Benchmark`, `Insight`,
  `Content`, and `LocalAuthorityFinances` APIs.
- **Authentication**: Integrates with **DfE Sign-in (OIDC)** via the
  `Web.Identity` sub-project.
- **Infrastructure**: References shared networking and Key Vault from
  `core-infrastructure` via Terraform data sources, and provisions its own
  Azure App Service, Front Door, and Cosmos DB via its local `terraform/`
  directory. Shares configuration and potentially core models with the `Core`
  and `Platform` modules.
- **Front-end Components**: Consumes shared UI logic from
  `front-end-components` (`npm` dependency).

## Development Standards

- **Surgical Updates**: Changes should be localized to the specific sub-project
  (`Web.App`, `Web.Identity`, etc.) and follow the established folder
  structure.
- **Frontend Build Execution**: C# compilation is not enough for UI changes.
  Frontend assets (`AssetSrc`) must be built via `npm run build` (which
  triggers `vite` and `gulp`) to populate the `wwwroot` directory.
- **Caching Strategy**: API responses that are static or slow to change MUST be
  cached using `IMemoryCache` (or Redis) within the Service layer to optimize
  performance and reduce backend load (e.g., `FinanceService`).
- **GOV.UK Consistency**: All UI elements MUST adhere to the GOV.UK Design
  System. Use GOV.UK Frontend classes exclusively for layout and typography.
- **Type Safety**: New C# code should have `Nullable` enabled. TypeScript
  should avoid `any` and use interfaces/types defined in `types/`.
- **Validation**: Form validation must be implemented using `FluentValidation`
  on the server and mirrored with GOV.UK-styled error summaries.
- **Accessibility**: All pages must pass WCAG 2.2 AA standards, verified by
  `Web.A11yTests`.

## Anti-Patterns

- **Fat Controllers**: Avoid placing complex business logic or data
  transformation directly in Controller actions. Use Services.
- **State Duplication**: Do not duplicate state management. Use server-side
  state (Session/TempData) for page-to-page navigation and Pinia purely for
  complex, isolated client-side interactions on a single page.
- **Hard-coded URLs**: Never hard-code API or internal URLs. Use `IUriBuilder`
  or configuration-based resolution.
- **Manual DOM Manipulation**: Avoid using jQuery or raw JS for UI updates. Use
  Vue 3 for complex client-side state.
- **Direct API Secrets**: Never store API keys in code or `appsettings.json`.
  Use Azure Key Vault or Environment Variables.
