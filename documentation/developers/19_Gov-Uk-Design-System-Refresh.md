# GOV.UK Design System Refresh

In June 2025, GOV.UK started to refresh the brand across its products and services.
The FBIT website followed suit and due to to being hosted on the `education.gov.uk` domain the related DfE branding was also applied.
This was also applied to the 'Shutter' site for consistency.

## GDS Refresh

The [GDS guidelines](https://frontend.design-system.service.gov.uk/brand-refresh-changes/#updating-your-service-to-use-the-new-brand) were followed to initially apply the brand refresh. This affected:

- [Page template](https://github.com/alphagov/govuk-frontend/releases/tag/v5.10.0) CSS class
- [Header](https://design-system.service.gov.uk/components/header/)
  - Including moving 'Sign in' and 'Sign out'from Header to [Service Navigation](https://design-system.service.gov.uk/components/service-navigation/)
- [Footer](https://design-system.service.gov.uk/components/footer/)
- [Assets](https://frontend.design-system.service.gov.uk/import-font-and-images-assets)

> 📅 The deadline for the GDS rebrand effort was [31 December 2025](https://design.education.gov.uk/design-system/govuk-rebrand#what-you-need-to-do)

## DfE Refresh

The [DfE guidelines](https://design.education.gov.uk/design-system/govuk-rebrand) were then followed to apply the brand refresh. This affected:

- [Header](https://design.education.gov.uk/design-system/govuk-rebrand/dfe-header-rebrand)
- Footer (removal of small left-hand crown logo)
- [Typeface](https://design.education.gov.uk/design-system/styles/typography)

> 📅 The deadline for the DfE rebrand effort was [31 March 2026](https://design.education.gov.uk/design-system/govuk-rebrand#what-you-need-to-do)

## DfE Frontend

[DfE Frontend](https://design.education.gov.uk/design-system/dfe-frontend) was **not** configured as part of the above changes. Due to:

- No consumption of DfE components such as [Card](https://design.education.gov.uk/design-system/components/card) or [Filter](https://design.education.gov.uk/design-system/components/filter)
- Lack of support for latest GDS features from DfE Frontend:
  - Conflicting type scales in latest published version requiring considerable rework (as identified from exploratory work)
  - [Repo](https://github.com/DFE-Digital/dfe-frontend) not updated since May 2024
  - [Package](https://www.npmjs.com/package/dfe-frontend?activeTab=versions) not updated since May 2024
