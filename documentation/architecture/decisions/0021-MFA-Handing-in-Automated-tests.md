# ADR: Approach for Handling MFA in Automated Tests

## Context and Problem Statement

Our automated end‑to‑end tests interact with services protected by Multi‑Factor Authentication (MFA). MFA blocks automation, and we need a reliable, maintainable, and cost‑effective approach to handle MFA within our CI/CD pipeline. The core question is: **How should we handle MFA for automated tests without compromising security or test coverage?**

## Decision Drivers

* Maintainability and long‑term sustainability
* Ease of integration with existing CI/CD pipeline
* Cost and licensing implications
* Security and compliance considerations
* Time required for R&D and implementation
* Reliability and stability of automated tests
* Minimising manual intervention

## Considered Options

* Option 1: Use Microsoft Graph API with a dedicated email account
* Option 2: Use Mailosaur (paid service)
* Option 3: Bypass/Mock DfE Sign‑in MFA in automated tests

## Decision Outcome

{This section will be completed once the team selects the preferred approach based on R&D outcomes and constraints.}

### Consequences

{To be added}

## Validation

Compliance with this ADR will be validated through:

* Code review of the implemented MFA handling approach
* Successful execution of automated tests in CI/CD
* Periodic review to ensure the solution remains compatible with DSI and pipeline changes

## Pros and Cons of the Options

### Option 1: Use Microsoft Graph API with a Dedicated Email Account

This approach uses Graph API to programmatically read MFA emails from a dedicated mailbox.

Pros:

* Good, because it is a clean, first‑party Microsoft solution with long‑term stability.
* Good, because it avoids external dependencies and keeps all data within Microsoft 365.
* Good, because it can be fully automated once implemented.
* Good, because it aligns well with enterprise security and compliance expectations.
* Neutral, because it requires service account setup and permissions, which may need IT involvement.
* Neutral, because rate limits and throttling must be considered, but are manageable.
* Bad, because it requires significant R&D and implementation effort.
* Bad, because handling token refresh, mailbox polling, and error cases adds complexity.
* Bad, because failures in Graph API or mailbox access can block the entire test pipeline.

### Option 2: Use Mailosaur (Paid Subscription)

Mailosaur provides test‑friendly email inboxes with APIs designed for automation.

Pros

* Good, because it is purpose‑built for automated testing, reducing implementation complexity.
* Good, because it offers simple APIs for retrieving MFA codes quickly.
* Good, because it provides dashboards, logs, and debugging tools that help diagnose test failures.
* Good, because it avoids the need to manage a real mailbox or Graph permissions.
* Neutral, because integration with our pipeline is likely straightforward but still needs validation.
* Neutral, because it introduces a third‑party dependency, which may require vendor assessment.
* Bad, because it requires a paid subscription, increasing ongoing costs.
* Bad, because reliance on an external service introduces availability risks outside our control.

### Option 3: Bypass / Mock DfE Sign‑in MFA in Automated Tests

This approach replaces real MFA with a mock or bypass mechanism for automated tests only.

Pros

* Good, because it is the fastest to implement with minimal R&D.
* Good, because it makes automated tests more stable, avoiding external MFA delays or failures.
* Good, because it reduces pipeline execution time, since no email polling is required.
* Good, because it isolates tests from DSI outages, improving reliability.
* Neutral, because manual smoke tests can still validate the real DSI integration.
* Bad, because it removes true end‑to‑end coverage of the authentication flow.
* Bad, because it may diverge from production behaviour, risking missed issues.

## More Information

This decision should be revisited if:

* DSI changes its MFA mechanism
* Our CI/CD pipeline evolves to support new authentication tools
* Costs or licensing constraints change
* The chosen solution becomes unstable or unmaintainable  
