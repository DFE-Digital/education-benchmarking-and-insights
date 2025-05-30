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

- Enable in Production: After successful validation in lower environments, the
feature can be enabled in production ensuring it has gone through the automated tests gate.

## Best Practices

- Use meaningful names: for feature flags that describe the feature's purpose.
- Document: each feature flag with its purpose, expected behavior, and owner.
- Remove: old or unused feature flags to reduce technical debt.
- Avoid long-lived flags: unless they are meant to permanently control optional behavior.

## Conclusion

Feature flags are a powerful tool when used correctly. This guide ensures that we enable features safely, maintain high quality, and protect the stability of our production environment. Always follow the environment rollout rules and treat feature flags as part of your deployment and testing strategy.

<!-- Leave the rest of this page blank -->
\newpage
