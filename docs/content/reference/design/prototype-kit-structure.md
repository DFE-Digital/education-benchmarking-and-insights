---
title: "Prototype kit structure"
layout: sub-navigation
sectionKey: "Reference"
includeInBreadcrumbs: true
eleventyNavigation:
  key: "Prototype kit structure"
  parent: "Design Reference"
---

FBIT designs are prototyped in the [DfE-FBIT repository](https://github.com/hippo-digital/DfE-FBIT), built with the [GOV.UK Prototype Kit](https://prototype-kit.service.gov.uk/docs/). To get started, see [make your first prototype change](/tutorials/prototype-kit/).

## Home page

The home page (`app/views/index.html`) lists every work item in a table:

| Column | Content |
| ------ | ------- |
| Name | Link to the work item's index page |
| Sprint | The sprint or sprints the work was done in |
| Version | The versions prototyped |
| Status | Concept, In progress, Paused, Ready for dev or Live |
| Service area | FBIT, High Needs, Trusts, Local Authorities or Schools |
| Research insights / Purpose | The user story link and what the work item is for |

## Work items

Each work item has one folder in `app/views`, such as `app/views/primary-progress`, with an `index.html` page in this order:

1. **Summary**: what the work item is, a link to the user story, and the user need.
2. **Final designs**: the designs agreed for development. Each row links to a page, has a status tag and lists what has changed.
3. **Other design concepts considered**: earlier concepts, not for development. Each lists what changed and why it was not preferred.
4. **Background** (optional): supporting information, such as how a measure is calculated.

Keeping rejected concepts, with reasons, records why the final design is the way it is.

## Folders

| Folder or file | Content |
| -------------- | ------- |
| `app/views/` | Page templates. The folder path is the page's address |
| `app/views/includes/` | Template pieces shared by several pages, such as the filter panel |
| `app/routes.js`, `app/routes/` | Routes, for pages that need data or several versions of one template |
| `app/data/` | Data files used by the prototypes |
| `app/assets/` | Styles, browser JavaScript and images |
| `tests/` | Automated checks, run with `npm test` |

The [prototype kit README](https://github.com/hippo-digital/DfE-FBIT#readme) has more detail on routes, versions and data.
