# ADR: Approach for Handling MFA in Automated Tests

## Context and Problem Statement

Our automated end‑to‑end tests require authentication through DfE Sign‑in, which enforces Multi‑Factor Authentication (MFA). MFA blocks automation unless we adopt a strategy to either bypass MFA or automate the retrieval of MFA codes.

## Decision Drivers

* Engineering and implementation effort required
* Risks associated with the approach
* Reliability and stability of automated tests
* Cost and licensing implications

## Considered Options

* **Option 1: Mock DfE Sign‑in MFA**
* **Option 2: Email‑Based MFA Handling**

## Decision Outcome

{To be decided by the team once both options have been reviewed.}

### Consequences

{To be added once the team selects an option.}

## Validation

Validation will occur through:

* Successful execution of automated tests in Azure DevOps pipelines
* Code review of the implemented MFA handling approach
* Periodic review to ensure compatibility with DSI and pipeline changes

## Comparison Table Based on Decision Drivers

| Decision Driver               | Option 1: Mock/Bypass MFA (Known) | Option 2: Email‑Based MFA (Graph API / Mailosaur) |
|-------------------------------|-----------------------------------|---------------------------------------------------|
| **Engineering & Test Effort** | Low                               | High                                              |
| **Implementation Complexity** | Low                               | Medium-High                                       |
| **Cost**                      | None                              | other options are licensed based                  |
| **Risks**                     | Medium                            | Low                                               |
| **Long‑Term Maintainability** | High                              | Depends on chosen service and integration         |

## Pros and Cons of the Options

### Option 1: Mock/Bypass DfE Sign‑in MFA

A mocking of MFA for automated tests, while retaining real MFA validation in manual smoke tests.

* Good, because it is a known, feasible solution that works with Microsoft‑hosted agents
* Good, because it requires minimal engineering and test effort
* Good, because it avoids test rework and integrates cleanly with existing flows
* Good, because it has no external dependencies
* Good, because it provides stable and predictable behaviour in CI/CD
* Neutral, because manual smoke tests will still validate real MFA behaviour
* Bad, because it does not provide true end‑to‑end MFA coverage
* Bad, because differences from production behaviour may require ongoing manual checks

### Option 2: Email‑Based MFA Handling (Graph API or Mailosaur)

This includes:

* Microsoft Graph API with a dedicated mailbox
* Mailosaur (paid service)

Both approaches retrieve MFA codes from email inboxes.

* Good, because they could reduce reliance on manual smoke tests
* Good, because they align more closely with real‑world authentication flows
* Neutral, because long‑term benefits depend on feasibility and stability
* Bad, because they require significant R&D effort before they can be evaluated properly
* Bad, because they may require substantial test rework
* Bad, because they introduce implementation complexity (API auth, error handling)
* Bad, because these add additional costs
