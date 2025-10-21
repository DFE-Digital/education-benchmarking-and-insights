# Sortable HTML tables

FBIT leverages the Ministry of Justice design pattern for the [Sortable table](https://design-patterns.service.justice.gov.uk/components/sortable-table/) component where there is a requirement for tables to have a header cell that may be selected by the user in order to sort the content of a table.

## Composition

The MOJ component uses a `<button>` for the action of performing the sort, with three states:

- unsorted (▴▾)
- sorted ascending (▲)
- sorted descending (▼)

Only one column may be sorted at a time, so when a new column sort is selected any others are set back to 'unsorted'.

e.g.:

```text
<!-- For conciseness, styles and button actions have been omitted -->
<table id="table-id">
    <thead>
        <tr>
            <th scope="col" aria-sort="ascending" aria-label="Sort by Field 1">
                <button aria-pressed="true" aria-label="Sort by Field 1, descending" aria-controls="table-id">
                    Field 1 ▲        
                </button>
            </th>
            <th scope="col" aria-sort="none" aria-label="Sort by Field 2">
                <button aria-pressed="false" aria-label="Sort by Field 2, ascending" aria-controls="table-id">
                    Field 2 ▴▾        
                </button>
            </th>
            <th scope="col" aria-sort="none" aria-label="Sort by Field 3">
                <button aria-pressed="false" aria-label="Sort by Field 3, ascending" aria-controls="table-id">
                    Field 3 ▴▾        
                </button>
            </th>
        </tr>
    </thead>
    <tbody>
        <!-- table data -->
    </tbody>
</table>
```

## Evaluation of the use of `<button>`

The `<button>` elements are designed to be very similar to the GOV.UK Design System [Link](https://design-system.service.gov.uk/styles/links/) style, which is usually seen as bad practice due to:

1. Semantic confusion
   - Here, buttons are performing the **action** of instigating a change to the sort order of a table of records.
   - Links represent a navigation to a different page or section, which is not the desired behaviour (albeit with a `<button>` causing a `<form>` to be posted resulting in a change to the current URL's query string).
1. Screen reader behaviour
   - Assistive technologies may be confused about the purpose of the button without additional hints.
1. Keyboard navigation expectations
   - Links are generally activated with the <kbd>Enter</kbd> key, but buttons with <kbd>Enter</kbd> or <kbd>Space</kbd>.
1. Inconsistent styling across browsers
   - Overriding button styles to mimic links may lead to inconsistent or unexpected UI on some devices or browsers.

The MOJ component runs client side, which may be why `<button>` was chosen originally. It is assumed that as per GDS, a significant amount of [user research](design-system.service.gov.uk/community/design-system-working-group/) has gone into the usability of this component. Even so, if feedback is received that advises that this is not suitable for use across FBIT than the `<a>` should be reverted to instead (see below).

### `aria-` attributes

To best support the use of a `<button>` the following accessibility attributes have been included and validated with assistive technologies:

- `aria-controls`: identifies the table whose contents are controlled by the button
- `aria-label`: describes the element where context cannot be resolved based on the [accessible name](https://developer.mozilla.org/en-US/docs/Glossary/Accessible_name) alone
- `aria-pressed`: turns the button into a toggle button
  - set to `true` if the sort button is currently active for that column
  - set to `false` if the sort button is available but not currently active
  - set to `mixed` if no sort buttons are currently active
- `aria-sort`: indicates the data cells in the column are sorted
  - set to `ascending` or `descending` on the currently sorted column only
  - other unsorted columns should be set to `none`

These may be seen in the basic composition above, with the addition of `scope="col"` on the `<th>`. This denotes that the header cell is a header for a column rather than a row, or a group of columns or rows.

## Reverting to `<a>`

The original header cell implementation was to use an `<a>` with the `href` attribute set to the current request URI with the `sort` query string component updated to the correct context. This was removed as part of [#3114](https://github.com/DFE-Digital/education-benchmarking-and-insights/pull/3114).
