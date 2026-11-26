---
title: "How the prototype kit works"
layout: sub-navigation
sectionKey: "Reference"
includeInBreadcrumbs: true
eleventyNavigation:
  key: "How the prototype kit works"
  parent: "Design Reference"
---

How the FBIT prototype kit, in the [DfE-FBIT repository](https://github.com/hippo-digital/DfE-FBIT), turns templates and data into pages. For where things are, see [prototype kit structure](/reference/design/prototype-kit-structure/).

## Technology

| Part | Used for |
| ---- | -------- |
| [GOV.UK Prototype Kit](https://prototype-kit.service.gov.uk/docs/) | Serves the pages, and saves each user's answers and settings |
| [Nunjucks](https://mozilla.github.io/nunjucks/) | Page templates |
| GOV.UK Frontend | GOV.UK Design System components |
| DfE Frontend | DfE header, Inter font and DfE styling |
| MOJ Frontend | Components GOV.UK does not have, such as the filter panel and sortable table |
| [Jest](https://jestjs.io/) | Automated tests |

Charts are hand-built in SVG and HTML, not with a charting library.

## Pages and routes

The kit shows the template that matches the address, so `/primary-progress/school-homepage` shows `app/views/primary-progress/school-homepage.html`. Most pages need nothing else.

A route is only needed when a page needs something extra, such as data loaded from a file. Routes are in `app/routes.js`. Larger prototypes have their own routes file in `app/routes/`, and their chart and data code in `app/utils/`.

## Data

* **Data files:** each prototype's data is in `app/data/`, such as `app/data/primary-progress/`. Example schools use real published data where possible.
* **Session data:** the kit keeps each user's form answers and settings as they move between pages. `app/data/session-data-defaults.js` sets the starting data.
* **Filters:** `app/filters.js` adds formatting for templates, such as `{% raw %}{{ 1234567 | locale }}{% endraw %}` showing 1,234,567.

## Prototype settings

Some pages have a prototype setting at the top: a box with a dashed border and a "Prototype setting" tag. It switches what the page shows, such as which example school, and keeps the page's other options. It is not part of the design.

## Versions

When a design changes a lot, the new version goes in its own folder within the work item, such as `app/views/attainment-8/v2/`. Earlier versions stay, so the work item's index page can link to them as other design concepts considered.

## Components

The kit uses design system components in this order:

1. [GOV.UK Design System](https://design-system.service.gov.uk/) components and patterns.
2. Other government design systems, such as the [DfE Design System](https://design.education.gov.uk/) and the [MOJ Design System](https://design-patterns.service.justice.gov.uk/), listed on [X-GOVUK](https://x-govuk.org/).
3. A custom component, only when no design system has one or production FBIT already has it. The template and styles say it is custom and why.

## Tests

`npm test` runs the Jest tests in `tests/`. Most render a page and check its content, so changing wording on a page may mean changing a test.

## Running and sharing

The kit runs on your computer with `npm run dev`, at [http://localhost:3000](http://localhost:3000). The repository has no deployment set up, so share changes through a branch and pull request.
