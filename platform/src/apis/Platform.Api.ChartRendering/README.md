# Chart Rendering API Module

## Module Purpose

A high-performance server-side SVG chart rendering engine optimized for educational benchmarking data. It provides Azure Functions to generate consistent, accessible, and theme-compliant visualizations (Horizontal and Vertical Bar Charts) for use in web applications and reports.

## Tech Stack

- **Runtime:** Node.js (>=22)
- **Framework:** Azure Functions (v4)
- **Visualization:** D3.js (v7)
- **Language:** TypeScript
- **DOM Simulation:** `@xmldom/xmldom` & `query-selector` (for DOM-based rendering paths)
- **API Documentation:** OpenAPI 3.0 via `ts-oas` and `swagger-ui-dist`
- **Monitoring:** Application Insights
- **Testing:** Jest & `jest-theories`

## Core Logic & Data Flow

The module operates as a stateless rendering service using a **Feature-based (Vertical Slice)** architecture. Each chart type (e.g., `horizontalBarChart`, `verticalBarChart`) is encapsulated in its own directory with its own routing, handler, validation, and rendering logic.

Data is processed through either a high-performance **Template** (string-based) or a **DOM Builder** (D3-native) pipeline.

1. **Normalization:** `normaliseData` ensures values (percent, currency, numeric) are in the correct format for D3 scales.
2. **Scaling:** D3 scales map data domains to SVG coordinate ranges.
3. **Rendering:** Templates manually construct SVG XML strings for maximum speed, while DOM Builders allow standard D3 selection patterns.

## Key Definitions

- `HorizontalBarChartDefinition` / `VerticalBarChartDefinition`: The primary public-facing contracts defining data points, layout, formatting, and axes. These are explicitly exported and annotated for OpenAPI generation.
- `ChartBuilderResult`: The standard output containing the unique chart `id` and the generated `html` (SVG) string.
- `ValueType`: Enum (`percent`, `currency`, `numeric`) determining how data is normalized and axes are formatted.
- `HorizontalBarChartTemplate`: Optimized class for rendering horizontal bar charts via manual XML string building.
- `HorizontalBarChartBuilder`: Alternative renderer using a virtual DOM for complex D3 manipulations.
- `normaliseData`: Utility that handles percentage division and null-value defaults across different data types.

## Integration Points

- **Inbound:** Triggered by frontend components or PDF generation services requiring static chart assets.
- **Outbound:** Telemetry sent to **Application Insights** (tracks `horizontalBarChartWorker` and `verticalBarChartWorker` dependencies).
- **Internal:** Relies on `src/functions/utils.ts` for shared mathematical and formatting logic.

## Development Standards

### Architecture & Engineering Guidelines

- **Performance First:** Prefer the `Template` rendering path for standard charts to minimize memory overhead from virtual DOMs.
- **Strict Typing:** All chart data must satisfy the `ChartBuilderOptions<T>` generic interface.
- **OpenAPI Synchronization:** Changes to public interfaces must be compatible with `ts-oas`. Run `npm run api` to regenerate `openapi.json` when modifying these types.
- **Error Handling:** Consistently format error responses as `{ error: "Summary", errors: ["Detail 1", "Detail 2"] }`. Use HTTP 400 for validation failures and HTTP 500 for rendering failures.
- **Unit Testing:** Every chart type and utility must have a corresponding `.test.ts` file in the `tests/` directory using Jest theories for boundary cases.
- **Accessibility:** SVG outputs must include appropriate `aria-label` attributes (e.g., in `linkFormat`) and semantic grouping.
- **Format Consistency:** Use `sprintf-js` for all dynamic label and link formatting to ensure predictable string interpolation.

### Anti-Patterns

- **Bypassing Scales:** Never hardcode pixel values for data-driven elements; always use D3 scales (`x()` or `y()`).
- **Browser-API Reliance:** Do not use `window`, `document`, or `HTMLElement` directly. Always use the provided DOM polyfills or the string-based Template path.
- **Raw Data Usage:** Never pass un-normalized data to scales. Always run data through `normaliseData` first to handle `ValueType` specific logic.
- **Bloated Functions:** Avoid putting rendering logic directly in `function.ts`. Keep it encapsulated in `Template` or `Builder` classes.
