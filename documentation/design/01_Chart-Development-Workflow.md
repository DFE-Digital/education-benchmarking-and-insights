# Chart Development Workflow

Developing data visualizations for the Financial Benchmarking and Insights Tool (FBIT) requires a highly collaborative process. Because our charts are server-side rendered as SVGs via a dedicated API, this approach integrates **Designers**, **Engineers**, and **Data Analysts** to ensure charts are visually compelling, technically robust, and data-accurate before hitting production.

## Responsibilities

| Phase                | Designer                         | Engineers                            | Data Analyst                         |
|----------------------|----------------------------------|--------------------------------------|--------------------------------------|
| Research & Discovery | Lead (visual/UX)                 | Support (technical feasibility)      | Lead (data validation & feasibility) |
| Spike / Prototype    | Lead (look & feel)               | Lead (build prototype)               | Support (validate data logic)        |
| Template & API Draft | Support (ensure visual fidelity) | Lead (scaffold/template + schema)    | Lead (confirm data coverage)         |
| Productionisation    | Support (final tweaks)           | Lead (production-ready code & tests) | Support (edge-case validation)       |
| Documentation        | Lead (visual docs)               | Lead (API docs)                      | Support (data notes & examples)      |

## Research & Discovery (Design / Engineers / Data Analysts)

**Designers & Data Analysts:**

- Explore D3 gallery for chart types.
- Sketch layout, labels, and interactions.
- Define accessibility (**WCAG 2.2 AA**) and usability requirements in line with the **GOV.UK Design System**.
- Examine data sources: types, distributions, hierarchies.
- Identify transformations needed for visualization (aggregations, normalization).
- Highlight edge cases (nulls, outliers, sparse data).

**Developers:**

- Inspect Observable examples for SVG structure (<g>, <path>, <text>).
- Identify core D3 constructs needed (scales, layouts).
- Define preliminary API input schema.

📌 Output: Chart concept brief with visual requirements, data considerations, and API schema draft.

## Spike / Prototype (Design / Engineers / Data Analysts)

- Quick prototype using sample datasets:
  - Developers use D3 locally to generate SVG based on template.
  - Designers provide visual feedback.
  - Data Analysts validate that chart logic correctly represents the data and highlights patterns of interest.
- Explore multiple variants (e.g., stacked vs grouped bar, logarithmic vs linear scales).

📌 Output: Working prototype SVGs validated by all three roles.

## Template & API Draft (Engineers + Data Analysts)

- Extract SVG scaffold from prototype.
- Define placeholders where D3 will inject geometry.
- Draft API schema (data + options).
- Data Analysts check that the template supports all required data types and structures.
- Designers confirm visual fidelity.

📌 Output: Draft API + template, validated across roles.

## Productionisation (Engineers)

- Refactor prototype into clean renderer: (data, options) → SVG string.
- Wrap in API endpoint
- Add tests: SVG structure, input validation.

📌 Output: Stable, test-covered chart renderer integrated into API.

## Documentation

- Document API inputs, outputs, and example SVGs.
- Designers confirm visual compliance.
- Analysts validate that chart conveys the correct data story.
- [Chart principles guide](../guides/chart-principles/01_Introduction.md) updated.

<!-- Leave the rest of this page blank -->
\newpage
