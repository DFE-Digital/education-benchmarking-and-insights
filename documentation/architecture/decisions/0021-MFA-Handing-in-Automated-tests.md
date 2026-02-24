# ADR: Approach for Handling MFA in Automated Tests

## Context and Problem Statement

Our automated end‑to‑end tests interact with services protected by Multi‑Factor Authentication (MFA). MFA blocks automation, and we need a reliable, maintainable, and cost‑effective approach to handle MFA within our Azure DevOps CI/CD pipeline.

This ADR is about Should we implement the known, feasible mock/bypass MFA solution, or invest time in researching alternative email‑based MFA solutions that require significant R&D, engineering effort, and their feasibility within our pipeline?

## Decision Drivers

* Must work reliably in Azure DevOps pipelines using Microsoft‑hosted agents
* Minimise engineering effort and avoid unnecessary test rework
* Delivery timelines and available team capacity
* Cost and licensing implications
* Security and compliance considerations
* Uncertainty and risk associated with unproven approaches
* Reliability and stability of automated tests

## Considered Approaches

### Email‑Based MFA Solutions (Graph API & Mailosaur)

This category includes:

* Microsoft Graph API with a dedicated mailbox
* Mailosaur (paid service)

These solutions rely on retrieving MFA codes from email inboxes.

#### Trade‑offs and uncertainties

* **R&D effort:** Both options require substantial investigation to confirm feasibility on Microsoft‑hosted Azure DevOps agents.
* **Pipeline integration complexity:** Email polling, API authentication, and message retrieval must be proven to work reliably in hosted agents.
* **Test rework:** Existing tests would need to be redesigned to incorporate email‑based MFA flows.
* **Implementation impact:** Additional infrastructure, configuration, and error‑handling logic would be required.
* **Cost:** Mailosaur introduces ongoing subscription costs.
* **Operational risk:** External dependencies (Mailosaur) or mailbox throttling/permissions (Graph API) may introduce instability.
* **Uncertain outcome:** After investing time and effort, these solutions may still prove incompatible or too costly to maintain.

### Mock/Bypass DfE Sign‑in MFA (Known Option)

A controlled bypass of MFA for automated tests, while retaining real MFA validation in manual smoke tests.

This is the approach we fully understand today, and it is known to be feasible within our decision drivers.

## Decision Outcome

We will proceed with implementing the mock/bypass MFA solution.

This decision is based on the following:

* It is the only option with known feasibility.
* It requires minimal engineering effort and avoids major test rework.
* It has no additional cost and no external dependencies.
* It allows us to deliver automated tests within current timelines.
* The alternative options carry significant uncertainty, R&D cost, implementation impact, and risk.

Given these trade‑offs, Mock/Bypass DfE Sign‑in MFA provides the best balance of reliability, effort, and predictability.

### Consequences

Pors:

* Fastest path to unblock automated testing
* Minimal implementation effort
* No dependency on external services or mailbox infrastructure
* Predictable and stable behaviour in CI/CD
* No additional licensing or subscription costs

Cons:

* Does not provide true end‑to‑end MFA coverage
* Real MFA behaviour will need to be validated through manual smoke tests

## Validation

This ADR will be validated through: 

* Successful implementation of the mock/bypass MFA mechanism
* Automated tests running reliably in Azure DevOps pipelines
* Periodic review to ensure compatibility with DSI and pipeline changes

## More Information

This decision should be revisited if the chosen solution becomes insufficient for future requirements.
