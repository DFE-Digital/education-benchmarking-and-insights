---
id: ADR-0002
title: "Progressive Dual-Surface Pagefind UI Pattern (Header Modal and Dedicated Search Page)"
component: "docs/search-ui"
domain: "documentation"
tags: ["pagefind", "govuk", "accessibility", "progressive-enhancement", "wcag"]
status: "proposed"
created: 2026-11-17
updated: 2026-11-17
---

## Summary

**Feature:** docs-pagefind-search  
**Date:** 2026-11-17  
**Deciders:** FBIT Engineering Team  
**Status:** Proposed

## Context

The documentation site uses the GOV.UK / Department for Education styling provided by `govuk-frontend` and `@x-govuk/govuk-eleventy-plugin`. Users need fast keyboard-accessible search from any page header as well as a shareable, full-page search view that supports filtering by Diátaxis quadrant (`Tutorials`, `How-to guides`, `Reference`, `Explanation`) and degrades gracefully when JavaScript is unavailable or fails to load.

## Options Considered

* **Option 1:** Dedicated `/search/` page only, linked from the header.
* **Option 2:** Header modal dialog (`<dialog>`) only, with no dedicated `/search/` page.
* **Option 3:** Progressive dual-surface pattern combining a dedicated `/search/` page (with `?q=` query synchronisation and Diátaxis filters) and a header quick-search button that opens a native HTML `<dialog>` modal when JavaScript is enabled.

## Evaluation

### Option 1: Dedicated `/search/` Page Only

* **Pros:**
  * Simple template implementation and shareable URLs.
* **Cons:**
  * Forces a full page navigation away from the document the developer is currently reading before typing a query.

### Option 2: Header Modal Dialog Only

* **Pros:**
  * Immediate in-page search from any document via click or keyboard shortcut (`/` or `Ctrl+K` / `Cmd+K`).
* **Cons:**
  * Provides no progressive-enhancement fallback link if JavaScript fails, and constrained modal height makes multi-facet Diátaxis filtering crowded on smaller viewports.

### Option 3: Progressive Dual-Surface Pattern (Header Modal + `/search/` Page)

* **Pros:**
  * Without JavaScript, the header renders a standard GOV.UK link to `/search/` (which includes a `<noscript>` fallback link to `/sitemap`).
  * With JavaScript enabled (`body.js-enabled`), the header displays an accessible trigger button (`aria-haspopup="dialog"`) supporting `/` and `Ctrl+K` / `Cmd+K` shortcuts to open a native `<dialog>` modal with focus management, `Escape` dismissal, highlighted excerpts, and sub-heading deep links.
  * The dedicated `/search/` page supports URL query state (`?q=`), full Diátaxis `section` facet filtering, and GOV.UK-aligned typography, focus states, and colour contrast (WCAG 2.2 Level AA).
* **Cons:**
  * Requires styling Pagefind UI custom properties and focus states in `docs/content/assets/styles.scss` so both surfaces match GOV.UK Frontend conventions.

## Decision

Adopt Option 3: implement a dedicated `/search/` page (`docs/content/search.md`) alongside a Nunjucks override for `components/search/template.njk` and a client module (`docs/content/assets/js/pagefind-search.js`) that progressively enhances the header search link into a native `<dialog>` modal powered by Pagefind UI.

## Consequences

### Positive

* Meets GOV.UK progressive enhancement principles and WCAG 2.2 Level AA keyboard, focus, and screen-reader requirements.
* Supports both instant quick-jumping from any page and deep shareable filtered searches via `/search/?q=...`.

### Negative

* Maintaining the `components/search/template.njk` Nunjucks override requires checking template compatibility during major version upgrades of `@x-govuk/govuk-eleventy-plugin`.
