# Module: Front-end Components

## Module Purpose
A React-based component library that provides interactive benchmarking visualizations and tools for the Education Benchmarking service. It is designed for progressive enhancement, mounting into an ASP.NET Core MVC host application via specific DOM element targeting.

## Tech Stack
- **Framework**: React 18
- **Build Tool**: Vite 7 (with TypeScript)
- **Styling**: SASS/CSS, strictly following the **GOV.UK Design System** (`govuk-frontend`).
- **Visualization**: Recharts for all charting needs.
- **Data Handling**: `date-fns` for time, `jszip` & `file-saver` for exports, `html-to-image` for sharing.
- **Testing**: Jest and React Testing Library.

## Core Logic & Data Flow
1. **Mounting**: `main.tsx` scans the DOM for specific IDs (e.g., `compare-your-costs`).
2. **Configuration**: Initialization data is passed from the host via `data-*` attributes.
3. **State Management**: Uses React Context (found in `src/contexts`) and custom hooks (`src/hooks`) for local and shared state.
4. **Data Fetching**: All external communication is abstracted into `src/services` using standard `fetch` or custom API wrappers.

## Key Definitions
- **`constants.tsx`**: The definitive source of truth for all DOM Element IDs (e.g., `CompareCostsElementId`) and `data-*` attribute names used to bridge the ASP.NET Core host and the React components.
- **`SchoolExpenditure`**: (`src/services/types.tsx`) The core domain model representing financial expenditure for a school.
- **`View`**: Top-level components in `src/views` that act as the entry points for the React application.
- **`ChartHandler`**: (`src/components/charts`) The primary abstraction for managing chart rendering, sorting, and interaction.
- **`EstablishmentTick`**: Custom Recharts component for rendering consistent establishment labels on axes.
- **`useGovUk`**: Hook to ensure React-rendered elements correctly initialize GOV.UK Frontend JS behaviors.

## Integration Points
- **ASP.NET Core Web App**: Consumes this module as a bundled JS/CSS package. Target elements must match IDs in `src/constants.tsx`.
- **Benchmarking APIs**: REST endpoints (Balance, Census, Expenditure, etc.) providing JSON data.
- **GOV.UK Frontend**: External dependency for core styles and accessible UI patterns.

## Development Standards
- **Progressive Enhancement**: Components must gracefully handle being mounted into existing server-rendered HTML.
- **GOV.UK Compliance**: All UI must strictly adhere to GDS (Government Digital Service) accessibility and style standards.
- **Surgical Mounts**: Avoid wrapping the entire page in React; target specific interactive widgets.
- **Strict Typing**: No `any`. Use the domain types defined in `src/services/types.tsx`.
- **Testing**: Every new view or complex component MUST have a corresponding test in the `__tests__` directory or alongside the file.
- **Formatting & Linting**: Code must pass `npm run lint` (ESLint) and adhere strictly to the local `.prettierrc` configuration.
- **Local Developer Loop**: When testing changes locally alongside the ASP.NET Core app, use the `copy-js.ps1` or `copy-js.sh` scripts to push the built bundle into the `web` project's static assets.

## Anti-Patterns
- **Ignoring Accessibility (a11y)**: Never compromise semantic HTML. Avoid building custom dropdowns or inputs if `govuk-frontend` or native HTML elements can achieve the same result.
- **Global State Overkill**: Do not introduce Redux or similar libraries; keep state as local as possible or use Context.
- **Direct DOM Manipulation**: Never touch the host DOM outside the React root; use refs if absolutely necessary.
- **Style Divergence**: Do not use custom CSS for things already provided by the GOV.UK Design System.
- **Bypassing Services**: Never call `fetch` directly from a component; always use a defined service wrapper.
