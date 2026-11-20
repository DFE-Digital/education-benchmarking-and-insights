---
title: "Add a design concept to the prototype kit"
layout: sub-navigation
sectionKey: "How-to guides"
includeInBreadcrumbs: true
eleventyNavigation:
  key: "Add a design concept to the prototype kit"
  parent: "How-to: Design"
---

This guide shows how to run the FBIT prototype kit on your computer, add a design concept to a work item and share it with the team.

FBIT designs are prototyped in the [DfE-FBIT repository](https://github.com/hippo-digital/DfE-FBIT), built with the [GOV.UK Prototype Kit](https://prototype-kit.service.gov.uk/docs/). For how the repository is organised, see [prototype kit structure](/reference/design/prototype-kit-structure/).

## Before you start

You need:

* [Node.js](https://nodejs.org/) and Git installed
* access to the `hippo-digital/DfE-FBIT` repository, which is private

## 1. Install the prototype kit

Clone the repository and install its dependencies:

```sh
git clone https://github.com/hippo-digital/DfE-FBIT.git
cd DfE-FBIT
npm install
```

## 2. Run it

```sh
npm run dev
```

Open [http://localhost:3000](http://localhost:3000). The home page lists every work item. While `npm run dev` is running, saving a file reloads the page.

## 3. Open a work item

Select a work item from the home page, such as "Primary school progress". Its index page shows the final designs first, then the other design concepts considered.

The page's template is in `app/views/primary-progress/index.html`. Each work item has its own folder in `app/views`.

## 4. Add a design concept

Create a new file, `app/views/primary-progress/my-concept.html`:

```njk
{% raw %}{% extends "layouts/main.html" %}

{% block content %}
  <h1 class="govuk-heading-l">My concept</h1>
  <p class="govuk-body">A first design concept.</p>
{% endblock %}{% endraw %}
```

Go to [http://localhost:3000/primary-progress/my-concept](http://localhost:3000/primary-progress/my-concept). The folder path in `app/views` is the page's address.

## 5. Link it from the work item index

In `app/views/primary-progress/index.html`, find the "Other design concepts considered" section and add a row to a table, with a link to `/primary-progress/my-concept` and a short note on what has changed.

Reload the index page and select your link. When the team agrees a design, it moves up to "Final designs".

## 6. Run the tests

```sh
npm test
```

Most tests check that a page renders the right content, so if you change wording on a page, a test may need the same change.

## 7. Share your change

Commit and push your change:

```sh
git checkout -b my-concept
git add app/views/primary-progress
git commit -m "Add my concept to primary progress"
git push -u origin my-concept
```

Then open a pull request on GitHub.
