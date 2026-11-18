---
title: "Highlight schools checkbox"
layout: sub-navigation
sectionKey: "Explanation"
includeInBreadcrumbs: true
eleventyNavigation:
  key: "Highlight schools checkbox"
  parent: "Design"
---

This page explains the pattern we use to highlight similar schools with strong outcomes on benchmarking pages, and why. Reuse it when adding an outcome measure, such as Attainment 8 or key stage 2 progress, to a benchmarking page.

> [!NOTE]
> **TL;DR:**
>
>* One checkbox, "Top 5 for [measure]", under "Highlight schools" in the view options.
>* It highlights the 5 best-performing schools in the comparator set, excluding the school itself.
>* Schools should compare themselves with similar schools that spend well and also achieve well for their pupils.

## The pattern

<div class="app-example">
  <div class="govuk-form-group govuk-!-margin-bottom-0">
    <fieldset class="govuk-fieldset">
      <legend class="govuk-fieldset__legend govuk-!-margin-bottom-0">
        <span class="govuk-label">Highlight schools</span>
      </legend>
      <div class="govuk-checkboxes govuk-checkboxes--small">
        <div class="govuk-checkboxes__item">
          <input class="govuk-checkboxes__input" id="example-top5" type="checkbox" aria-describedby="example-top5-hint" checked>
          <label class="govuk-label govuk-checkboxes__label" for="example-top5">Top 5 for Attainment 8</label>
          <div id="example-top5-hint" class="govuk-hint govuk-checkboxes__hint">Of similar schools, excluding this one</div>
        </div>
      </div>
    </fieldset>
  </div>
</div>

* Legend: "Highlight schools".
* Label: "Top 5 for [measure]", for example "Top 5 for Attainment 8".
* Hint, shown only once ticked: "Of similar schools, excluding this one".
* Chart view: highlighted schools' bars use one highlight colour, with a legend and a tag in the hover box.
* Table view: a tag against each highlighted school, with no scores. Scores are in the download file.
* The data source inset text names the outcome data source once the checkbox is ticked.

## Why the top 5

We previously highlighted schools by banding, such as "above average" or "well above average" Progress 8. Well-performing schools tend to have well-performing comparators, and low-performing schools low-performing ones. So banding highlighted almost the whole comparator set for some schools, and none for others.

Ranking within the comparator set means almost every school sees a short list of strong peers to learn from and contact, including the schools that need it most.

## Why a single checkbox

A single checkbox has low cognitive load: users can see what it does and use it with one tick, with nothing to decide first.

Alternatives such as radios for local authority or national average, checkboxes per subject, or a select of banding combinations all ask users to weigh up 3 to 12 options before they see anything useful. Deciding the method as a team is what lets the control be a single checkbox.

## Only highlight schools doing well

The pattern labels schools with strong outcomes. It does not tag schools as below average, because the aim is to help users find schools to learn from.

## Using the pattern for a new measure

* Keep the legend, label, hint, colour and tags consistent with existing pages.
* Rank within each comparator set, excluding the school itself.
* If a measure has more than one score, such as reading, writing and maths, agree a single ranking method as a team. Show the detail in the hover box rather than adding options.
