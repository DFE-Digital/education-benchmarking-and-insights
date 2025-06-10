# Feature Flags Management Guide

## Purpose

This document provides guidance on how to use feature flags in our service. Feature flags allow us to enable or disable specific functionality without deploying new code. This approach supports safe deployments, controlled rollouts, and easier testing of new features.

We have several useful features that are gated behind feature flags. This guide outlines how and when to enable those flags across environments, ensuring stability and best practices in our development and release process.

## What Are Feature Flags?

Feature flags are configuration switches used to turn features on or off in an application at runtime. They allow us to:

- Deploy code to production which is WIP and without exposing new features.
- Test features in lower environments before exposing them broadly.
- Roll out features incrementally and safely.
- Quickly disable a feature in case of failure without needing a rollback.

## Feature Flag Environment Rules

To ensure that features are properly tested before they are live, we should follow the principle that features must be enabled in lower environments before they are enabled in production.

| Lower Environments | Production | Allowed? | Notes                           |
|--------------------|------------|----------|---------------------------------|
| ON                 | ON         | ✅ Yes   | Correct rollout pattern.        |
| ON                 | OFF        | ✅ Yes   |Testing before production rollout.|
| OFF                | OFF        | ✅ Yes   | Feature is disabled everywhere. |
| OFF                | ON         | ❌ No    | Not allowed. Risky and untested. |

## Enabling a Feature Flag

- Identify the Feature: Ensure the feature is ready and has been tested.
- Enable in Lower Environment(s): Turn the flag ON in development, test, or feature environments. Validate expected functionality.
- Monitor and Test: Ensure there are no regressions or side effects. Gather feedback or metrics if applicable.
- Enable in Production: After successful validation in lower environments, the feature can be enabled in production ensuring it has gone through the automated tests gate.

## Currently Implemented Feature Flags

Below is a list of feature flags currently implemented in our service at the time of writing this document:

``` text
CurriculumFinancialPlanning  
CustomData  
DisableOrganisationClaimCheck  
FilteredSearch  
FinancialBenchmarkingInsightsSummary  
ForecastRisk  
HighNeeds  
HistoricalTrends  
LocalAuthorities  
SchoolSpendingPrioritiesSsrCharts  
TrustComparison  
Trusts  
UserDefinedComparators  
```

These flags are typically managed via App Service configuration.

## When to Implement a Feature Flag?

You should consider implementing a feature flag when:

- **Feature is incomplete** but needs to be merged or deployed (e.g., to meet a release deadline).
- **You need to test a feature** in non-production environments before enabling it for all users.
- **You want to control rollout** (e.g., progressive or per-environment rollout).
- **You want a quick rollback option** in case the new feature causes issues in production.
- **The feature is experimental** or intended for a limited group of users.
- **The feature has external dependencies** that are not yet available or enabled.

## When to Deprecate (Remove) a Feature Flag?

Feature flags should not live indefinitely. You should deprecate and remove a flag when:

- The feature has been **fully released** and is no longer optional.
- The flag is **no longer used** in any environment (i.e., it’s ON everywhere).
- The feature or code path behind the flag has been **abandoned** or is obsolete.
- It has become a **source of confusion or tech debt** (especially if left undocumented).

## Best Practices

- Use meaningful names: for feature flags that describe the feature's purpose.
- Document: each feature flag with its purpose and expected behavior.
- Remove: old or unused feature flags to reduce technical debt.
- Avoid long-lived flags: unless they are meant to permanently control optional behavior.

## Using Feature Flags in Tests

We use feature flags to control the execution of E2E, accessibility (a11y), and integration tests based on the state of each flag.

### E2E Tests

In E2E tests, we apply a category tag using the `Xunit.TraitAttribute`:
adding @{featureflagTag} automatically adds the below in the code

```csharp
[Xunit.TraitAttribute("Category", "{Tag Name}")]
```

In pipeline runs, we include or exclude tests using command-line arguments:

```yaml
arguments: --filter Category!={Tag Name}
```

This allows dynamic test execution based on which features are currently enabled.

### Accessibility (a11y) Tests

For a11y tests, the trait is added at the **class level**:

```csharp
[Trait("Category", "{FilteredSearchEnabled}")]
```

These tags are similarly controlled through pipeline arguments for inclusion or exclusion during test runs.

### Integration Tests

For integration tests:

- **All feature flags are considered enabled by default.**
- If specific flags need to be **disabled** for a test, we explicitly mock them using:

```csharp
.SetupDisableFeatureFlags(disabledFlags);
```

Where `disabledFlags` might look like:

```csharp
disabledFlags = [FeatureFlags.FilteredSearch];
```

## Conclusion

Feature flags are a powerful tool when used correctly. This guide ensures that we enable features safely, maintain high quality, and protect the stability of our production environment. Always follow the environment rollout rules and treat feature flags as part of your deployment and testing strategy.

<!-- Leave the rest of this page blank -->
\newpage
