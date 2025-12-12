# Developer Feature Documentation: Branding

## Introduction

This document provides detailed information for developers about the implementation, usage, and integration of the GOV.UK Design System (GDS) and DfE Branding within the public facing FBIT website.

## Overview

The website on the `education.gov.uk` should be rebranded according to the [DfE guidelines](https://design.education.gov.uk/design-system/govuk-rebrand) by 31 December 2025. This includes new template colours and header logo, including repositioning of the Sign in/out button.

## Goals

### Primary Goal

Website is branded according to the [DfE design system](https://design.education.gov.uk/design-system).

![Ultimate DfE branding](./images/dfe-rebrand.png)

### Secondary Goals

Interim branding using the [GDS June 2025 visual identity](https://design-system.service.gov.uk/get-started/), along with progressively enhanced components and passing automated tests.

![Interim GDS branding](./images/gds-rebrand.png)

## Prerequisites/Dependencies

Versions of `govuk-frontend` from [5.10.0](https://github.com/alphagov/govuk-frontend/releases/tag/v5.10.0) include the June 2025 branding.

The npm package `dfe-frontend` should not be used as it contains out-of-date assets, and no additional client component support is required here. Instead the assets and CSS changes should be applied by following the [guidelines](https://design.education.gov.uk/design-system/govuk-rebrand/dfe-header-rebrand).

## Usage

The designs requite that the Sign in/out button be moved to the [Service Navigation](https://design-system.service.gov.uk/components/service-navigation/) component following the 'Show service name and navigation links' pattern. This component has a responsive view that collapses the navigation items into a drop-down menu when JavaScript is enabled:

![Interim GDS branding](./images/gds-rebrand-progressive.png)

Every server rendered view within the website therefore needs to register the GDS components using `initAll()` in `_Layout.cshtml`, where previously this was only done on pages that contained components such as the [Accordion](https://design-system.service.gov.uk/components/accordion/). This in turn may conflict with initialisation done within the `front-end` client side components and errors may end up being logged to the console, e.g.:

```text
InitError: govuk-accordion: Root element (`$root`) already initialised
    at _Accordion.checkInitialised (govuk-frontend.js:150:13)
    at new Component (govuk-frontend.js:142:10)
    at new ConfigurableComponent (govuk-frontend.js:177:5)
    at new _Accordion (govuk-frontend.js:391:5)
    at govuk-frontend.js:2264:81
    at Array.map (<anonymous>)
    at createAll (govuk-frontend.js:2262:57)
    at govuk-frontend.js:2232:5
    at Array.forEach (<anonymous>)
    at initAll (govuk-frontend.js:2231:14)
```

The above issue may be resolved by:

1. Adding scope to the `initAll()` function within `front-end`, e.g.:

    ```ts
    initAll({ scope: rootElement });
    ```

1. Ensuring the `front-end` module is loaded after `govuk-frontend`

An alternative solution would be to, as well as the above, scope to the Service Navigation component in the `_Layout.cshtml` call to `initAll()` and the desired target element (e.g. `<main>`) in individual pages that require GDS components, similar to the pre-rebrand behaviour.

## Known Issues

The `already initialised` error above occurs when running the [Vite](https://vite.dev/) dev server within `front-end`, which appears to be [by design](https://react.dev/reference/react/useEffect#my-effect-runs-twice-when-the-component-mounts) due to the lifecycle event being run multiple times. This does not occur on production builds.

Moving the location of the Sign in/out buttons may affect E2E and integration tests.

<!-- Leave the rest of this page blank -->
\newpage
