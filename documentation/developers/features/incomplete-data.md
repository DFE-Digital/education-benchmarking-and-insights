# Developer Feature Documentation: Incomplete Data

## Introduction

This document provides detailed information for developers about the implementation, usage, and integration of the Incomplete Data feature within the system.

## Overview

The intention of this feature is to provide a warning for users of the service flagging when the data they are viewing contains incomplete data for the year. This will be used in school to school comparisons on the Spending and Costs, Workforce and Compare your Costs pages.

In Cosmos DB the record for a school has a field "Periods covered by return". This has an int value representing the number of months the school has submitted data for.

This is used to create a HasIncompleteData prop which is used to flag when financial data is incomplete. See the Usage section for a list of where this is used relating to this feature [here](#usage).

For the Spending and costs page this is used to conditionally render a warning text banner in the Razor view.

For the Compare your costs and Benchmark workforce data this is again conditionally rendered but in the React components.

## Goals

### Primary Goal

When data submitted for the school is incomplete it is important this is flagged to the user when they are viewing comparisons so they can make informed decisions with how they proceed with the information we provide

## Usage

### HasIncompleteData prop

This feature currently makes a HasIncompleteData prop available in the following:

In platform

- [Expenditure](../../../platform/src/abstractions/Platform.Domain/Responses/SchoolExpenditureResponseModel.cs)
- [Workforce](../../../platform/src/abstractions/Platform.Domain/Responses/WorkforceResponseModel.cs)

In web app

- [SchoolExpenditure](../../../web/src/Web.App/Domain/SchoolExpenditure.cs)
- [Workforce](../../../web/src/Web.App/Domain/Workforce.cs)
- [SchoolSpendingViewModel](../../../web/src/Web.App/ViewModels/SchoolSpendingViewModel.cs)

In front-end-components

- [Workforce and ExpenditureData](../../../front-end-components/src/services/types.tsx)

### Views

A warning banner is rendered in the Razor view for the Spending and costs page here

- [SchoolSpending view](../../../web/src/Web.App/Views/SchoolSpending/Index.cshtml)

A warning banner is rendered within the React component for the Workforce benchmark data page here

- [SchoolWorkforce view](../../../web/src/Web.App/Views/SchoolWorkforce/Index.cshtml)
- [compare-your-workforce front-end-components](../../../front-end-components/src/views/compare-your-workforce)

A warning banner is rendered within the React component for the Compare your costs page here

- [SchoolComparion view](../../../web/src/Web.App/Views/SchoolComparion/Index.cshtml)
- [compare-your-costs front-end-components](../../../front-end-components/src/views/compare-your-costs)

### WarningBanner

A React component is used in front-end-components which is used to display the warning if the HasIncompleteData is true. This is styled as per GDS guidelines.

[warning-banner](../../../front-end-components/src/components/warning-banner)

#### WarningBanner Props

- **icon**
  - *Type:* String
  - *Description:* This is used for the warning icon

- **visuallyHiddenText**
  - *Type:* String
  - *Description:* This is for screen readers

- **message**
  - *Type:* String
  - *Description:* The warning text that will be displayed

#### WarningBanner Example

The below JSX

```jsx
<WarningBanner
    icon="!"
    visuallyHiddenText="Warning"
    message="Some schools don't have a complete set of financial data for this period"
/>
```

is rendered as the below HTML

```html
<div class="govuk-warning-text">
    <span class="govuk-warning-text__icon" aria-hidden="true">!</span>
    <strong class="govuk-warning-text__text">
        <span class="govuk-visually-hidden">Warning</span>
        Some schools don't have a complete set of financial data for this period
    </strong>
</div>
```
