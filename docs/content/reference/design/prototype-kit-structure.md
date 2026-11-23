---
title: "Prototype kit structure"
layout: sub-navigation
sectionKey: "Reference"
includeInBreadcrumbs: true
eleventyNavigation:
  key: "Prototype kit structure"
  parent: "Design Reference"
---

FBIT designs are prototyped in the [DfE-FBIT repository](https://github.com/hippo-digital/DfE-FBIT), built with the [GOV.UK Prototype Kit](https://prototype-kit.service.gov.uk/docs/). To get started, see [add a design concept to the prototype kit](/how-to/design/add-a-design-concept/) and the [GOV.UK Prototype Kit tutorials and guides](https://prototype-kit.service.gov.uk/tutorials-and-guides/).

## Home page

The home page (`app/views/index.html`) lists every work item in a table, newest first, which can be filtered by status:

| Column | Content |
| ------ | ------- |
| Name | Link to the work item's index page |
| Sprint started | The sprint the work started in |
| Status | Concept, In progress, Paused, Ready for dev or Live |
| Research insights / Purpose | The user story link and what the work item is for |

A status key below the table explains each status. "Ready for dev" means the final design has been handed over to development.

## Work items

Each work item has one folder in `app/views`, such as `app/views/primary-progress`, with an `index.html` page in this order:

1. **Summary**: what the work item is, a link to the user story, and the user need.
2. **Final designs**: the designs agreed for development. Each row links to a page, has a status tag and lists what has changed. Some older work items, such as Attainment 8, call this section "Ready for dev".
3. **Other design concepts considered**: earlier concepts, not for development. Each lists what changed and why it was not preferred.
4. **Background** (optional): supporting information, such as how a measure is calculated.

Keeping rejected concepts, with reasons, records why the final design is the way it is.

## Folders

| Folder or file | Content |
| -------------- | ------- |
| `app/views/` | Page templates. The folder path is the page's address |
| `app/views/includes/` | Template pieces shared by several pages, such as the filter panel |
| `app/routes.js`, `app/routes/` | Routes, for pages that need data. Most pages have no route |
| `app/data/` | Data files used by the prototypes |
| `app/assets/` | Styles (`app/assets/sass/application.scss`), browser JavaScript and images |
| `tests/` | Automated checks, run with `npm test` |

For how routes, data, versions and tests work, see [how the prototype kit works](/reference/design/how-the-prototype-kit-works/).
