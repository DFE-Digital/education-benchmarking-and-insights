# Front-end Components: Agent Mandates

This file defines specialized mandates and procedural constraints for AI agents working within the `front-end-components` module.

## Development Standards

- **Progressive Enhancement**: Components must gracefully handle being mounted into existing server-rendered HTML.
- **GOV.UK Compliance**: All UI must strictly adhere to GDS (Government Digital Service) accessibility and style standards.
- **Surgical Mounts**: Avoid wrapping the entire page in React; target specific interactive widgets.
- **Strict Typing**: No `any`. Use the domain types defined in `src/services/types.tsx`.
- **Testing**: Every new view or complex component MUST have a corresponding test in the `__tests__` directory or alongside the file.
- **Formatting & Linting**: TypeScript/React code must pass `npm run lint` (ESLint) and adhere strictly to the local `.prettierrc` configuration.

## Anti-Patterns

- **Ignoring Accessibility (a11y)**: Never compromise semantic HTML. Avoid building custom dropdowns or inputs if `govuk-frontend` or native HTML elements can achieve the same result.
- **Global State Overkill**: Do not introduce Redux or similar libraries; keep state as local as possible or use Context.
- **Direct DOM Manipulation**: Never touch the host DOM outside the React root; use refs if absolutely necessary.
- **Style Divergence**: Do not use custom CSS for things already provided by the GOV.UK Design System.
- **Bypassing Services**: Never call `fetch` directly from a component; always use a defined service wrapper.
