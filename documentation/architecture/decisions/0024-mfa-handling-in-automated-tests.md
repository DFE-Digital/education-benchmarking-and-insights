# ADR: Approach for Handling MFA in Automated Tests

## Context and Problem Statement

Our automated end‑to‑end tests require authentication through DfE Sign‑in (DSI), which enforces Multi‑Factor Authentication (MFA). MFA blocks automation unless we adopt a strategy to either mock MFA or automate the retrieval of MFA codes.

## Decision Drivers

* Engineering and implementation effort required
* Reliability and stability of automated tests
* Risks associated with the approach, both coverage risk and operational risk
* Cost and licensing implications
* Long‑term maintainability

## Considered Options

* **Option 1: Mock DfE Sign‑in MFA**
* **Option 2: Email‑Based MFA Handling (Graph API or Mailosaur)**

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

| Decision Driver                         | Option 1: Mock DfE Sign‑in MFA  | Option 2: Email‑Based MFA (Graph API / Mailosaur)      |
|-----------------------------------------|---------------------------------|--------------------------------------------------------|
| **Engineering & implementation effort** | Low                             | Medium to High                                         |
| **Reliability & stability**             | High (no external dependencies) | Medium (depends on email delivery / external service)  |
| **Coverage risk**                       | Higher (does not test real MFA) | Lower (exercises the real authentication flow)         |
| **Operational risk**                    | Lower                           | Higher (external dependency and code delivery latency) |
| **Cost & licensing**                    | None                            | Paid / licensing required                              |
| **Long‑term maintainability**           | High                            | Depends on chosen service and integration              |

## Pros and Cons of the Options

### Option 1: Mock DfE Sign‑in MFA

In our integration tests, DfE Sign‑in is replaced with a stubbed authentication scheme that signs tests in as a pre‑authenticated user, so they never reach the DSI login or its MFA step. We will use the same approach to mock MFA for the end‑to‑end tests.

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
