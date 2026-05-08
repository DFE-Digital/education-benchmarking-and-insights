# Chart Renderer Module: Agent Mandates

This file defines AI-specific operational guidelines for agents working within the `chart-renderer` module.
You **MUST** read the `README.md` in this directory before proposing changes or writing code for all human-readable architecture, tech stack, development standards, and anti-patterns.

## AI Operational Guidelines

- **Vertical Slices**: When generating new chart types, ensure all related files (Functions, Templates, Builders, Validators) are grouped together in a cohesive vertical slice.
- **Performance First**: Prioritise the `Template` (string-based) rendering path over `Builder` (DOM-based) paths for standard charts to minimize overhead.
- **Strict Typing**: Ensure all new chart definitions satisfy the `ChartBuilderOptions<T>` generic interface.
- **OpenAPI Synchronization**: Changes to public interfaces must be compatible with `ts-oas`. Run `npm run api` within the API project to regenerate `openapi.json` when modifying these types.
- **Accessibility**: Ensure SVG outputs include appropriate `aria-label` attributes and semantic grouping.
- **Unit Testing**: Every new chart type or utility must have a corresponding `.test.ts` file in the `tests/` directory.

## Anti-Patterns

- **Bypassing Scales**: Never hardcode pixel values for data-driven elements; always use D3 scales.
- **Browser-API Reliance**: Do not use `window` or `document` directly; use the provided DOM polyfills or the string-based Template path.
- **Raw Data Usage**: Never pass un-normalised data to scales. Always run data through `normaliseData` first to handle `ValueType` specific logic.
