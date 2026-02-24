# ADR: Approach for Handling MFA in Automated Tests

## Context and Problem Statement

Our automated end‑to‑end tests interact with services protected by Multi‑Factor Authentication (MFA). MFA blocks automation, and we need a reliable, maintainable, and cost‑effective approach to handle MFA within our CI/CD pipeline. The core question is: **How should we handle MFA for automated tests without compromising security or test coverage?**

## Decision Drivers

* Must work reliably in Azure DevOps pipelines using Microsoft‑hosted agents
* Minimise implementation effort and avoid unnecessary test rework
* Maintainability and long‑term sustainability
* Cost and licensing implications
* Security and compliance considerations
* Uncertainty and risk associated with unproven approaches
* Reliability and stability of automated tests
* Time constraints and delivery commitments

## Considered Options

* Option 1: Use Microsoft Graph API with a dedicated email account
* Option 2: Use Mailosaur (paid service)
* Option 3: Bypass/Mock DfE Sign‑in MFA in automated tests

## Decision Outcome

We will proceed with Option 3.

Options 1 and 2 require further investigation to determine whether they are technically feasible on Azure DevOps Microsoft‑hosted agents. Both would require significant engineering effort, test rework, and R&D time, with no guarantee of success.

Option 3 is the only approach we fully understand today, and it is known to be feasible within our constraints. It meets our immediate needs, unblocks automated testing, and avoids the risk of investing time into solutions that may ultimately prove incompatible with our pipeline.

### Consequences

* Good:
  * We can implement MFA handling quickly and unblock automated test development.
  * No dependency on external services or mailbox infrastructure.
  * Minimal rework required for existing tests.
  * Predictable and stable behaviour in CI/CD.

* Bad:
  * We do not achieve full end‑to‑end MFA coverage.
  * Real MFA behaviour must continue to be validated through manual smoke tests.
  * If future requirements demand real MFA automation, additional work may be needed.

## Validation

Compliance with this ADR will be validated through:

* Implementation of the mock/bypass MFA mechanism in automated tests
* Successful execution of automated tests in Azure DevOps pipelines
* Periodic review to ensure the solution remains compatible with DSI and pipeline changes
* Optional future R&D spikes to reassess Options 1 and 2 if requirements evolve

## Pros and Cons of the Options

### Option 1: Use Microsoft Graph API with a Dedicated Email Account

Status: Requires investigation - feasibility on Microsoft‑hosted agents unknown

Pros:

* Good, because it is a clean, first‑party Microsoft solution with long‑term stability.
* Good, because it avoids external dependencies and keeps all data within Microsoft 365.
* Good, because it aligns well with enterprise security and compliance expectations.

Cons:

* Bad, because feasibility on Microsoft‑hosted agents is unconfirmed.
* Bad, because it requires significant R&D and engineering effort.
* Bad, because it may require substantial test rework to integrate email‑based MFA flows.
* Bad, because Graph API throttling, permissions, and token handling add complexity.
* Bad, because failure modes (mailbox access issues, throttling) could block the pipeline.

### Option 2: Use Mailosaur (Paid Subscription)

Status: Requires investigation - likely feasible but not yet validated

Pros:

* Good, because it is purpose‑built for automated testing.
* Good, because it offers simple APIs for retrieving MFA codes.
* Good, because it avoids managing real mailboxes or Graph permissions.

Cons:

* Bad, because it introduces ongoing subscription costs.
* Bad, because feasibility on Microsoft‑hosted agents must be validated.
* Bad, because it introduces a third‑party dependency and availability risk.
* Bad, because adopting it would require test rework and integration effort.

### Option 3: Bypass / Mock DfE Sign‑in MFA in Automated Tests

Status: Known feasible approach - selected

Pros:

* Good, because it is the fastest to implement with minimal R&D.
* Good, because it avoids external dependencies and infrastructure.
* Good, because it provides stable, predictable behaviour in CI/CD.
* Good, because it requires no major rework of existing tests.

Cons:

* Bad, because it does not provide true end‑to‑end MFA coverage.
* Bad, because differences from production behaviour must be validated manually.

## More Information

This decision should be revisited if:

* R&D confirms that Options 1 or 2 are feasible and provide clear benefits
* DSI changes its MFA mechanism
* Our CI/CD pipeline evolves to support new authentication tools
* The chosen solution becomes unstable or insufficient for future requirements  
