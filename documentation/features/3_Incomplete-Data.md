# Incomplete Data

## Introduction

This document provides detailed information for developers about the implementation, usage, and integration of the Incomplete Data feature within the system.

## Overview

The intention of this feature is to provide a warning for users of the service flagging when the data they are viewing contains incomplete data for the year. This will be used in school to school comparisons on the Spending and Costs, Workforce and Compare your Costs pages.

In Cosmos DB the record for a school has a field "Periods covered by return". This has a value representing the number of months the school has submitted data for.

This is used to create a HasIncompleteData prop which is used to flag when financial data is incomplete. See the Usage section for a list of where this is used relating to this feature [here](#usage).

Warning text is rendered for the Spending and costs page, Compare your costs and Benchmark workforce data when any of the schools in the comparison are flagged as having incomplete data.

## Goals

### Primary Goal

When data submitted for the school is incomplete it is important this is flagged to the user when they are viewing comparisons, so they can make informed decisions with how they proceed with the information we provide

## Usage

### HasIncompleteData prop

This feature currently makes a HasIncompleteData prop available in the following:

In platform

- [Expenditure](../../platform/src/abstractions/Platform.Domain/Responses/SchoolExpenditureResponseModel.cs)
- [Census](../../platform/src/abstractions/Platform.Domain/Responses/CensusResponseModel.cs)

In web app

- [SchoolExpenditure](../../web/src/Web.App/Domain/SchoolExpenditure.cs)
- [Census](../../web/src/Web.App/Domain/Census.cs)
- [SchoolSpendingViewModel](../../web/src/Web.App/ViewModels/SchoolSpendingViewModel.cs)

In front-end-components

- [Workforce and ExpenditureData](../../front-end-components/src/services/types.tsx)

### WarningBanner

A React component is used in front-end-components which is used to display the warning if the HasIncompleteData is true. This is styled as per GDS guidelines as inset text.

[warning-banner](../../front-end-components/src/components/warning-banner)

#### WarningBanner Props

- **isRendered**
  - *Type:* Boolean
  - *Description:* This controls whether this component returns the warning banner or null.

- **message**
  - *Type:* String
  - *Description:* The warning text that will be displayed.

#### WarningBanner Example

The below JSX

```jsx
<WarningBanner
  isRendered={true}
  message="Warning message"
/>
```

is rendered as the below HTML

```html
<div class="govuk-inset-text">
  Warning message
</div>
```

#### Composed components

This component is used as part of the composed components below to render the warning along with the chart or table data.

- [comparison-chart-summary](../../front-end-components/src/composed/comparison-chart-summary/composed.tsx)
- [horizontal-bar-chart-wrapper](../../front-end-components/src/composed/horizontal-bar-chart-wrapper/composed.tsx)

#### Usage in MVC web app

The React component is used in the following views in the MVC web app

- [SchoolSpending view](../../web/src/Web.App/Views/SchoolSpending/Index.cshtml)
- [SchoolCensus view](../../web/src/Web.App/Views/SchoolCensus/Index.cshtml)
- [SchoolComparison view](../../web/src/Web.App/Views/SchoolComparison/Index.cshtml)
