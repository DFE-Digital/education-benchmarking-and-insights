---
id: ASSUME-003
title: "Diataxis Quadrant Section Metadata Availability"
component: "docs/content"
domain: "documentation"
tags: ["diataxis", "eleventy", "pagefind", "filters"]
status: "validated"
created: 2026-11-17
updated: 2026-11-17
resolved: 2026-11-17
---

## Assumption

All documentation pages within the four Diátaxis quadrants expose a consistent `sectionKey` data property (`Tutorials`, `How-to guides`, `Reference`, or `Explanation`) through front matter or directory `.11tydata.json` inheritance.

## Context

Faceted filtering on the `/search/` page relies on emitting `data-pagefind-filter="section"` in rendered content layouts whenever `sectionKey` is present. Root-level pages such as the homepage (`docs/content/index.md`) omit `sectionKey` and remain searchable without being misclassified into a quadrant.

## Resolution

Validated on 2026-11-17 via `pagefind.filters()` against the built documentation index (`TC-03`), confirming all four Diátaxis quadrants populate accurately (`Tutorials`: 2, `How-to guides`: 48, `Reference`: 159, `Explanation`: 36).
